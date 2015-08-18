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

namespace fcmd.base_plugins.fs
{
    public partial class localFileSystem : pluginner.IFSPlugin
    {
        public const string FilePrefix = "file://";

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

#pragma warning disable 0649, 0414  // is assigned but never used
        public event pluginner.TypedEvent<string> StatusChanged;
        public event pluginner.TypedEvent<double> ProgressChanged;
        public event pluginner.TypedEvent<object[]> APICallHost = null;

        protected void RaiseProgressChanged(double data)
        {
            Application.Invoke(delegate ()
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
            Application.Invoke(delegate ()
            {
                var handler = StatusChanged;
                if (handler != null)
                {
                    handler(data);
                }
            });
        }

        List<pluginner.DirItem> DirContent = new List<pluginner.DirItem>();

        #region dir

        protected string curDir;
        protected string rootDir;

        // public void GetDirectoryContent(ref List<pluginner.DirItem> output, FileSystemOperationStatus FSOS)
        public IEnumerable<pluginner.DirItem> GetDirectoryContent(FileSystemOperationStatus FSOS)
        {
#if DEBUG
            Console.WriteLine("DEBUG: {0} Loading the {1} has been started", DateTime.Now.ToLongTimeString(), curDir);
#endif
            DirContent.Clear();
            string InternalURL = curDir.Replace(localFileSystem.FilePrefix, string.Empty);
            FSOS.StatusMessage = string.Format(Localizator.GetString("DoingListdir"), "", InternalURL);

            pluginner.DirItem tmpVar = new pluginner.DirItem();

            // 
            string[] filesNames;
            IEnumerable<FileDataInfo> files = null;

            if (!OSVersionEx.IsWindows)
                filesNames = System.IO.Directory.GetFiles(InternalURL);
            else
                files = AiLib.IOFile.DirectoryEnum.ReadFilesInfo(InternalURL);

            string[] dirs = System.IO.Directory.GetDirectories(InternalURL);

            // UpDir element : элемент "вверх по древу"
            DirectoryInfo currentdir = new DirectoryInfo(InternalURL);
            if (currentdir.Parent != null)
            {
                tmpVar.URL = localFileSystem.FilePrefix + currentdir.Parent.FullName;   //  "file://"

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
                tmpVar.URL = localFileSystem.FilePrefix + directory;
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

                output.Add(tmpVar);
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

                tmpVar.URL = localFileSystem.FilePrefix + Path.Combine(InternalURL, Name).Replace('\\', '/');
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

                output.Add(tmpVar);
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
            Console.WriteLine("DEBUG: {0} Loading the {1} has been completed", DateTime.Now.ToLongTimeString(), InternalURL);
#endif
        }

        public string CurrentDirectory
        {
            get { return curDir; }
            set
            {
                curDir = value;
            }
        }

        public string Prefix { get { return FilePrefix; } }

        public string RootDirectory
        {
            get { return rootDir; }
            set { rootDir = value.Contains(Prefix) ? value : Prefix + value; }
        }
        // return OSVersionEx.IsWindows

        private void _CheckProtocol(string url)
        { //проверка на то, чтобы нечаянно через localfs не попытались зайти в ftp, webdav, реестр и т.п. :-)
            if (!url.StartsWith(Prefix))
                throw new pluginner.PleaseSwitchPluginException();
        }

        #endregion

        #region Methods

        public string DirSeparator { get { return Path.DirectorySeparatorChar.ToString(); } }

        public bool FileExists(string URL)
        {//проверить наличие файла
            _CheckProtocol(URL);
            string InternalURL = URL.Replace(localFileSystem.FilePrefix, string.Empty);
            if (File.Exists(InternalURL)) return true; //файл е?
            return false; //та ничого нэма! [не забываем, что return xxx прекращает выполнение подпрограммы]
        }

        public bool DirectoryExists(string URL)
        {//проверить наличие папки
            _CheckProtocol(URL);
            string InternalURL = URL.Replace(localFileSystem.FilePrefix, string.Empty);
            if (Directory.Exists(InternalURL)) return true; //каталох е?
            return false; //та ничого нэма! [не забываем, что return xxx прекращает выполнение подпрограммы]
        }

        public bool CanBeRead(string url)
        { //проверить файл/папку "URL" на читаемость
            _CheckProtocol(url);
            string InternalURL = url.Replace(localFileSystem.FilePrefix, string.Empty);

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
            string InternalURL = url.Replace(localFileSystem.FilePrefix, string.Empty);

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
            string InternalURL = URL.Replace(localFileSystem.FilePrefix, string.Empty);

            pluginner.FSEntryMetadata newmd = new pluginner.FSEntryMetadata();
            newmd.FullURL = InternalURL;
            newmd.CreationTimeUTC = DateTime.UtcNow;
            newmd.LastWriteTimeUTC = DateTime.UtcNow;
            Touch(newmd);
        }

        public System.IO.Stream GetFileStream(string url, bool Lock = false)
        { //запрос потока для файла
            _CheckProtocol(url);
            string InternalURL = url.Replace(localFileSystem.FilePrefix, string.Empty);

            FileAccess fa = (Lock ? FileAccess.ReadWrite : FileAccess.Read);

            return new FileStream(InternalURL, FileMode.Open, fa);
        }

        public byte[] GetFileContent(string url)
        {
            _CheckProtocol(url);
            string InternalURL = url.Replace(localFileSystem.FilePrefix, string.Empty);
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
            string InternalURL = url.Replace(localFileSystem.FilePrefix, string.Empty);

            FileStream fistr = new FileStream(InternalURL, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BinaryWriter biwr = new BinaryWriter(fistr);
            biwr.Write(Content, Start, Content.Length);
        }

        public void DeleteFile(string url)
        {//удалить файл
            _CheckProtocol(url);
            string InternalURL = url.Replace(localFileSystem.FilePrefix, string.Empty);

            File.Delete(InternalURL);
        }

        public void DeleteDirectory(string url, bool TryFirst)
        {//удалить папку
            _CheckProtocol(url);
            string InternalURL = url.Replace(localFileSystem.FilePrefix, string.Empty);
            if (TryFirst)
            {
                if (!CheckForDeletePossiblity(InternalURL)) throw new pluginner.ThisDirCannotBeRemovedException();
            }
            Directory.Delete(InternalURL, true);//рекурсивное удаление
        }

        public void CreateDirectory(string url)
        {//создать каталог
            _CheckProtocol(url);
            string InternalURL = url.Replace(localFileSystem.FilePrefix, string.Empty);

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
            string internalSource = source.Replace(localFileSystem.FilePrefix, string.Empty);
            string internalDestination = destination.Replace(localFileSystem.FilePrefix, string.Empty);

            File.Move(internalSource, internalDestination);
        }

        public void MoveDirectory(string source, string destination)
        {
            _CheckProtocol(source);
            string internalSource = source.Replace(localFileSystem.FilePrefix, string.Empty);
            string internalDestination = destination.Replace(localFileSystem.FilePrefix, string.Empty);

            Directory.Move(internalSource, internalDestination);
        }

        #endregion

        public pluginner.FSEntryMetadata GetMetadata(string url)
        {
            _CheckProtocol(url);
            string InternalURL = url.Replace(localFileSystem.FilePrefix, string.Empty);
            pluginner.FSEntryMetadata lego = new pluginner.FSEntryMetadata();
            FileInfo metadatasource = new FileInfo(InternalURL);

            lego.Name = metadatasource.Name;
            lego.FullURL = url;
            try
            {
                lego.UpperDirectory = localFileSystem.FilePrefix + metadatasource.DirectoryName;
                lego.RootDirectory = localFileSystem.FilePrefix + metadatasource.Directory.Root.FullName;
                lego.Attrubutes = metadatasource.Attributes;
                lego.CreationTimeUTC = metadatasource.CreationTimeUtc;
                lego.IsReadOnly = metadatasource.IsReadOnly;
                lego.LastAccessTimeUTC = metadatasource.LastAccessTimeUtc;
                lego.LastWriteTimeUTC = metadatasource.LastWriteTimeUtc;
                if (!Directory.Exists(InternalURL)) lego.Lenght = metadatasource.Length;
            }
            catch (Exception ex) { Console.WriteLine("WARNING: can't build metadata lego for " + url + ": " + ex.Message + ex.StackTrace); }

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
    }
}
