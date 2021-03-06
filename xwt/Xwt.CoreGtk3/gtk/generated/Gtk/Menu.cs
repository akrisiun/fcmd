// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace Gtk {

	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Runtime.InteropServices;

#region Autogenerated code
	public partial class Menu : Gtk.MenuShell {

		public Menu (IntPtr raw) : base(raw) {}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gtk_menu_new();

		public Menu () : base (IntPtr.Zero)
		{
			if (GetType () != typeof (Menu)) {
				CreateNativeObject (new string [0], new GLib.Value[0]);
				return;
			}
			Raw = gtk_menu_new();
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gtk_menu_get_active(IntPtr raw);

		[GLib.Property ("active")]
		public Gtk.Widget Active {
			get  {
				IntPtr raw_ret = gtk_menu_get_active(Handle);
				Gtk.Widget ret = GLib.Object.GetObject(raw_ret) as Gtk.Widget;
				return ret;
			}
			set {
				GLib.Value val = new GLib.Value(value);
				SetProperty("active", val);
				val.Dispose ();
			}
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gtk_menu_get_accel_group(IntPtr raw);

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gtk_menu_set_accel_group(IntPtr raw, IntPtr accel_group);

		[GLib.Property ("accel-group")]
		public Gtk.AccelGroup AccelGroup {
			get  {
				IntPtr raw_ret = gtk_menu_get_accel_group(Handle);
				Gtk.AccelGroup ret = GLib.Object.GetObject(raw_ret) as Gtk.AccelGroup;
				return ret;
			}
			set  {
				gtk_menu_set_accel_group(Handle, value == null ? IntPtr.Zero : value.Handle);
			}
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gtk_menu_get_accel_path(IntPtr raw);

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gtk_menu_set_accel_path(IntPtr raw, IntPtr accel_path);

		[GLib.Property ("accel-path")]
		public string AccelPath {
			get  {
				IntPtr raw_ret = gtk_menu_get_accel_path(Handle);
				string ret = GLib.Marshaller.Utf8PtrToString (raw_ret);
				return ret;
			}
			set  {
				IntPtr native_value = GLib.Marshaller.StringToPtrGStrdup (value);
				gtk_menu_set_accel_path(Handle, native_value);
				GLib.Marshaller.Free (native_value);
			}
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gtk_menu_get_attach_widget(IntPtr raw);

		[GLib.Property ("attach-widget")]
		public Gtk.Widget AttachWidget {
			get  {
				IntPtr raw_ret = gtk_menu_get_attach_widget(Handle);
				Gtk.Widget ret = GLib.Object.GetObject(raw_ret) as Gtk.Widget;
				return ret;
			}
			set {
				GLib.Value val = new GLib.Value(value);
				SetProperty("attach-widget", val);
				val.Dispose ();
			}
		}

		[GLib.Property ("tearoff-title")]
		public string TearoffTitle {
			get {
				GLib.Value val = GetProperty ("tearoff-title");
				string ret = (string) val;
				val.Dispose ();
				return ret;
			}
			set {
				GLib.Value val = new GLib.Value(value);
				SetProperty("tearoff-title", val);
				val.Dispose ();
			}
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool gtk_menu_get_tearoff_state(IntPtr raw);

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gtk_menu_set_tearoff_state(IntPtr raw, bool torn_off);

		[GLib.Property ("tearoff-state")]
		public bool TearoffState {
			get  {
				bool raw_ret = gtk_menu_get_tearoff_state(Handle);
				bool ret = raw_ret;
				return ret;
			}
			set  {
				gtk_menu_set_tearoff_state(Handle, value);
			}
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern int gtk_menu_get_monitor(IntPtr raw);

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gtk_menu_set_monitor(IntPtr raw, int monitor_num);

		[GLib.Property ("monitor")]
		public int Monitor {
			get  {
				int raw_ret = gtk_menu_get_monitor(Handle);
				int ret = raw_ret;
				return ret;
			}
			set  {
				gtk_menu_set_monitor(Handle, value);
			}
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool gtk_menu_get_reserve_toggle_size(IntPtr raw);

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gtk_menu_set_reserve_toggle_size(IntPtr raw, bool reserve_toggle_size);

		[GLib.Property ("reserve-toggle-size")]
		public bool ReserveToggleSize {
			get  {
				bool raw_ret = gtk_menu_get_reserve_toggle_size(Handle);
				bool ret = raw_ret;
				return ret;
			}
			set  {
				gtk_menu_set_reserve_toggle_size(Handle, value);
			}
		}

		public class MenuChild : Gtk.Container.ContainerChild {
			protected internal MenuChild (Gtk.Container parent, Gtk.Widget child) : base (parent, child) {}

			[Gtk.ChildProperty ("left-attach")]
			public int LeftAttach {
				get {
					GLib.Value val = parent.ChildGetProperty (child, "left-attach");
					int ret = (int) val;
					val.Dispose ();
					return ret;
				}
				set {
					GLib.Value val = new GLib.Value(value);
					parent.ChildSetProperty(child, "left-attach", val);
					val.Dispose ();
				}
			}

			[Gtk.ChildProperty ("right-attach")]
			public int RightAttach {
				get {
					GLib.Value val = parent.ChildGetProperty (child, "right-attach");
					int ret = (int) val;
					val.Dispose ();
					return ret;
				}
				set {
					GLib.Value val = new GLib.Value(value);
					parent.ChildSetProperty(child, "right-attach", val);
					val.Dispose ();
				}
			}

			[Gtk.ChildProperty ("top-attach")]
			public int TopAttach {
				get {
					GLib.Value val = parent.ChildGetProperty (child, "top-attach");
					int ret = (int) val;
					val.Dispose ();
					return ret;
				}
				set {
					GLib.Value val = new GLib.Value(value);
					parent.ChildSetProperty(child, "top-attach", val);
					val.Dispose ();
				}
			}

			[Gtk.ChildProperty ("bottom-attach")]
			public int BottomAttach {
				get {
					GLib.Value val = parent.ChildGetProperty (child, "bottom-attach");
					int ret = (int) val;
					val.Dispose ();
					return ret;
				}
				set {
					GLib.Value val = new GLib.Value(value);
					parent.ChildSetProperty(child, "bottom-attach", val);
					val.Dispose ();
				}
			}

		}

		public override Gtk.Container.ContainerChild this [Gtk.Widget child] {
			get {
				return new MenuChild (this, child);
			}
		}

		[GLib.Signal("move-scroll")]
		public event Gtk.MoveScrollHandler MoveScroll {
			add {
				this.AddSignalHandler ("move-scroll", value, typeof (Gtk.MoveScrollArgs));
			}
			remove {
				this.RemoveSignalHandler ("move-scroll", value);
			}
		}

		static MoveScrollNativeDelegate MoveScroll_cb_delegate;
		static MoveScrollNativeDelegate MoveScrollVMCallback {
			get {
				if (MoveScroll_cb_delegate == null)
					MoveScroll_cb_delegate = new MoveScrollNativeDelegate (MoveScroll_cb);
				return MoveScroll_cb_delegate;
			}
		}

		static void OverrideMoveScroll (GLib.GType gtype)
		{
			OverrideMoveScroll (gtype, MoveScrollVMCallback);
		}

		static void OverrideMoveScroll (GLib.GType gtype, MoveScrollNativeDelegate callback)
		{
			OverrideVirtualMethod (gtype, "move-scroll", callback);
		}
		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate void MoveScrollNativeDelegate (IntPtr inst, int p0);

		static void MoveScroll_cb (IntPtr inst, int p0)
		{
			try {
				Menu __obj = GLib.Object.GetObject (inst, false) as Menu;
				__obj.OnMoveScroll ((Gtk.ScrollType) p0);
			} catch (Exception e) {
				GLib.ExceptionManager.RaiseUnhandledException (e, false);
			}
		}

		[GLib.DefaultSignalHandler(Type=typeof(Gtk.Menu), ConnectionMethod="OverrideMoveScroll")]
		protected virtual void OnMoveScroll (Gtk.ScrollType p0)
		{
			InternalMoveScroll (p0);
		}

		private void InternalMoveScroll (Gtk.ScrollType p0)
		{
			GLib.Value ret = GLib.Value.Empty;
			GLib.ValueArray inst_and_params = new GLib.ValueArray (2);
			GLib.Value[] vals = new GLib.Value [2];
			vals [0] = new GLib.Value (this);
			inst_and_params.Append (vals [0]);
			vals [1] = new GLib.Value (p0);
			inst_and_params.Append (vals [1]);
			g_signal_chain_from_overridden (inst_and_params.ArrayPtr, ref ret);
			foreach (GLib.Value v in vals)
				v.Dispose ();
		}

		[StructLayout (LayoutKind.Sequential)]
		struct GtkMenuClass {
			IntPtr GtkReserved1;
			IntPtr GtkReserved2;
			IntPtr GtkReserved3;
			IntPtr GtkReserved4;
		}

		static uint class_offset = ((GLib.GType) typeof (Gtk.MenuShell)).GetClassSize ();
		static Dictionary<GLib.GType, GtkMenuClass> class_structs;

		static GtkMenuClass GetClassStruct (GLib.GType gtype, bool use_cache)
		{
			if (class_structs == null)
				class_structs = new Dictionary<GLib.GType, GtkMenuClass> ();

			if (use_cache && class_structs.ContainsKey (gtype))
				return class_structs [gtype];
			else {
				IntPtr class_ptr = new IntPtr (gtype.GetClassPtr ().ToInt64 () + class_offset);
				GtkMenuClass class_struct = (GtkMenuClass) Marshal.PtrToStructure (class_ptr, typeof (GtkMenuClass));
				if (use_cache)
					class_structs.Add (gtype, class_struct);
				return class_struct;
			}
		}

		static void OverrideClassStruct (GLib.GType gtype, GtkMenuClass class_struct)
		{
			IntPtr class_ptr = new IntPtr (gtype.GetClassPtr ().ToInt64 () + class_offset);
			Marshal.StructureToPtr (class_struct, class_ptr, false);
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gtk_menu_attach(IntPtr raw, IntPtr child, uint left_attach, uint right_attach, uint top_attach, uint bottom_attach);

		public void Attach(Gtk.Widget child, uint left_attach, uint right_attach, uint top_attach, uint bottom_attach) {
			gtk_menu_attach(Handle, child == null ? IntPtr.Zero : child.Handle, left_attach, right_attach, top_attach, bottom_attach);
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gtk_menu_attach_to_widget(IntPtr raw, IntPtr attach_widget, GtkSharp.MenuDetachFuncNative detacher);

		public void AttachToWidget(Gtk.Widget attach_widget, Gtk.MenuDetachFunc detacher) {
			GtkSharp.MenuDetachFuncWrapper detacher_wrapper = new GtkSharp.MenuDetachFuncWrapper (detacher);
			detacher_wrapper.PersistUntilCalled ();
			gtk_menu_attach_to_widget(Handle, attach_widget == null ? IntPtr.Zero : attach_widget.Handle, detacher_wrapper.NativeDelegate);
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gtk_menu_detach(IntPtr raw);

		public void Detach() {
			gtk_menu_detach(Handle);
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gtk_menu_get_for_attach_widget(IntPtr widget);

		public static Gtk.Widget[] GetForAttachWidget(Gtk.Widget widget) {
			IntPtr raw_ret = gtk_menu_get_for_attach_widget(widget == null ? IntPtr.Zero : widget.Handle);
			Gtk.Widget[] ret = (Gtk.Widget[]) GLib.Marshaller.ListPtrToArray (raw_ret, typeof(GLib.List), false, false, typeof(Gtk.Widget));
			return ret;
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gtk_menu_get_title(IntPtr raw);

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gtk_menu_set_title(IntPtr raw, IntPtr title);

		public string Title { 
			get {
				IntPtr raw_ret = gtk_menu_get_title(Handle);
				string ret = GLib.Marshaller.Utf8PtrToString (raw_ret);
				return ret;
			}
			set {
				IntPtr native_value = GLib.Marshaller.StringToPtrGStrdup (value);
				gtk_menu_set_title(Handle, native_value);
				GLib.Marshaller.Free (native_value);
			}
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gtk_menu_get_type();

		public static new GLib.GType GType { 
			get {
				IntPtr raw_ret = gtk_menu_get_type();
				GLib.GType ret = new GLib.GType(raw_ret);
				return ret;
			}
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gtk_menu_popdown(IntPtr raw);

		public void Popdown() {
			gtk_menu_popdown(Handle);
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gtk_menu_popup(IntPtr raw, IntPtr parent_menu_shell, IntPtr parent_menu_item, GtkSharp.MenuPositionFuncNative func, IntPtr data, uint button, uint activate_time);

		public void Popup(Gtk.Widget parent_menu_shell, Gtk.Widget parent_menu_item, Gtk.MenuPositionFunc func, uint button, uint activate_time) {
			GtkSharp.MenuPositionFuncWrapper func_wrapper = new GtkSharp.MenuPositionFuncWrapper (func);
			func_wrapper.PersistUntilCalled ();
			gtk_menu_popup(Handle, parent_menu_shell == null ? IntPtr.Zero : parent_menu_shell.Handle, parent_menu_item == null ? IntPtr.Zero : parent_menu_item.Handle, func_wrapper.NativeDelegate, IntPtr.Zero, button, activate_time);
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gtk_menu_popup_for_device(IntPtr raw, IntPtr device, IntPtr parent_menu_shell, IntPtr parent_menu_item, GtkSharp.MenuPositionFuncNative func, IntPtr data, GLib.DestroyNotify destroy, uint button, uint activate_time);

		public void PopupForDevice(Gdk.Device device, Gtk.Widget parent_menu_shell, Gtk.Widget parent_menu_item, Gtk.MenuPositionFunc func, GLib.DestroyNotify destroy, uint button, uint activate_time) {
			GtkSharp.MenuPositionFuncWrapper func_wrapper = new GtkSharp.MenuPositionFuncWrapper (func);
			func_wrapper.PersistUntilCalled ();
			gtk_menu_popup_for_device(Handle, device == null ? IntPtr.Zero : device.Handle, parent_menu_shell == null ? IntPtr.Zero : parent_menu_shell.Handle, parent_menu_item == null ? IntPtr.Zero : parent_menu_item.Handle, func_wrapper.NativeDelegate, IntPtr.Zero, destroy, button, activate_time);
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gtk_menu_reorder_child(IntPtr raw, IntPtr child, int position);

		public void ReorderChild(Gtk.Widget child, int position) {
			gtk_menu_reorder_child(Handle, child == null ? IntPtr.Zero : child.Handle, position);
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gtk_menu_reposition(IntPtr raw);

		public void Reposition() {
			gtk_menu_reposition(Handle);
		}

#endregion
	}
}
