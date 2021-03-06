// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace Atk {

	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Runtime.InteropServices;

#region Autogenerated code
	public partial class GObjectAccessible : Atk.Object {

		public GObjectAccessible (IntPtr raw) : base(raw) {}

		protected GObjectAccessible() : base(IntPtr.Zero)
		{
			CreateNativeObject (new string [0], new GLib.Value [0]);
		}

		[StructLayout (LayoutKind.Sequential)]
		struct AtkGObjectAccessibleClass {
			private AtkSharp.FunctionNative pad1;
			public Atk.Function Pad1 {
				get {
					return AtkSharp.FunctionWrapper.GetManagedDelegate (pad1);
				}
			}
			private AtkSharp.FunctionNative pad2;
			public Atk.Function Pad2 {
				get {
					return AtkSharp.FunctionWrapper.GetManagedDelegate (pad2);
				}
			}
		}

		static uint class_offset = ((GLib.GType) typeof (Atk.Object)).GetClassSize ();
		static Dictionary<GLib.GType, AtkGObjectAccessibleClass> class_structs;

		static AtkGObjectAccessibleClass GetClassStruct (GLib.GType gtype, bool use_cache)
		{
			if (class_structs == null)
				class_structs = new Dictionary<GLib.GType, AtkGObjectAccessibleClass> ();

			if (use_cache && class_structs.ContainsKey (gtype))
				return class_structs [gtype];
			else {
				IntPtr class_ptr = new IntPtr (gtype.GetClassPtr ().ToInt64 () + class_offset);
				AtkGObjectAccessibleClass class_struct = (AtkGObjectAccessibleClass) Marshal.PtrToStructure (class_ptr, typeof (AtkGObjectAccessibleClass));
				if (use_cache)
					class_structs.Add (gtype, class_struct);
				return class_struct;
			}
		}

		static void OverrideClassStruct (GLib.GType gtype, AtkGObjectAccessibleClass class_struct)
		{
			IntPtr class_ptr = new IntPtr (gtype.GetClassPtr ().ToInt64 () + class_offset);
			Marshal.StructureToPtr (class_struct, class_ptr, false);
		}

		[DllImport("libatk-1.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr atk_gobject_accessible_for_object(IntPtr obj);

		public static Atk.Object ForObject(GLib.Object obj) {
			IntPtr raw_ret = atk_gobject_accessible_for_object(obj == null ? IntPtr.Zero : obj.Handle);
			Atk.Object ret = GLib.Object.GetObject(raw_ret) as Atk.Object;
			return ret;
		}

		[DllImport("libatk-1.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr atk_gobject_accessible_get_object(IntPtr raw);

		public GLib.Object Object { 
			get {
				IntPtr raw_ret = atk_gobject_accessible_get_object(Handle);
				GLib.Object ret = GLib.Object.GetObject (raw_ret);
				return ret;
			}
		}

		[DllImport("libatk-1.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr atk_gobject_accessible_get_type();

		public static new GLib.GType GType { 
			get {
				IntPtr raw_ret = atk_gobject_accessible_get_type();
				GLib.GType ret = new GLib.GType(raw_ret);
				return ret;
			}
		}

#endregion
	}
}
