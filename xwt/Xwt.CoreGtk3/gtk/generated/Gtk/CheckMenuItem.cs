// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace Gtk {

	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Runtime.InteropServices;

#region Autogenerated code
	public partial class CheckMenuItem : Gtk.MenuItem {

		public CheckMenuItem (IntPtr raw) : base(raw) {}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gtk_check_menu_item_new();

		public CheckMenuItem () : base (IntPtr.Zero)
		{
			if (GetType () != typeof (CheckMenuItem)) {
				CreateNativeObject (new string [0], new GLib.Value[0]);
				return;
			}
			Raw = gtk_check_menu_item_new();
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool gtk_check_menu_item_get_active(IntPtr raw);

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gtk_check_menu_item_set_active(IntPtr raw, bool is_active);

		[GLib.Property ("active")]
		public bool Active {
			get  {
				bool raw_ret = gtk_check_menu_item_get_active(Handle);
				bool ret = raw_ret;
				return ret;
			}
			set  {
				gtk_check_menu_item_set_active(Handle, value);
			}
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool gtk_check_menu_item_get_inconsistent(IntPtr raw);

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gtk_check_menu_item_set_inconsistent(IntPtr raw, bool setting);

		[GLib.Property ("inconsistent")]
		public bool Inconsistent {
			get  {
				bool raw_ret = gtk_check_menu_item_get_inconsistent(Handle);
				bool ret = raw_ret;
				return ret;
			}
			set  {
				gtk_check_menu_item_set_inconsistent(Handle, value);
			}
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool gtk_check_menu_item_get_draw_as_radio(IntPtr raw);

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gtk_check_menu_item_set_draw_as_radio(IntPtr raw, bool draw_as_radio);

		[GLib.Property ("draw-as-radio")]
		public bool DrawAsRadio {
			get  {
				bool raw_ret = gtk_check_menu_item_get_draw_as_radio(Handle);
				bool ret = raw_ret;
				return ret;
			}
			set  {
				gtk_check_menu_item_set_draw_as_radio(Handle, value);
			}
		}

		[GLib.Signal("toggled")]
		public event System.EventHandler Toggled {
			add {
				this.AddSignalHandler ("toggled", value);
			}
			remove {
				this.RemoveSignalHandler ("toggled", value);
			}
		}

		static ToggledNativeDelegate Toggled_cb_delegate;
		static ToggledNativeDelegate ToggledVMCallback {
			get {
				if (Toggled_cb_delegate == null)
					Toggled_cb_delegate = new ToggledNativeDelegate (Toggled_cb);
				return Toggled_cb_delegate;
			}
		}

		static void OverrideToggled (GLib.GType gtype)
		{
			OverrideToggled (gtype, ToggledVMCallback);
		}

		static void OverrideToggled (GLib.GType gtype, ToggledNativeDelegate callback)
		{
			GtkCheckMenuItemClass class_iface = GetClassStruct (gtype, false);
			class_iface.Toggled = callback;
			OverrideClassStruct (gtype, class_iface);
		}

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate void ToggledNativeDelegate (IntPtr inst);

		static void Toggled_cb (IntPtr inst)
		{
			try {
				CheckMenuItem __obj = GLib.Object.GetObject (inst, false) as CheckMenuItem;
				__obj.OnToggled ();
			} catch (Exception e) {
				GLib.ExceptionManager.RaiseUnhandledException (e, false);
			}
		}

		[GLib.DefaultSignalHandler(Type=typeof(Gtk.CheckMenuItem), ConnectionMethod="OverrideToggled")]
		protected virtual void OnToggled ()
		{
			InternalToggled ();
		}

		private void InternalToggled ()
		{
			ToggledNativeDelegate unmanaged = GetClassStruct (this.LookupGType ().GetThresholdType (), true).Toggled;
			if (unmanaged == null) return;

			unmanaged (this.Handle);
		}

		static DrawIndicatorNativeDelegate DrawIndicator_cb_delegate;
		static DrawIndicatorNativeDelegate DrawIndicatorVMCallback {
			get {
				if (DrawIndicator_cb_delegate == null)
					DrawIndicator_cb_delegate = new DrawIndicatorNativeDelegate (DrawIndicator_cb);
				return DrawIndicator_cb_delegate;
			}
		}

		static void OverrideDrawIndicator (GLib.GType gtype)
		{
			OverrideDrawIndicator (gtype, DrawIndicatorVMCallback);
		}

		static void OverrideDrawIndicator (GLib.GType gtype, DrawIndicatorNativeDelegate callback)
		{
			GtkCheckMenuItemClass class_iface = GetClassStruct (gtype, false);
			class_iface.DrawIndicator = callback;
			OverrideClassStruct (gtype, class_iface);
		}

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate void DrawIndicatorNativeDelegate (IntPtr inst, IntPtr cr);

		static void DrawIndicator_cb (IntPtr inst, IntPtr cr)
		{
			Cairo.Context mycr = null;

			try {
				CheckMenuItem __obj = GLib.Object.GetObject (inst, false) as CheckMenuItem;
				mycr = new Cairo.Context (cr, false);
				__obj.OnDrawIndicator (mycr);
			} catch (Exception e) {
				GLib.ExceptionManager.RaiseUnhandledException (e, false);
			} finally {
				var disposable_cr = mycr as IDisposable;
				if (disposable_cr != null)
					disposable_cr.Dispose ();
			}
		}

		[GLib.DefaultSignalHandler(Type=typeof(Gtk.CheckMenuItem), ConnectionMethod="OverrideDrawIndicator")]
		protected virtual void OnDrawIndicator (Cairo.Context cr)
		{
			InternalDrawIndicator (cr);
		}

		private void InternalDrawIndicator (Cairo.Context cr)
		{
			DrawIndicatorNativeDelegate unmanaged = GetClassStruct (this.LookupGType ().GetThresholdType (), true).DrawIndicator;
			if (unmanaged == null) return;

			unmanaged (this.Handle, cr == null ? IntPtr.Zero : cr.Handle);
		}

		[StructLayout (LayoutKind.Sequential)]
		struct GtkCheckMenuItemClass {
			public ToggledNativeDelegate Toggled;
			public DrawIndicatorNativeDelegate DrawIndicator;
			IntPtr GtkReserved1;
			IntPtr GtkReserved2;
			IntPtr GtkReserved3;
			IntPtr GtkReserved4;
		}

		static uint class_offset = ((GLib.GType) typeof (Gtk.MenuItem)).GetClassSize ();
		static Dictionary<GLib.GType, GtkCheckMenuItemClass> class_structs;

		static GtkCheckMenuItemClass GetClassStruct (GLib.GType gtype, bool use_cache)
		{
			if (class_structs == null)
				class_structs = new Dictionary<GLib.GType, GtkCheckMenuItemClass> ();

			if (use_cache && class_structs.ContainsKey (gtype))
				return class_structs [gtype];
			else {
				IntPtr class_ptr = new IntPtr (gtype.GetClassPtr ().ToInt64 () + class_offset);
				GtkCheckMenuItemClass class_struct = (GtkCheckMenuItemClass) Marshal.PtrToStructure (class_ptr, typeof (GtkCheckMenuItemClass));
				if (use_cache)
					class_structs.Add (gtype, class_struct);
				return class_struct;
			}
		}

		static void OverrideClassStruct (GLib.GType gtype, GtkCheckMenuItemClass class_struct)
		{
			IntPtr class_ptr = new IntPtr (gtype.GetClassPtr ().ToInt64 () + class_offset);
			Marshal.StructureToPtr (class_struct, class_ptr, false);
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gtk_check_menu_item_get_type();

		public static new GLib.GType GType { 
			get {
				IntPtr raw_ret = gtk_check_menu_item_get_type();
				GLib.GType ret = new GLib.GType(raw_ret);
				return ret;
			}
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gtk_check_menu_item_toggled(IntPtr raw);

		public void EmitToggled() {
			gtk_check_menu_item_toggled(Handle);
		}

#endregion
	}
}
