// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace GLib {

	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Runtime.InteropServices;

#region Autogenerated code
	public partial class TcpConnection : GLib.SocketConnection {

		public TcpConnection (IntPtr raw) : base(raw) {}

		protected TcpConnection() : base(IntPtr.Zero)
		{
			CreateNativeObject (new string [0], new GLib.Value [0]);
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool g_tcp_connection_get_graceful_disconnect(IntPtr raw);

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void g_tcp_connection_set_graceful_disconnect(IntPtr raw, bool graceful_disconnect);

		[GLib.Property ("graceful-disconnect")]
		public bool GracefulDisconnect {
			get  {
				bool raw_ret = g_tcp_connection_get_graceful_disconnect(Handle);
				bool ret = raw_ret;
				return ret;
			}
			set  {
				g_tcp_connection_set_graceful_disconnect(Handle, value);
			}
		}

		[StructLayout (LayoutKind.Sequential)]
		struct GTcpConnectionClass {
		}

		static uint class_offset = ((GLib.GType) typeof (GLib.SocketConnection)).GetClassSize ();
		static Dictionary<GLib.GType, GTcpConnectionClass> class_structs;

		static GTcpConnectionClass GetClassStruct (GLib.GType gtype, bool use_cache)
		{
			if (class_structs == null)
				class_structs = new Dictionary<GLib.GType, GTcpConnectionClass> ();

			if (use_cache && class_structs.ContainsKey (gtype))
				return class_structs [gtype];
			else {
				IntPtr class_ptr = new IntPtr (gtype.GetClassPtr ().ToInt64 () + class_offset);
				GTcpConnectionClass class_struct = (GTcpConnectionClass) Marshal.PtrToStructure (class_ptr, typeof (GTcpConnectionClass));
				if (use_cache)
					class_structs.Add (gtype, class_struct);
				return class_struct;
			}
		}

		static void OverrideClassStruct (GLib.GType gtype, GTcpConnectionClass class_struct)
		{
			IntPtr class_ptr = new IntPtr (gtype.GetClassPtr ().ToInt64 () + class_offset);
			Marshal.StructureToPtr (class_struct, class_ptr, false);
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr g_tcp_connection_get_type();

		public static new GLib.GType GType { 
			get {
				IntPtr raw_ret = g_tcp_connection_get_type();
				GLib.GType ret = new GLib.GType(raw_ret);
				return ret;
			}
		}

#endregion
	}
}