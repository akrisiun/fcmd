// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace GLib {

	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Runtime.InteropServices;

#region Autogenerated code
	public partial class SimplePermission : GLib.Permission {

		public SimplePermission (IntPtr raw) : base(raw) {}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr g_simple_permission_new(bool allowed);

		public SimplePermission (bool allowed) : base (IntPtr.Zero)
		{
			if (GetType () != typeof (SimplePermission)) {
				var vals = new List<GLib.Value> ();
				var names = new List<string> ();
				names.Add ("allowed");
				vals.Add (new GLib.Value (allowed));
				CreateNativeObject (names.ToArray (), vals.ToArray ());
				return;
			}
			Raw = g_simple_permission_new(allowed);
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr g_simple_permission_get_type();

		public static new GLib.GType GType { 
			get {
				IntPtr raw_ret = g_simple_permission_get_type();
				GLib.GType ret = new GLib.GType(raw_ret);
				return ret;
			}
		}

#endregion
	}
}