﻿/* The File Commander base plugins - Local filesystem adapter
 * The FTP filesystem plugin
 * (C) The File Commander Team - https://github.com/atauenis/fcmd
 * (C) 2014, Alexander Tauenis (atauenis@yandex.ru)
 * Contributors should place own signs here.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using pluginner;
using pluginner.Toolkit;

namespace fcmd.base_plugins.fs
{
	public class FTPFileSystem : IFSPlugin
	{
		private FTPClient ftp;
		private string currentDirectory = "";
		private List<DirItem> directoryContent = new List<DirItem>();
		private string hostname;

		private static Regex FtpListDirectoryDetailsRegex = new Regex(@".*(?<month>(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec))\s*(?<day>[0-9]*)\s*(?<yearTime>([0-9]|:)*)\s*(?<fileName>.*)", RegexOptions.Compiled | RegexOptions.IgnoreCase); //undone: add style switching (windows, unix, etc)

		private void _CheckProtocol(string url)
		{
			if (url == null) throw new ArgumentNullException("url");
			//проверка на то, чтобы нечаянно через ftpfs не попытались зайти в локальную ФС, webdav, реестр и т.п. :-)
			if (!url.ToLowerInvariant().StartsWith("ftp:")) throw new PleaseSwitchPluginException();

			Uri URI = new Uri(url);
			if (URI.Host != hostname) Connect(url);
		}

		private void LoadDir(string url)
		{
			currentDirectory = url;
			_CheckProtocol(url);

			Uri URI = new Uri(url);

			if (ftp == null)
				Connect(url);

// ReSharper disable once PossibleNullReferenceException, because Connect(url) would initialize it or would crash this constructor
			Socket sck = ftp.GetDataSocket();//possible ftpexception, todo add try...catch
			string ListResult;
			ftp.SendCommand("CWD " + URI.PathAndQuery);
			ftp.SendCommand("TYPE A");
			ftp.SendCommand("LIST", out ListResult);

			string directoryListing = "";//убрать после разборки с форматами ответов на "LIST"

			StreamReader sr = new StreamReader(new NetworkStream(sck));
			directoryContent.Clear();

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
				directoryListing += CurItem+"\n";

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
			ftp.ReadResponse();
		}

		private void Connect(string url)
		{	Uri adr = new Uri(url);
			hostname = adr.Host;
			_CheckProtocol(url);
			
			ftp = new FTPClient(
				adr.Host,
				adr.Port,
				"anonymous",
				@"test@test.ru"
			);
			hostname = adr.Host;
		}

		public IEnumerable<DirItem> DirectoryContent
		{
			get { return directoryContent; }
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
			if (Write)
			{
				//write mode
				NetworkStream ns = new NetworkStream(ftp.GetDataSocket(),FileAccess.ReadWrite);
				return ns;
			}
			else
			{
				//read-only mode
				NetworkStream ns = new NetworkStream(ftp.GetDataSocket(), FileAccess.Read);
				ftp.SendCommand("RETR " + URI.PathAndQuery);
				return ns;
			}
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
			ftp.SendCommand("DELE " + URI.AbsolutePath);
		}

		public void MoveFile(string oldURL, string newURL)
		{
			throw new NotImplementedException();
		}

		public void DeleteDirectory(string URL, bool TrySafe)
		{
			Uri URI = new Uri(URL);
			ftp.SendCommand("RMD " + URI.AbsolutePath);
		}

		public void CreateDirectory(string URL)
		{
			Uri URI = new Uri(URL);
			ftp.SendCommand("MKD " + URI.AbsolutePath);
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
				int[] fapiver = { 0, 1, 0,  0, 1, 0 };
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
