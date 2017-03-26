/* The File Commander base plugins - Local filesystem adapter
 * The main part of the LocalFS FS plugin
 * (C) The File Commander Team - https://github.com/atauenis/fcmd
 * (C) 2013-15, Alexander Tauenis (atauenis@yandex.ru)
 * (С) 2014, Zhigunov Andrew (breakneck11@gmail.com)
 * (C) 2014, Evgeny Akhtimirov (wilbit@me.com)
 * Contributors should place own signs here.
 */
using System;
using System.Collections.Generic;
using System.IO;
using pluginner;
using pluginner.Toolkit;
using Xwt;
using AiLib.IOFile;
using System.Runtime.Versioning;
using System.Security;
using System.Security.Permissions;
using System.Runtime.InteropServices;

namespace fcmd.base_plugins.fs
{
    public partial class NetworkFileSystem : LocalFileSystem, pluginner.IFSPlugin
    {
        public const string NetworkPrefix = "file://"; // ???
        public const string ShortPrefix = @"\\";

        public static bool IsNetworkPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return false;

            if (path.StartsWith(@"\\"))
                return true;

            if (path.StartsWith("192.168") && !DirectorySafe.Exists(path))
                return true;

            return false;
        }
    }

    // Directory Safe handler, exception safe
    public static class DirectorySafe
    {
        public static bool SetCurrentDirectory(string path)
        {
            bool error = false;
            try
            {
                Directory.SetCurrentDirectory(path);
            }
            catch { error = true; }
            return !error;
        }

        [SecurityCritical]
        [ResourceExposure(ResourceScope.Machine)]
        [ResourceConsumption(ResourceScope.Machine)]
        public static bool Exists(String path)
        {
            try
            {
                if (path == null)
                    return false;
                if (path.Length == 0)
                    return false;

                // Get fully qualified file name ending in \* for security check

                String fullPath = NormalizePath(path);
                // String demandPath = GetDemandDir(fullPath, true);
                // FileIOPermission.QuickDemand(FileIOPermissionAccess.Read, demandPath, false, false);

                int lastError = 0;
                bool exist = InternalExists(fullPath, out lastError);
                if (exist)
                    return true;
            }
            catch (ArgumentException) { }
            catch (NotSupportedException) { }  // Security can throw this on ":"
            catch (SecurityException) { }
            catch (IOException) { }
            catch (UnauthorizedAccessException)
            {
            }
            return false;
        }

        [SecuritySafeCritical]  // auto-generated
        [ResourceExposure(ResourceScope.Machine)]
        [ResourceConsumption(ResourceScope.Machine)]
        // internal unsafe 
        public static string NormalizePath(string path, bool fullCheck = false)
        {
            return Path.GetFullPath(path);
            // NormalizePath(path, fullCheck, AppContextSwitches.BlockLongPaths ? PathInternal.MaxShortPath : PathInternal.MaxLongPath);
        }

        [SecurityCritical]  // auto-generated
        [ResourceExposure(ResourceScope.Machine)]
        [ResourceConsumption(ResourceScope.Machine)]
        internal static bool InternalExists(String path, out int lastError)
        {
            Win32Native.WIN32_FILE_ATTRIBUTE_DATA data = new Win32Native.WIN32_FILE_ATTRIBUTE_DATA();
            lastError = Win32Native.FillAttributeInfo(path, ref data, false, true);

            return (lastError == Win32Native.ERROR_SUCCESS) && (data.fileAttributes != -1)
                    && ((data.fileAttributes & Win32Native.FILE_ATTRIBUTE_DIRECTORY) != 0);
        }

        public class Win32Native
        {
            internal static int FillAttributeInfo(String path,
                ref Win32Native.WIN32_FILE_ATTRIBUTE_DATA data, bool tryagain, bool returnErrorOnNotFound)
            {
                int dataInitialised = ERROR_SUCCESS;
                bool error = false;
                // if (tryagain) // someone has a handle to the file open, or other error

                Win32Native.WIN32_FIND_DATA findData;
                findData = new Win32Native.WIN32_FIND_DATA();

                // Remove trialing slash since this can cause grief to FindFirstFile. You will get an invalid argument error
                String tempPath = path.TrimEnd(new char[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar });
                // int oldMode = Win32Native.SetErrorMode(Win32Native.SEM_FAILCRITICALERRORS);
                try
                {
                    // SafeFindHandle 
                    SafeHandle handle = FindFirstFile(tempPath, findData);
                    try
                    {
                        if (handle.IsInvalid)
                        {
                            error = true;
                            dataInitialised = Marshal.GetLastWin32Error();

                            if (dataInitialised == Win32Native.ERROR_FILE_NOT_FOUND ||
                                dataInitialised == Win32Native.ERROR_PATH_NOT_FOUND ||
                                dataInitialised == Win32Native.ERROR_NOT_READY)  // floppy device not ready
                            {
                                if (!returnErrorOnNotFound)
                                {
                                    // Return default value for backward compatibility
                                    dataInitialised = ERROR_SUCCESS;
                                    data.fileAttributes = -1;
                                }
                            }
                            return dataInitialised;
                        }
                    }
                    finally
                    {
                        // Close the Win32 handle
                        try
                        {
                            handle.Close();
                        }
                        catch
                        {
                            error = true;
                            // if we're already returning an error, don't throw another one. 
                        }
                    }
                }
                finally
                {
                    // Win32Native.SetErrorMode(oldMode);
                }

                if (error)
                    return ERROR_PATH_NOT_FOUND;

                // Copy the information to data
                data.PopulateFrom(findData);

                return ERROR_SUCCESS;     // Success 
            }


            [DllImport(KERNEL32, SetLastError = true, CharSet = CharSet.Auto, BestFitMapping = false)]
            [ResourceExposure(ResourceScope.None)]
            internal static extern SafeHandle FindFirstFile(String fileName, [In, Out] Win32Native.WIN32_FIND_DATA data);

            [DllImport(KERNEL32, SetLastError = true, CharSet = CharSet.Auto, BestFitMapping = false)]
            [ResourceExposure(ResourceScope.None)]
            internal static extern bool FindNextFile(
                        SafeHandle hndFindFile, [In, Out, MarshalAs(UnmanagedType.LPStruct)]
                        WIN32_FIND_DATA lpFindFileData);

            internal const String KERNEL32 = "kernel32.dll";
            internal const String USER32 = "user32.dll";
            internal const String SHELL32 = "shell32.dll";

            // Constants from WinNT.h
            internal const int FILE_ATTRIBUTE_READONLY = 0x00000001;
            internal const int FILE_ATTRIBUTE_DIRECTORY = 0x00000010;
            // Error codes from WinError.h
            internal const int ERROR_SUCCESS = 0x0;
            internal const int ERROR_INVALID_FUNCTION = 0x1;
            internal const int ERROR_FILE_NOT_FOUND = 0x2;
            internal const int ERROR_PATH_NOT_FOUND = 0x3;
            internal const int ERROR_ACCESS_DENIED = 0x5;
            internal const int ERROR_NOT_READY = 0x15;  // floppy no ready :-)

            [Serializable]
            [StructLayout(LayoutKind.Sequential)]
            internal struct WIN32_FILE_ATTRIBUTE_DATA
            {
                internal int fileAttributes;
                internal uint ftCreationTimeLow;
                internal uint ftCreationTimeHigh;
                internal uint ftLastAccessTimeLow;
                internal uint ftLastAccessTimeHigh;
                internal uint ftLastWriteTimeLow;
                internal uint ftLastWriteTimeHigh;
                internal int fileSizeHigh;
                internal int fileSizeLow;

                [System.Security.SecurityCritical]
                internal void PopulateFrom(WIN32_FIND_DATA findData)
                {
                    // Copy the information to data
                    fileAttributes = findData.dwFileAttributes;
                    ftCreationTimeLow = findData.ftCreationTime_dwLowDateTime;
                    ftCreationTimeHigh = findData.ftCreationTime_dwHighDateTime;
                    ftLastAccessTimeLow = findData.ftLastAccessTime_dwLowDateTime;
                    ftLastAccessTimeHigh = findData.ftLastAccessTime_dwHighDateTime;
                    ftLastWriteTimeLow = findData.ftLastWriteTime_dwLowDateTime;
                    ftLastWriteTimeHigh = findData.ftLastWriteTime_dwHighDateTime;
                    fileSizeHigh = findData.nFileSizeHigh;
                    fileSizeLow = findData.nFileSizeLow;
                }
            }

            // Win32 Structs in N/Direct style
            [Serializable]
            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
            [BestFitMapping(false)]
            internal class WIN32_FIND_DATA
            {
                internal int dwFileAttributes = 0;
                // ftCreationTime was a by-value FILETIME structure
                internal uint ftCreationTime_dwLowDateTime = 0;
                internal uint ftCreationTime_dwHighDateTime = 0;
                // ftLastAccessTime was a by-value FILETIME structure
                internal uint ftLastAccessTime_dwLowDateTime = 0;
                internal uint ftLastAccessTime_dwHighDateTime = 0;
                // ftLastWriteTime was a by-value FILETIME structure
                internal uint ftLastWriteTime_dwLowDateTime = 0;
                internal uint ftLastWriteTime_dwHighDateTime = 0;
                internal int nFileSizeHigh = 0;
                internal int nFileSizeLow = 0;
                // If the file attributes' reparse point flag is set, then
                // dwReserved0 is the file tag (aka reparse tag) for the 
                // reparse point.  Use this to figure out whether something is
                // a volume mount point or a symbolic link.
                internal int dwReserved0 = 0;
                internal int dwReserved1 = 0;
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
                internal String cFileName = null;
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
                internal String cAlternateFileName = null;
            }

        }

    }


    public partial class LocalFileSystem : pluginner.IFSPlugin
    {
        public const string FilePrefix = "file://";
        #region Properties

        /* ЗАМЕТКА РАЗРАБОТЧИКУ				DEVELOPER NOTES
		 * В данном файле содержится код	This file contanis the local FS
		 * плагина доступа к локальным ФС.	adapter for the File Commander.
		 * Не забывайте про отличия сред	Please don't forget about the differences
		 * *nix и MacOS X от Windows.		between OSX, *nix and Win32.
		 * Код должен работать везде!		THE CODE MUST WORK EVERYWHERE!
		 */
        public string Name { get { return Localizator.GetString("LocalFSVer"); } }
        public string Version { get { return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(); } }
        public string Author { get { return "A.T.;ankr"; } }

        public System.Configuration.Configuration FCConfig { set { } } //it can be a placeholder because the LFS can use the fcmd.Properties.Settings...
        public IEnumerable<pluginner.DirItem> DirectoryContent { get { return DirContent; } } //возврат директории в FC

#pragma warning disable 0649, 0414, 0067  // is assigned but never used
        public event pluginner.TypedEvent<string> StatusChanged;
        public event pluginner.TypedEvent<double> ProgressChanged;
        public event pluginner.TypedEvent<object[]> APICallHost = null;

        public Exception LastError { get; set; }

        protected void RaiseProgressChanged(double data)
        {
            Application.Invoke(delegate()
            {
                var handler = ProgressChanged;
                if (handler != null)
                {
                    handler(data);
                }
            });
        }

        protected void RaiseStatusChanged(string data)
        {
            Application.Invoke(delegate()
            {
                var handler = StatusChanged;
                if (handler != null)
                {
                    handler(data);
                }
            });
        }

        List<pluginner.DirItem> DirContent = new List<pluginner.DirItem>();

        #endregion

        #region Folder

        protected string curDir;
        protected string rootDir;

        public IEnumerable<pluginner.DirItem> GetDirectoryContent(FileSystemOperationStatus FSOS)
        {
#if DEBUG
            Console.WriteLine("{0:HH:mm:ss.fff} DEBUG: Loading started {1} ", DateTime.Now, curDir);
#endif
            LastError = null;
            DirContent.Clear();
            string InternalURL = curDir.Replace(LocalFileSystem.FilePrefix, string.Empty);
            FSOS.StatusMessage = string.Format(Localizator.GetString("DoingListdir"), "", InternalURL);

            pluginner.DirItem tmpVar = new pluginner.DirItem();

            // 
            string[] filesNames;
            IEnumerable<FileDataInfo> files = null;

            if (!OSVersionEx.IsWindows)
                filesNames = System.IO.Directory.GetFiles(InternalURL);
            else
                files = AiLib.IOFile.DirectoryEnum.ReadFilesInfo(InternalURL);

            string[] dirs = null;
            Exception error = null;
            try
            {
                dirs = System.IO.Directory.GetDirectories(InternalURL);
            }
            catch (Exception ex) { error = ex; }    // access denied
            if (error != null)
            {
                this.LastError = error;
                yield break; // return System.Linq.Enumerable.Empty<pluginner.DirItem>();
            }

            // UpDir element : элемент "вверх по древу"
            DirectoryInfo currentdir = new DirectoryInfo(InternalURL);
            if (currentdir.Parent != null)
            {
                tmpVar.URL = LocalFileSystem.FilePrefix + currentdir.Parent.FullName;   //  "file://"

                // if (!MainWindow.AppLoading) { }

                tmpVar.TextToShow = "..";
                tmpVar.MIMEType = "x-fcmd/up";
                // tmpVar.IconSmall = Utilities.GetIconForMIME("x-fcmd/up");
                tmpVar.IsDirectory = true;
                // output.Add(tmpVar);
                yield return tmpVar;
            }

            float Progress = 0;
            // float FileWeight = 1 / ((float)files.Length + (float)dirs.Length);
            // uint counter = 0;
            // 2 ** 10 ~= 1000 (is about 1000)
            // so dispatching will be done every time 1000 files will have been looked throught
            // update_every == 00...0011...11 in binary format and count of '1' is 10
            // so (++counter & update_every) == 0 will be true after every 2 ** 10 ~= 1000
            // passed files

            // ankr
            // const uint update_every = ~(((~(uint)0) >> 10) << 10);

            foreach (string directory in dirs)
            {
                //перебираю каталоги
                DirectoryInfo di = new DirectoryInfo(directory);
                tmpVar.IsDirectory = true;
                tmpVar.URL = LocalFileSystem.FilePrefix + directory;
                tmpVar.TextToShow = di.Name;
                tmpVar.Date = di.CreationTime;

                if (di.Name.StartsWith("."))        // .git, .vs, .svn and other ignores
                {
                    tmpVar.Hidden = true;
                }
                else
                {
                    tmpVar.Hidden = false;
                }

                tmpVar.MIMEType = "x-fcmd/directory";
                //tmpVar.IconSmall = Utilities.GetIconForMIME("x-fcmd/directory");
                //output.Add(tmpVar);
                yield return tmpVar;

                FSOS.CompletePercents = (int)Progress * 100;

                // Progress += FileWeight;
                /*if ((++counter & update_every) == 0)
				{
					Xwt.Application.MainLoop.DispatchPendingEvents();
				}*/
            }

            if (files != null)
                foreach (FileDataInfo curFile in files)
                {
                    //FileInfo fi = new FileInfo(curFile);

                    tmpVar.IsDirectory = false;
                    var Name = curFile.Name;

                    tmpVar.URL = LocalFileSystem.FilePrefix + Path.Combine(InternalURL, Name).Replace('\\', '/');
                    tmpVar.TextToShow = curFile.cFileName; // fi.Name;
                    tmpVar.Date = curFile.LastWriteTime;   // fi.LastWriteTime;
                    tmpVar.Size = curFile.Length;          // fi.Length;

                    if (Name.StartsWith("."))
                    {
                        tmpVar.Hidden = true;
                    }
                    else
                    {
                        tmpVar.Hidden = false;
                    }

                    if (Name.LastIndexOf('.') > 0)
                        tmpVar.MIMEType = Utilities.GetContentType(
                            Name.Substring(Name.LastIndexOf('.') + 1));
                    else
                        tmpVar.MIMEType = "application/octet-stream";

                    // tmpVar.IconSmall = Utilities.GetIconForMIME(tmpVar.MIMEType);
                    // output.Add(tmpVar);
                    yield return tmpVar;

                    // Progress += FileWeight;
                    if (Progress <= 1)
                    {
                        FSOS.CompletePercents = (int)Progress * 100;
                    }
                    /*if ((++counter & update_every) == 0)
                    {
                        //Xwt.Application.MainLoop.DispatchPendingEvents();
                    }*/
                }


            RaiseCLIpromptChanged("FC: " + InternalURL + ">");
#if DEBUG
            Console.WriteLine("{0:HH:mm:ss.fff} DEBUG: Loading completed {1}", DateTime.Now, InternalURL);
#endif
        }

        public string CurrentFolder
        {
            get { return this.NoPrefix(curDir); }
        }

        public string CurrentDirectory
        {
            get { return curDir; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    curDir = value.StartsWith(Prefix) ? value : Prefix + value;
            }
        }

        public string Prefix { get { return FilePrefix; } }

        public string RootDirectory
        {
            get { return rootDir; }
            set { rootDir = value.Contains(Prefix) ? value : Prefix + value; }
        }

        private void _CheckProtocol(string url)
        {
            //проверка на то, чтобы нечаянно через localfs не попытались зайти в ftp, webdav, реестр и т.п. :-)
            if (!url.StartsWith(Prefix))
                throw new pluginner.PleaseSwitchPluginException();
        }

        #endregion

        #region Methods

        public string DirSeparator { get { return Path.DirectorySeparatorChar.ToString(); } }

        public bool FileExists(string URL)
        {//проверить наличие файла
            _CheckProtocol(URL);
            string InternalURL = URL.Replace(LocalFileSystem.FilePrefix, string.Empty);
            if (File.Exists(InternalURL)) return true; //файл е?
            return false; //та ничого нэма! [не забываем, что return xxx прекращает выполнение подпрограммы]
        }

        public bool DirectoryExists(string URL)
        {//проверить наличие папки
            _CheckProtocol(URL);
            string InternalURL = URL.Replace(LocalFileSystem.FilePrefix, string.Empty);
            if (Directory.Exists(InternalURL)) return true; //каталох е?
            return false; //та ничого нэма! [не забываем, что return xxx прекращает выполнение подпрограммы]
        }

        public bool CanBeRead(string url)
        { //проверить файл/папку "URL" на читаемость
            _CheckProtocol(url);
            string InternalURL = url.Replace(LocalFileSystem.FilePrefix, string.Empty);

            try
            {
                bool IsDir = Directory.Exists(InternalURL);
                if (IsDir)
                {//проверка читаемости каталога
                    System.IO.Directory.GetFiles(InternalURL);
                }
                else
                {//проверка читаемости файла
                    File.ReadAllBytes(InternalURL);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("LocalFS: Can't get access to " + InternalURL + "\nThe blocking reason is: " + ex.Message);
                return false;
            }
        }

        public void Touch(pluginner.FSEntryMetadata Metadata)
        {
            string url = Metadata.FullURL;
            _CheckProtocol(url);
            string InternalURL = url.Replace(LocalFileSystem.FilePrefix, string.Empty);

            if (!Directory.Exists(InternalURL) && !File.Exists(InternalURL))
            {
                StreamWriter sw = File.CreateText(InternalURL);
                sw.Close();
                sw.Dispose();
            }

            try
            {
                File.SetAttributes(InternalURL, Metadata.Attrubutes);
                File.SetCreationTime(InternalURL, Metadata.CreationTimeUTC);
                File.SetLastWriteTime(InternalURL, DateTime.Now);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Touch(string URL)
        {
            _CheckProtocol(URL);
            string InternalURL = URL.Replace(LocalFileSystem.FilePrefix, string.Empty);

            pluginner.FSEntryMetadata newmd = new pluginner.FSEntryMetadata();
            newmd.FullURL = InternalURL;
            newmd.CreationTimeUTC = DateTime.UtcNow;
            newmd.LastWriteTimeUTC = DateTime.UtcNow;
            Touch(newmd);
        }

        public System.IO.Stream GetFileStream(string url, bool Lock = false)
        { //запрос потока для файла
            _CheckProtocol(url);
            string InternalURL = url.Replace(LocalFileSystem.FilePrefix, string.Empty);

            FileAccess fa = (Lock ? FileAccess.ReadWrite : FileAccess.Read);

            return new FileStream(InternalURL, FileMode.Open, fa);
        }

        public byte[] GetFileContent(string url)
        {
            _CheckProtocol(url);
            string InternalURL = url.Replace(LocalFileSystem.FilePrefix, string.Empty);
            FileStream fistr = new FileStream(InternalURL, FileMode.Open, FileAccess.Read, FileShare.Read);
            BinaryReader bire = new BinaryReader(fistr);
            int Length = 0;
            try
            { Length = (int)fistr.Length; }
            catch
            { Length = int.MaxValue; Console.WriteLine("LOCALFS: the file is too long, reading only first " + int.MaxValue + " bytes.\nCall fs.GetFileContent() with length definition."); }
            return bire.ReadBytes(Length);
        }

        public void WriteFileContent(string url, Int32 Start, byte[] Content)
        {
            _CheckProtocol(url);
            string InternalURL = url.Replace(LocalFileSystem.FilePrefix, string.Empty);

            FileStream fistr = new FileStream(InternalURL, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BinaryWriter biwr = new BinaryWriter(fistr);
            biwr.Write(Content, Start, Content.Length);
        }

        public void DeleteFile(string url)
        {//удалить файл
            _CheckProtocol(url);
            string InternalURL = url.Replace(LocalFileSystem.FilePrefix, string.Empty);

            File.Delete(InternalURL);
        }

        public void DeleteDirectory(string url, bool TryFirst)
        {//удалить папку
            _CheckProtocol(url);
            string InternalURL = url.Replace(LocalFileSystem.FilePrefix, string.Empty);
            if (TryFirst)
            {
                if (!CheckForDeletePossiblity(InternalURL)) throw new pluginner.ThisDirCannotBeRemovedException();
            }
            Directory.Delete(InternalURL, true);//рекурсивное удаление
        }

        public void CreateDirectory(string url)
        {//создать каталог
            _CheckProtocol(url);
            string InternalURL = url.Replace(LocalFileSystem.FilePrefix, string.Empty);

            Directory.CreateDirectory(InternalURL);
        }

        /// <summary>
        /// Check the directory "url", it is may be purged&deleted
        /// </summary>
        /// <param name="url"></param>
        private bool CheckForDeletePossiblity(string url)
        {
            try
            {
                DirectoryInfo d = new DirectoryInfo(url);
                foreach (FileInfo file in d.GetFiles())
                {
                    //перебираю все файлы в каталоге
                    string newName = file.FullName + ".fcdeltest";
                    string oldName = file.FullName;
                    try
                    {
                        file.MoveTo(newName);
                        new FileInfo(newName).MoveTo(oldName);
                    }
                    catch (Exception nesudba)
                    {
#if DEBUG
                        Console.WriteLine("Check for deleteability was breaked by " + oldName + ": " + nesudba.Message);
#endif
                        return false;
                    }
                }

                foreach (DirectoryInfo dir in d.GetDirectories())
                {
                    //рекурсивно перебираю все подкаталоги в каталоге (папки хранятся в фейле, фейлы в подкаталогах, подкаталог в каталоге. Марь Иванна, правильно?)
                    return CheckForDeletePossiblity(dir.FullName);
                }
                return true;
            }
            catch (Exception ex) { Console.WriteLine("ERROR: CheckForDeletePossiblity failed: " + ex.Message + ex.StackTrace + "\nThe FC's crash was prevented. Please inform the program authors."); return false; }
        }

        public void MoveFile(string source, string destination)
        {
            _CheckProtocol(source);
            string internalSource = source.Replace(LocalFileSystem.FilePrefix, string.Empty);
            string internalDestination = destination.Replace(LocalFileSystem.FilePrefix, string.Empty);

            File.Move(internalSource, internalDestination);
        }

        public void MoveDirectory(string source, string destination)
        {
            _CheckProtocol(source);
            string internalSource = source.Replace(LocalFileSystem.FilePrefix, string.Empty);
            string internalDestination = destination.Replace(LocalFileSystem.FilePrefix, string.Empty);

            Directory.Move(internalSource, internalDestination);
        }

        #endregion

        public pluginner.FSEntryMetadata GetMetadata(string url)
        {
            _CheckProtocol(url);
            string InternalURL = url.Replace(LocalFileSystem.FilePrefix, string.Empty);
            pluginner.FSEntryMetadata lego = new pluginner.FSEntryMetadata();
            FileInfo metadatasource = new FileInfo(InternalURL);

            lego.Name = metadatasource.Name;
            lego.FullURL = url;
            try
            {
                if (string.IsNullOrWhiteSpace(lego.Name))
                    lego.Name = Path.GetDirectoryName(InternalURL) ?? InternalURL;  // Root dir

                var dirName = metadatasource.DirectoryName ?? InternalURL;
                lego.UpperDirectory = LocalFileSystem.FilePrefix + dirName;
                lego.RootDirectory = LocalFileSystem.FilePrefix + Path.GetPathRoot(dirName);

                lego.Attrubutes = metadatasource.Attributes;
                lego.CreationTimeUTC = metadatasource.CreationTimeUtc;
                lego.IsReadOnly = metadatasource.IsReadOnly;
                lego.LastAccessTimeUTC = metadatasource.LastAccessTimeUtc;
                lego.LastWriteTimeUTC = metadatasource.LastWriteTimeUtc;
                if (!Directory.Exists(InternalURL)) lego.Lenght = metadatasource.Length;
            }
            catch (Exception ex)
            {
                Console.WriteLine("WARNING: can't build metadata lego for " + url + ": " + ex.Message + ex.StackTrace);
            }

            return lego;
        }

        #region api

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

        #endregion

        /// <summary> Send new feedback data to UI</summary>
        /// <param name="Progress">The new progress value (or -1.79769e+308 if it should stay w/o changes): from 0.0 to 1.0 (or > 1.0 to hide the bar)</param>
        /// <param name="Status">The new status text (or null if it should stay w/o changes)</param>
        private void SetFeedback(double Progress = double.MinValue, string Status = null)
        {
            if (Progress != double.MinValue)
            {
                RaiseProgressChanged(Progress);
            }

            if (Status != null)
            {
                RaiseStatusChanged(Status);
            }
        }

        // public void GetDirectoryContent(ref List<pluginner.DirItem> output, FileSystemOperationStatus FSOS)
    }
}
