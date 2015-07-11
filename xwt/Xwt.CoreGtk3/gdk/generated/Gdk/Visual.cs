// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace Gdk {

	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Runtime.InteropServices;

#region Autogenerated code
	public partial class Visual : GLib.Object {

		public Visual (IntPtr raw) : base(raw) {}

		protected Visual() : base(IntPtr.Zero)
		{
			CreateNativeObject (new string [0], new GLib.Value [0]);
		}

		[StructLayout (LayoutKind.Sequential)]
		struct GdkVisualClass {
		}

		static uint class_offset = ((GLib.GType) typeof (GLib.Object)).GetClassSize ();
		static Dictionary<GLib.GType, GdkVisualClass> class_structs;

		static GdkVisualClass GetClassStruct (GLib.GType gtype, bool use_cache)
		{
			if (class_structs == null)
				class_structs = new Dictionary<GLib.GType, GdkVisualClass> ();

			if (use_cache && class_structs.ContainsKey (gtype))
				return class_structs [gtype];
			else {
				IntPtr class_ptr = new IntPtr (gtype.GetClassPtr ().ToInt64 () + class_offset);
				GdkVisualClass class_struct = (GdkVisualClass) Marshal.PtrToStructure (class_ptr, typeof (GdkVisualClass));
				if (use_cache)
					class_structs.Add (gtype, class_struct);
				return class_struct;
			}
		}

		static void OverrideClassStruct (GLib.GType gtype, GdkVisualClass class_struct)
		{
			IntPtr class_ptr = new IntPtr (gtype.GetClassPtr ().ToInt64 () + class_offset);
			Marshal.StructureToPtr (class_struct, class_ptr, false);
		}

		[DllImport("libgdk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gdk_visual_get_best();

		public static Gdk.Visual Best { 
			get {
				IntPtr raw_ret = gdk_visual_get_best();
				Gdk.Visual ret = GLib.Object.GetObject(raw_ret) as Gdk.Visual;
				return ret;
			}
		}

		[DllImport("libgdk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern int gdk_visual_get_best_depth();

		public static int BestDepth { 
			get {
				int raw_ret = gdk_visual_get_best_depth();
				int ret = raw_ret;
				return ret;
			}
		}

		[DllImport("libgdk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern int gdk_visual_get_best_type();

		public static Gdk.VisualType BestType { 
			get {
				int raw_ret = gdk_visual_get_best_type();
				Gdk.VisualType ret = (Gdk.VisualType) raw_ret;
				return ret;
			}
		}

		[DllImport("libgdk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gdk_visual_get_best_with_both(int depth, int visual_type);

		public static Gdk.Visual GetBestWithBoth(int depth, Gdk.VisualType visual_type) {
			IntPtr raw_ret = gdk_visual_get_best_with_both(depth, (int) visual_type);
			Gdk.Visual ret = GLib.Object.GetObject(raw_ret) as Gdk.Visual;
			return ret;
		}

		[DllImport("libgdk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gdk_visual_get_best_with_depth(int depth);

		public static Gdk.Visual GetBestWithDepth(int depth) {
			IntPtr raw_ret = gdk_visual_get_best_with_depth(depth);
			Gdk.Visual ret = GLib.Object.GetObject(raw_ret) as Gdk.Visual;
			return ret;
		}

		[DllImport("libgdk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gdk_visual_get_best_with_type(int visual_type);

		public static Gdk.Visual GetBestWithType(Gdk.VisualType visual_type) {
			IntPtr raw_ret = gdk_visual_get_best_with_type((int) visual_type);
			Gdk.Visual ret = GLib.Object.GetObject(raw_ret) as Gdk.Visual;
			return ret;
		}

		[DllImport("libgdk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern int gdk_visual_get_bits_per_rgb(IntPtr raw);

		public int BitsPerRgb { 
			get {
				int raw_ret = gdk_visual_get_bits_per_rgb(Handle);
				int ret = raw_ret;
				return ret;
			}
		}

		[DllImport("libgdk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gdk_visual_get_blue_pixel_details(IntPtr raw, out uint mask, out int shift, out int precision);

		public void GetBluePixelDetails(out uint mask, out int shift, out int precision) {
			gdk_visual_get_blue_pixel_details(Handle, out mask, out shift, out precision);
		}

		[DllImport("libgdk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern int gdk_visual_get_byte_order(IntPtr raw);

		public Gdk.ByteOrder ByteOrder { 
			get {
				int raw_ret = gdk_visual_get_byte_order(Handle);
				Gdk.ByteOrder ret = (Gdk.ByteOrder) raw_ret;
				return ret;
			}
		}

		[DllImport("libgdk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern int gdk_visual_get_colormap_size(IntPtr raw);

		public int ColormapSize { 
			get {
				int raw_ret = gdk_visual_get_colormap_size(Handle);
				int ret = raw_ret;
				return ret;
			}
		}

		[DllImport("libgdk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern int gdk_visual_get_depth(IntPtr raw);

		public int Depth { 
			get {
				int raw_ret = gdk_visual_get_depth(Handle);
				int ret = raw_ret;
				return ret;
			}
		}

		[DllImport("libgdk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gdk_visual_get_green_pixel_details(IntPtr raw, out uint mask, out int shift, out int precision);

		public void GetGreenPixelDetails(out uint mask, out int shift, out int precision) {
			gdk_visual_get_green_pixel_details(Handle, out mask, out shift, out precision);
		}

		[DllImport("libgdk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gdk_visual_get_red_pixel_details(IntPtr raw, out uint mask, out int shift, out int precision);

		public void GetRedPixelDetails(out uint mask, out int shift, out int precision) {
			gdk_visual_get_red_pixel_details(Handle, out mask, out shift, out precision);
		}

		[DllImport("libgdk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gdk_visual_get_screen(IntPtr raw);

		public Gdk.Screen Screen { 
			get {
				IntPtr raw_ret = gdk_visual_get_screen(Handle);
				Gdk.Screen ret = GLib.Object.GetObject(raw_ret) as Gdk.Screen;
				return ret;
			}
		}

		[DllImport("libgdk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gdk_visual_get_system();

		public static Gdk.Visual System { 
			get {
				IntPtr raw_ret = gdk_visual_get_system();
				Gdk.Visual ret = GLib.Object.GetObject(raw_ret) as Gdk.Visual;
				return ret;
			}
		}

		[DllImport("libgdk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gdk_visual_get_type();

		public static new GLib.GType GType { 
			get {
				IntPtr raw_ret = gdk_visual_get_type();
				GLib.GType ret = new GLib.GType(raw_ret);
				return ret;
			}
		}

		[DllImport("libgdk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern int gdk_visual_get_visual_type(IntPtr raw);

		public Gdk.VisualType VisualType { 
			get {
				int raw_ret = gdk_visual_get_visual_type(Handle);
				Gdk.VisualType ret = (Gdk.VisualType) raw_ret;
				return ret;
			}
		}

#endregion
	}
}
