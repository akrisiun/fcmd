// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace GLib {

	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Runtime.InteropServices;

#region Autogenerated code
	public partial class DesktopAppInfo : GLib.Object, GLib.IAppInfo {

		public DesktopAppInfo (IntPtr raw) : base(raw) {}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr g_desktop_app_info_new(IntPtr desktop_id);

		public DesktopAppInfo (string desktop_id) : base (IntPtr.Zero)
		{
			if (GetType () != typeof (DesktopAppInfo)) {
				throw new InvalidOperationException ("Can't override this constructor.");
			}
			IntPtr native_desktop_id = GLib.Marshaller.StringToPtrGStrdup (desktop_id);
			Raw = g_desktop_app_info_new(native_desktop_id);
			GLib.Marshaller.Free (native_desktop_id);
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr g_desktop_app_info_new_from_filename(IntPtr filename);

		public static DesktopAppInfo NewFromFilename(string filename)
		{
			IntPtr native_filename = GLib.Marshaller.StringToPtrGStrdup (filename);
			DesktopAppInfo result = new DesktopAppInfo (g_desktop_app_info_new_from_filename(native_filename));
			GLib.Marshaller.Free (native_filename);
			return result;
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr g_desktop_app_info_new_from_keyfile(IntPtr key_file);

		public DesktopAppInfo (GLib.KeyFile key_file) : base (IntPtr.Zero)
		{
			if (GetType () != typeof (DesktopAppInfo)) {
				throw new InvalidOperationException ("Can't override this constructor.");
			}
			Raw = g_desktop_app_info_new_from_keyfile(key_file == null ? IntPtr.Zero : key_file.Handle);
		}

		[StructLayout (LayoutKind.Sequential)]
		struct GDesktopAppInfoClass {
		}

		static uint class_offset = ((GLib.GType) typeof (GLib.Object)).GetClassSize ();
		static Dictionary<GLib.GType, GDesktopAppInfoClass> class_structs;

		static GDesktopAppInfoClass GetClassStruct (GLib.GType gtype, bool use_cache)
		{
			if (class_structs == null)
				class_structs = new Dictionary<GLib.GType, GDesktopAppInfoClass> ();

			if (use_cache && class_structs.ContainsKey (gtype))
				return class_structs [gtype];
			else {
				IntPtr class_ptr = new IntPtr (gtype.GetClassPtr ().ToInt64 () + class_offset);
				GDesktopAppInfoClass class_struct = (GDesktopAppInfoClass) Marshal.PtrToStructure (class_ptr, typeof (GDesktopAppInfoClass));
				if (use_cache)
					class_structs.Add (gtype, class_struct);
				return class_struct;
			}
		}

		static void OverrideClassStruct (GLib.GType gtype, GDesktopAppInfoClass class_struct)
		{
			IntPtr class_ptr = new IntPtr (gtype.GetClassPtr ().ToInt64 () + class_offset);
			Marshal.StructureToPtr (class_struct, class_ptr, false);
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr g_desktop_app_info_get_filename(IntPtr raw);

		public string Filename { 
			get {
				IntPtr raw_ret = g_desktop_app_info_get_filename(Handle);
				string ret = GLib.Marshaller.Utf8PtrToString (raw_ret);
				return ret;
			}
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool g_desktop_app_info_get_is_hidden(IntPtr raw);

		public bool IsHidden { 
			get {
				bool raw_ret = g_desktop_app_info_get_is_hidden(Handle);
				bool ret = raw_ret;
				return ret;
			}
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr g_desktop_app_info_get_type();

		public static new GLib.GType GType { 
			get {
				IntPtr raw_ret = g_desktop_app_info_get_type();
				GLib.GType ret = new GLib.GType(raw_ret);
				return ret;
			}
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void g_desktop_app_info_set_desktop_env(IntPtr desktop_env);

		public static string DesktopEnv { 
			set {
				IntPtr native_value = GLib.Marshaller.StringToPtrGStrdup (value);
				g_desktop_app_info_set_desktop_env(native_value);
				GLib.Marshaller.Free (native_value);
			}
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool g_app_info_add_supports_type(IntPtr raw, IntPtr content_type, out IntPtr error);

		public bool AddSupportsType(string content_type) {
			IntPtr native_content_type = GLib.Marshaller.StringToPtrGStrdup (content_type);
			IntPtr error = IntPtr.Zero;
			bool raw_ret = g_app_info_add_supports_type(Handle, native_content_type, out error);
			bool ret = raw_ret;
			GLib.Marshaller.Free (native_content_type);
			if (error != IntPtr.Zero) throw new GLib.GException (error);
			return ret;
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool g_app_info_can_delete(IntPtr raw);

		public bool CanDelete() {
			bool raw_ret = g_app_info_can_delete(Handle);
			bool ret = raw_ret;
			return ret;
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool g_app_info_can_remove_supports_type(IntPtr raw);

		public bool CanRemoveSupportsType { 
			get {
				bool raw_ret = g_app_info_can_remove_supports_type(Handle);
				bool ret = raw_ret;
				return ret;
			}
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool g_app_info_delete(IntPtr raw);

		public bool Delete() {
			bool raw_ret = g_app_info_delete(Handle);
			bool ret = raw_ret;
			return ret;
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr g_app_info_dup(IntPtr raw);

		public GLib.IAppInfo Dup() {
			IntPtr raw_ret = g_app_info_dup(Handle);
			GLib.IAppInfo ret = GLib.AppInfoAdapter.GetObject (raw_ret, false);
			return ret;
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool g_app_info_equal(IntPtr raw, IntPtr appinfo2);

		public bool Equal(GLib.IAppInfo appinfo2) {
			bool raw_ret = g_app_info_equal(Handle, appinfo2 == null ? IntPtr.Zero : ((appinfo2 is GLib.Object) ? (appinfo2 as GLib.Object).Handle : (appinfo2 as GLib.AppInfoAdapter).Handle));
			bool ret = raw_ret;
			return ret;
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr g_app_info_get_commandline(IntPtr raw);

		public string Commandline { 
			get {
				IntPtr raw_ret = g_app_info_get_commandline(Handle);
				string ret = GLib.Marshaller.Utf8PtrToString (raw_ret);
				return ret;
			}
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr g_app_info_get_description(IntPtr raw);

		public string Description { 
			get {
				IntPtr raw_ret = g_app_info_get_description(Handle);
				string ret = GLib.Marshaller.Utf8PtrToString (raw_ret);
				return ret;
			}
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr g_app_info_get_display_name(IntPtr raw);

		public string DisplayName { 
			get {
				IntPtr raw_ret = g_app_info_get_display_name(Handle);
				string ret = GLib.Marshaller.Utf8PtrToString (raw_ret);
				return ret;
			}
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr g_app_info_get_executable(IntPtr raw);

		public string Executable { 
			get {
				IntPtr raw_ret = g_app_info_get_executable(Handle);
				string ret = GLib.Marshaller.Utf8PtrToString (raw_ret);
				return ret;
			}
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr g_app_info_get_icon(IntPtr raw);

		public GLib.IIcon Icon { 
			get {
				IntPtr raw_ret = g_app_info_get_icon(Handle);
				GLib.IIcon ret = GLib.IconAdapter.GetObject (raw_ret, false);
				return ret;
			}
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr g_app_info_get_id(IntPtr raw);

		public string Id { 
			get {
				IntPtr raw_ret = g_app_info_get_id(Handle);
				string ret = GLib.Marshaller.Utf8PtrToString (raw_ret);
				return ret;
			}
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr g_app_info_get_name(IntPtr raw);

		public string Name { 
			get {
				IntPtr raw_ret = g_app_info_get_name(Handle);
				string ret = GLib.Marshaller.Utf8PtrToString (raw_ret);
				return ret;
			}
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool g_app_info_launch(IntPtr raw, IntPtr files, IntPtr launch_context, out IntPtr error);

		public bool Launch(GLib.List files, GLib.AppLaunchContext launch_context) {
			IntPtr error = IntPtr.Zero;
			bool raw_ret = g_app_info_launch(Handle, files == null ? IntPtr.Zero : files.Handle, launch_context == null ? IntPtr.Zero : launch_context.Handle, out error);
			bool ret = raw_ret;
			if (error != IntPtr.Zero) throw new GLib.GException (error);
			return ret;
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool g_app_info_launch_uris(IntPtr raw, IntPtr uris, IntPtr launch_context, out IntPtr error);

		public bool LaunchUris(GLib.List uris, GLib.AppLaunchContext launch_context) {
			IntPtr error = IntPtr.Zero;
			bool raw_ret = g_app_info_launch_uris(Handle, uris == null ? IntPtr.Zero : uris.Handle, launch_context == null ? IntPtr.Zero : launch_context.Handle, out error);
			bool ret = raw_ret;
			if (error != IntPtr.Zero) throw new GLib.GException (error);
			return ret;
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool g_app_info_remove_supports_type(IntPtr raw, IntPtr content_type, out IntPtr error);

		public bool RemoveSupportsType(string content_type) {
			IntPtr native_content_type = GLib.Marshaller.StringToPtrGStrdup (content_type);
			IntPtr error = IntPtr.Zero;
			bool raw_ret = g_app_info_remove_supports_type(Handle, native_content_type, out error);
			bool ret = raw_ret;
			GLib.Marshaller.Free (native_content_type);
			if (error != IntPtr.Zero) throw new GLib.GException (error);
			return ret;
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool g_app_info_set_as_default_for_extension(IntPtr raw, IntPtr extension, out IntPtr error);

		public bool SetAsDefaultForExtension(string extension) {
			IntPtr native_extension = GLib.Marshaller.StringToPtrGStrdup (extension);
			IntPtr error = IntPtr.Zero;
			bool raw_ret = g_app_info_set_as_default_for_extension(Handle, native_extension, out error);
			bool ret = raw_ret;
			GLib.Marshaller.Free (native_extension);
			if (error != IntPtr.Zero) throw new GLib.GException (error);
			return ret;
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool g_app_info_set_as_default_for_type(IntPtr raw, IntPtr content_type, out IntPtr error);

		public bool SetAsDefaultForType(string content_type) {
			IntPtr native_content_type = GLib.Marshaller.StringToPtrGStrdup (content_type);
			IntPtr error = IntPtr.Zero;
			bool raw_ret = g_app_info_set_as_default_for_type(Handle, native_content_type, out error);
			bool ret = raw_ret;
			GLib.Marshaller.Free (native_content_type);
			if (error != IntPtr.Zero) throw new GLib.GException (error);
			return ret;
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool g_app_info_set_as_last_used_for_type(IntPtr raw, IntPtr content_type, out IntPtr error);

		public bool SetAsLastUsedForType(string content_type) {
			IntPtr native_content_type = GLib.Marshaller.StringToPtrGStrdup (content_type);
			IntPtr error = IntPtr.Zero;
			bool raw_ret = g_app_info_set_as_last_used_for_type(Handle, native_content_type, out error);
			bool ret = raw_ret;
			GLib.Marshaller.Free (native_content_type);
			if (error != IntPtr.Zero) throw new GLib.GException (error);
			return ret;
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool g_app_info_should_show(IntPtr raw);

		public bool ShouldShow { 
			get {
				bool raw_ret = g_app_info_should_show(Handle);
				bool ret = raw_ret;
				return ret;
			}
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool g_app_info_supports_files(IntPtr raw);

		public bool SupportsFiles { 
			get {
				bool raw_ret = g_app_info_supports_files(Handle);
				bool ret = raw_ret;
				return ret;
			}
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool g_app_info_supports_uris(IntPtr raw);

		public bool SupportsUris { 
			get {
				bool raw_ret = g_app_info_supports_uris(Handle);
				bool ret = raw_ret;
				return ret;
			}
		}

#endregion
	}
}
