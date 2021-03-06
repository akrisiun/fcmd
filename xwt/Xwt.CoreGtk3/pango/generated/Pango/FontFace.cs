// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace Pango {

	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Runtime.InteropServices;

#region Autogenerated code
	public partial class FontFace : GLib.Object {

		public FontFace (IntPtr raw) : base(raw) {}

		protected FontFace() : base(IntPtr.Zero)
		{
			CreateNativeObject (new string [0], new GLib.Value [0]);
		}

		[DllImport("libpango-1.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr pango_font_face_describe(IntPtr raw);

		public Pango.FontDescription Describe() {
			IntPtr raw_ret = pango_font_face_describe(Handle);
			Pango.FontDescription ret = raw_ret == IntPtr.Zero ? null : (Pango.FontDescription) GLib.Opaque.GetOpaque (raw_ret, typeof (Pango.FontDescription), true);
			return ret;
		}

		[DllImport("libpango-1.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr pango_font_face_get_face_name(IntPtr raw);

		public string FaceName { 
			get {
				IntPtr raw_ret = pango_font_face_get_face_name(Handle);
				string ret = GLib.Marshaller.Utf8PtrToString (raw_ret);
				return ret;
			}
		}

		[DllImport("libpango-1.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr pango_font_face_get_type();

		public static new GLib.GType GType { 
			get {
				IntPtr raw_ret = pango_font_face_get_type();
				GLib.GType ret = new GLib.GType(raw_ret);
				return ret;
			}
		}

		[DllImport("libpango-1.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool pango_font_face_is_synthesized(IntPtr raw);

		public bool IsSynthesized { 
			get {
				bool raw_ret = pango_font_face_is_synthesized(Handle);
				bool ret = raw_ret;
				return ret;
			}
		}

		[DllImport("libpango-1.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void pango_font_face_list_sizes(IntPtr raw, out int sizes, out int n_sizes);

		public void ListSizes(out int sizes, out int n_sizes) {
			pango_font_face_list_sizes(Handle, out sizes, out n_sizes);
		}

#endregion
	}
}
