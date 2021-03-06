// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace GtkSharp {

	using System;
	using System.Runtime.InteropServices;

#region Autogenerated code
	[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
	internal delegate void ColorSelectionChangePaletteWithScreenFuncNative(IntPtr screen, IntPtr colors, int n_colors);

	internal class ColorSelectionChangePaletteWithScreenFuncInvoker {

		ColorSelectionChangePaletteWithScreenFuncNative native_cb;
		IntPtr __data;
		GLib.DestroyNotify __notify;

		~ColorSelectionChangePaletteWithScreenFuncInvoker ()
		{
			if (__notify == null)
				return;
			__notify (__data);
		}

		internal ColorSelectionChangePaletteWithScreenFuncInvoker (ColorSelectionChangePaletteWithScreenFuncNative native_cb) : this (native_cb, IntPtr.Zero, null) {}

		internal ColorSelectionChangePaletteWithScreenFuncInvoker (ColorSelectionChangePaletteWithScreenFuncNative native_cb, IntPtr data) : this (native_cb, data, null) {}

		internal ColorSelectionChangePaletteWithScreenFuncInvoker (ColorSelectionChangePaletteWithScreenFuncNative native_cb, IntPtr data, GLib.DestroyNotify notify)
		{
			this.native_cb = native_cb;
			__data = data;
			__notify = notify;
		}

		internal Gtk.ColorSelectionChangePaletteWithScreenFunc Handler {
			get {
				return new Gtk.ColorSelectionChangePaletteWithScreenFunc(InvokeNative);
			}
		}

		void InvokeNative (Gdk.Screen screen, Gdk.Color colors, int n_colors)
		{
			IntPtr native_colors = GLib.Marshaller.StructureToPtrAlloc (colors);
			native_cb (screen == null ? IntPtr.Zero : screen.Handle, native_colors, n_colors);
			colors = Gdk.Color.New (native_colors);
			Marshal.FreeHGlobal (native_colors);
		}
	}

	internal class ColorSelectionChangePaletteWithScreenFuncWrapper {

		public void NativeCallback (IntPtr screen, IntPtr colors, int n_colors)
		{
			try {
				managed (GLib.Object.GetObject(screen) as Gdk.Screen, Gdk.Color.New (colors), n_colors);
				if (release_on_call)
					gch.Free ();
			} catch (Exception e) {
				GLib.ExceptionManager.RaiseUnhandledException (e, false);
			}
		}

		bool release_on_call = false;
		GCHandle gch;

		public void PersistUntilCalled ()
		{
			release_on_call = true;
			gch = GCHandle.Alloc (this);
		}

		internal ColorSelectionChangePaletteWithScreenFuncNative NativeDelegate;
		Gtk.ColorSelectionChangePaletteWithScreenFunc managed;

		public ColorSelectionChangePaletteWithScreenFuncWrapper (Gtk.ColorSelectionChangePaletteWithScreenFunc managed)
		{
			this.managed = managed;
			if (managed != null)
				NativeDelegate = new ColorSelectionChangePaletteWithScreenFuncNative (NativeCallback);
		}

		public static Gtk.ColorSelectionChangePaletteWithScreenFunc GetManagedDelegate (ColorSelectionChangePaletteWithScreenFuncNative native)
		{
			if (native == null)
				return null;
			ColorSelectionChangePaletteWithScreenFuncWrapper wrapper = (ColorSelectionChangePaletteWithScreenFuncWrapper) native.Target;
			if (wrapper == null)
				return null;
			return wrapper.managed;
		}
	}
#endregion
}
