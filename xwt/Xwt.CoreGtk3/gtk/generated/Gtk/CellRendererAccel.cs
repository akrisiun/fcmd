// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace Gtk {

	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Runtime.InteropServices;

#region Autogenerated code
	public partial class CellRendererAccel : Gtk.CellRendererText {

		public CellRendererAccel (IntPtr raw) : base(raw) {}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gtk_cell_renderer_accel_new();

		public CellRendererAccel () : base (IntPtr.Zero)
		{
			if (GetType () != typeof (CellRendererAccel)) {
				CreateNativeObject (new string [0], new GLib.Value[0]);
				return;
			}
			Raw = gtk_cell_renderer_accel_new();
		}

		[GLib.Property ("accel-key")]
		public uint AccelKey {
			get {
				GLib.Value val = GetProperty ("accel-key");
				uint ret = (uint) val;
				val.Dispose ();
				return ret;
			}
			set {
				GLib.Value val = new GLib.Value(value);
				SetProperty("accel-key", val);
				val.Dispose ();
			}
		}

		[GLib.Property ("accel-mods")]
		public Gdk.ModifierType AccelMods {
			get {
				GLib.Value val = GetProperty ("accel-mods");
				Gdk.ModifierType ret = (Gdk.ModifierType) (Enum) val;
				val.Dispose ();
				return ret;
			}
			set {
				GLib.Value val = new GLib.Value((Enum) value);
				SetProperty("accel-mods", val);
				val.Dispose ();
			}
		}

		[GLib.Property ("keycode")]
		public uint Keycode {
			get {
				GLib.Value val = GetProperty ("keycode");
				uint ret = (uint) val;
				val.Dispose ();
				return ret;
			}
			set {
				GLib.Value val = new GLib.Value(value);
				SetProperty("keycode", val);
				val.Dispose ();
			}
		}

		[GLib.Property ("accel-mode")]
		public Gtk.CellRendererAccelMode AccelMode {
			get {
				GLib.Value val = GetProperty ("accel-mode");
				Gtk.CellRendererAccelMode ret = (Gtk.CellRendererAccelMode) (Enum) val;
				val.Dispose ();
				return ret;
			}
			set {
				GLib.Value val = new GLib.Value((Enum) value);
				SetProperty("accel-mode", val);
				val.Dispose ();
			}
		}

		[GLib.Signal("accel-cleared")]
		public event Gtk.AccelClearedHandler AccelCleared {
			add {
				this.AddSignalHandler ("accel-cleared", value, typeof (Gtk.AccelClearedArgs));
			}
			remove {
				this.RemoveSignalHandler ("accel-cleared", value);
			}
		}

		[GLib.Signal("accel-edited")]
		public event Gtk.AccelEditedHandler AccelEdited {
			add {
				this.AddSignalHandler ("accel-edited", value, typeof (Gtk.AccelEditedArgs));
			}
			remove {
				this.RemoveSignalHandler ("accel-edited", value);
			}
		}

		static AccelEditedNativeDelegate AccelEdited_cb_delegate;
		static AccelEditedNativeDelegate AccelEditedVMCallback {
			get {
				if (AccelEdited_cb_delegate == null)
					AccelEdited_cb_delegate = new AccelEditedNativeDelegate (AccelEdited_cb);
				return AccelEdited_cb_delegate;
			}
		}

		static void OverrideAccelEdited (GLib.GType gtype)
		{
			OverrideAccelEdited (gtype, AccelEditedVMCallback);
		}

		static void OverrideAccelEdited (GLib.GType gtype, AccelEditedNativeDelegate callback)
		{
			GtkCellRendererAccelClass class_iface = GetClassStruct (gtype, false);
			class_iface.AccelEdited = callback;
			OverrideClassStruct (gtype, class_iface);
		}

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate void AccelEditedNativeDelegate (IntPtr inst, IntPtr path_string, uint accel_key, int accel_mods, uint hardware_keycode);

		static void AccelEdited_cb (IntPtr inst, IntPtr path_string, uint accel_key, int accel_mods, uint hardware_keycode)
		{
			try {
				CellRendererAccel __obj = GLib.Object.GetObject (inst, false) as CellRendererAccel;
				__obj.OnAccelEdited (GLib.Marshaller.Utf8PtrToString (path_string), accel_key, (Gdk.ModifierType) accel_mods, hardware_keycode);
			} catch (Exception e) {
				GLib.ExceptionManager.RaiseUnhandledException (e, false);
			}
		}

		[GLib.DefaultSignalHandler(Type=typeof(Gtk.CellRendererAccel), ConnectionMethod="OverrideAccelEdited")]
		protected virtual void OnAccelEdited (string path_string, uint accel_key, Gdk.ModifierType accel_mods, uint hardware_keycode)
		{
			InternalAccelEdited (path_string, accel_key, accel_mods, hardware_keycode);
		}

		private void InternalAccelEdited (string path_string, uint accel_key, Gdk.ModifierType accel_mods, uint hardware_keycode)
		{
			AccelEditedNativeDelegate unmanaged = GetClassStruct (this.LookupGType ().GetThresholdType (), true).AccelEdited;
			if (unmanaged == null) return;

			IntPtr native_path_string = GLib.Marshaller.StringToPtrGStrdup (path_string);
			unmanaged (this.Handle, native_path_string, accel_key, (int) accel_mods, hardware_keycode);
			GLib.Marshaller.Free (native_path_string);
		}

		static AccelClearedNativeDelegate AccelCleared_cb_delegate;
		static AccelClearedNativeDelegate AccelClearedVMCallback {
			get {
				if (AccelCleared_cb_delegate == null)
					AccelCleared_cb_delegate = new AccelClearedNativeDelegate (AccelCleared_cb);
				return AccelCleared_cb_delegate;
			}
		}

		static void OverrideAccelCleared (GLib.GType gtype)
		{
			OverrideAccelCleared (gtype, AccelClearedVMCallback);
		}

		static void OverrideAccelCleared (GLib.GType gtype, AccelClearedNativeDelegate callback)
		{
			GtkCellRendererAccelClass class_iface = GetClassStruct (gtype, false);
			class_iface.AccelCleared = callback;
			OverrideClassStruct (gtype, class_iface);
		}

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate void AccelClearedNativeDelegate (IntPtr inst, IntPtr path_string);

		static void AccelCleared_cb (IntPtr inst, IntPtr path_string)
		{
			try {
				CellRendererAccel __obj = GLib.Object.GetObject (inst, false) as CellRendererAccel;
				__obj.OnAccelCleared (GLib.Marshaller.Utf8PtrToString (path_string));
			} catch (Exception e) {
				GLib.ExceptionManager.RaiseUnhandledException (e, false);
			}
		}

		[GLib.DefaultSignalHandler(Type=typeof(Gtk.CellRendererAccel), ConnectionMethod="OverrideAccelCleared")]
		protected virtual void OnAccelCleared (string path_string)
		{
			InternalAccelCleared (path_string);
		}

		private void InternalAccelCleared (string path_string)
		{
			AccelClearedNativeDelegate unmanaged = GetClassStruct (this.LookupGType ().GetThresholdType (), true).AccelCleared;
			if (unmanaged == null) return;

			IntPtr native_path_string = GLib.Marshaller.StringToPtrGStrdup (path_string);
			unmanaged (this.Handle, native_path_string);
			GLib.Marshaller.Free (native_path_string);
		}

		[StructLayout (LayoutKind.Sequential)]
		struct GtkCellRendererAccelClass {
			public AccelEditedNativeDelegate AccelEdited;
			public AccelClearedNativeDelegate AccelCleared;
			IntPtr GtkReserved0;
			IntPtr GtkReserved1;
			IntPtr GtkReserved2;
			IntPtr GtkReserved3;
			IntPtr GtkReserved4;
		}

		static uint class_offset = ((GLib.GType) typeof (Gtk.CellRendererText)).GetClassSize ();
		static Dictionary<GLib.GType, GtkCellRendererAccelClass> class_structs;

		static GtkCellRendererAccelClass GetClassStruct (GLib.GType gtype, bool use_cache)
		{
			if (class_structs == null)
				class_structs = new Dictionary<GLib.GType, GtkCellRendererAccelClass> ();

			if (use_cache && class_structs.ContainsKey (gtype))
				return class_structs [gtype];
			else {
				IntPtr class_ptr = new IntPtr (gtype.GetClassPtr ().ToInt64 () + class_offset);
				GtkCellRendererAccelClass class_struct = (GtkCellRendererAccelClass) Marshal.PtrToStructure (class_ptr, typeof (GtkCellRendererAccelClass));
				if (use_cache)
					class_structs.Add (gtype, class_struct);
				return class_struct;
			}
		}

		static void OverrideClassStruct (GLib.GType gtype, GtkCellRendererAccelClass class_struct)
		{
			IntPtr class_ptr = new IntPtr (gtype.GetClassPtr ().ToInt64 () + class_offset);
			Marshal.StructureToPtr (class_struct, class_ptr, false);
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gtk_cell_renderer_accel_get_type();

		public static new GLib.GType GType { 
			get {
				IntPtr raw_ret = gtk_cell_renderer_accel_get_type();
				GLib.GType ret = new GLib.GType(raw_ret);
				return ret;
			}
		}

#endregion
	}
}
