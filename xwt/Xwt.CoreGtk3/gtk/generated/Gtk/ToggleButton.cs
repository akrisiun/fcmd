// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace Gtk {

	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Runtime.InteropServices;

#region Autogenerated code
	public partial class ToggleButton : Gtk.Button {

		public ToggleButton (IntPtr raw) : base(raw) {}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gtk_toggle_button_new();

		public ToggleButton () : base (IntPtr.Zero)
		{
			if (GetType () != typeof (ToggleButton)) {
				CreateNativeObject (new string [0], new GLib.Value[0]);
				return;
			}
			Raw = gtk_toggle_button_new();
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gtk_toggle_button_new_with_label(IntPtr label);

		public static new ToggleButton NewWithLabel(string label)
		{
			IntPtr native_label = GLib.Marshaller.StringToPtrGStrdup (label);
			ToggleButton result = new ToggleButton (gtk_toggle_button_new_with_label(native_label));
			GLib.Marshaller.Free (native_label);
			return result;
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gtk_toggle_button_new_with_mnemonic(IntPtr label);

		public ToggleButton (string label) : base (IntPtr.Zero)
		{
			if (GetType () != typeof (ToggleButton)) {
				var vals = new List<GLib.Value> ();
				var names = new List<string> ();
				names.Add ("label");
				vals.Add (new GLib.Value (label));
				CreateNativeObject (names.ToArray (), vals.ToArray ());
				return;
			}
			IntPtr native_label = GLib.Marshaller.StringToPtrGStrdup (label);
			Raw = gtk_toggle_button_new_with_mnemonic(native_label);
			GLib.Marshaller.Free (native_label);
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool gtk_toggle_button_get_active(IntPtr raw);

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gtk_toggle_button_set_active(IntPtr raw, bool is_active);

		[GLib.Property ("active")]
		public bool Active {
			get  {
				bool raw_ret = gtk_toggle_button_get_active(Handle);
				bool ret = raw_ret;
				return ret;
			}
			set  {
				gtk_toggle_button_set_active(Handle, value);
			}
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool gtk_toggle_button_get_inconsistent(IntPtr raw);

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gtk_toggle_button_set_inconsistent(IntPtr raw, bool setting);

		[GLib.Property ("inconsistent")]
		public bool Inconsistent {
			get  {
				bool raw_ret = gtk_toggle_button_get_inconsistent(Handle);
				bool ret = raw_ret;
				return ret;
			}
			set  {
				gtk_toggle_button_set_inconsistent(Handle, value);
			}
		}

		[GLib.Property ("draw-indicator")]
		public bool DrawIndicator {
			get {
				GLib.Value val = GetProperty ("draw-indicator");
				bool ret = (bool) val;
				val.Dispose ();
				return ret;
			}
			set {
				GLib.Value val = new GLib.Value(value);
				SetProperty("draw-indicator", val);
				val.Dispose ();
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
			GtkToggleButtonClass class_iface = GetClassStruct (gtype, false);
			class_iface.Toggled = callback;
			OverrideClassStruct (gtype, class_iface);
		}

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate void ToggledNativeDelegate (IntPtr inst);

		static void Toggled_cb (IntPtr inst)
		{
			try {
				ToggleButton __obj = GLib.Object.GetObject (inst, false) as ToggleButton;
				__obj.OnToggled ();
			} catch (Exception e) {
				GLib.ExceptionManager.RaiseUnhandledException (e, false);
			}
		}

		[GLib.DefaultSignalHandler(Type=typeof(Gtk.ToggleButton), ConnectionMethod="OverrideToggled")]
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

		[StructLayout (LayoutKind.Sequential)]
		struct GtkToggleButtonClass {
			public ToggledNativeDelegate Toggled;
			IntPtr GtkReserved1;
			IntPtr GtkReserved2;
			IntPtr GtkReserved3;
			IntPtr GtkReserved4;
		}

		static uint class_offset = ((GLib.GType) typeof (Gtk.Button)).GetClassSize ();
		static Dictionary<GLib.GType, GtkToggleButtonClass> class_structs;

		static GtkToggleButtonClass GetClassStruct (GLib.GType gtype, bool use_cache)
		{
			if (class_structs == null)
				class_structs = new Dictionary<GLib.GType, GtkToggleButtonClass> ();

			if (use_cache && class_structs.ContainsKey (gtype))
				return class_structs [gtype];
			else {
				IntPtr class_ptr = new IntPtr (gtype.GetClassPtr ().ToInt64 () + class_offset);
				GtkToggleButtonClass class_struct = (GtkToggleButtonClass) Marshal.PtrToStructure (class_ptr, typeof (GtkToggleButtonClass));
				if (use_cache)
					class_structs.Add (gtype, class_struct);
				return class_struct;
			}
		}

		static void OverrideClassStruct (GLib.GType gtype, GtkToggleButtonClass class_struct)
		{
			IntPtr class_ptr = new IntPtr (gtype.GetClassPtr ().ToInt64 () + class_offset);
			Marshal.StructureToPtr (class_struct, class_ptr, false);
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool gtk_toggle_button_get_mode(IntPtr raw);

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gtk_toggle_button_set_mode(IntPtr raw, bool draw_indicator);

		public bool Mode { 
			get {
				bool raw_ret = gtk_toggle_button_get_mode(Handle);
				bool ret = raw_ret;
				return ret;
			}
			set {
				gtk_toggle_button_set_mode(Handle, value);
			}
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gtk_toggle_button_get_type();

		public static new GLib.GType GType { 
			get {
				IntPtr raw_ret = gtk_toggle_button_get_type();
				GLib.GType ret = new GLib.GType(raw_ret);
				return ret;
			}
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gtk_toggle_button_toggled(IntPtr raw);

		public void Toggle() {
			gtk_toggle_button_toggled(Handle);
		}

#endregion
	}
}
