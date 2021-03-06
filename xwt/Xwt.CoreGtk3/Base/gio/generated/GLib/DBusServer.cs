// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace GLib {

	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Runtime.InteropServices;

#region Autogenerated code
	public partial class DBusServer : GLib.Object, GLib.IInitable {

		public DBusServer (IntPtr raw) : base(raw) {}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern unsafe IntPtr g_dbus_server_new_sync(IntPtr address, int flags, IntPtr guid, IntPtr observer, IntPtr cancellable, out IntPtr error);

		public unsafe DBusServer (string address, GLib.DBusServerFlags flags, string guid, GLib.DBusAuthObserver observer, GLib.Cancellable cancellable) : base (IntPtr.Zero)
		{
			if (GetType () != typeof (DBusServer)) {
				throw new InvalidOperationException ("Can't override this constructor.");
			}
			IntPtr native_address = GLib.Marshaller.StringToPtrGStrdup (address);
			IntPtr native_guid = GLib.Marshaller.StringToPtrGStrdup (guid);
			IntPtr error = IntPtr.Zero;
			Raw = g_dbus_server_new_sync(native_address, (int) flags, native_guid, observer == null ? IntPtr.Zero : observer.Handle, cancellable == null ? IntPtr.Zero : cancellable.Handle, out error);
			GLib.Marshaller.Free (native_address);
			GLib.Marshaller.Free (native_guid);
			if (error != IntPtr.Zero) throw new GLib.GException (error);
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern int g_dbus_server_get_flags(IntPtr raw);

		[GLib.Property ("flags")]
		public GLib.DBusServerFlags Flags {
			get  {
				int raw_ret = g_dbus_server_get_flags(Handle);
				GLib.DBusServerFlags ret = (GLib.DBusServerFlags) raw_ret;
				return ret;
			}
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr g_dbus_server_get_guid(IntPtr raw);

		[GLib.Property ("guid")]
		public string Guid {
			get  {
				IntPtr raw_ret = g_dbus_server_get_guid(Handle);
				string ret = GLib.Marshaller.Utf8PtrToString (raw_ret);
				return ret;
			}
		}

		[GLib.Property ("address")]
		public string Address {
			get {
				GLib.Value val = GetProperty ("address");
				string ret = (string) val;
				val.Dispose ();
				return ret;
			}
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr g_dbus_server_get_client_address(IntPtr raw);

		[GLib.Property ("client-address")]
		public string ClientAddress {
			get  {
				IntPtr raw_ret = g_dbus_server_get_client_address(Handle);
				string ret = GLib.Marshaller.Utf8PtrToString (raw_ret);
				return ret;
			}
		}

		[GLib.Property ("active")]
		public bool Active {
			get {
				GLib.Value val = GetProperty ("active");
				bool ret = (bool) val;
				val.Dispose ();
				return ret;
			}
		}

		[GLib.Property ("authentication-observer")]
		public GLib.DBusAuthObserver AuthenticationObserver {
			get {
				GLib.Value val = GetProperty ("authentication-observer");
				GLib.DBusAuthObserver ret = (GLib.DBusAuthObserver) val;
				val.Dispose ();
				return ret;
			}
		}

		[GLib.Signal("new-connection")]
		public event GLib.NewConnectionHandler NewConnection {
			add {
				this.AddSignalHandler ("new-connection", value, typeof (GLib.NewConnectionArgs));
			}
			remove {
				this.RemoveSignalHandler ("new-connection", value);
			}
		}

		static NewConnectionNativeDelegate NewConnection_cb_delegate;
		static NewConnectionNativeDelegate NewConnectionVMCallback {
			get {
				if (NewConnection_cb_delegate == null)
					NewConnection_cb_delegate = new NewConnectionNativeDelegate (NewConnection_cb);
				return NewConnection_cb_delegate;
			}
		}

		static void OverrideNewConnection (GLib.GType gtype)
		{
			OverrideNewConnection (gtype, NewConnectionVMCallback);
		}

		static void OverrideNewConnection (GLib.GType gtype, NewConnectionNativeDelegate callback)
		{
			GDBusServerClass class_iface = GetClassStruct (gtype, false);
			class_iface.NewConnection = callback;
			OverrideClassStruct (gtype, class_iface);
		}

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate bool NewConnectionNativeDelegate (IntPtr inst, IntPtr connection);

		static bool NewConnection_cb (IntPtr inst, IntPtr connection)
		{
			try {
				DBusServer __obj = GLib.Object.GetObject (inst, false) as DBusServer;
				bool __result;
				__result = __obj.OnNewConnection (GLib.Object.GetObject(connection) as GLib.DBusConnection);
				return __result;
			} catch (Exception e) {
				GLib.ExceptionManager.RaiseUnhandledException (e, true);
				// NOTREACHED: above call does not return.
				throw e;
			}
		}

		[GLib.DefaultSignalHandler(Type=typeof(GLib.DBusServer), ConnectionMethod="OverrideNewConnection")]
		protected virtual bool OnNewConnection (GLib.DBusConnection connection)
		{
			return InternalNewConnection (connection);
		}

		private bool InternalNewConnection (GLib.DBusConnection connection)
		{
			NewConnectionNativeDelegate unmanaged = GetClassStruct (this.LookupGType ().GetThresholdType (), true).NewConnection;
			if (unmanaged == null) return false;

			bool __result = unmanaged (this.Handle, connection == null ? IntPtr.Zero : connection.Handle);
			return __result;
		}

		[StructLayout (LayoutKind.Sequential)]
		struct GDBusServerClass {
			public NewConnectionNativeDelegate NewConnection;
		}

		static uint class_offset = ((GLib.GType) typeof (GLib.Object)).GetClassSize ();
		static Dictionary<GLib.GType, GDBusServerClass> class_structs;

		static GDBusServerClass GetClassStruct (GLib.GType gtype, bool use_cache)
		{
			if (class_structs == null)
				class_structs = new Dictionary<GLib.GType, GDBusServerClass> ();

			if (use_cache && class_structs.ContainsKey (gtype))
				return class_structs [gtype];
			else {
				IntPtr class_ptr = new IntPtr (gtype.GetClassPtr ().ToInt64 () + class_offset);
				GDBusServerClass class_struct = (GDBusServerClass) Marshal.PtrToStructure (class_ptr, typeof (GDBusServerClass));
				if (use_cache)
					class_structs.Add (gtype, class_struct);
				return class_struct;
			}
		}

		static void OverrideClassStruct (GLib.GType gtype, GDBusServerClass class_struct)
		{
			IntPtr class_ptr = new IntPtr (gtype.GetClassPtr ().ToInt64 () + class_offset);
			Marshal.StructureToPtr (class_struct, class_ptr, false);
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr g_dbus_server_get_type();

		public static new GLib.GType GType { 
			get {
				IntPtr raw_ret = g_dbus_server_get_type();
				GLib.GType ret = new GLib.GType(raw_ret);
				return ret;
			}
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool g_dbus_server_is_active(IntPtr raw);

		public bool IsActive { 
			get {
				bool raw_ret = g_dbus_server_is_active(Handle);
				bool ret = raw_ret;
				return ret;
			}
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void g_dbus_server_start(IntPtr raw);

		public void Start() {
			g_dbus_server_start(Handle);
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void g_dbus_server_stop(IntPtr raw);

		public void Stop() {
			g_dbus_server_stop(Handle);
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool g_initable_init(IntPtr raw, IntPtr cancellable, out IntPtr error);

		public bool Init(GLib.Cancellable cancellable) {
			IntPtr error = IntPtr.Zero;
			bool raw_ret = g_initable_init(Handle, cancellable == null ? IntPtr.Zero : cancellable.Handle, out error);
			bool ret = raw_ret;
			if (error != IntPtr.Zero) throw new GLib.GException (error);
			return ret;
		}

#endregion
	}
}
