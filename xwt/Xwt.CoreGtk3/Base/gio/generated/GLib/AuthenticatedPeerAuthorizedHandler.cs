// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace GLib {

	using System;

	public delegate void AuthenticatedPeerAuthorizedHandler(object o, AuthenticatedPeerAuthorizedArgs args);

	public class AuthenticatedPeerAuthorizedArgs : GLib.SignalArgs {
		public GLib.IOStream Stream{
			get {
				return (GLib.IOStream) Args [0];
			}
		}

		public GLib.Credentials Credentials{
			get {
				return (GLib.Credentials) Args [1];
			}
		}

	}
}
