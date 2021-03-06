// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace GLib {

	using System;

#region Autogenerated code
	public partial interface IMount : GLib.IWrapper {

		event System.EventHandler PreUnmount;
		event System.EventHandler Changed;
		event System.EventHandler Unmounted;
		bool CanEject();
		bool CanUnmount { 
			get;
		}
		void Eject(GLib.MountUnmountFlags flags, GLib.Cancellable cancellable, GLib.AsyncReadyCallback cb);
		bool EjectFinish(GLib.IAsyncResult result);
		void EjectWithOperation(GLib.MountUnmountFlags flags, GLib.MountOperation mount_operation, GLib.Cancellable cancellable, GLib.AsyncReadyCallback cb);
		bool EjectWithOperationFinish(GLib.IAsyncResult result);
		GLib.IFile DefaultLocation { 
			get;
		}
		GLib.IDrive Drive { 
			get;
		}
		GLib.IIcon Icon { 
			get;
		}
		string Name { 
			get;
		}
		GLib.IFile Root { 
			get;
		}
		string Uuid { 
			get;
		}
		GLib.IVolume Volume { 
			get;
		}
		void GuessContentType(bool force_rescan, GLib.Cancellable cancellable, GLib.AsyncReadyCallback cb);
		string[] GuessContentTypeFinish(GLib.IAsyncResult result);
		string[] GuessContentTypeSync(bool force_rescan, GLib.Cancellable cancellable);
		bool IsShadowed { 
			get;
		}
		void Remount(GLib.MountMountFlags flags, GLib.MountOperation mount_operation, GLib.Cancellable cancellable, GLib.AsyncReadyCallback cb);
		bool RemountFinish(GLib.IAsyncResult result);
		void Shadow();
		void Unmount(GLib.MountUnmountFlags flags, GLib.Cancellable cancellable, GLib.AsyncReadyCallback cb);
		bool UnmountFinish(GLib.IAsyncResult result);
		void UnmountWithOperation(GLib.MountUnmountFlags flags, GLib.MountOperation mount_operation, GLib.Cancellable cancellable, GLib.AsyncReadyCallback cb);
		bool UnmountWithOperationFinish(GLib.IAsyncResult result);
		void Unshadow();
	}
#endregion
}
