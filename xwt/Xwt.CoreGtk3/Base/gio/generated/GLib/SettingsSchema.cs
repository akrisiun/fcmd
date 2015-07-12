// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace GLib {

	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Runtime.InteropServices;

#region Autogenerated code
	public partial class SettingsSchema : GLib.Object {

		public SettingsSchema (IntPtr raw) : base(raw) {}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr g_settings_schema_new(IntPtr name);

		public SettingsSchema (string name) : base (IntPtr.Zero)
		{
			if (GetType () != typeof (SettingsSchema)) {
				throw new InvalidOperationException ("Can't override this constructor.");
			}
			IntPtr native_name = GLib.Marshaller.StringToPtrGStrdup (name);
			Raw = g_settings_schema_new(native_name);
			GLib.Marshaller.Free (native_name);
		}

		[StructLayout (LayoutKind.Sequential)]
		struct GSettingsSchemaClass {
		}

		static uint class_offset = ((GLib.GType) typeof (GLib.Object)).GetClassSize ();
		static Dictionary<GLib.GType, GSettingsSchemaClass> class_structs;

		static GSettingsSchemaClass GetClassStruct (GLib.GType gtype, bool use_cache)
		{
			if (class_structs == null)
				class_structs = new Dictionary<GLib.GType, GSettingsSchemaClass> ();

			if (use_cache && class_structs.ContainsKey (gtype))
				return class_structs [gtype];
			else {
				IntPtr class_ptr = new IntPtr (gtype.GetClassPtr ().ToInt64 () + class_offset);
				GSettingsSchemaClass class_struct = (GSettingsSchemaClass) Marshal.PtrToStructure (class_ptr, typeof (GSettingsSchemaClass));
				if (use_cache)
					class_structs.Add (gtype, class_struct);
				return class_struct;
			}
		}

		static void OverrideClassStruct (GLib.GType gtype, GSettingsSchemaClass class_struct)
		{
			IntPtr class_ptr = new IntPtr (gtype.GetClassPtr ().ToInt64 () + class_offset);
			Marshal.StructureToPtr (class_struct, class_ptr, false);
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr g_settings_schema_get_gettext_domain(IntPtr raw);

		public string GettextDomain { 
			get {
				IntPtr raw_ret = g_settings_schema_get_gettext_domain(Handle);
				string ret = GLib.Marshaller.Utf8PtrToString (raw_ret);
				return ret;
			}
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr g_settings_schema_get_path(IntPtr raw);

		public string Path { 
			get {
				IntPtr raw_ret = g_settings_schema_get_path(Handle);
				string ret = GLib.Marshaller.Utf8PtrToString (raw_ret);
				return ret;
			}
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr g_settings_schema_get_string(IntPtr raw, IntPtr key);

		public string GetString(string key) {
			IntPtr native_key = GLib.Marshaller.StringToPtrGStrdup (key);
			IntPtr raw_ret = g_settings_schema_get_string(Handle, native_key);
			string ret = GLib.Marshaller.Utf8PtrToString (raw_ret);
			GLib.Marshaller.Free (native_key);
			return ret;
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr g_settings_schema_get_type();

		public static new GLib.GType GType { 
			get {
				IntPtr raw_ret = g_settings_schema_get_type();
				GLib.GType ret = new GLib.GType(raw_ret);
				return ret;
			}
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool g_settings_schema_has_key(IntPtr raw, IntPtr key);

		public bool HasKey(string key) {
			IntPtr native_key = GLib.Marshaller.StringToPtrGStrdup (key);
			bool raw_ret = g_settings_schema_has_key(Handle, native_key);
			bool ret = raw_ret;
			GLib.Marshaller.Free (native_key);
			return ret;
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern int g_settings_schema_list(IntPtr raw, out int n_items);

		public int List(out int n_items) {
			int raw_ret = g_settings_schema_list(Handle, out n_items);
			int ret = raw_ret;
			return ret;
		}

#endregion
	}
}