// ankr : ftp over SSL plugin

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using pluginner;
using System.Net.Sockets;
using System.IO;
using pluginner.Toolkit;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using Xwt;

namespace fcmd.ftps
{
    public class FtpSecure : IFSPlugin
    {
        // System.Net.FtpWebRequest
        // private FTPClient ftp;
        protected FtpWebRequest ftp;
        protected bool IsSsl = true;
        protected bool success = false;

        private string currentDirectory = "";
        private List<DirItem> dirContent = new List<DirItem>();
        private string hostname;

        private static Regex FtpListDirectoryDetailsRegex
            = new Regex(@".*(?<month>(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec))\s*(?<day>[0-9]*)\s*(?<yearTime>([0-9]|:)*)\s*(?<fileName>.*)", RegexOptions.Compiled | RegexOptions.IgnoreCase); //undone: add style switching (windows, unix, etc)

        private void _CheckProtocol(string url)
        {
            if (url == null) throw new ArgumentNullException("url");
            //проверка на то, чтобы нечаянно через ftpfs не попытались зайти в локальную ФС, webdav, реестр и т.п. :-)
            if (!url.ToLowerInvariant().StartsWith("ftp:")
                && !url.ToLowerInvariant().StartsWith("ftps:"))
                throw new PleaseSwitchPluginException();

            Uri URI = new Uri(url);
            if (URI.Host != hostname)
                Connect(url);
        }

        private void LoadDir(string url)
        {
            currentDirectory = url;
            _CheckProtocol(url);

            Uri URI = new Uri(url);

            if (ftp == null || !success)
                Connect(url);

            success = false;

            //Socket sck = ftp.GetDataSocket();//possible ftpexception, todo add try...catch
            //ftp.SendCommand("CWD " + URI.PathAndQuery);
            //ftp.SendCommand("TYPE A");
            //ftp.SendCommand("LIST", out ListResult);

            FtpWebRequest ftpRequest = this.ftp; // (FtpWebRequest)WebRequest.Create(myUrl);
            ftpRequest.Proxy = FtpWebRequest.DefaultWebProxy;

            // http://stackoverflow.com/questions/4584789/connecting-ftp-server-with-credentials
            if (ftpRequest.Credentials == null
                || (ftpRequest.Credentials as NetworkCredential) == null)
            {
                NetworkCredential myCredentials = new NetworkCredential("anonymous", "ftp@ftp.com");
                if (!string.IsNullOrWhiteSpace(URI.UserInfo))
                {
                    string[] userParts = URI.UserInfo.Split(":".ToCharArray());
                    myCredentials = new NetworkCredential(userParts[0], userParts[1],
                        ""); // System.Environment.UserDomainName);
                }
                ftpRequest.Credentials = myCredentials;
            }

            // socket in Secure Socket Layer(SSL) mode, we use the class System.Net.Security.SslStream.
            ftpRequest.EnableSsl = IsSsl;
            ftpRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            FtpWebResponse directoryListResponse = (FtpWebResponse)ftpRequest.GetResponse();
            // using (FtpWebResponse ftpResponse = (FtpWebResponse)ftpRequest.GetResponse())
            //  StreamReader sr = new StreamReader(new NetworkStream(sck));

            string directoryListing = string.Empty;
            Stream req = null;
            StreamReader sr = null;
            List<DirItem> dirContent = null;
            try
            {
                req = directoryListResponse.GetResponseStream();
                sr = new StreamReader(req);

                if (this.dirContent.Count > 0)
                    this.dirContent.Clear();
                dirContent = this.dirContent;
            }
            catch (Exception)
            {
                sr = null;
            }
            if (sr == null)
                return;

            dirContent = dirContent ?? new List<DirItem>();

            //todo: элемент "вверх по древу"
            /*
            if (URI.PathAndQuery != "/")
            {
                string upDirUrl = "ftp://" + URI.Host + ":" + URI.Port + URI.PathAndQuery;
                upDirUrl = URI.AbsolutePath.Substring(0, URI.AbsolutePath.LastIndexOf('/',0,1));
                DirItem updir = new DirItem();
                updir.URL = upDirUrl;
                updir.TextToShow = "..";
                updir.MIMEType = "x-fcmd/up";
                updir.IconSmall = Utilities.GetIconForMIME("x-fcmd/up");
                directoryContent.Add(updir);
            }*/

            while (!sr.EndOfStream)
            {
                string CurItem = sr.ReadLine();
                if (CurItem == null) continue;
                directoryListing += CurItem + "\n";

                DirItem di = new DirItem();
                Match m = FtpListDirectoryDetailsRegex.Match(CurItem);
                string filename = m.Groups["fileName"].Value;

                if (m.Length == 0 && CurItem.Length >= 41)
                {//brute FTP string parse
                    filename = CurItem.Substring(39);

                    string dateOrig = CurItem.Substring(0, 19);
                    var date = dateOrig
                           .Replace("AM", "")
                           .Replace("PM", "")
                           .TrimEnd().Replace("  ", " ");
                    DateTime.TryParseExact(date, 
                        new string[] {"MM-dd-yy hh:mm PM"}, 
                        new System.Globalization.CultureInfo("en-US"),
                        System.Globalization.DateTimeStyles.None,
                        out di.Date);
                    if (dateOrig.Contains("PM") && di.Date != DateTime.MinValue)
                        di.Date.AddHours(12.0);

                    string size = CurItem.Substring(20, 18);
                    Int64.TryParse(size, out di.Size);
                }

                di.IsDirectory = CurItem.StartsWith("d");
                di.TextToShow = filename;

                try
                {
                    if (di.IsDirectory)
                    {
                        di.IconSmall = Utilities.GetIconForMIME("x-fcmd/directory");
                    }
                    else
                    {
                        di.MIMEType = filename.LastIndexOf('.') > 0
                            ? Utilities.GetContentType(filename.Substring(filename.LastIndexOf('.') + 1))
                            : "application/octet-stream";

                        if (Toolkit.CurrentEngine != null)
                            di.IconSmall = Utilities.GetIconForMIME(di.MIMEType);
                    }
                    di.Date = DateTime.Parse(
                        m.Groups["month"].Value + " " + m.Groups["day"].Value + " " + 
                        m.Groups["yearTime"].Value);
                }
                // ReSharper disable once EmptyGeneralCatchClause
                catch { } //to prevent crash if date is received in an invalid format

                di.URL = "ftp://" + URI.Host + ":" + URI.Port + URI.PathAndQuery + filename;
                if (di.IsDirectory && !di.URL.EndsWith("/"))
                    di.URL += "/";

                if (!string.IsNullOrWhiteSpace(di.TextToShow))
                    dirContent.Add(di);
            }

            //ftp.ReadResponse();
            this.dirContent = dirContent;
            req.Dispose();
            directoryListResponse.Dispose();
            success = true;
        }

