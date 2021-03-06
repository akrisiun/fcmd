// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace GLib {

	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Runtime.InteropServices;

#region Autogenerated code
	public partial class ThreadedSocketService : GLib.SocketService {

		public ThreadedSocketService (IntPtr raw) : base(raw) {}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr g_threaded_socket_service_new(int max_threads);

		public ThreadedSocketService (int max_threads) : base (IntPtr.Zero)
		{
			if (GetType () != typeof (ThreadedSocketService)) {
				var vals = new List<GLib.Value> ();
				var names = new List<string> ();
				names.Add ("max_threads");
				vals.Add (new GLib.Value (max_threads));
				CreateNativeObject (names.ToArray (), vals.ToArray ());
				return;
			}
			Raw = g_threaded_socket_service_new(max_threads);
		}

		[GLib.Property ("max-threads")]
		public int MaxThreads {
			get {
				GLib.Value val = GetProperty ("max-threads");
				int ret = (int) val;
				val.Dispose ();
				return ret;
			}
		}

		[GLib.Signal("run")]
		public event GLib.RunHandler Run {
			add {
				this.AddSignalHandler ("run", value, typeof (GLib.RunArgs));
			}
			remove {
				this.RemoveSignalHandler ("run", value);
			}
		}

		static RunNativeDelegate Run_cb_delegate;
		static RunNativeDelegate RunVMCallback {
			get {
				if (Run_cb_delegate == null)
					Run_cb_delegate = new RunNativeDelegate (Run_cb);
				return Run_cb_delegate;
			}
		}

		static void OverrideRun (GLib.GType gtype)
		{
			OverrideRun (gtype, RunVMCallback);
		}

		static void OverrideRun (GLib.GType gtype, RunNativeDelegate callback)
		{
			GThreadedSocketServiceClass class_iface = GetClassStruct (gtype, false);
			class_iface.Run = callback;
			OverrideClassStruct (gtype, class_iface);
		}

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate bool RunNativeDelegate (IntPtr inst, IntPtr connection, IntPtr source_object);

		static bool Run_cb (IntPtr inst, IntPtr connection, IntPtr source_object)
		{
			try {
				ThreadedSocketService __obj = GLib.Object.GetObject (inst, false) as ThreadedSocketService;
				bool __result;
				__result = __obj.OnRun (GLib.Object.GetObject(connection) as GLib.SocketConnection, GLib.Object.GetObject (source_object));
				return __result;
			} catch (Exception e) {
				GLib.ExceptionManager.RaiseUnhandledException (e, true);
				// NOTREACHED: above call does not return.
				throw e;
			}
		}

		[GLib.DefaultSignalHandler(Type=typeof(GLib.ThreadedSocketService), ConnectionMethod="OverrideRun")]
		protected virtual bool OnRun (GLib.SocketConnection connection, GLib.Object source_object)
		{
			return InternalRun (connection, source_object);
		}

		private bool InternalRun (GLib.SocketConnection connection, GLib.Object source_object)
		{
			RunNativeDelegate unmanaged = GetClassStruct (this.LookupGType ().GetThresholdType (), true).Run;
			if (unmanaged == null) return false;

			bool __result = unmanaged (this.Handle, connection == null ? IntPtr.Zero : connection.Handle, source_object == null ? IntPtr.Zero : source_object.Handle);
			return __result;
		}

		[StructLayout (LayoutKind.Sequential)]
		struct GThreadedSocketServiceClass {
			public RunNativeDelegate Run;
			IntPtr GReserved1;
			IntPtr GReserved2;
			IntPtr GReserved3;
			IntPtr GReserved4;
			IntPtr GReserved5;
		}

		static uint class_offset = ((GLib.GType) typeof (GLib.SocketService)).GetClassSize ();
		static Dictionary<GLib.GType, GThreadedSocketServiceClass> class_structs;

		static GThreadedSocketServiceClass GetClassStruct (GLib.GType gtype, bool use_cache)
		{
			if (class_structs == null)
				class_structs = new Dictionary<GLib.GType, GThreadedSocketServiceClass> ();

			if (use_cache && class_structs.ContainsKey (gtype))
				return class_structs [gtype];
			else {
				IntPtr class_ptr = new IntPtr (gtype.GetClassPtr ().ToInt64 () + class_offset);
				GThreadedSocketServiceClass class_struct = (GThreadedSocketServiceClass) Marshal.PtrToStructure (class_ptr, typeof (GThreadedSocketServiceClass));
				if (use_cache)
					class_structs.Add (gtype, class_struct);
				return class_struct;
			}
		}

		static void OverrideClassStruct (GLib.GType gtype, GThreadedSocketServiceClass class_struct)
		{
			IntPtr class_ptr = new IntPtr (gtype.GetClassPtr ().ToInt64 () + class_offset);
			Marshal.StructureToPtr (class_struct, class_ptr, false);
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr g_threaded_socket_service_get_type();

		public static new GLib.GType GType { 
			get {
				IntPtr raw_ret = g_threaded_socket_service_get_type();
				GLib.GType ret = new GLib.GType(raw_ret);
				return ret;
			}
		}

#endregion
	}
}
