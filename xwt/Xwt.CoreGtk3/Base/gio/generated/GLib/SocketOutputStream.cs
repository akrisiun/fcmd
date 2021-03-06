// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace GLib {

	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Runtime.InteropServices;

#region Autogenerated code
	public partial class SocketOutputStream : GLib.OutputStream, GLib.IPollableOutputStream {

		public SocketOutputStream (IntPtr raw) : base(raw) {}

		protected SocketOutputStream() : base(IntPtr.Zero)
		{
			CreateNativeObject (new string [0], new GLib.Value [0]);
		}

		[GLib.Property ("socket")]
		public GLib.Socket Socket {
			get {
				GLib.Value val = GetProperty ("socket");
				GLib.Socket ret = (GLib.Socket) val;
				val.Dispose ();
				return ret;
			}
		}

		[StructLayout (LayoutKind.Sequential)]
		struct GSocketOutputStreamClass {
		}

		static uint class_offset = ((GLib.GType) typeof (GLib.OutputStream)).GetClassSize ();
		static Dictionary<GLib.GType, GSocketOutputStreamClass> class_structs;

		static GSocketOutputStreamClass GetClassStruct (GLib.GType gtype, bool use_cache)
		{
			if (class_structs == null)
				class_structs = new Dictionary<GLib.GType, GSocketOutputStreamClass> ();

			if (use_cache && class_structs.ContainsKey (gtype))
				return class_structs [gtype];
			else {
				IntPtr class_ptr = new IntPtr (gtype.GetClassPtr ().ToInt64 () + class_offset);
				GSocketOutputStreamClass class_struct = (GSocketOutputStreamClass) Marshal.PtrToStructure (class_ptr, typeof (GSocketOutputStreamClass));
				if (use_cache)
					class_structs.Add (gtype, class_struct);
				return class_struct;
			}
		}

		static void OverrideClassStruct (GLib.GType gtype, GSocketOutputStreamClass class_struct)
		{
			IntPtr class_ptr = new IntPtr (gtype.GetClassPtr ().ToInt64 () + class_offset);
			Marshal.StructureToPtr (class_struct, class_ptr, false);
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool g_pollable_output_stream_can_poll(IntPtr raw);

		public bool CanPoll() {
			bool raw_ret = g_pollable_output_stream_can_poll(Handle);
			bool ret = raw_ret;
			return ret;
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr g_pollable_output_stream_create_source(IntPtr raw, IntPtr cancellable);

		public GLib.Source CreateSource(GLib.Cancellable cancellable) {
			IntPtr raw_ret = g_pollable_output_stream_create_source(Handle, cancellable == null ? IntPtr.Zero : cancellable.Handle);
			GLib.Source ret = new GLib.Source(raw_ret);
			return ret;
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool g_pollable_output_stream_is_writable(IntPtr raw);

		public bool IsWritable { 
			get {
				bool raw_ret = g_pollable_output_stream_is_writable(Handle);
				bool ret = raw_ret;
				return ret;
			}
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr g_pollable_output_stream_write_nonblocking(IntPtr raw, IntPtr buffer, UIntPtr size, IntPtr cancellable, out IntPtr error);

		public long WriteNonblocking(IntPtr buffer, ulong size, GLib.Cancellable cancellable) {
			IntPtr error = IntPtr.Zero;
			IntPtr raw_ret = g_pollable_output_stream_write_nonblocking(Handle, buffer, new UIntPtr (size), cancellable == null ? IntPtr.Zero : cancellable.Handle, out error);
			long ret = (long) raw_ret;
			if (error != IntPtr.Zero) throw new GLib.GException (error);
			return ret;
		}

#endregion
	}
}
