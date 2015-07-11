// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace GLib {

	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Runtime.InteropServices;

#region Autogenerated code
	public partial class SocketService : GLib.SocketListener {

		public SocketService (IntPtr raw) : base(raw) {}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr g_socket_service_new();

		public SocketService () : base (IntPtr.Zero)
		{
			if (GetType () != typeof (SocketService)) {
				CreateNativeObject (new string [0], new GLib.Value[0]);
				return;
			}
			Raw = g_socket_service_new();
		}

		[GLib.Signal("incoming")]
		public event GLib.IncomingHandler Incoming {
			add {
				this.AddSignalHandler ("incoming", value, typeof (GLib.IncomingArgs));
			}
			remove {
				this.RemoveSignalHandler ("incoming", value);
			}
		}

		static IncomingNativeDelegate Incoming_cb_delegate;
		static IncomingNativeDelegate IncomingVMCallback {
			get {
				if (Incoming_cb_delegate == null)
					Incoming_cb_delegate = new IncomingNativeDelegate (Incoming_cb);
				return Incoming_cb_delegate;
			}
		}

		static void OverrideIncoming (GLib.GType gtype)
		{
			OverrideIncoming (gtype, IncomingVMCallback);
		}

		static void OverrideIncoming (GLib.GType gtype, IncomingNativeDelegate callback)
		{
			GSocketServiceClass class_iface = GetClassStruct (gtype, false);
			class_iface.Incoming = callback;
			OverrideClassStruct (gtype, class_iface);
		}

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate bool IncomingNativeDelegate (IntPtr inst, IntPtr connection, IntPtr source_object);

		static bool Incoming_cb (IntPtr inst, IntPtr connection, IntPtr source_object)
		{
			try {
				SocketService __obj = GLib.Object.GetObject (inst, false) as SocketService;
				bool __result;
				__result = __obj.OnIncoming (GLib.Object.GetObject(connection) as GLib.SocketConnection, GLib.Object.GetObject (source_object));
				return __result;
			} catch (Exception e) {
				GLib.ExceptionManager.RaiseUnhandledException (e, true);
				// NOTREACHED: above call does not return.
				throw e;
			}
		}

		[GLib.DefaultSignalHandler(Type=typeof(GLib.SocketService), ConnectionMethod="OverrideIncoming")]
		protected virtual bool OnIncoming (GLib.SocketConnection connection, GLib.Object source_object)
		{
			return InternalIncoming (connection, source_object);
		}

		private bool InternalIncoming (GLib.SocketConnection connection, GLib.Object source_object)
		{
			IncomingNativeDelegate unmanaged = GetClassStruct (this.LookupGType ().GetThresholdType (), true).Incoming;
			if (unmanaged == null) return false;

			bool __result = unmanaged (this.Handle, connection == null ? IntPtr.Zero : connection.Handle, source_object == null ? IntPtr.Zero : source_object.Handle);
			return __result;
		}

		[StructLayout (LayoutKind.Sequential)]
		struct GSocketServiceClass {
			public IncomingNativeDelegate Incoming;
			IntPtr GReserved1;
			IntPtr GReserved2;
			IntPtr GReserved3;
			IntPtr GReserved4;
			IntPtr GReserved5;
			IntPtr GReserved6;
		}

		static uint class_offset = ((GLib.GType) typeof (GLib.SocketListener)).GetClassSize ();
		static Dictionary<GLib.GType, GSocketServiceClass> class_structs;

		static GSocketServiceClass GetClassStruct (GLib.GType gtype, bool use_cache)
		{
			if (class_structs == null)
				class_structs = new Dictionary<GLib.GType, GSocketServiceClass> ();

			if (use_cache && class_structs.ContainsKey (gtype))
				return class_structs [gtype];
			else {
				IntPtr class_ptr = new IntPtr (gtype.GetClassPtr ().ToInt64 () + class_offset);
				GSocketServiceClass class_struct = (GSocketServiceClass) Marshal.PtrToStructure (class_ptr, typeof (GSocketServiceClass));
				if (use_cache)
					class_structs.Add (gtype, class_struct);
				return class_struct;
			}
		}

		static void OverrideClassStruct (GLib.GType gtype, GSocketServiceClass class_struct)
		{
			IntPtr class_ptr = new IntPtr (gtype.GetClassPtr ().ToInt64 () + class_offset);
			Marshal.StructureToPtr (class_struct, class_ptr, false);
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr g_socket_service_get_type();

		public static new GLib.GType GType { 
			get {
				IntPtr raw_ret = g_socket_service_get_type();
				GLib.GType ret = new GLib.GType(raw_ret);
				return ret;
			}
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool g_socket_service_is_active(IntPtr raw);

		public bool IsActive { 
			get {
				bool raw_ret = g_socket_service_is_active(Handle);
				bool ret = raw_ret;
				return ret;
			}
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void g_socket_service_start(IntPtr raw);

		public void Start() {
			g_socket_service_start(Handle);
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void g_socket_service_stop(IntPtr raw);

		public void Stop() {
			g_socket_service_stop(Handle);
		}

#endregion
	}
}
