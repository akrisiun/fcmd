// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace Gtk {

	using System;

	public delegate void PageAddedHandler(object o, PageAddedArgs args);

	public class PageAddedArgs : GLib.SignalArgs {
		public Gtk.Widget Child{
			get {
				return (Gtk.Widget) Args [0];
			}
		}

		public uint PageNum{
			get {
				return (uint) Args [1];
			}
		}

	}
}
