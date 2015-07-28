using System;
// using System.Drawing;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;

namespace WbxMonoExcel
{
	public partial class AppDelegate : NSApplicationDelegate
	{
		MainWindowController mainWindowController;

		public AppDelegate ()
		{
		}

		public override void FinishedLaunching (NSObject notification)
		{
			mainWindowController = new MainWindowController ();
			mainWindowController.Window.MakeKeyAndOrderFront (this);
		}

		/*
		public override bool ApplicationShouldHandleReopen (NSApplication sender, bool hasVisibleWindows)
		{
			return base.ApplicationShouldHandleReopen (sender, hasVisibleWindows);
		}

		optional func applicationWillBecomeActive(_ aNotification: NSNotification)
		OBJECTIVE-C
		- (void)applicationWillBecomeActive:(NSNotification *)aNotification
		Parameters
		aNotification	
		A notification named NSApplicationWillBecomeActiveNotification. Calling the object method of this notification returns the NSApplication ob
*/

		public override void AwakeFromNib ()
		{
			base.AwakeFromNib ();
			if (mainWindowController != null)
				mainWindowController.Window.MakeKeyAndOrderFront (this);
		}
	}
}

