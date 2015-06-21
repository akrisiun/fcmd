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

namespace fcmd.ftps
{
    public class FtpSecure : IFSPlugin
    {
        // System.Net.FtpWebRequest
        // private FTPClient ftp;
        protected FtpWebRequest ftp;
        protected bool IsSsl = true;

        private string currentDirectory = "";
        private List<DirItem> directoryContent = new List<DirItem>();
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

            // if (ftp == null)
                Connect(url);

            // ReSharper disable once PossibleNullReferenceException, because Connect(url) would initialize this FTPClient or would crash this constructor
            // string ListResult;
            //Socket sck = ftp.GetDataSocket();//possible ftpexception, todo add try...catch
            //ftp.SendCommand("CWD " + URI.PathAndQuery);
            //ftp.SendCommand("TYPE A");
            //ftp.SendCommand("LIST", out ListResult);

            FtpWebRequest ftpRequest = this.ftp; // (FtpWebRequest)WebRequest.Create(myUrl);
            ftpRequest.Proxy = FtpWebRequest.DefaultWebProxy;

            NetworkCredential myCredentials = new NetworkCredential("anonymous", "ftp@ftp.com");
            // ftpRequest.Proxy = GlobalProxySelection.GetEmptyWebProxy();
            // requestDir.Credentials = new NetworkCredential("username", "password");
            if (!string.IsNullOrWhiteSpace(URI.UserInfo))
            {
                string[] userParts = url.Split(":@".ToCharArray());
                myCredentials = new NetworkCredential(URI.UserInfo, "win"); // temp;
            }

            // http://stackoverflow.com/questions/4584789/connecting-ftp-server-with-credentials
            ftpRequest.Credentials = myCredentials;
            ftpRequest.EnableSsl = true;
            // socket in Secure Socket Layer(SSL) mode, we use the class System.Net.Security.SslStream.

            ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;

            FtpWebResponse directoryListResponse = (FtpWebResponse)ftpRequest.GetResponse();

            //{
            //    using (StreamReader directoryListResponseReader 
            //        = new StreamReader(directoryListResponse.GetResponseStream()))
            //    {
            //        string responseString = 
            //            directoryListResponseReader.ReadToEnd();
            //        string[] results = responseString.Split(new string[] { "\r\n", "\n" },
            //            StringSplitOptions.RemoveEmptyEntries);
            //        return results;
            //    }
            //}

            string directoryListing = string.Empty;
            var req = directoryListResponse.GetResponseStream();
            StreamReader sr = new StreamReader(req);

            //using (FtpWebResponse ftpResponse = (FtpWebResponse)ftpRequest.GetResponse())
            //    string directoryListing = "";//убрать после разборки с форматами ответов на "LIST"
            // StreamReader sr = new StreamReader(new NetworkStream(sck));
            if (this.directoryContent.Count > 0)
                this.directoryContent.Clear();

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

                di.IsDirectory = CurItem.StartsWith("d");
                if (di.IsDirectory)
                {
                    di.IconSmall = Utilities.GetIconForMIME("x-fcmd/directory");
                }
                else
                {
                    di.MIMEType = filename.LastIndexOf('.') > 0
                        ? Utilities.GetContentType(filename.Substring(filename.LastIndexOf('.') + 1))
                        : "application/octet-stream";
                    di.IconSmall = Utilities.GetIconForMIME(di.MIMEType);
                }
                di.TextToShow = filename;
                try
                { di.Date = DateTime.Parse(m.Groups["month"].Value + " " + m.Groups["day"].Value + " " + m.Groups["yearTime"].Value); }
                // ReSharper disable once EmptyGeneralCatchClause
                catch { } //to prevent crash if date is received in an invalid format
                di.URL = "ftp://" + URI.Host + ":" + URI.Port + URI.PathAndQuery + filename;
                if (di.IsDirectory && !di.URL.EndsWith("/")) di.URL += "/";
                directoryContent.Add(di);
            }

            //ftp.ReadResponse();
            directoryListResponse.Dispose();

        }

        private void Connect(string url)
        {
            if (url.Contains("ftps:"))
            {
                IsSsl = true;
                url = url.Replace("ftps:", "ftp:");
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
        get { return directoryContent; }
    }

    public void GetDirectoryContent(ref List<pluginner.DirItem> output, FileSystemOperationStatus FSOS)
    {
        output = directoryContent;
    }

    public string CurrentDirectory
    {
        get { return currentDirectory; }
        set { LoadDir(value); }
    }

    public bool FileExists(string URL)
    {
        return directoryContent.Any(di => di.URL == URL);
    }

    public bool DirectoryExists(string URL)
    {
        return directoryContent.Any(di => di.URL == URL);
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