// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace GtkSharp {

	using System;
	using System.Runtime.InteropServices;

#region Autogenerated code
	[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
	internal delegate bool TreeModelFilterVisibleFuncNative(IntPtr model, IntPtr iter, IntPtr data);

	internal class TreeModelFilterVisibleFuncInvoker {

		TreeModelFilterVisibleFuncNative native_cb;
		IntPtr __data;
		GLib.DestroyNotify __notify;

		~TreeModelFilterVisibleFuncInvoker ()
		{
			if (__notify == null)
				return;
			__notify (__data);
		}

		internal TreeModelFilterVisibleFuncInvoker (TreeModelFilterVisibleFuncNative native_cb) : this (native_cb, IntPtr.Zero, null) {}

		internal TreeModelFilterVisibleFuncInvoker (TreeModelFilterVisibleFuncNative native_cb, IntPtr data) : this (native_cb, data, null) {}

		internal TreeModelFilterVisibleFuncInvoker (TreeModelFilterVisibleFuncNative native_cb, IntPtr data, GLib.DestroyNotify notify)
		{
			this.native_cb = native_cb;
			__data = data;
			__notify = notify;
		}

		internal Gtk.TreeModelFilterVisibleFunc Handler {
			get {
				return new Gtk.TreeModelFilterVisibleFunc(InvokeNative);
			}
		}

		bool InvokeNative (Gtk.ITreeModel model, Gtk.TreeIter iter)
		{
			IntPtr native_iter = GLib.Marshaller.StructureToPtrAlloc (iter);
			bool __result = native_cb (model == null ? IntPtr.Zero : ((model is GLib.Object) ? (model as GLib.Object).Handle : (model as Gtk.TreeModelAdapter).Handle), native_iter, __data);
			iter = Gtk.TreeIter.New (native_iter);
			Marshal.FreeHGlobal (native_iter);
			return __result;
		}
	}

	internal class TreeModelFilterVisibleFuncWrapper {

		public bool NativeCallback (IntPtr model, IntPtr iter, IntPtr data)
		{
			try {
				bool __ret = managed (Gtk.TreeModelAdapter.GetObject (model, false), Gtk.TreeIter.New (iter));
				if (release_on_call)
					gch.Free ();
				return __ret;
			} catch (Exception e) {
				GLib.ExceptionManager.RaiseUnhandledException (e, false);
				return false;
			}
		}

		bool release_on_call = false;
		GCHandle gch;

		public void PersistUntilCalled ()
		{
			release_on_call = true;
			gch = GCHandle.Alloc (this);
		}

		internal TreeModelFilterVisibleFuncNative NativeDelegate;
		Gtk.TreeModelFilterVisibleFunc managed;

		public TreeModelFilterVisibleFuncWrapper (Gtk.TreeModelFilterVisibleFunc managed)
		{
			this.managed = managed;
			if (managed != null)
				NativeDelegate = new TreeModelFilterVisibleFuncNative (NativeCallback);
		}

		public static Gtk.TreeModelFilterVisibleFunc GetManagedDelegate (TreeModelFilterVisibleFuncNative native)
		{
			if (native == null)
				return null;
			TreeModelFilterVisibleFuncWrapper wrapper = (TreeModelFilterVisibleFuncWrapper) native.Target;
			if (wrapper == null)
				return null;
			return wrapper.managed;
		}
	}
#endregion
}
