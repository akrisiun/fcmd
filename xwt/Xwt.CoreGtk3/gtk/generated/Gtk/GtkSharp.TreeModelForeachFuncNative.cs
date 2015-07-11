// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace GtkSharp {

	using System;
	using System.Runtime.InteropServices;

#region Autogenerated code
	[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
	internal delegate bool TreeModelForeachFuncNative(IntPtr model, IntPtr path, IntPtr iter, IntPtr data);

	internal class TreeModelForeachFuncInvoker {

		TreeModelForeachFuncNative native_cb;
		IntPtr __data;
		GLib.DestroyNotify __notify;

		~TreeModelForeachFuncInvoker ()
		{
			if (__notify == null)
				return;
			__notify (__data);
		}

		internal TreeModelForeachFuncInvoker (TreeModelForeachFuncNative native_cb) : this (native_cb, IntPtr.Zero, null) {}

		internal TreeModelForeachFuncInvoker (TreeModelForeachFuncNative native_cb, IntPtr data) : this (native_cb, data, null) {}

		internal TreeModelForeachFuncInvoker (TreeModelForeachFuncNative native_cb, IntPtr data, GLib.DestroyNotify notify)
		{
			this.native_cb = native_cb;
			__data = data;
			__notify = notify;
		}

		internal Gtk.TreeModelForeachFunc Handler {
			get {
				return new Gtk.TreeModelForeachFunc(InvokeNative);
			}
		}

		bool InvokeNative (Gtk.ITreeModel model, Gtk.TreePath path, Gtk.TreeIter iter)
		{
			IntPtr native_iter = GLib.Marshaller.StructureToPtrAlloc (iter);
			bool __result = native_cb (model == null ? IntPtr.Zero : ((model is GLib.Object) ? (model as GLib.Object).Handle : (model as Gtk.TreeModelAdapter).Handle), path == null ? IntPtr.Zero : path.Handle, native_iter, __data);
			iter = Gtk.TreeIter.New (native_iter);
			Marshal.FreeHGlobal (native_iter);
			return __result;
		}
	}

	internal class TreeModelForeachFuncWrapper {

		public bool NativeCallback (IntPtr model, IntPtr path, IntPtr iter, IntPtr data)
		{
			try {
				bool __ret = managed (Gtk.TreeModelAdapter.GetObject (model, false), path == IntPtr.Zero ? null : (Gtk.TreePath) GLib.Opaque.GetOpaque (path, typeof (Gtk.TreePath), false), Gtk.TreeIter.New (iter));
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

		internal TreeModelForeachFuncNative NativeDelegate;
		Gtk.TreeModelForeachFunc managed;

		public TreeModelForeachFuncWrapper (Gtk.TreeModelForeachFunc managed)
		{
			this.managed = managed;
			if (managed != null)
				NativeDelegate = new TreeModelForeachFuncNative (NativeCallback);
		}

		public static Gtk.TreeModelForeachFunc GetManagedDelegate (TreeModelForeachFuncNative native)
		{
			if (native == null)
				return null;
			TreeModelForeachFuncWrapper wrapper = (TreeModelForeachFuncWrapper) native.Target;
			if (wrapper == null)
				return null;
			return wrapper.managed;
		}
	}
#endregion
}
