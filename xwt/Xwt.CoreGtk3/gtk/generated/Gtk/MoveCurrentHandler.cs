// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace Gtk {

	using System;

	public delegate void MoveCurrentHandler(object o, MoveCurrentArgs args);

	public class MoveCurrentArgs : GLib.SignalArgs {
		public Gtk.MenuDirectionType Direction{
			get {
				return (Gtk.MenuDirectionType) Args [0];
			}
		}

	}
}
