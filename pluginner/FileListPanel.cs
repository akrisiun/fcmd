﻿/* The File Commander - plugin API
 * The file list widget
 * (C) The File Commander Team - https://github.com/atauenis/fcmd
 * (C) 2013-14, Alexander Tauenis (atauenis@yandex.ru)
 * Contributors should place own signs here.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Reflection;
using System.IO;

namespace pluginner
{
	/// <summary>Filelist panel</summary>
	public class FileListPanel : Xwt.VBox
	{
		public int dfIcon = 0;
		public int dfURL = 1;
		public int dfDisplayName = 2;
		public int dfSize = 3;
		public int dfChanged = 4;

		public pluginner.IFSPlugin FS;
		public Xwt.TextEntry UrlBox = new Xwt.TextEntry();
		public pluginner.LightScroller DiskBox = new LightScroller();
		public Xwt.HBox DiskList = new Xwt.HBox();
		public List<Xwt.Button> DiskButtons = new List<Xwt.Button>();
		public ListView2 ListingView = new ListView2();
		public Xwt.HBox QuickSearchBox = new Xwt.HBox();
		public Xwt.TextEntry QuickSearchText = new Xwt.TextEntry();//по возможность заменить на SearchTextEntry (не раб. на wpf, see xwt bug 330)
		public Xwt.Label StatusBar = new Xwt.Label();
		public Xwt.Table StatusTable = new Xwt.Table();
		public Xwt.ProgressBar StatusProgressbar = new Xwt.ProgressBar();
		Xwt.TextEntry CLIoutput = new Xwt.TextEntry() { MultiLine = true, ShowFrame = true, Visible = false, HeightRequest = 50 };
		Xwt.TextEntry CLIprompt = new Xwt.TextEntry();

		/// <summary>User navigates into another directory</summary>
		public event TypedEvent<string> Navigate;
		/// <summary>User tried to open the highlighted file</summary>
		public event TypedEvent<string> OpenFile;

		public SizeDisplayPolicy CurShortenKB, CurShortenMB, CurShortenGB;
		private bool ProgressShown = false;
		private string QABarXML;
		private string SBtext1, SBtext2;
		private Stylist s;

		/// <summary>Initialize the FLP</summary>
		/// <param name="BookmarkXML">The bookmark database</param>
		/// <param name="CSS">The user theme (or null if it's need to use internal theme)</param>
		/// <param name="InfobarText1">The mask for infobar text when a file is selected</param>
		/// <param name="InfobarText2">The mask for infobar text when no files are selected</param>
		public FileListPanel(string BookmarkXML = null, string CSS=null, string InfobarText1 = "{Name}", string InfobarText2 = "F: {FileS}, D: {DirS}")
		{
			s = new Stylist(CSS);
			SBtext1 = InfobarText1;
			SBtext2 = InfobarText2;
			BuildUI(BookmarkXML);
			DiskBox.Content = DiskList;
			DiskBox.CanScrollByY = false;

			this.PackStart(DiskBox, false, true);
			this.PackStart(UrlBox, false, true);
			this.PackStart(ListingView, true, true);
			this.PackStart(QuickSearchBox, false, false);
			this.PackStart(CLIoutput);
			this.PackStart(CLIprompt);
			this.PackStart(StatusBar, false, true);

			WriteDefaultStatusLabel();

			CLIprompt.KeyReleased += new EventHandler<Xwt.KeyEventArgs>(CLIprompt_KeyReleased);

			QuickSearchText.GotFocus += (o, ea) => { this.OnGotFocus(ea); };
			QuickSearchText.KeyPressed += new EventHandler<Xwt.KeyEventArgs>(QuickSearchText_KeyPressed);
			QuickSearchBox.PackStart(QuickSearchText, true, true);
			QuickSearchBox.Visible = false;
		}

		void QuickSearchText_KeyPressed(object sender, Xwt.KeyEventArgs e)
		{
			if (e.Key == Xwt.Key.Escape)
			{
				QuickSearchText.Text = "";
				QuickSearchBox.Visible = false;
				ListingView.AllowedToPoint.Clear();
				return;
			}

			//search for good items
			ListingView.Sensitive = false;
			ListingView.AllowedToPoint.Clear();
			foreach (ListView2Item lvi in ListingView.Items)
			{
				if(lvi.Data[1].ToString().StartsWith(QuickSearchText.Text)){
					ListingView.AllowedToPoint.Add(lvi.RowNo);
				}
			}
			ListingView.Sensitive = true;

			//set pointer to the first good item (if need)
			if (ListingView.AllowedToPoint.Count > 0){
				if (ListingView.SelectedRow < ListingView.AllowedToPoint[0]
					||
					ListingView.SelectedRow > ListingView.AllowedToPoint[ListingView.AllowedToPoint.Count-1]
					)
				{
					ListingView.SelectedRow = ListingView.AllowedToPoint[0];
					ListingView.ScrollToRow(ListingView.AllowedToPoint[0]);
				}
			}
		}

		void CLIprompt_KeyReleased(object sender, Xwt.KeyEventArgs e)
		{
			if (e.Key == Xwt.Key.Return){
				CLIoutput.Visible = true;
				string stdin = CLIprompt.Text;
				CLIprompt.Text = "";
				FS.CLIstdinWriteLine(stdin);
			}
		}

		void FS_CLIpromptChanged(string data)
		{
			CLIprompt.PlaceholderText = data;
		}

		void FS_CLIstdoutDataReceived(string data)
		{
			Xwt.Application.Invoke(new Action(delegate
			{
				CLIoutput.Text += "\n" + data;
			}));
		}

		/// <summary>Make the panel's widgets</summary>
		/// <param name="BookmarkXML">Bookmark list XML data</param>
		public void BuildUI(string BookmarkXML = null)
		{
			//URL BOX
			UrlBox.ShowFrame = false;
			UrlBox.Text = @"file://C:\NC";
			UrlBox.GotFocus += (o, ea) => { this.OnGotFocus(ea); };
			UrlBox.KeyReleased += new EventHandler<Xwt.KeyEventArgs>(UrlBox_KeyReleased);

			//QUICK ACCESS BAR
			if (BookmarkXML == null){
				if (QABarXML == null){
					BookmarkXML = Utilities.GetEmbeddedResource("DefaultBookmarks.xml");
					if (BookmarkXML == null) throw new Exception("Cannot load pluginner.dll::DefaultBookmarks.xml");
					QABarXML = BookmarkXML;
				}
				else{
					BookmarkXML = QABarXML;
				}
			}
			DiskList.Clear();
			XmlDocument bmDoc = new XmlDocument();
			bmDoc.LoadXml(BookmarkXML);
			XmlNodeList items = bmDoc.GetElementsByTagName("SpeedDial");
			foreach (XmlNode x in items)
			{//parsing speed dials
				if (
					x.Attributes.GetNamedItem("type") != null
					&&
					x.Attributes.GetNamedItem("type").Value == "QuickAccessBar"
				)
				{
					foreach (XmlNode xc in x.ChildNodes)
					{//parsing bookmark list
						if (xc.Name == "AutoBookmarks")//автозакладка
						{
							switch (xc.Attributes.GetNamedItem("type").Value)
							{
								case "System.IO.DriveInfo.GetDrives":
									AddSysDrives();
									break;
								case "LinuxMounts":
									AddLinuxMounts();
									break;
								//todo: LinuxMounts (/mnt/), LinuxSystemDirs (/)
							}
						}
						else if (xc.Name == "Bookmark")//простая закладка
						{
							string url = xc.Attributes.GetNamedItem("url").Value;
							Xwt.Button NewBtn = new Xwt.Button(null, xc.Attributes.GetNamedItem("title").Value);
							NewBtn.Clicked += (o, ea) => { NavigateTo(url); };
							NewBtn.CanGetFocus = false;
							NewBtn.Style = Xwt.ButtonStyle.Flat;
							NewBtn.Margin = -3;
							NewBtn.Cursor = Xwt.CursorType.Hand;
							s.Stylize(NewBtn);
							DiskList.PackStart(NewBtn);
							/* todo: rewrite the code; possibly change the XWT to add toolbars
							 */
						}
						//todo: bookmark folders
					}
				}
			}

			foreach (Xwt.Button b in DiskButtons)
			{
				s.Stylize(b);
			}
			s.Stylize(DiskBox);
			s.Stylize(UrlBox);
			s.Stylize(ListingView);
			s.Stylize(QuickSearchBox);
			s.Stylize(CLIoutput,"TerminalOutput");
			s.Stylize(CLIprompt,"TerminalPrompt");
			s.Stylize(StatusBar);

			ListingView.ButtonPressed += new EventHandler<Xwt.ButtonEventArgs>(ListingView_ButtonPressed);
			ListingView.KeyReleased += new EventHandler<Xwt.KeyEventArgs>(ListingView_KeyReleased);
			ListingView.GotFocus += (o, ea) =>{ this.OnGotFocus(ea); };
			ListingView.PointerMoved += new TypedEvent<ListView2Item>(ListingView_PointerMoved);
			ListingView.SelectionChanged += new TypedEvent<List<ListView2Item>>(ListingView_SelectionChanged);
			StatusBar.Wrap = Xwt.WrapMode.Word;
		}

		void ListingView_SelectionChanged(List<ListView2Item> data)
		{
			WriteDefaultStatusLabel();
		}

		void ListingView_PointerMoved(ListView2Item data)
		{
			WriteDefaultStatusLabel();
		}

		void UrlBox_KeyReleased(object sender, Xwt.KeyEventArgs e)
		{
			if (e.Key == Xwt.Key.Return)
			{
				LoadDir(UrlBox.Text);
			}
		}

		void ListingView_KeyReleased(object sender, Xwt.KeyEventArgs e)
		{
			if (e.Key == Xwt.Key.Return && ListingView.SelectedRow > -1)
			{
				NavigateTo(ListingView.PointedItem.Data[dfURL].ToString());
				return;
			}
			if ((int)e.Key < 65000) //keys before 65000th are characters, numbers & other human stuff
			{
				QuickSearchText.Text += e.Key.ToString();
				QuickSearchBox.Visible = true;
				QuickSearchText.SetFocus();
				return;
			}
			if(Utilities.GetXwtBackendName() == "WPF")
			ListingView.OnKeyPressed(e);
		}

		void ListingView_ButtonPressed(object sender, Xwt.ButtonEventArgs e)
		{//FIXME: possibly unreachable code, archaism from Winforms/Xwt ListView-based ListPanel
			if (e.MultiplePress == 2)//double click
				NavigateTo(ListingView.PointedItem.Data[dfURL].ToString());
		}

		/// <summary>
		/// Open the FS item at <paramref name="url"/> (if it's file, load; if it's directory, go to)
		/// </summary>
		private void NavigateTo(string url)
		{
			try
			{

				if (FS.DirectoryExists(url))
				{//it's directory
					if (Navigate != null) Navigate(url); //raise event
					else Console.WriteLine("WARNING: the event FLP.Navigate was not handled by the host");

					LoadDir(url);
					return;
				}
				else
				{//it's file
					if (OpenFile != null) OpenFile(url); //raise event
					else Console.WriteLine("WARNING: the event FLP.OpenFile was not handled by the host");
				}

			}
			catch (pluginner.PleaseSwitchPluginException)
			{
				throw; //delegate authority to the mainwindow (it is it's business).
			}
			catch (Exception ex)
			{
				ListingView.Sensitive = true;
				ListingView.Cursor = Xwt.CursorType.Arrow;

				Xwt.MessageDialog.ShowError(ex.Message);
				Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
				WriteDefaultStatusLabel();
			}
		}

		/// <summary>
		/// Load the directory into the panel and set view options
		/// </summary>
		/// <param name="URL">Full path of the directory</param>
		/// <param name="ShortenKB">How kilobyte sizes should be humanized</param>
		/// <param name="ShortenMB">How megabyte sizes should be humanized</param>
		/// <param name="ShortenGB">How gigabyte sizes should be humanized</param> //плохой перевод? "так nбайтные размеры должны очеловечиваться"
		public void LoadDir(string URL, SizeDisplayPolicy ShortenKB, SizeDisplayPolicy ShortenMB, SizeDisplayPolicy ShortenGB)
		{
			CurShortenKB = ShortenKB; CurShortenMB = ShortenMB; CurShortenGB = ShortenGB;

			if (FS == null) throw new InvalidOperationException("No filesystem is binded to this FileListPanel");

			//неспешное TODO:придумать, куда лучше закорячить; не забываем, что во время работы FS может меняться полностью
			FS.CLIstdoutDataReceived += new TypedEvent<string>(FS_CLIstdoutDataReceived);
			FS.CLIpromptChanged += new TypedEvent<string>(FS_CLIpromptChanged);

			LoadDir(
				URL,
				FS.DirectoryContent,
				ShortenKB,
				ShortenMB,
				ShortenGB
				);
		}

		/// <summary>
		/// Load the specifed directory with specifed content into the panel and set view options
		/// </summary>
		/// <param name="URL">The full URL of the directory (for reference needs)</param>
		/// <param name="dis">Directory item list</param>
		/// <param name="ShortenKB">How kilobyte sizes should be humanized</param>
		/// <param name="ShortenMB">How megabyte sizes should be humanized</param>
		/// <param name="ShortenGB">How gigabyte sizes should be humanized</param> //плохой перевод? "так nбайтные размеры должны очеловечиваться"
		public void LoadDir(string URL, List<DirItem> dis, SizeDisplayPolicy ShortenKB, SizeDisplayPolicy ShortenMB, SizeDisplayPolicy ShortenGB)
		{
			ListingView.Cursor = Xwt.CursorType.IBeam;//todo: modify XWT and add hourglass cursor
			ListingView.Sensitive = false;

			try
			{
				FS.CurrentDirectory = URL;
				ListingView.Clear();
				UrlBox.Text = URL;
				FS.StatusChanged += new TypedEvent<string>(FS_StatusChanged);
				FS.ProgressChanged += new TypedEvent<double>(FS_ProgressChanged);

				foreach (DirItem di in dis)
				{
					List<Object> Data = new List<Object>();
					Data.Add(Utilities.GetIconForMIME(di.MIMEType));
					Data.Add(di.Path);
					Data.Add(di.TextToShow);
					if (di.TextToShow == "..")
					{//parent dir
						Data.Add("<↑ UP>");
						Data.Add(FS.GetMetadata(di.Path).LastWriteTimeUTC.ToLocalTime());
					}
					else if (di.IsDirectory)
					{//dir
						Data.Add("<DIR>");
						Data.Add(di.Date);
					}
					else
					{//file
						Data.Add(KiloMegaGigabyteConvert(di.Size, ShortenKB, ShortenMB, ShortenGB));
						Data.Add(di.Date);
					}
					Data.Add(di);
					ListingView.AddItem(Data, di.Path);
				}
			}
			catch (Exception ex)
			{
				if(ex.Message == "Object reference not set to an instance of an object."){
					Xwt.MessageDialog.ShowWarning(ex.Message, ex.StackTrace + "\nInner exception: " + ex.InnerException.Message ?? "none");
				}else
				Xwt.MessageDialog.ShowWarning(ex.Message);
			}
			ListingView.Sensitive = true;
			ListingView.Cursor = Xwt.CursorType.Arrow;
			if (ListingView.Items.Count > 0)
			{ ListingView.SelectedRow = 0; ListingView.ScrollerIn.ScrollTo(0, 0); }
			ListingView.SetFocus();//one fixed bug may make many other bugs...уточнить необходимость!
		
		}

		void FS_StatusChanged(string data)
		{
			if (data.Length == 0)
				WriteDefaultStatusLabel();
			else
				StatusBar.Text = data;
		}

		void FS_ProgressChanged(double data)
		{
			if (data > 0 && data <= 1){
				//show
				StatusProgressbar.Fraction = data;
				if (ProgressShown)
				{
					//do nothing; it's already updated
				}
				else
				{
					//show it
					this.Remove(StatusBar);
					this.PackStart(StatusTable);

					StatusTable.Clear();
#if !MONO //workaround for xwt/XWT bug https://github.com/mono/xwt/issues/283
					StatusTable.Add(new Xwt.Spinner() { Animate = true }, 0, 0, 1);
#endif
					StatusTable.Add(StatusBar, 1, 0);
					StatusTable.Add(StatusProgressbar, 1, 1);
					ProgressShown = true;
				}
			}
			else {
				//hide
				ProgressShown = false;
				try { this.Remove(StatusTable); StatusTable.Clear(); this.Remove(StatusBar); }
				catch { }

				try{this.PackStart(StatusBar);}
				catch { }
			}
		}

		/// <summary>
		/// Reloads the current directory
		/// </summary>
		public void LoadDir()
		{
			LoadDir(FS.CurrentDirectory);
		}

		/// <summary>
		/// Load the directory into the panel
		/// </summary>
		/// <param name="URL">Full path of the directory</param>
		public void LoadDir(string URL)
		{
			LoadDir(URL, CurShortenKB, CurShortenMB, CurShortenGB);
		}

		/// <summary>Converts the file size (in bytes) to human-readable string</summary>
		/// <param name="Input">The input value</param>
		/// <param name="ShortestNonhumanity">The miminal file size that should be shortened</param>
		/// <returns>Human-readable string (xxx yB)</returns>
		private string KiloMegaGigabyteConvert(long Input, SizeDisplayPolicy ShortenKB, SizeDisplayPolicy ShortenMB, SizeDisplayPolicy ShortenGB)
		{
			double ShortenedSize; //here will be writed the decimal value of the hum. readable size

			//TeraByte (will be shortened everywhen)
			if (Input > 1099511627776) return (Input / 1099511627776).ToString() + " TB";

			//GigaByte
			if (Input > 1073741824)
			{
				ShortenedSize = Input / 1073741824;
				switch (ShortenGB)
				{
					case SizeDisplayPolicy.OneNumeral:
						return string.Format("{0:0.#} GB", ShortenedSize);
					case SizeDisplayPolicy.TwoNumeral:
						return string.Format("{0:0.##} GB", ShortenedSize);
				}
			}

			//MegaByte
			if (Input > 1048576)
			{
				ShortenedSize = Input / 1048576;
				switch (ShortenMB)
				{
					case SizeDisplayPolicy.OneNumeral:
						return string.Format("{0:0.#} MB", ShortenedSize);
					case SizeDisplayPolicy.TwoNumeral:
						return string.Format("{0:0.##} MB", ShortenedSize);
				}
			}

			//KiloByte
			if (Input > 1024)
			{
				ShortenedSize = Input / 1024;
				switch (ShortenKB)
				{
					case SizeDisplayPolicy.OneNumeral:
						return string.Format("{0:0.#} KB", ShortenedSize);
					case SizeDisplayPolicy.TwoNumeral:
						return string.Format("{0:0.##} KB", ShortenedSize);
				}
			}

			return Input.ToString() + " B"; //if Input is less than 1k or shortening is disallowed
		}

		/// <summary>Defines the size shortening policy</summary>
		public enum SizeDisplayPolicy
		{
			DontShorten=0, OneNumeral=1, TwoNumeral=2
			//2048 B, 2 KB, 2.0 KB
		}
		
		/// <summary>
		/// Gets the selected row's value from the collumn №<paramref name="Field"/>
		/// </summary>
		/// <typeparam name="T">The type of the data</typeparam>
		/// <param name="Field">The field number</param>
		/// <returns>The value</returns>
		public T GetValue<T>(int Field){
			return (T)ListingView.PointedItem.Data[Field];
		}

		public string GetValue(int Field){
			return (string)ListingView.PointedItem.Data[Field];
		}

		/// <summary>Add autobookmark "system disks" onto disk toolbar</summary>
		private void AddSysDrives()
		{
			foreach (System.IO.DriveInfo di in System.IO.DriveInfo.GetDrives())
			{
				string d = di.Name;
				Xwt.Button NewBtn = new Xwt.Button(null, d);
				NewBtn.Clicked += (o, ea) => { NavigateTo("file://" + d); };
				NewBtn.CanGetFocus = false;
				NewBtn.Style = Xwt.ButtonStyle.Flat;
				NewBtn.Margin = -3;
				NewBtn.Cursor = Xwt.CursorType.Hand;
				NewBtn.Sensitive = di.IsReady;
				if (di.IsReady)
				{
					NewBtn.TooltipText = di.VolumeLabel + " (" + di.DriveFormat + ")";
				}
				/* todo: rewrite the code; possibly change the XWT to allow
				 * change the internal padding of the button.
				 */
				switch (di.DriveType)
				{
					case System.IO.DriveType.Fixed:
						NewBtn.Image = Xwt.Drawing.Image.FromResource(GetType(), "pluginner.Resources.drive-harddisk.png");
						break;
					case System.IO.DriveType.CDRom:
						NewBtn.Image = Xwt.Drawing.Image.FromResource(GetType(), "pluginner.Resources.drive-optical.png");
						break;
					case System.IO.DriveType.Removable:
						NewBtn.Image = Xwt.Drawing.Image.FromResource(GetType(), "pluginner.Resources.drive-removable-media.png");
						break;
					case System.IO.DriveType.Network:
						NewBtn.Image = Xwt.Drawing.Image.FromResource(GetType(), "pluginner.Resources.network-server.png");
						break;
					case System.IO.DriveType.Ram:
						NewBtn.Image = Xwt.Drawing.Image.FromResource(GetType(), "pluginner.Resources.emblem-system.png");
						break;
					case System.IO.DriveType.Unknown:
						NewBtn.Image = Xwt.Drawing.Image.FromResource(GetType(), "pluginner.Resources.image-missing.png");
						break;
				}

				//OS-specific icons
				if (d.StartsWith("A:")) NewBtn.Image = Xwt.Drawing.Image.FromResource(GetType(), "pluginner.Resources.media-floppy.png");
				if (d.StartsWith("B:")) NewBtn.Image = Xwt.Drawing.Image.FromResource(GetType(), "pluginner.Resources.media-floppy.png");
				if (d.StartsWith("/dev")) NewBtn.Image = Xwt.Drawing.Image.FromResource(GetType(), "pluginner.Resources.preferences-desktop-peripherals.png");
				if (d.StartsWith("/proc")) NewBtn.Image = Xwt.Drawing.Image.FromResource(GetType(), "pluginner.Resources.emblem-system.png");
				if (d == "/") NewBtn.Image = Xwt.Drawing.Image.FromResource(GetType(), "pluginner.Resources.root-folder.png");

				s.Stylize(NewBtn);
				DiskList.PackStart(NewBtn);
			}
		}
		
		/// <summary>Add buttons of mounted medias (*nix)</summary>
		private void AddLinuxMounts()
		{
			if (Directory.Exists(@"/mnt"))
			{
				foreach (string dir in Directory.GetDirectories(@"/mnt/"))
				{
					Xwt.Button NewBtn = new Xwt.Button(null, dir.Replace("/mnt/",""));
					NewBtn.Clicked += (o, ea) => { NavigateTo("file://" + dir); };
					NewBtn.CanGetFocus = false;
					NewBtn.Style = Xwt.ButtonStyle.Flat;
					NewBtn.Margin = -3;
					NewBtn.Cursor = Xwt.CursorType.Hand;
					NewBtn.Image = Xwt.Drawing.Image.FromResource(GetType(), "pluginner.Resources.drive-removable-media.png");

					s.Stylize(NewBtn);
					DiskList.PackStart(NewBtn);
				}
			}
			else AddSysDrives(); //fallback for Windows
		}

		/// <summary>
		/// Writes to statusbar the default text
		/// </summary>
		private void WriteDefaultStatusLabel()
		{
			if(ListingView.SelectedItems.Count<1)
			StatusBar.Text = MakeStatusbarText(SBtext1);
			else
			StatusBar.Text = MakeStatusbarText(SBtext2);
		}

		private string MakeStatusbarText(string Template)
		{
			string txt = Template;
			try
			{
				DirItem di = (DirItem)ListingView.PointedItem.Data[4];
				txt = txt.Replace("{FullName}", di.TextToShow);
				txt = txt.Replace("{AutoSize}", KiloMegaGigabyteConvert(di.Size,CurShortenKB,CurShortenMB,CurShortenMB));
				txt = txt.Replace("{Date}", di.Date.ToShortDateString());
				txt = txt.Replace("{Time}", di.Date.ToLocalTime().ToShortTimeString());
				txt = txt.Replace("{SelectedItems}", ListingView.SelectedItems.Count.ToString());
				//todo: add masks SizeB, SizeKB, SizeMB, TimeUTC, Name, Extension
			}
			catch { }
			return txt;
		}



	}
}
