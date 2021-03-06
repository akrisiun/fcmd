// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace Gtk {

	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Runtime.InteropServices;

#region Autogenerated code
	public partial class ComboBoxText : Gtk.ComboBox {

		public ComboBoxText (IntPtr raw) : base(raw) {}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gtk_combo_box_text_new();

		public ComboBoxText () : base (IntPtr.Zero)
		{
			if (GetType () != typeof (ComboBoxText)) {
				CreateNativeObject (new string [0], new GLib.Value[0]);
				return;
			}
			Raw = gtk_combo_box_text_new();
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gtk_combo_box_text_new_with_entry();

		public static new ComboBoxText NewWithEntry()
		{
			ComboBoxText result = new ComboBoxText (gtk_combo_box_text_new_with_entry());
			return result;
		}

		[StructLayout (LayoutKind.Sequential)]
		struct GtkComboBoxTextClass {
			IntPtr GtkReserved1;
			IntPtr GtkReserved2;
			IntPtr GtkReserved3;
			IntPtr GtkReserved4;
		}

		static uint class_offset = ((GLib.GType) typeof (Gtk.ComboBox)).GetClassSize ();
		static Dictionary<GLib.GType, GtkComboBoxTextClass> class_structs;

		static GtkComboBoxTextClass GetClassStruct (GLib.GType gtype, bool use_cache)
		{
			if (class_structs == null)
				class_structs = new Dictionary<GLib.GType, GtkComboBoxTextClass> ();

			if (use_cache && class_structs.ContainsKey (gtype))
				return class_structs [gtype];
			else {
				IntPtr class_ptr = new IntPtr (gtype.GetClassPtr ().ToInt64 () + class_offset);
				GtkComboBoxTextClass class_struct = (GtkComboBoxTextClass) Marshal.PtrToStructure (class_ptr, typeof (GtkComboBoxTextClass));
				if (use_cache)
					class_structs.Add (gtype, class_struct);
				return class_struct;
			}
		}

		static void OverrideClassStruct (GLib.GType gtype, GtkComboBoxTextClass class_struct)
		{
			IntPtr class_ptr = new IntPtr (gtype.GetClassPtr ().ToInt64 () + class_offset);
			Marshal.StructureToPtr (class_struct, class_ptr, false);
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gtk_combo_box_text_append(IntPtr raw, IntPtr id, IntPtr text);

		public void Append(string id, string text) {
			IntPtr native_id = GLib.Marshaller.StringToPtrGStrdup (id);
			IntPtr native_text = GLib.Marshaller.StringToPtrGStrdup (text);
			gtk_combo_box_text_append(Handle, native_id, native_text);
			GLib.Marshaller.Free (native_id);
			GLib.Marshaller.Free (native_text);
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gtk_combo_box_text_append_text(IntPtr raw, IntPtr text);

		public void AppendText(string text) {
			IntPtr native_text = GLib.Marshaller.StringToPtrGStrdup (text);
			gtk_combo_box_text_append_text(Handle, native_text);
			GLib.Marshaller.Free (native_text);
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gtk_combo_box_text_get_active_text(IntPtr raw);

		public string ActiveText { 
			get {
				IntPtr raw_ret = gtk_combo_box_text_get_active_text(Handle);
				string ret = GLib.Marshaller.PtrToStringGFree(raw_ret);
				return ret;
			}
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gtk_combo_box_text_get_type();

		public static new GLib.GType GType { 
			get {
				IntPtr raw_ret = gtk_combo_box_text_get_type();
				GLib.GType ret = new GLib.GType(raw_ret);
				return ret;
			}
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gtk_combo_box_text_insert(IntPtr raw, int position, IntPtr id, IntPtr text);

		public void Insert(int position, string id, string text) {
			IntPtr native_id = GLib.Marshaller.StringToPtrGStrdup (id);
			IntPtr native_text = GLib.Marshaller.StringToPtrGStrdup (text);
			gtk_combo_box_text_insert(Handle, position, native_id, native_text);
			GLib.Marshaller.Free (native_id);
			GLib.Marshaller.Free (native_text);
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gtk_combo_box_text_insert_text(IntPtr raw, int position, IntPtr text);

		public void InsertText(int position, string text) {
			IntPtr native_text = GLib.Marshaller.StringToPtrGStrdup (text);
			gtk_combo_box_text_insert_text(Handle, position, native_text);
			GLib.Marshaller.Free (native_text);
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gtk_combo_box_text_prepend(IntPtr raw, IntPtr id, IntPtr text);

		public void Prepend(string id, string text) {
			IntPtr native_id = GLib.Marshaller.StringToPtrGStrdup (id);
			IntPtr native_text = GLib.Marshaller.StringToPtrGStrdup (text);
			gtk_combo_box_text_prepend(Handle, native_id, native_text);
			GLib.Marshaller.Free (native_id);
			GLib.Marshaller.Free (native_text);
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gtk_combo_box_text_prepend_text(IntPtr raw, IntPtr text);

		public void PrependText(string text) {
			IntPtr native_text = GLib.Marshaller.StringToPtrGStrdup (text);
			gtk_combo_box_text_prepend_text(Handle, native_text);
			GLib.Marshaller.Free (native_text);
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gtk_combo_box_text_remove(IntPtr raw, int position);

		public void Remove(int position) {
			gtk_combo_box_text_remove(Handle, position);
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gtk_combo_box_text_remove_all(IntPtr raw);

		public void RemoveAll() {
			gtk_combo_box_text_remove_all(Handle);
		}

#endregion
	}
}
