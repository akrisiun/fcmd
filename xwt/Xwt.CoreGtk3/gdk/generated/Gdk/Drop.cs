// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace Gdk {

	using System;
	using System.Runtime.InteropServices;

#region Autogenerated code
	public partial class Drop {

		[DllImport("libgdk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gdk_drop_finish(IntPtr context, bool success, uint time_);

		public static void Finish(Gdk.DragContext context, bool success, uint time_) {
			gdk_drop_finish(context == null ? IntPtr.Zero : context.Handle, success, time_);
		}

		[DllImport("libgdk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gdk_drop_reply(IntPtr context, bool accepted, uint time_);

		public static void Reply(Gdk.DragContext context, bool accepted, uint time_) {
			gdk_drop_reply(context == null ? IntPtr.Zero : context.Handle, accepted, time_);
		}

#endregion
	}
}