        public void Connect(string url)
        {
            if (url.Contains("ftps:") || url.Contains(":22/"))
            {
                IsSsl = true;
                // url = url.Replace("ftps:", "ftp:");
            }
            else
                IsSsl = false;

            Uri adr = new Uri(url);
            if (IsSsl)
                adr = new Uri("ftp://"
                        + (adr.UserInfo.Length > 0 ? adr.UserInfo + "@" : string.Empty)
                        + adr.Host + ":22" + adr.PathAndQuery);

            hostname = adr.Host;

            _CheckProtocol(url);

            // FtpWebRequest.DefaultWebProxy 

            ServicePointManager.ServerCertificateValidationCallback = ServerCertificateValidationCallback;
            ftp = (FtpWebRequest)FtpWebRequest.CreateDefault(adr);
            ftp.UsePassive = true;

            //new FTPClient(
            //    adr.Host,
            //    adr.Port,
            //    "anonymous",
            //    @"test@test.org" // ru"
            //);
            hostname = adr.Host;
        }

        private bool ServerCertificateValidationCallback(object sender,
            X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            // If the certificate is a valid, signed certificate, return true.
            if (sslPolicyErrors == System.Net.Security.SslPolicyErrors.None)
            {
                return true;
            }

            // If there are errors in the certificate chain, look at each error to determine the cause.
            if ((sslPolicyErrors & System.Net.Security.SslPolicyErrors.RemoteCertificateChainErrors) != 0)
            {
                if (chain != null && chain.ChainStatus != null)
                {
                    foreach (System.Security.Cryptography.X509Certificates.X509ChainStatus status in chain.ChainStatus)
                    {
                        if ((certificate.Subject == certificate.Issuer) &&
                           (status.Status == System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.UntrustedRoot))
                        {
                            // Self-signed certificates with an untrusted root are valid. 
                            continue;
                        }
                        else
                        {
                            if (status.Status != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
                            {
                                // If there are any other errors in the certificate chain, the certificate is invalid,
                                // so the method returns false.
                                return false;
                            }
                        }
                    }
                }

                // When processing reaches this line, the only errors in the certificate chain are 
                // untrusted root errors for self-signed certificates. These certificates are valid
                // for default Exchange server installations, so return true.
                return true;
            }
            else
            {
                // In all other cases, return false.
                return false;
            }
        }

        public IEnumerable<DirItem> DirectoryContent
        {
            get { return dirContent; }
        }

        public void GetDirectoryContent(ref List<pluginner.DirItem> output, FileSystemOperationStatus FSOS)
        {
            output = dirContent;
        }

        public string CurrentDirectory
        {
            get { return currentDirectory; }
            set { LoadDir(value); }
        }

        public bool FileExists(string URL)
        {
            return dirContent.Any(di => di.URL == URL);
        }

        public bool DirectoryExists(string URL)
        {
            return dirContent.Any(di => di.URL == URL);
        }

        public bool CanBeRead(string URL)
        {
            throw new NotImplementedException();
        }

        public FSEntryMetadata GetMetadata(string URL)
        {
            //throw new NotImplementedException();
            return new FSEntryMetadata();
        }

        public byte[] GetFileContent(string URL)
        {
            throw new NotImplementedException();
        }

        public Stream GetFileStream(string URL, bool Write = false)
        {
            Uri URI = new Uri(URL);
            throw new NotImplementedException("no GetFileStream");

            //if (Write)
            //{
            //    //write mode
            //    NetworkStream ns = null; // new NetworkStream(ftp.GetDataSocket(), FileAccess.ReadWrite);
            //    return ns;
            //}
            //else
            //{
            //    //read-only mode
            //    NetworkStream ns = null; //new NetworkStream(ftp.GetDataSocket(), FileAccess.Read);
            //    //ftp.SendCommand("RETR " + URI.PathAndQuery);
            //    return ns;
            //}
        }

        public void WriteFileContent(string URL, int Start, byte[] Content)
        {
            throw new NotImplementedException();
        }

        public void Touch(FSEntryMetadata metadata)
        {
            throw new NotImplementedException();
        }

        public void Touch(string URL)
        {
            throw new NotImplementedException();
        }

        public void DeleteFile(string URL)
        {
            Uri URI = new Uri(URL);
            //ftp.SendCommand("DELE " + URI.AbsolutePath);
        }

        public void MoveFile(string oldURL, string newURL)
        {
            throw new NotImplementedException();
        }

        public void DeleteDirectory(string URL, bool TrySafe)
        {
            Uri URI = new Uri(URL);
            //ftp.SendCommand("RMD " + URI.AbsolutePath);
        }

        public void CreateDirectory(string URL)
        {
            Uri URI = new Uri(URL);
            //ftp.SendCommand("MKD " + URI.AbsolutePath);
        }

        public void MoveDirectory(string OldURL, string NewURL)
        {
            throw new NotImplementedException();
        }

        public string DirSeparator
        {
            get { return "/"; }
        }

        public event TypedEvent<string> StatusChanged;

        public event TypedEvent<double> ProgressChanged;

        public void CLIstdinWriteLine(string StdIn)
        {
            throw new NotImplementedException();
        }

        public event TypedEvent<string> CLIstdoutDataReceived;

        public event TypedEvent<string> CLIstderrDataReceived;

        public event TypedEvent<string> CLIpromptChanged;

        public string Name { get { return "File Transfer Protocol"; } }
        public string Version { get { return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(); } }
        public string Author { get { return "A.T."; } }

        public int[] APICompatibility
        {
            get
            {
                int[] fapiver = { 0, 1, 0, 0, 1, 0 };
                return fapiver;
            }
        }

        public object APICallPlugin(string call, params object[] arguments)
        {
            return null;
        }

        public event TypedEvent<object[]> APICallHost;

        public System.Configuration.Configuration FCConfig
        {
            set { /*not used because the plugin is internal*/ }
        }
    }
}