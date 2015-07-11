// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace Gdk {

	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Runtime.InteropServices;

#region Autogenerated code
	public partial class DragContext : GLib.Object {

		public DragContext (IntPtr raw) : base(raw) {}

		protected DragContext() : base(IntPtr.Zero)
		{
			CreateNativeObject (new string [0], new GLib.Value [0]);
		}

		static FindWindowNativeDelegate FindWindow_cb_delegate;
		static FindWindowNativeDelegate FindWindowVMCallback {
			get {
				if (FindWindow_cb_delegate == null)
					FindWindow_cb_delegate = new FindWindowNativeDelegate (FindWindow_cb);
				return FindWindow_cb_delegate;
			}
		}

		static void OverrideFindWindow (GLib.GType gtype)
		{
			OverrideFindWindow (gtype, FindWindowVMCallback);
		}

		static void OverrideFindWindow (GLib.GType gtype, FindWindowNativeDelegate callback)
		{
			GdkDragContextClass class_iface = GetClassStruct (gtype, false);
			class_iface.FindWindow = callback;
			OverrideClassStruct (gtype, class_iface);
		}

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate IntPtr FindWindowNativeDelegate (IntPtr inst, IntPtr drag_window, IntPtr screen, int x_root, int y_root, out int protocol);

		static IntPtr FindWindow_cb (IntPtr inst, IntPtr drag_window, IntPtr screen, int x_root, int y_root, out int protocol)
		{
			try {
				DragContext __obj = GLib.Object.GetObject (inst, false) as DragContext;
				Gdk.Window __result;
				Gdk.DragProtocol myprotocol;
				__result = __obj.OnFindWindow (GLib.Object.GetObject(drag_window) as Gdk.Window, GLib.Object.GetObject(screen) as Gdk.Screen, x_root, y_root, out myprotocol);
				protocol = (int) myprotocol;
				return __result == null ? IntPtr.Zero : __result.Handle;
			} catch (Exception e) {
				GLib.ExceptionManager.RaiseUnhandledException (e, true);
				// NOTREACHED: above call does not return.
				throw e;
			}
		}

		[GLib.DefaultSignalHandler(Type=typeof(Gdk.DragContext), ConnectionMethod="OverrideFindWindow")]
		protected virtual Gdk.Window OnFindWindow (Gdk.Window drag_window, Gdk.Screen screen, int x_root, int y_root, out Gdk.DragProtocol protocol)
		{
			return InternalFindWindow (drag_window, screen, x_root, y_root, out protocol);
		}

		private Gdk.Window InternalFindWindow (Gdk.Window drag_window, Gdk.Screen screen, int x_root, int y_root, out Gdk.DragProtocol protocol)
		{
			FindWindowNativeDelegate unmanaged = GetClassStruct (this.LookupGType ().GetThresholdType (), true).FindWindow;
			if (unmanaged == null) throw new InvalidOperationException ("No base method to invoke");

			int native_protocol;
			IntPtr __result = unmanaged (this.Handle, drag_window == null ? IntPtr.Zero : drag_window.Handle, screen == null ? IntPtr.Zero : screen.Handle, x_root, y_root, out native_protocol);
			protocol = (Gdk.DragProtocol) native_protocol;
			return GLib.Object.GetObject(__result) as Gdk.Window;
		}

		static GetSelectionNativeDelegate GetSelection_cb_delegate;
		static GetSelectionNativeDelegate GetSelectionVMCallback {
			get {
				if (GetSelection_cb_delegate == null)
					GetSelection_cb_delegate = new GetSelectionNativeDelegate (GetSelection_cb);
				return GetSelection_cb_delegate;
			}
		}

		static void OverrideGetSelection (GLib.GType gtype)
		{
			OverrideGetSelection (gtype, GetSelectionVMCallback);
		}

		static void OverrideGetSelection (GLib.GType gtype, GetSelectionNativeDelegate callback)
		{
			GdkDragContextClass class_iface = GetClassStruct (gtype, false);
			class_iface.GetSelection = callback;
			OverrideClassStruct (gtype, class_iface);
		}

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate IntPtr GetSelectionNativeDelegate (IntPtr inst);

		static IntPtr GetSelection_cb (IntPtr inst)
		{
			try {
				DragContext __obj = GLib.Object.GetObject (inst, false) as DragContext;
				Gdk.Atom __result;
				__result = __obj.OnGetSelection ();
				return __result == null ? IntPtr.Zero : __result.Handle;
			} catch (Exception e) {
				GLib.ExceptionManager.RaiseUnhandledException (e, true);
				// NOTREACHED: above call does not return.
				throw e;
			}
		}

		[GLib.DefaultSignalHandler(Type=typeof(Gdk.DragContext), ConnectionMethod="OverrideGetSelection")]
		protected virtual Gdk.Atom OnGetSelection ()
		{
			return InternalGetSelection ();
		}

		private Gdk.Atom InternalGetSelection ()
		{
			GetSelectionNativeDelegate unmanaged = GetClassStruct (this.LookupGType ().GetThresholdType (), true).GetSelection;
			if (unmanaged == null) return null;

			IntPtr __result = unmanaged (this.Handle);
			return __result == IntPtr.Zero ? null : (Gdk.Atom) GLib.Opaque.GetOpaque (__result, typeof (Gdk.Atom), false);
		}

		static DragMotionNativeDelegate DragMotion_cb_delegate;
		static DragMotionNativeDelegate DragMotionVMCallback {
			get {
				if (DragMotion_cb_delegate == null)
					DragMotion_cb_delegate = new DragMotionNativeDelegate (DragMotion_cb);
				return DragMotion_cb_delegate;
			}
		}

		static void OverrideDragMotion (GLib.GType gtype)
		{
			OverrideDragMotion (gtype, DragMotionVMCallback);
		}

		static void OverrideDragMotion (GLib.GType gtype, DragMotionNativeDelegate callback)
		{
			GdkDragContextClass class_iface = GetClassStruct (gtype, false);
			class_iface.DragMotion = callback;
			OverrideClassStruct (gtype, class_iface);
		}

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate bool DragMotionNativeDelegate (IntPtr inst, IntPtr dest_window, int protocol, int root_x, int root_y, int suggested_action, int possible_actions, uint time_);

		static bool DragMotion_cb (IntPtr inst, IntPtr dest_window, int protocol, int root_x, int root_y, int suggested_action, int possible_actions, uint time_)
		{
			try {
				DragContext __obj = GLib.Object.GetObject (inst, false) as DragContext;
				bool __result;
				__result = __obj.OnDragMotion (GLib.Object.GetObject(dest_window) as Gdk.Window, (Gdk.DragProtocol) protocol, root_x, root_y, (Gdk.DragAction) suggested_action, (Gdk.DragAction) possible_actions, time_);
				return __result;
			} catch (Exception e) {
				GLib.ExceptionManager.RaiseUnhandledException (e, true);
				// NOTREACHED: above call does not return.
				throw e;
			}
		}

		[GLib.DefaultSignalHandler(Type=typeof(Gdk.DragContext), ConnectionMethod="OverrideDragMotion")]
		protected virtual bool OnDragMotion (Gdk.Window dest_window, Gdk.DragProtocol protocol, int root_x, int root_y, Gdk.DragAction suggested_action, Gdk.DragAction possible_actions, uint time_)
		{
			return InternalDragMotion (dest_window, protocol, root_x, root_y, suggested_action, possible_actions, time_);
		}

		private bool InternalDragMotion (Gdk.Window dest_window, Gdk.DragProtocol protocol, int root_x, int root_y, Gdk.DragAction suggested_action, Gdk.DragAction possible_actions, uint time_)
		{
			DragMotionNativeDelegate unmanaged = GetClassStruct (this.LookupGType ().GetThresholdType (), true).DragMotion;
			if (unmanaged == null) return false;

			bool __result = unmanaged (this.Handle, dest_window == null ? IntPtr.Zero : dest_window.Handle, (int) protocol, root_x, root_y, (int) suggested_action, (int) possible_actions, time_);
			return __result;
		}

		static DragStatusNativeDelegate DragStatus_cb_delegate;
		static DragStatusNativeDelegate DragStatusVMCallback {
			get {
				if (DragStatus_cb_delegate == null)
					DragStatus_cb_delegate = new DragStatusNativeDelegate (DragStatus_cb);
				return DragStatus_cb_delegate;
			}
		}

		static void OverrideDragStatus (GLib.GType gtype)
		{
			OverrideDragStatus (gtype, DragStatusVMCallback);
		}

		static void OverrideDragStatus (GLib.GType gtype, DragStatusNativeDelegate callback)
		{
			GdkDragContextClass class_iface = GetClassStruct (gtype, false);
			class_iface.DragStatus = callback;
			OverrideClassStruct (gtype, class_iface);
		}

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate void DragStatusNativeDelegate (IntPtr inst, int action, uint time_);

		static void DragStatus_cb (IntPtr inst, int action, uint time_)
		{
			try {
				DragContext __obj = GLib.Object.GetObject (inst, false) as DragContext;
				__obj.OnDragStatus ((Gdk.DragAction) action, time_);
			} catch (Exception e) {
				GLib.ExceptionManager.RaiseUnhandledException (e, false);
			}
		}

		[GLib.DefaultSignalHandler(Type=typeof(Gdk.DragContext), ConnectionMethod="OverrideDragStatus")]
		protected virtual void OnDragStatus (Gdk.DragAction action, uint time_)
		{
			InternalDragStatus (action, time_);
		}

		private void InternalDragStatus (Gdk.DragAction action, uint time_)
		{
			DragStatusNativeDelegate unmanaged = GetClassStruct (this.LookupGType ().GetThresholdType (), true).DragStatus;
			if (unmanaged == null) return;

			unmanaged (this.Handle, (int) action, time_);
		}

		static DragAbortNativeDelegate DragAbort_cb_delegate;
		static DragAbortNativeDelegate DragAbortVMCallback {
			get {
				if (DragAbort_cb_delegate == null)
					DragAbort_cb_delegate = new DragAbortNativeDelegate (DragAbort_cb);
				return DragAbort_cb_delegate;
			}
		}

		static void OverrideDragAbort (GLib.GType gtype)
		{
			OverrideDragAbort (gtype, DragAbortVMCallback);
		}

		static void OverrideDragAbort (GLib.GType gtype, DragAbortNativeDelegate callback)
		{
			GdkDragContextClass class_iface = GetClassStruct (gtype, false);
			class_iface.DragAbort = callback;
			OverrideClassStruct (gtype, class_iface);
		}

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate void DragAbortNativeDelegate (IntPtr inst, uint time_);

		static void DragAbort_cb (IntPtr inst, uint time_)
		{
			try {
				DragContext __obj = GLib.Object.GetObject (inst, false) as DragContext;
				__obj.OnDragAbort (time_);
			} catch (Exception e) {
				GLib.ExceptionManager.RaiseUnhandledException (e, false);
			}
		}

		[GLib.DefaultSignalHandler(Type=typeof(Gdk.DragContext), ConnectionMethod="OverrideDragAbort")]
		protected virtual void OnDragAbort (uint time_)
		{
			InternalDragAbort (time_);
		}

		private void InternalDragAbort (uint time_)
		{
			DragAbortNativeDelegate unmanaged = GetClassStruct (this.LookupGType ().GetThresholdType (), true).DragAbort;
			if (unmanaged == null) return;

			unmanaged (this.Handle, time_);
		}

		static DragDropNativeDelegate DragDrop_cb_delegate;
		static DragDropNativeDelegate DragDropVMCallback {
			get {
				if (DragDrop_cb_delegate == null)
					DragDrop_cb_delegate = new DragDropNativeDelegate (DragDrop_cb);
				return DragDrop_cb_delegate;
			}
		}

		static void OverrideDragDrop (GLib.GType gtype)
		{
			OverrideDragDrop (gtype, DragDropVMCallback);
		}

		static void OverrideDragDrop (GLib.GType gtype, DragDropNativeDelegate callback)
		{
			GdkDragContextClass class_iface = GetClassStruct (gtype, false);
			class_iface.DragDrop = callback;
			OverrideClassStruct (gtype, class_iface);
		}

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate void DragDropNativeDelegate (IntPtr inst, uint time_);

		static void DragDrop_cb (IntPtr inst, uint time_)
		{
			try {
				DragContext __obj = GLib.Object.GetObject (inst, false) as DragContext;
				__obj.OnDragDrop (time_);
			} catch (Exception e) {
				GLib.ExceptionManager.RaiseUnhandledException (e, false);
			}
		}

		[GLib.DefaultSignalHandler(Type=typeof(Gdk.DragContext), ConnectionMethod="OverrideDragDrop")]
		protected virtual void OnDragDrop (uint time_)
		{
			InternalDragDrop (time_);
		}

		private void InternalDragDrop (uint time_)
		{
			DragDropNativeDelegate unmanaged = GetClassStruct (this.LookupGType ().GetThresholdType (), true).DragDrop;
			if (unmanaged == null) return;

			unmanaged (this.Handle, time_);
		}

		static DropReplyNativeDelegate DropReply_cb_delegate;
		static DropReplyNativeDelegate DropReplyVMCallback {
			get {
				if (DropReply_cb_delegate == null)
					DropReply_cb_delegate = new DropReplyNativeDelegate (DropReply_cb);
				return DropReply_cb_delegate;
			}
		}

		static void OverrideDropReply (GLib.GType gtype)
		{
			OverrideDropReply (gtype, DropReplyVMCallback);
		}

		static void OverrideDropReply (GLib.GType gtype, DropReplyNativeDelegate callback)
		{
			GdkDragContextClass class_iface = GetClassStruct (gtype, false);
			class_iface.DropReply = callback;
			OverrideClassStruct (gtype, class_iface);
		}

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate void DropReplyNativeDelegate (IntPtr inst, bool accept, uint time_);

		static void DropReply_cb (IntPtr inst, bool accept, uint time_)
		{
			try {
				DragContext __obj = GLib.Object.GetObject (inst, false) as DragContext;
				__obj.OnDropReply (accept, time_);
			} catch (Exception e) {
				GLib.ExceptionManager.RaiseUnhandledException (e, false);
			}
		}

		[GLib.DefaultSignalHandler(Type=typeof(Gdk.DragContext), ConnectionMethod="OverrideDropReply")]
		protected virtual void OnDropReply (bool accept, uint time_)
		{
			InternalDropReply (accept, time_);
		}

		private void InternalDropReply (bool accept, uint time_)
		{
			DropReplyNativeDelegate unmanaged = GetClassStruct (this.LookupGType ().GetThresholdType (), true).DropReply;
			if (unmanaged == null) return;

			unmanaged (this.Handle, accept, time_);
		}

		static DropFinishNativeDelegate DropFinish_cb_delegate;
		static DropFinishNativeDelegate DropFinishVMCallback {
			get {
				if (DropFinish_cb_delegate == null)
					DropFinish_cb_delegate = new DropFinishNativeDelegate (DropFinish_cb);
				return DropFinish_cb_delegate;
			}
		}

		static void OverrideDropFinish (GLib.GType gtype)
		{
			OverrideDropFinish (gtype, DropFinishVMCallback);
		}

		static void OverrideDropFinish (GLib.GType gtype, DropFinishNativeDelegate callback)
		{
			GdkDragContextClass class_iface = GetClassStruct (gtype, false);
			class_iface.DropFinish = callback;
			OverrideClassStruct (gtype, class_iface);
		}

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate void DropFinishNativeDelegate (IntPtr inst, bool success, uint time_);

		static void DropFinish_cb (IntPtr inst, bool success, uint time_)
		{
			try {
				DragContext __obj = GLib.Object.GetObject (inst, false) as DragContext;
				__obj.OnDropFinish (success, time_);
			} catch (Exception e) {
				GLib.ExceptionManager.RaiseUnhandledException (e, false);
			}
		}

		[GLib.DefaultSignalHandler(Type=typeof(Gdk.DragContext), ConnectionMethod="OverrideDropFinish")]
		protected virtual void OnDropFinish (bool success, uint time_)
		{
			InternalDropFinish (success, time_);
		}

		private void InternalDropFinish (bool success, uint time_)
		{
			DropFinishNativeDelegate unmanaged = GetClassStruct (this.LookupGType ().GetThresholdType (), true).DropFinish;
			if (unmanaged == null) return;

			unmanaged (this.Handle, success, time_);
		}

		static DropStatusNativeDelegate DropStatus_cb_delegate;
		static DropStatusNativeDelegate DropStatusVMCallback {
			get {
				if (DropStatus_cb_delegate == null)
					DropStatus_cb_delegate = new DropStatusNativeDelegate (DropStatus_cb);
				return DropStatus_cb_delegate;
			}
		}

		static void OverrideDropStatus (GLib.GType gtype)
		{
			OverrideDropStatus (gtype, DropStatusVMCallback);
		}

		static void OverrideDropStatus (GLib.GType gtype, DropStatusNativeDelegate callback)
		{
			GdkDragContextClass class_iface = GetClassStruct (gtype, false);
			class_iface.DropStatus = callback;
			OverrideClassStruct (gtype, class_iface);
		}

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate bool DropStatusNativeDelegate (IntPtr inst);

		static bool DropStatus_cb (IntPtr inst)
		{
			try {
				DragContext __obj = GLib.Object.GetObject (inst, false) as DragContext;
				bool __result;
				__result = __obj.OnDropStatus ();
				return __result;
			} catch (Exception e) {
				GLib.ExceptionManager.RaiseUnhandledException (e, true);
				// NOTREACHED: above call does not return.
				throw e;
			}
		}

		[GLib.DefaultSignalHandler(Type=typeof(Gdk.DragContext), ConnectionMethod="OverrideDropStatus")]
		protected virtual bool OnDropStatus ()
		{
			return InternalDropStatus ();
		}

		private bool InternalDropStatus ()
		{
			DropStatusNativeDelegate unmanaged = GetClassStruct (this.LookupGType ().GetThresholdType (), true).DropStatus;
			if (unmanaged == null) return false;

			bool __result = unmanaged (this.Handle);
			return __result;
		}

		[StructLayout (LayoutKind.Sequential)]
		struct GdkDragContextClass {
			public FindWindowNativeDelegate FindWindow;
			public GetSelectionNativeDelegate GetSelection;
			public DragMotionNativeDelegate DragMotion;
			public DragStatusNativeDelegate DragStatus;
			public DragAbortNativeDelegate DragAbort;
			public DragDropNativeDelegate DragDrop;
			public DropReplyNativeDelegate DropReply;
			public DropFinishNativeDelegate DropFinish;
			public DropStatusNativeDelegate DropStatus;
		}

		static uint class_offset = ((GLib.GType) typeof (GLib.Object)).GetClassSize ();
		static Dictionary<GLib.GType, GdkDragContextClass> class_structs;

		static GdkDragContextClass GetClassStruct (GLib.GType gtype, bool use_cache)
		{
			if (class_structs == null)
				class_structs = new Dictionary<GLib.GType, GdkDragContextClass> ();

			if (use_cache && class_structs.ContainsKey (gtype))
				return class_structs [gtype];
			else {
				IntPtr class_ptr = new IntPtr (gtype.GetClassPtr ().ToInt64 () + class_offset);
				GdkDragContextClass class_struct = (GdkDragContextClass) Marshal.PtrToStructure (class_ptr, typeof (GdkDragContextClass));
				if (use_cache)
					class_structs.Add (gtype, class_struct);
				return class_struct;
			}
		}

		static void OverrideClassStruct (GLib.GType gtype, GdkDragContextClass class_struct)
		{
			IntPtr class_ptr = new IntPtr (gtype.GetClassPtr ().ToInt64 () + class_offset);
			Marshal.StructureToPtr (class_struct, class_ptr, false);
		}

		[DllImport("libgdk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern int gdk_drag_context_get_actions(IntPtr raw);

		public Gdk.DragAction Actions { 
			get {
				int raw_ret = gdk_drag_context_get_actions(Handle);
				Gdk.DragAction ret = (Gdk.DragAction) raw_ret;
				return ret;
			}
		}

		[DllImport("libgdk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gdk_drag_context_get_dest_window(IntPtr raw);

		public Gdk.Window DestWindow { 
			get {
				IntPtr raw_ret = gdk_drag_context_get_dest_window(Handle);
				Gdk.Window ret = GLib.Object.GetObject(raw_ret) as Gdk.Window;
				return ret;
			}
		}

		[DllImport("libgdk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gdk_drag_context_get_device(IntPtr raw);

		[DllImport("libgdk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gdk_drag_context_set_device(IntPtr raw, IntPtr device);

		public Gdk.Device Device { 
			get {
				IntPtr raw_ret = gdk_drag_context_get_device(Handle);
				Gdk.Device ret = GLib.Object.GetObject(raw_ret) as Gdk.Device;
				return ret;
			}
			set {
				gdk_drag_context_set_device(Handle, value == null ? IntPtr.Zero : value.Handle);
			}
		}

		[DllImport("libgdk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern int gdk_drag_context_get_protocol(IntPtr raw);

		public Gdk.DragProtocol Protocol { 
			get {
				int raw_ret = gdk_drag_context_get_protocol(Handle);
				Gdk.DragProtocol ret = (Gdk.DragProtocol) raw_ret;
				return ret;
			}
		}

		[DllImport("libgdk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern int gdk_drag_context_get_selected_action(IntPtr raw);

		public Gdk.DragAction SelectedAction { 
			get {
				int raw_ret = gdk_drag_context_get_selected_action(Handle);
				Gdk.DragAction ret = (Gdk.DragAction) raw_ret;
				return ret;
			}
		}

		[DllImport("libgdk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gdk_drag_context_get_source_window(IntPtr raw);

		public Gdk.Window SourceWindow { 
			get {
				IntPtr raw_ret = gdk_drag_context_get_source_window(Handle);
				Gdk.Window ret = GLib.Object.GetObject(raw_ret) as Gdk.Window;
				return ret;
			}
		}

		[DllImport("libgdk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern int gdk_drag_context_get_suggested_action(IntPtr raw);

		public Gdk.DragAction SuggestedAction { 
			get {
				int raw_ret = gdk_drag_context_get_suggested_action(Handle);
				Gdk.DragAction ret = (Gdk.DragAction) raw_ret;
				return ret;
			}
		}

		[DllImport("libgdk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gdk_drag_context_get_type();

		public static new GLib.GType GType { 
			get {
				IntPtr raw_ret = gdk_drag_context_get_type();
				GLib.GType ret = new GLib.GType(raw_ret);
				return ret;
			}
		}

		[DllImport("libgdk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gdk_drag_context_list_targets(IntPtr raw);

		public Gdk.Atom[] ListTargets() {
			IntPtr raw_ret = gdk_drag_context_list_targets(Handle);
			Gdk.Atom[] ret = (Gdk.Atom[]) GLib.Marshaller.ListPtrToArray (raw_ret, typeof(GLib.List), false, false, typeof(Gdk.Atom));
			return ret;
		}

#endregion
	}
}
