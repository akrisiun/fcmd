// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace GLibSharp {

	using System;
	using System.Runtime.InteropServices;

#region Autogenerated code
	[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
	internal delegate void DBusInterfaceMethodCallFuncNative(IntPtr connection, IntPtr sender, IntPtr object_path, IntPtr interface_name, IntPtr method_name, IntPtr parameters, IntPtr invocation, IntPtr user_data);

	internal class DBusInterfaceMethodCallFuncInvoker {

		DBusInterfaceMethodCallFuncNative native_cb;
		IntPtr __data;
		GLib.DestroyNotify __notify;

		~DBusInterfaceMethodCallFuncInvoker ()
		{
			if (__notify == null)
				return;
			__notify (__data);
		}

		internal DBusInterfaceMethodCallFuncInvoker (DBusInterfaceMethodCallFuncNative native_cb) : this (native_cb, IntPtr.Zero, null) {}

		internal DBusInterfaceMethodCallFuncInvoker (DBusInterfaceMethodCallFuncNative native_cb, IntPtr data) : this (native_cb, data, null) {}

		internal DBusInterfaceMethodCallFuncInvoker (DBusInterfaceMethodCallFuncNative native_cb, IntPtr data, GLib.DestroyNotify notify)
		{
			this.native_cb = native_cb;
			__data = data;
			__notify = notify;
		}

		internal GLib.DBusInterfaceMethodCallFunc Handler {
			get {
				return new GLib.DBusInterfaceMethodCallFunc(InvokeNative);
			}
		}

		void InvokeNative (GLib.DBusConnection connection, string sender, string object_path, string interface_name, string method_name, GLib.Variant parameters, GLib.DBusMethodInvocation invocation)
		{
			IntPtr native_sender = GLib.Marshaller.StringToPtrGStrdup (sender);
			IntPtr native_object_path = GLib.Marshaller.StringToPtrGStrdup (object_path);
			IntPtr native_interface_name = GLib.Marshaller.StringToPtrGStrdup (interface_name);
			IntPtr native_method_name = GLib.Marshaller.StringToPtrGStrdup (method_name);
			native_cb (connection == null ? IntPtr.Zero : connection.Handle, native_sender, native_object_path, native_interface_name, native_method_name, parameters == null ? IntPtr.Zero : parameters.Handle, invocation == null ? IntPtr.Zero : invocation.Handle, __data);
			GLib.Marshaller.Free (native_sender);
			GLib.Marshaller.Free (native_object_path);
			GLib.Marshaller.Free (native_interface_name);
			GLib.Marshaller.Free (native_method_name);
		}
	}

	internal class DBusInterfaceMethodCallFuncWrapper {

		public void NativeCallback (IntPtr connection, IntPtr sender, IntPtr object_path, IntPtr interface_name, IntPtr method_name, IntPtr parameters, IntPtr invocation, IntPtr user_data)
		{
			try {
				managed (GLib.Object.GetObject(connection) as GLib.DBusConnection, GLib.Marshaller.Utf8PtrToString (sender), GLib.Marshaller.Utf8PtrToString (object_path), GLib.Marshaller.Utf8PtrToString (interface_name), GLib.Marshaller.Utf8PtrToString (method_name), new GLib.Variant(parameters), GLib.Object.GetObject(invocation) as GLib.DBusMethodInvocation);
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

		internal DBusInterfaceMethodCallFuncNative NativeDelegate;
		GLib.DBusInterfaceMethodCallFunc managed;

		public DBusInterfaceMethodCallFuncWrapper (GLib.DBusInterfaceMethodCallFunc managed)
		{
			this.managed = managed;
			if (managed != null)
				NativeDelegate = new DBusInterfaceMethodCallFuncNative (NativeCallback);
		}

		public static GLib.DBusInterfaceMethodCallFunc GetManagedDelegate (DBusInterfaceMethodCallFuncNative native)
		{
			if (native == null)
				return null;
			DBusInterfaceMethodCallFuncWrapper wrapper = (DBusInterfaceMethodCallFuncWrapper) native.Target;
			if (wrapper == null)
				return null;
			return wrapper.managed;
		}
	}
#endregion
}
