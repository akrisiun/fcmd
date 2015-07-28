
using System;
using System.Collections.Generic;
// using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.WebKit;
using WbxReport;

namespace WbxMonoExcel
{
	public partial class MainWindowController : MonoMac.AppKit.NSWindowController
	{
		#region Constructors

		// Called when created from unmanaged code
		public MainWindowController (IntPtr handle) : base (handle)
		{
			Initialize ();
		}
		
		// Called when created directly from a XIB file
		[Export ("initWithCoder:")]
		public MainWindowController (NSCoder coder) : base (coder)
		{
			Initialize ();
		}
		
		// Call to load from the XIB/NIB file
		public MainWindowController () : base ("MainWindow")
		{
			Initialize ();
		}
		
		// Shared initialization code
		void Initialize ()
		{
		}

		#endregion

		//strongly typed window accessor
		public new MainWindow Window {
			get {
				return (MainWindow)base.Window;
			}
		}

		NSObject requestParam { get; set; }

		public override void AwakeFromNib ()
		{
			base.AwakeFromNib();
			this.Window.Title = "WbxMonoExcel";

			// web1.MainFrameUrl = @"https://www.google.com";
			// web1.MainFrame.LoadHtmlString (htmlString, null);
			MainWindowHelper.Init (this);

			web1.OnReceivedResponse += (object sender, WebResourceReceivedResponseEventArgs e) => 
			{
				requestParam = e.Identifier;
			};
			/*
			web1.OnSendRequest.Invoke(WebView sender, NSObject 
			virtual MonoMac.Foundation.NSUrlRequest 
				OnSendRequest (WebView sender, MonoMac.Foundation.NSObject identifier, 
					MonoMac.Foundation.NSUrlRequest request,
					MonoMac.Foundation.NSUrlResponse redirectResponse, WebDataSource dataSource)
				*/

		}

		partial void intoExcelClicked (MonoMac.Foundation.NSObject sender) 
		{
			if (IntoExcelClicked != null)
				IntoExcelClicked(sender, EventArgs.Empty);
		}

		partial void pasteClicked (MonoMac.Foundation.NSObject sender)
		{
			if (PasteClicked != null)
				PasteClicked(sender, EventArgs.Empty);
		}

		public event EventHandler PasteClicked;
		public event EventHandler IntoExcelClicked;

	}

}

/* webView.FinishedLoad += delegate {
String was_redirected_to_url = WEBVIEW.MainFrameUrl.ToString ();
Console.WriteLine( "in OnFinishedLoad, landed at: " + was_redirected_to_url );
}
*/
