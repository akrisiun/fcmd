// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace Gtk {

	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Runtime.InteropServices;

#region Autogenerated code
	public partial class HBox : Gtk.Box {

		public HBox (IntPtr raw) : base(raw) {}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gtk_hbox_new(bool homogeneous, int spacing);

		public HBox (bool homogeneous, int spacing) : base (IntPtr.Zero)
		{
			if (GetType () != typeof (HBox)) {
				var vals = new List<GLib.Value> ();
				var names = new List<string> ();
				names.Add ("homogeneous");
				vals.Add (new GLib.Value (homogeneous));
				names.Add ("spacing");
				vals.Add (new GLib.Value (spacing));
				CreateNativeObject (names.ToArray (), vals.ToArray ());
				return;
			}
			Raw = gtk_hbox_new(homogeneous, spacing);
		}

		[StructLayout (LayoutKind.Sequential)]
		struct GtkHBoxClass {
		}

		static uint class_offset = ((GLib.GType) typeof (Gtk.Box)).GetClassSize ();
		static Dictionary<GLib.GType, GtkHBoxClass> class_structs;

		static GtkHBoxClass GetClassStruct (GLib.GType gtype, bool use_cache)
		{
			if (class_structs == null)
				class_structs = new Dictionary<GLib.GType, GtkHBoxClass> ();

			if (use_cache && class_structs.ContainsKey (gtype))
				return class_structs [gtype];
			else {
				IntPtr class_ptr = new IntPtr (gtype.GetClassPtr ().ToInt64 () + class_offset);
				GtkHBoxClass class_struct = (GtkHBoxClass) Marshal.PtrToStructure (class_ptr, typeof (GtkHBoxClass));
				if (use_cache)
					class_structs.Add (gtype, class_struct);
				return class_struct;
			}
		}

		static void OverrideClassStruct (GLib.GType gtype, GtkHBoxClass class_struct)
		{
			IntPtr class_ptr = new IntPtr (gtype.GetClassPtr ().ToInt64 () + class_offset);
			Marshal.StructureToPtr (class_struct, class_ptr, false);
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gtk_hbox_get_type();

		public static new GLib.GType GType { 
			get {
				IntPtr raw_ret = gtk_hbox_get_type();
				GLib.GType ret = new GLib.GType(raw_ret);
				return ret;
			}
		}

#endregion
	}
}