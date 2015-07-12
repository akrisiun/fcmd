// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace Gtk {

	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Runtime.InteropServices;

#region Autogenerated code
	public partial class Timeline : GLib.Object {

		public Timeline (IntPtr raw) : base(raw) {}

		protected Timeline() : base(IntPtr.Zero)
		{
			CreateNativeObject (new string [0], new GLib.Value [0]);
		}

		[GLib.Property ("fps")]
		public uint Fps {
			get {
				GLib.Value val = GetProperty ("fps");
				uint ret = (uint) val;
				val.Dispose ();
				return ret;
			}
			set {
				GLib.Value val = new GLib.Value(value);
				SetProperty("fps", val);
				val.Dispose ();
			}
		}

		[GLib.Property ("duration")]
		public uint Duration {
			get {
				GLib.Value val = GetProperty ("duration");
				uint ret = (uint) val;
				val.Dispose ();
				return ret;
			}
			set {
				GLib.Value val = new GLib.Value(value);
				SetProperty("duration", val);
				val.Dispose ();
			}
		}

		[GLib.Property ("loop")]
		public bool Loop {
			get {
				GLib.Value val = GetProperty ("loop");
				bool ret = (bool) val;
				val.Dispose ();
				return ret;
			}
			set {
				GLib.Value val = new GLib.Value(value);
				SetProperty("loop", val);
				val.Dispose ();
			}
		}

		[GLib.Property ("screen")]
		public Gdk.Screen Screen {
			get {
				GLib.Value val = GetProperty ("screen");
				Gdk.Screen ret = (Gdk.Screen) val;
				val.Dispose ();
				return ret;
			}
			set {
				GLib.Value val = new GLib.Value(value);
				SetProperty("screen", val);
				val.Dispose ();
			}
		}

		[GLib.Signal("started")]
		public event System.EventHandler Started {
			add {
				this.AddSignalHandler ("started", value);
			}
			remove {
				this.RemoveSignalHandler ("started", value);
			}
		}

		[GLib.Signal("finished")]
		public event System.EventHandler Finished {
			add {
				this.AddSignalHandler ("finished", value);
			}
			remove {
				this.RemoveSignalHandler ("finished", value);
			}
		}

		[GLib.Signal("frame")]
		public event Gtk.FrameHandler Frame {
			add {
				this.AddSignalHandler ("frame", value, typeof (Gtk.FrameArgs));
			}
			remove {
				this.RemoveSignalHandler ("frame", value);
			}
		}

		[GLib.Signal("paused")]
		public event System.EventHandler Paused {
			add {
				this.AddSignalHandler ("paused", value);
			}
			remove {
				this.RemoveSignalHandler ("paused", value);
			}
		}

		static StartedNativeDelegate Started_cb_delegate;
		static StartedNativeDelegate StartedVMCallback {
			get {
				if (Started_cb_delegate == null)
					Started_cb_delegate = new StartedNativeDelegate (Started_cb);
				return Started_cb_delegate;
			}
		}

		static void OverrideStarted (GLib.GType gtype)
		{
			OverrideStarted (gtype, StartedVMCallback);
		}

		static void OverrideStarted (GLib.GType gtype, StartedNativeDelegate callback)
		{
			GtkTimelineClass class_iface = GetClassStruct (gtype, false);
			class_iface.Started = callback;
			OverrideClassStruct (gtype, class_iface);
		}

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate void StartedNativeDelegate (IntPtr inst);

		static void Started_cb (IntPtr inst)
		{
			try {
				Timeline __obj = GLib.Object.GetObject (inst, false) as Timeline;
				__obj.OnStarted ();
			} catch (Exception e) {
				GLib.ExceptionManager.RaiseUnhandledException (e, false);
			}
		}

		[GLib.DefaultSignalHandler(Type=typeof(Gtk.Timeline), ConnectionMethod="OverrideStarted")]
		protected virtual void OnStarted ()
		{
			InternalStarted ();
		}

		private void InternalStarted ()
		{
			StartedNativeDelegate unmanaged = GetClassStruct (this.LookupGType ().GetThresholdType (), true).Started;
			if (unmanaged == null) return;

			unmanaged (this.Handle);
		}

		static FinishedNativeDelegate Finished_cb_delegate;
		static FinishedNativeDelegate FinishedVMCallback {
			get {
				if (Finished_cb_delegate == null)
					Finished_cb_delegate = new FinishedNativeDelegate (Finished_cb);
				return Finished_cb_delegate;
			}
		}

		static void OverrideFinished (GLib.GType gtype)
		{
			OverrideFinished (gtype, FinishedVMCallback);
		}

		static void OverrideFinished (GLib.GType gtype, FinishedNativeDelegate callback)
		{
			GtkTimelineClass class_iface = GetClassStruct (gtype, false);
			class_iface.Finished = callback;
			OverrideClassStruct (gtype, class_iface);
		}

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate void FinishedNativeDelegate (IntPtr inst);

		static void Finished_cb (IntPtr inst)
		{
			try {
				Timeline __obj = GLib.Object.GetObject (inst, false) as Timeline;
				__obj.OnFinished ();
			} catch (Exception e) {
				GLib.ExceptionManager.RaiseUnhandledException (e, false);
			}
		}

		[GLib.DefaultSignalHandler(Type=typeof(Gtk.Timeline), ConnectionMethod="OverrideFinished")]
		protected virtual void OnFinished ()
		{
			InternalFinished ();
		}

		private void InternalFinished ()
		{
			FinishedNativeDelegate unmanaged = GetClassStruct (this.LookupGType ().GetThresholdType (), true).Finished;
			if (unmanaged == null) return;

			unmanaged (this.Handle);
		}

		static PausedNativeDelegate Paused_cb_delegate;
		static PausedNativeDelegate PausedVMCallback {
			get {
				if (Paused_cb_delegate == null)
					Paused_cb_delegate = new PausedNativeDelegate (Paused_cb);
				return Paused_cb_delegate;
			}
		}

		static void OverridePaused (GLib.GType gtype)
		{
			OverridePaused (gtype, PausedVMCallback);
		}

		static void OverridePaused (GLib.GType gtype, PausedNativeDelegate callback)
		{
			GtkTimelineClass class_iface = GetClassStruct (gtype, false);
			class_iface.Paused = callback;
			OverrideClassStruct (gtype, class_iface);
		}

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate void PausedNativeDelegate (IntPtr inst);

		static void Paused_cb (IntPtr inst)
		{
			try {
				Timeline __obj = GLib.Object.GetObject (inst, false) as Timeline;
				__obj.OnPaused ();
			} catch (Exception e) {
				GLib.ExceptionManager.RaiseUnhandledException (e, false);
			}
		}

		[GLib.DefaultSignalHandler(Type=typeof(Gtk.Timeline), ConnectionMethod="OverridePaused")]
		protected virtual void OnPaused ()
		{
			InternalPaused ();
		}

		private void InternalPaused ()
		{
			PausedNativeDelegate unmanaged = GetClassStruct (this.LookupGType ().GetThresholdType (), true).Paused;
			if (unmanaged == null) return;

			unmanaged (this.Handle);
		}

		static FrameNativeDelegate Frame_cb_delegate;
		static FrameNativeDelegate FrameVMCallback {
			get {
				if (Frame_cb_delegate == null)
					Frame_cb_delegate = new FrameNativeDelegate (Frame_cb);
				return Frame_cb_delegate;
			}
		}

		static void OverrideFrame (GLib.GType gtype)
		{
			OverrideFrame (gtype, FrameVMCallback);
		}

		static void OverrideFrame (GLib.GType gtype, FrameNativeDelegate callback)
		{
			GtkTimelineClass class_iface = GetClassStruct (gtype, false);
			class_iface.Frame = callback;
			OverrideClassStruct (gtype, class_iface);
		}

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate void FrameNativeDelegate (IntPtr inst, double progress);

		static void Frame_cb (IntPtr inst, double progress)
		{
			try {
				Timeline __obj = GLib.Object.GetObject (inst, false) as Timeline;
				__obj.OnFrame (progress);
			} catch (Exception e) {
				GLib.ExceptionManager.RaiseUnhandledException (e, false);
			}
		}

		[GLib.DefaultSignalHandler(Type=typeof(Gtk.Timeline), ConnectionMethod="OverrideFrame")]
		protected virtual void OnFrame (double progress)
		{
			InternalFrame (progress);
		}

		private void InternalFrame (double progress)
		{
			FrameNativeDelegate unmanaged = GetClassStruct (this.LookupGType ().GetThresholdType (), true).Frame;
			if (unmanaged == null) return;

			unmanaged (this.Handle, progress);
		}

		[StructLayout (LayoutKind.Sequential)]
		struct GtkTimelineClass {
			public StartedNativeDelegate Started;
			public FinishedNativeDelegate Finished;
			public PausedNativeDelegate Paused;
			public FrameNativeDelegate Frame;
			IntPtr _GtkReserved1;
			IntPtr _GtkReserved2;
			IntPtr _GtkReserved3;
			IntPtr _GtkReserved4;
		}

		static uint class_offset = ((GLib.GType) typeof (GLib.Object)).GetClassSize ();
		static Dictionary<GLib.GType, GtkTimelineClass> class_structs;

		static GtkTimelineClass GetClassStruct (GLib.GType gtype, bool use_cache)
		{
			if (class_structs == null)
				class_structs = new Dictionary<GLib.GType, GtkTimelineClass> ();

			if (use_cache && class_structs.ContainsKey (gtype))
				return class_structs [gtype];
			else {
				IntPtr class_ptr = new IntPtr (gtype.GetClassPtr ().ToInt64 () + class_offset);
				GtkTimelineClass class_struct = (GtkTimelineClass) Marshal.PtrToStructure (class_ptr, typeof (GtkTimelineClass));
				if (use_cache)
					class_structs.Add (gtype, class_struct);
				return class_struct;
			}
		}

		static void OverrideClassStruct (GLib.GType gtype, GtkTimelineClass class_struct)
		{
			IntPtr class_ptr = new IntPtr (gtype.GetClassPtr ().ToInt64 () + class_offset);
			Marshal.StructureToPtr (class_struct, class_ptr, false);
		}

#endregion
	}
}