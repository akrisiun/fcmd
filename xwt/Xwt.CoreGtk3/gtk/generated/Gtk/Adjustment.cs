// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace Gtk {

	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Runtime.InteropServices;

#region Autogenerated code
	public partial class Adjustment : GLib.InitiallyUnowned {

		public Adjustment (IntPtr raw) : base(raw) {}

		protected Adjustment() : base(IntPtr.Zero)
		{
			CreateNativeObject (new string [0], new GLib.Value [0]);
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern double gtk_adjustment_get_value(IntPtr raw);

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gtk_adjustment_set_value(IntPtr raw, double value);

		[GLib.Property ("value")]
		public double Value {
			get  {
				double raw_ret = gtk_adjustment_get_value(Handle);
				double ret = raw_ret;
				return ret;
			}
			set  {
				gtk_adjustment_set_value(Handle, value);
			}
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern double gtk_adjustment_get_lower(IntPtr raw);

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gtk_adjustment_set_lower(IntPtr raw, double lower);

		[GLib.Property ("lower")]
		public double Lower {
			get  {
				double raw_ret = gtk_adjustment_get_lower(Handle);
				double ret = raw_ret;
				return ret;
			}
			set  {
				gtk_adjustment_set_lower(Handle, value);
			}
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern double gtk_adjustment_get_upper(IntPtr raw);

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gtk_adjustment_set_upper(IntPtr raw, double upper);

		[GLib.Property ("upper")]
		public double Upper {
			get  {
				double raw_ret = gtk_adjustment_get_upper(Handle);
				double ret = raw_ret;
				return ret;
			}
			set  {
				gtk_adjustment_set_upper(Handle, value);
			}
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern double gtk_adjustment_get_step_increment(IntPtr raw);

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gtk_adjustment_set_step_increment(IntPtr raw, double step_increment);

		[GLib.Property ("step-increment")]
		public double StepIncrement {
			get  {
				double raw_ret = gtk_adjustment_get_step_increment(Handle);
				double ret = raw_ret;
				return ret;
			}
			set  {
				gtk_adjustment_set_step_increment(Handle, value);
			}
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern double gtk_adjustment_get_page_increment(IntPtr raw);

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gtk_adjustment_set_page_increment(IntPtr raw, double page_increment);

		[GLib.Property ("page-increment")]
		public double PageIncrement {
			get  {
				double raw_ret = gtk_adjustment_get_page_increment(Handle);
				double ret = raw_ret;
				return ret;
			}
			set  {
				gtk_adjustment_set_page_increment(Handle, value);
			}
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern double gtk_adjustment_get_page_size(IntPtr raw);

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gtk_adjustment_set_page_size(IntPtr raw, double page_size);

		[GLib.Property ("page-size")]
		public double PageSize {
			get  {
				double raw_ret = gtk_adjustment_get_page_size(Handle);
				double ret = raw_ret;
				return ret;
			}
			set  {
				gtk_adjustment_set_page_size(Handle, value);
			}
		}

		[GLib.Signal("value-changed")]
		public event System.EventHandler ValueChanged {
			add {
				this.AddSignalHandler ("value-changed", value);
			}
			remove {
				this.RemoveSignalHandler ("value-changed", value);
			}
		}

		[GLib.Signal("changed")]
		public event System.EventHandler Changed {
			add {
				this.AddSignalHandler ("changed", value);
			}
			remove {
				this.RemoveSignalHandler ("changed", value);
			}
		}

		static ChangedNativeDelegate Changed_cb_delegate;
		static ChangedNativeDelegate ChangedVMCallback {
			get {
				if (Changed_cb_delegate == null)
					Changed_cb_delegate = new ChangedNativeDelegate (Changed_cb);
				return Changed_cb_delegate;
			}
		}

		static void OverrideChanged (GLib.GType gtype)
		{
			OverrideChanged (gtype, ChangedVMCallback);
		}

		static void OverrideChanged (GLib.GType gtype, ChangedNativeDelegate callback)
		{
			GtkAdjustmentClass class_iface = GetClassStruct (gtype, false);
			class_iface.Changed = callback;
			OverrideClassStruct (gtype, class_iface);
		}

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate void ChangedNativeDelegate (IntPtr inst);

		static void Changed_cb (IntPtr inst)
		{
			try {
				Adjustment __obj = GLib.Object.GetObject (inst, false) as Adjustment;
				__obj.OnChanged ();
			} catch (Exception e) {
				GLib.ExceptionManager.RaiseUnhandledException (e, false);
			}
		}

		[GLib.DefaultSignalHandler(Type=typeof(Gtk.Adjustment), ConnectionMethod="OverrideChanged")]
		protected virtual void OnChanged ()
		{
			InternalChanged ();
		}

		private void InternalChanged ()
		{
			ChangedNativeDelegate unmanaged = GetClassStruct (this.LookupGType ().GetThresholdType (), true).Changed;
			if (unmanaged == null) return;

			unmanaged (this.Handle);
		}

		static ValueChangedNativeDelegate ValueChanged_cb_delegate;
		static ValueChangedNativeDelegate ValueChangedVMCallback {
			get {
				if (ValueChanged_cb_delegate == null)
					ValueChanged_cb_delegate = new ValueChangedNativeDelegate (ValueChanged_cb);
				return ValueChanged_cb_delegate;
			}
		}

		static void OverrideValueChanged (GLib.GType gtype)
		{
			OverrideValueChanged (gtype, ValueChangedVMCallback);
		}

		static void OverrideValueChanged (GLib.GType gtype, ValueChangedNativeDelegate callback)
		{
			GtkAdjustmentClass class_iface = GetClassStruct (gtype, false);
			class_iface.ValueChanged = callback;
			OverrideClassStruct (gtype, class_iface);
		}

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate void ValueChangedNativeDelegate (IntPtr inst);

		static void ValueChanged_cb (IntPtr inst)
		{
			try {
				Adjustment __obj = GLib.Object.GetObject (inst, false) as Adjustment;
				__obj.OnValueChanged ();
			} catch (Exception e) {
				GLib.ExceptionManager.RaiseUnhandledException (e, false);
			}
		}

		[GLib.DefaultSignalHandler(Type=typeof(Gtk.Adjustment), ConnectionMethod="OverrideValueChanged")]
		protected virtual void OnValueChanged ()
		{
			InternalValueChanged ();
		}

		private void InternalValueChanged ()
		{
			ValueChangedNativeDelegate unmanaged = GetClassStruct (this.LookupGType ().GetThresholdType (), true).ValueChanged;
			if (unmanaged == null) return;

			unmanaged (this.Handle);
		}

		[StructLayout (LayoutKind.Sequential)]
		struct GtkAdjustmentClass {
			public ChangedNativeDelegate Changed;
			public ValueChangedNativeDelegate ValueChanged;
			IntPtr GtkReserved1;
			IntPtr GtkReserved2;
			IntPtr GtkReserved3;
			IntPtr GtkReserved4;
		}

		static uint class_offset = ((GLib.GType) typeof (GLib.InitiallyUnowned)).GetClassSize ();
		static Dictionary<GLib.GType, GtkAdjustmentClass> class_structs;

		static GtkAdjustmentClass GetClassStruct (GLib.GType gtype, bool use_cache)
		{
			if (class_structs == null)
				class_structs = new Dictionary<GLib.GType, GtkAdjustmentClass> ();

			if (use_cache && class_structs.ContainsKey (gtype))
				return class_structs [gtype];
			else {
				IntPtr class_ptr = new IntPtr (gtype.GetClassPtr ().ToInt64 () + class_offset);
				GtkAdjustmentClass class_struct = (GtkAdjustmentClass) Marshal.PtrToStructure (class_ptr, typeof (GtkAdjustmentClass));
				if (use_cache)
					class_structs.Add (gtype, class_struct);
				return class_struct;
			}
		}

		static void OverrideClassStruct (GLib.GType gtype, GtkAdjustmentClass class_struct)
		{
			IntPtr class_ptr = new IntPtr (gtype.GetClassPtr ().ToInt64 () + class_offset);
			Marshal.StructureToPtr (class_struct, class_ptr, false);
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gtk_adjustment_changed(IntPtr raw);

		public void Change() {
			gtk_adjustment_changed(Handle);
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gtk_adjustment_clamp_page(IntPtr raw, double lower, double upper);

		public void ClampPage(double lower, double upper) {
			gtk_adjustment_clamp_page(Handle, lower, upper);
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gtk_adjustment_configure(IntPtr raw, double value, double lower, double upper, double step_increment, double page_increment, double page_size);

		public void Configure(double value, double lower, double upper, double step_increment, double page_increment, double page_size) {
			gtk_adjustment_configure(Handle, value, lower, upper, step_increment, page_increment, page_size);
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gtk_adjustment_get_type();

		public static new GLib.GType GType { 
			get {
				IntPtr raw_ret = gtk_adjustment_get_type();
				GLib.GType ret = new GLib.GType(raw_ret);
				return ret;
			}
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gtk_adjustment_value_changed(IntPtr raw);

		public void ChangeValue() {
			gtk_adjustment_value_changed(Handle);
		}

#endregion
	}
}
