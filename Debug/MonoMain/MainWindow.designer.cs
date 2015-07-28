// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoMac.Foundation;
using System.CodeDom.Compiler;

namespace WbxMonoExcel
{
	[Register ("MainWindowController")]
	partial class MainWindowController
	{
		[Outlet]
		public MonoMac.AppKit.NSComboBox cbo1 { get; private set; }

		[Outlet]
		public MonoMac.AppKit.NSButton cmdIntoExcel { get; private set; }

		[Outlet]
		public MonoMac.AppKit.NSButton cmdPaste { get; private set; }

		[Outlet]
		public MonoMac.WebKit.WebView web1 { get; private set; }

		[Action ("intoExcelClicked:")]
		partial void intoExcelClicked (MonoMac.Foundation.NSObject sender);

		[Action ("pasteClicked:")]
		partial void pasteClicked (MonoMac.Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (cbo1 != null) {
				cbo1.Dispose ();
				cbo1 = null;
			}

			if (cmdIntoExcel != null) {
				cmdIntoExcel.Dispose ();
				cmdIntoExcel = null;
			}

			if (cmdPaste != null) {
				cmdPaste.Dispose ();
				cmdPaste = null;
			}

			if (web1 != null) {
				web1.Dispose ();
				web1 = null;
			}
		}
	}

	[Register ("MainWindow")]
	partial class MainWindow
	{
		
		void ReleaseDesignerOutlets ()
		{
		}
	}
}
