// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace Gdk {

	using System;
	using System.Runtime.InteropServices;

#region Autogenerated code
	[Flags]
	[GLib.GType (typeof (Gdk.ModifierTypeGType))]
	public enum ModifierType {

		ShiftMask = 1 << 0,
		LockMask = 1 << 1,
		ControlMask = 1 << 2,
		Mod1Mask = 1 << 3,
		Mod2Mask = 1 << 4,
		Mod3Mask = 1 << 5,
		Mod4Mask = 1 << 6,
		Mod5Mask = 1 << 7,
		Button1Mask = 1 << 8,
		Button2Mask = 1 << 9,
		Button3Mask = 1 << 10,
		Button4Mask = 1 << 11,
		Button5Mask = 1 << 12,
		SuperMask = 1 << 26,
		HyperMask = 1 << 27,
		MetaMask = 1 << 28,
		ReleaseMask = 1 << 30,
		ModifierMask = ReleaseMask | 0x1fff,
		None = 0,
	}

	internal class ModifierTypeGType {
		[DllImport ("libgdk-3-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gdk_modifier_type_get_type ();

		public static GLib.GType GType {
			get {
				return new GLib.GType (gdk_modifier_type_get_type ());
			}
		}
	}
#endregion
}
