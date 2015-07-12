// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace Gtk {

	using System;
	using System.Runtime.InteropServices;

#region Autogenerated code
	public partial class Targets {

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool gtk_targets_include_image(IntPtr targets, int n_targets, bool writable);

		public static bool IncludeImage(Gdk.Atom targets, int n_targets, bool writable) {
			bool raw_ret = gtk_targets_include_image(targets == null ? IntPtr.Zero : targets.Handle, n_targets, writable);
			bool ret = raw_ret;
			return ret;
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool gtk_targets_include_rich_text(IntPtr targets, int n_targets, IntPtr buffer);

		public static bool IncludeRichText(Gdk.Atom targets, int n_targets, Gtk.TextBuffer buffer) {
			bool raw_ret = gtk_targets_include_rich_text(targets == null ? IntPtr.Zero : targets.Handle, n_targets, buffer == null ? IntPtr.Zero : buffer.Handle);
			bool ret = raw_ret;
			return ret;
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool gtk_targets_include_text(IntPtr targets, int n_targets);

		public static bool IncludeText(Gdk.Atom targets, int n_targets) {
			bool raw_ret = gtk_targets_include_text(targets == null ? IntPtr.Zero : targets.Handle, n_targets);
			bool ret = raw_ret;
			return ret;
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool gtk_targets_include_uri(IntPtr targets, int n_targets);

		public static bool IncludeUri(Gdk.Atom targets, int n_targets) {
			bool raw_ret = gtk_targets_include_uri(targets == null ? IntPtr.Zero : targets.Handle, n_targets);
			bool ret = raw_ret;
			return ret;
		}

#endregion
	}
}