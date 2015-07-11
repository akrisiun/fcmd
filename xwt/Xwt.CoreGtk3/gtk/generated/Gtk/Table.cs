// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace Gtk {

	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Runtime.InteropServices;

#region Autogenerated code
	public partial class Table : Gtk.Container {

		public Table (IntPtr raw) : base(raw) {}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gtk_table_new(uint rows, uint columns, bool homogeneous);

		public Table (uint rows, uint columns, bool homogeneous) : base (IntPtr.Zero)
		{
			if (GetType () != typeof (Table)) {
				var vals = new List<GLib.Value> ();
				var names = new List<string> ();
				names.Add ("n_rows");
				vals.Add (new GLib.Value (rows));
				names.Add ("n_columns");
				vals.Add (new GLib.Value (columns));
				names.Add ("homogeneous");
				vals.Add (new GLib.Value (homogeneous));
				CreateNativeObject (names.ToArray (), vals.ToArray ());
				return;
			}
			Raw = gtk_table_new(rows, columns, homogeneous);
		}

		[GLib.Property ("n-rows")]
		public uint NRows {
			get {
				GLib.Value val = GetProperty ("n-rows");
				uint ret = (uint) val;
				val.Dispose ();
				return ret;
			}
			set {
				GLib.Value val = new GLib.Value(value);
				SetProperty("n-rows", val);
				val.Dispose ();
			}
		}

		[GLib.Property ("n-columns")]
		public uint NColumns {
			get {
				GLib.Value val = GetProperty ("n-columns");
				uint ret = (uint) val;
				val.Dispose ();
				return ret;
			}
			set {
				GLib.Value val = new GLib.Value(value);
				SetProperty("n-columns", val);
				val.Dispose ();
			}
		}

		[GLib.Property ("row-spacing")]
		public uint RowSpacing {
			get {
				GLib.Value val = GetProperty ("row-spacing");
				uint ret = (uint) val;
				val.Dispose ();
				return ret;
			}
			set {
				GLib.Value val = new GLib.Value(value);
				SetProperty("row-spacing", val);
				val.Dispose ();
			}
		}

		[GLib.Property ("column-spacing")]
		public uint ColumnSpacing {
			get {
				GLib.Value val = GetProperty ("column-spacing");
				uint ret = (uint) val;
				val.Dispose ();
				return ret;
			}
			set {
				GLib.Value val = new GLib.Value(value);
				SetProperty("column-spacing", val);
				val.Dispose ();
			}
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool gtk_table_get_homogeneous(IntPtr raw);

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gtk_table_set_homogeneous(IntPtr raw, bool homogeneous);

		[GLib.Property ("homogeneous")]
		public bool Homogeneous {
			get  {
				bool raw_ret = gtk_table_get_homogeneous(Handle);
				bool ret = raw_ret;
				return ret;
			}
			set  {
				gtk_table_set_homogeneous(Handle, value);
			}
		}

		public class TableChild : Gtk.Container.ContainerChild {
			protected internal TableChild (Gtk.Container parent, Gtk.Widget child) : base (parent, child) {}

			[Gtk.ChildProperty ("left-attach")]
			public uint LeftAttach {
				get {
					GLib.Value val = parent.ChildGetProperty (child, "left-attach");
					uint ret = (uint) val;
					val.Dispose ();
					return ret;
				}
				set {
					GLib.Value val = new GLib.Value(value);
					parent.ChildSetProperty(child, "left-attach", val);
					val.Dispose ();
				}
			}

			[Gtk.ChildProperty ("right-attach")]
			public uint RightAttach {
				get {
					GLib.Value val = parent.ChildGetProperty (child, "right-attach");
					uint ret = (uint) val;
					val.Dispose ();
					return ret;
				}
				set {
					GLib.Value val = new GLib.Value(value);
					parent.ChildSetProperty(child, "right-attach", val);
					val.Dispose ();
				}
			}

			[Gtk.ChildProperty ("top-attach")]
			public uint TopAttach {
				get {
					GLib.Value val = parent.ChildGetProperty (child, "top-attach");
					uint ret = (uint) val;
					val.Dispose ();
					return ret;
				}
				set {
					GLib.Value val = new GLib.Value(value);
					parent.ChildSetProperty(child, "top-attach", val);
					val.Dispose ();
				}
			}

			[Gtk.ChildProperty ("bottom-attach")]
			public uint BottomAttach {
				get {
					GLib.Value val = parent.ChildGetProperty (child, "bottom-attach");
					uint ret = (uint) val;
					val.Dispose ();
					return ret;
				}
				set {
					GLib.Value val = new GLib.Value(value);
					parent.ChildSetProperty(child, "bottom-attach", val);
					val.Dispose ();
				}
			}

			[Gtk.ChildProperty ("x-options")]
			public Gtk.AttachOptions XOptions {
				get {
					GLib.Value val = parent.ChildGetProperty (child, "x-options");
					Gtk.AttachOptions ret = (Gtk.AttachOptions) (Enum) val;
					val.Dispose ();
					return ret;
				}
				set {
					GLib.Value val = new GLib.Value((Enum) value);
					parent.ChildSetProperty(child, "x-options", val);
					val.Dispose ();
				}
			}

			[Gtk.ChildProperty ("y-options")]
			public Gtk.AttachOptions YOptions {
				get {
					GLib.Value val = parent.ChildGetProperty (child, "y-options");
					Gtk.AttachOptions ret = (Gtk.AttachOptions) (Enum) val;
					val.Dispose ();
					return ret;
				}
				set {
					GLib.Value val = new GLib.Value((Enum) value);
					parent.ChildSetProperty(child, "y-options", val);
					val.Dispose ();
				}
			}

			[Gtk.ChildProperty ("x-padding")]
			public uint XPadding {
				get {
					GLib.Value val = parent.ChildGetProperty (child, "x-padding");
					uint ret = (uint) val;
					val.Dispose ();
					return ret;
				}
				set {
					GLib.Value val = new GLib.Value(value);
					parent.ChildSetProperty(child, "x-padding", val);
					val.Dispose ();
				}
			}

			[Gtk.ChildProperty ("y-padding")]
			public uint YPadding {
				get {
					GLib.Value val = parent.ChildGetProperty (child, "y-padding");
					uint ret = (uint) val;
					val.Dispose ();
					return ret;
				}
				set {
					GLib.Value val = new GLib.Value(value);
					parent.ChildSetProperty(child, "y-padding", val);
					val.Dispose ();
				}
			}

		}

		public override Gtk.Container.ContainerChild this [Gtk.Widget child] {
			get {
				return new TableChild (this, child);
			}
		}

		[StructLayout (LayoutKind.Sequential)]
		struct GtkTableClass {
			IntPtr GtkReserved1;
			IntPtr GtkReserved2;
			IntPtr GtkReserved3;
			IntPtr GtkReserved4;
		}

		static uint class_offset = ((GLib.GType) typeof (Gtk.Container)).GetClassSize ();
		static Dictionary<GLib.GType, GtkTableClass> class_structs;

		static GtkTableClass GetClassStruct (GLib.GType gtype, bool use_cache)
		{
			if (class_structs == null)
				class_structs = new Dictionary<GLib.GType, GtkTableClass> ();

			if (use_cache && class_structs.ContainsKey (gtype))
				return class_structs [gtype];
			else {
				IntPtr class_ptr = new IntPtr (gtype.GetClassPtr ().ToInt64 () + class_offset);
				GtkTableClass class_struct = (GtkTableClass) Marshal.PtrToStructure (class_ptr, typeof (GtkTableClass));
				if (use_cache)
					class_structs.Add (gtype, class_struct);
				return class_struct;
			}
		}

		static void OverrideClassStruct (GLib.GType gtype, GtkTableClass class_struct)
		{
			IntPtr class_ptr = new IntPtr (gtype.GetClassPtr ().ToInt64 () + class_offset);
			Marshal.StructureToPtr (class_struct, class_ptr, false);
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gtk_table_attach(IntPtr raw, IntPtr child, uint left_attach, uint right_attach, uint top_attach, uint bottom_attach, int xoptions, int yoptions, uint xpadding, uint ypadding);

		public void Attach(Gtk.Widget child, uint left_attach, uint right_attach, uint top_attach, uint bottom_attach, Gtk.AttachOptions xoptions, Gtk.AttachOptions yoptions, uint xpadding, uint ypadding) {
			gtk_table_attach(Handle, child == null ? IntPtr.Zero : child.Handle, left_attach, right_attach, top_attach, bottom_attach, (int) xoptions, (int) yoptions, xpadding, ypadding);
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gtk_table_attach_defaults(IntPtr raw, IntPtr widget, uint left_attach, uint right_attach, uint top_attach, uint bottom_attach);

		public void Attach(Gtk.Widget widget, uint left_attach, uint right_attach, uint top_attach, uint bottom_attach) {
			gtk_table_attach_defaults(Handle, widget == null ? IntPtr.Zero : widget.Handle, left_attach, right_attach, top_attach, bottom_attach);
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern uint gtk_table_get_col_spacing(IntPtr raw, uint column);

		public uint GetColSpacing(uint column) {
			uint raw_ret = gtk_table_get_col_spacing(Handle, column);
			uint ret = raw_ret;
			return ret;
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern uint gtk_table_get_default_col_spacing(IntPtr raw);

		public uint DefaultColSpacing { 
			get {
				uint raw_ret = gtk_table_get_default_col_spacing(Handle);
				uint ret = raw_ret;
				return ret;
			}
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern uint gtk_table_get_default_row_spacing(IntPtr raw);

		public uint DefaultRowSpacing { 
			get {
				uint raw_ret = gtk_table_get_default_row_spacing(Handle);
				uint ret = raw_ret;
				return ret;
			}
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern uint gtk_table_get_row_spacing(IntPtr raw, uint row);

		public uint GetRowSpacing(uint row) {
			uint raw_ret = gtk_table_get_row_spacing(Handle, row);
			uint ret = raw_ret;
			return ret;
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gtk_table_get_size(IntPtr raw, out uint rows, out uint columns);

		public void GetSize(out uint rows, out uint columns) {
			gtk_table_get_size(Handle, out rows, out columns);
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gtk_table_get_type();

		public static new GLib.GType GType { 
			get {
				IntPtr raw_ret = gtk_table_get_type();
				GLib.GType ret = new GLib.GType(raw_ret);
				return ret;
			}
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gtk_table_resize(IntPtr raw, uint rows, uint columns);

		public void Resize(uint rows, uint columns) {
			gtk_table_resize(Handle, rows, columns);
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gtk_table_set_col_spacing(IntPtr raw, uint column, uint spacing);

		public void SetColSpacing(uint column, uint spacing) {
			gtk_table_set_col_spacing(Handle, column, spacing);
		}

		[DllImport("libgtk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gtk_table_set_row_spacing(IntPtr raw, uint row, uint spacing);

		public void SetRowSpacing(uint row, uint spacing) {
			gtk_table_set_row_spacing(Handle, row, spacing);
		}

#endregion
	}
}
