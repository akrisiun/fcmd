// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace GLibSharp {

	using System;
	using System.Runtime.InteropServices;

#region Autogenerated code
	[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
	internal delegate IntPtr DBusInterfaceGetPropertyFuncNative(IntPtr connection, IntPtr sender, IntPtr object_path, IntPtr interface_name, IntPtr property_name, out IntPtr error, IntPtr user_data);

	internal class DBusInterfaceGetPropertyFuncInvoker {

		DBusInterfaceGetPropertyFuncNative native_cb;
		IntPtr __data;
		GLib.DestroyNotify __notify;

		~DBusInterfaceGetPropertyFuncInvoker ()
		{
			if (__notify == null)
				return;
			__notify (__data);
		}

		internal DBusInterfaceGetPropertyFuncInvoker (DBusInterfaceGetPropertyFuncNative native_cb) : this (native_cb, IntPtr.Zero, null) {}

		internal DBusInterfaceGetPropertyFuncInvoker (DBusInterfaceGetPropertyFuncNative native_cb, IntPtr data) : this (native_cb, data, null) {}

		internal DBusInterfaceGetPropertyFuncInvoker (DBusInterfaceGetPropertyFuncNative native_cb, IntPtr data, GLib.DestroyNotify notify)
		{
			this.native_cb = native_cb;
			__data = data;
			__notify = notify;
		}

		internal GLib.DBusInterfaceGetPropertyFunc Handler {
			get {
				return new GLib.DBusInterfaceGetPropertyFunc(InvokeNative);
			}
		}

		GLib.Variant InvokeNative (GLib.DBusConnection connection, string sender, string object_path, string interface_name, string property_name)
		{
			IntPtr native_sender = GLib.Marshaller.StringToPtrGStrdup (sender);
			IntPtr native_object_path = GLib.Marshaller.StringToPtrGStrdup (object_path);
			IntPtr native_interface_name = GLib.Marshaller.StringToPtrGStrdup (interface_name);
			IntPtr native_property_name = GLib.Marshaller.StringToPtrGStrdup (property_name);
			IntPtr error = IntPtr.Zero;
			GLib.Variant __result = new GLib.Variant(native_cb (connection == null ? IntPtr.Zero : connection.Handle, native_sender, native_object_path, native_interface_name, native_property_name, out error, __data));
			GLib.Marshaller.Free (native_sender);
			GLib.Marshaller.Free (native_object_path);
			GLib.Marshaller.Free (native_interface_name);
			GLib.Marshaller.Free (native_property_name);
			return __result;
		}
	}

	internal class DBusInterfaceGetPropertyFuncWrapper {

		public IntPtr NativeCallback (IntPtr connection, IntPtr sender, IntPtr object_path, IntPtr interface_name, IntPtr property_name, out IntPtr error, IntPtr user_data)
		{
			error = IntPtr.Zero;

			try {
				GLib.Variant __ret = managed (GLib.Object.GetObject(connection) as GLib.DBusConnection, GLib.Marshaller.Utf8PtrToString (sender), GLib.Marshaller.Utf8PtrToString (object_path), GLib.Marshaller.Utf8PtrToString (interface_name), GLib.Marshaller.Utf8PtrToString (property_name));
				if (release_on_call)
					gch.Free ();
				return __ret == null ? IntPtr.Zero : __ret.Handle;
			} catch (Exception e) {
				GLib.ExceptionManager.RaiseUnhandledException (e, true);
				// NOTREACHED: Above call does not return.
				throw e;
			}
		}

		bool release_on_call = false;
		GCHandle gch;

		public void PersistUntilCalled ()
		{
			release_on_call = true;
			gch = GCHandle.Alloc (this);
		}

		internal DBusInterfaceGetPropertyFuncNative NativeDelegate;
		GLib.DBusInterfaceGetPropertyFunc managed;

		public DBusInterfaceGetPropertyFuncWrapper (GLib.DBusInterfaceGetPropertyFunc managed)
		{
			this.managed = managed;
			if (managed != null)
				NativeDelegate = new DBusInterfaceGetPropertyFuncNative (NativeCallback);
		}

		public static GLib.DBusInterfaceGetPropertyFunc GetManagedDelegate (DBusInterfaceGetPropertyFuncNative native)
		{
			if (native == null)
				return null;
			DBusInterfaceGetPropertyFuncWrapper wrapper = (DBusInterfaceGetPropertyFuncWrapper) native.Target;
			if (wrapper == null)
				return null;
			return wrapper.managed;
		}
	}
#endregion
}