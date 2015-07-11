// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace GLib {

	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Runtime.InteropServices;

#region Autogenerated code
	public partial class OutputStream : GLib.Object {

		public OutputStream (IntPtr raw) : base(raw) {}

		protected OutputStream() : base(IntPtr.Zero)
		{
			CreateNativeObject (new string [0], new GLib.Value [0]);
		}

		static WriteFnNativeDelegate WriteFn_cb_delegate;
		static WriteFnNativeDelegate WriteFnVMCallback {
			get {
				if (WriteFn_cb_delegate == null)
					WriteFn_cb_delegate = new WriteFnNativeDelegate (WriteFn_cb);
				return WriteFn_cb_delegate;
			}
		}

		static void OverrideWriteFn (GLib.GType gtype)
		{
			OverrideWriteFn (gtype, WriteFnVMCallback);
		}

		static void OverrideWriteFn (GLib.GType gtype, WriteFnNativeDelegate callback)
		{
			GOutputStreamClass class_iface = GetClassStruct (gtype, false);
			class_iface.WriteFn = callback;
			OverrideClassStruct (gtype, class_iface);
		}

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate IntPtr WriteFnNativeDelegate (IntPtr inst, IntPtr buffer, UIntPtr count, IntPtr cancellable, out IntPtr error);

		static IntPtr WriteFn_cb (IntPtr inst, IntPtr buffer, UIntPtr count, IntPtr cancellable, out IntPtr error)
		{
			error = IntPtr.Zero;

			try {
				OutputStream __obj = GLib.Object.GetObject (inst, false) as OutputStream;
				long __result;
				__result = __obj.OnWriteFn (buffer, (ulong) count, GLib.Object.GetObject(cancellable) as GLib.Cancellable);
				return new IntPtr (__result);
			} catch (Exception e) {
				GLib.ExceptionManager.RaiseUnhandledException (e, true);
				// NOTREACHED: above call does not return.
				throw e;
			}
		}

		[GLib.DefaultSignalHandler(Type=typeof(GLib.OutputStream), ConnectionMethod="OverrideWriteFn")]
		protected virtual long OnWriteFn (IntPtr buffer, ulong count, GLib.Cancellable cancellable)
		{
			return InternalWriteFn (buffer, count, cancellable);
		}

		private long InternalWriteFn (IntPtr buffer, ulong count, GLib.Cancellable cancellable)
		{
			WriteFnNativeDelegate unmanaged = GetClassStruct (this.LookupGType ().GetThresholdType (), true).WriteFn;
			if (unmanaged == null) throw new InvalidOperationException ("No base method to invoke");

			IntPtr error = IntPtr.Zero;
			IntPtr __result = unmanaged (this.Handle, buffer, new UIntPtr (count), cancellable == null ? IntPtr.Zero : cancellable.Handle, out error);
			return (long) __result;
		}

		static SpliceNativeDelegate Splice_cb_delegate;
		static SpliceNativeDelegate SpliceVMCallback {
			get {
				if (Splice_cb_delegate == null)
					Splice_cb_delegate = new SpliceNativeDelegate (Splice_cb);
				return Splice_cb_delegate;
			}
		}

		static void OverrideSplice (GLib.GType gtype)
		{
			OverrideSplice (gtype, SpliceVMCallback);
		}

		static void OverrideSplice (GLib.GType gtype, SpliceNativeDelegate callback)
		{
			GOutputStreamClass class_iface = GetClassStruct (gtype, false);
			class_iface.Splice = callback;
			OverrideClassStruct (gtype, class_iface);
		}

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate IntPtr SpliceNativeDelegate (IntPtr inst, IntPtr source, int flags, IntPtr cancellable, out IntPtr error);

		static IntPtr Splice_cb (IntPtr inst, IntPtr source, int flags, IntPtr cancellable, out IntPtr error)
		{
			error = IntPtr.Zero;

			try {
				OutputStream __obj = GLib.Object.GetObject (inst, false) as OutputStream;
				long __result;
				__result = __obj.OnSplice (GLib.Object.GetObject(source) as GLib.InputStream, (GLib.OutputStreamSpliceFlags) flags, GLib.Object.GetObject(cancellable) as GLib.Cancellable);
				return new IntPtr (__result);
			} catch (Exception e) {
				GLib.ExceptionManager.RaiseUnhandledException (e, true);
				// NOTREACHED: above call does not return.
				throw e;
			}
		}

		[GLib.DefaultSignalHandler(Type=typeof(GLib.OutputStream), ConnectionMethod="OverrideSplice")]
		protected virtual long OnSplice (GLib.InputStream source, GLib.OutputStreamSpliceFlags flags, GLib.Cancellable cancellable)
		{
			return InternalSplice (source, flags, cancellable);
		}

		private long InternalSplice (GLib.InputStream source, GLib.OutputStreamSpliceFlags flags, GLib.Cancellable cancellable)
		{
			SpliceNativeDelegate unmanaged = GetClassStruct (this.LookupGType ().GetThresholdType (), true).Splice;
			if (unmanaged == null) throw new InvalidOperationException ("No base method to invoke");

			IntPtr error = IntPtr.Zero;
			IntPtr __result = unmanaged (this.Handle, source == null ? IntPtr.Zero : source.Handle, (int) flags, cancellable == null ? IntPtr.Zero : cancellable.Handle, out error);
			return (long) __result;
		}

		static FlushNativeDelegate Flush_cb_delegate;
		static FlushNativeDelegate FlushVMCallback {
			get {
				if (Flush_cb_delegate == null)
					Flush_cb_delegate = new FlushNativeDelegate (Flush_cb);
				return Flush_cb_delegate;
			}
		}

		static void OverrideFlush (GLib.GType gtype)
		{
			OverrideFlush (gtype, FlushVMCallback);
		}

		static void OverrideFlush (GLib.GType gtype, FlushNativeDelegate callback)
		{
			GOutputStreamClass class_iface = GetClassStruct (gtype, false);
			class_iface.Flush = callback;
			OverrideClassStruct (gtype, class_iface);
		}

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate bool FlushNativeDelegate (IntPtr inst, IntPtr cancellable, out IntPtr error);

		static bool Flush_cb (IntPtr inst, IntPtr cancellable, out IntPtr error)
		{
			error = IntPtr.Zero;

			try {
				OutputStream __obj = GLib.Object.GetObject (inst, false) as OutputStream;
				bool __result;
				__result = __obj.OnFlush (GLib.Object.GetObject(cancellable) as GLib.Cancellable);
				return __result;
			} catch (Exception e) {
				GLib.ExceptionManager.RaiseUnhandledException (e, true);
				// NOTREACHED: above call does not return.
				throw e;
			}
		}

		[GLib.DefaultSignalHandler(Type=typeof(GLib.OutputStream), ConnectionMethod="OverrideFlush")]
		protected virtual bool OnFlush (GLib.Cancellable cancellable)
		{
			return InternalFlush (cancellable);
		}

		private bool InternalFlush (GLib.Cancellable cancellable)
		{
			FlushNativeDelegate unmanaged = GetClassStruct (this.LookupGType ().GetThresholdType (), true).Flush;
			if (unmanaged == null) throw new InvalidOperationException ("No base method to invoke");

			IntPtr error = IntPtr.Zero;
			bool __result = unmanaged (this.Handle, cancellable == null ? IntPtr.Zero : cancellable.Handle, out error);
			return __result;
		}

		static CloseFnNativeDelegate CloseFn_cb_delegate;
		static CloseFnNativeDelegate CloseFnVMCallback {
			get {
				if (CloseFn_cb_delegate == null)
					CloseFn_cb_delegate = new CloseFnNativeDelegate (CloseFn_cb);
				return CloseFn_cb_delegate;
			}
		}

		static void OverrideCloseFn (GLib.GType gtype)
		{
			OverrideCloseFn (gtype, CloseFnVMCallback);
		}

		static void OverrideCloseFn (GLib.GType gtype, CloseFnNativeDelegate callback)
		{
			GOutputStreamClass class_iface = GetClassStruct (gtype, false);
			class_iface.CloseFn = callback;
			OverrideClassStruct (gtype, class_iface);
		}

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate bool CloseFnNativeDelegate (IntPtr inst, IntPtr cancellable, out IntPtr error);

		static bool CloseFn_cb (IntPtr inst, IntPtr cancellable, out IntPtr error)
		{
			error = IntPtr.Zero;

			try {
				OutputStream __obj = GLib.Object.GetObject (inst, false) as OutputStream;
				bool __result;
				__result = __obj.OnCloseFn (GLib.Object.GetObject(cancellable) as GLib.Cancellable);
				return __result;
			} catch (Exception e) {
				GLib.ExceptionManager.RaiseUnhandledException (e, true);
				// NOTREACHED: above call does not return.
				throw e;
			}
		}

		[GLib.DefaultSignalHandler(Type=typeof(GLib.OutputStream), ConnectionMethod="OverrideCloseFn")]
		protected virtual bool OnCloseFn (GLib.Cancellable cancellable)
		{
			return InternalCloseFn (cancellable);
		}

		private bool InternalCloseFn (GLib.Cancellable cancellable)
		{
			CloseFnNativeDelegate unmanaged = GetClassStruct (this.LookupGType ().GetThresholdType (), true).CloseFn;
			if (unmanaged == null) throw new InvalidOperationException ("No base method to invoke");

			IntPtr error = IntPtr.Zero;
			bool __result = unmanaged (this.Handle, cancellable == null ? IntPtr.Zero : cancellable.Handle, out error);
			return __result;
		}

		static WriteAsyncNativeDelegate WriteAsync_cb_delegate;
		static WriteAsyncNativeDelegate WriteAsyncVMCallback {
			get {
				if (WriteAsync_cb_delegate == null)
					WriteAsync_cb_delegate = new WriteAsyncNativeDelegate (WriteAsync_cb);
				return WriteAsync_cb_delegate;
			}
		}

		static void OverrideWriteAsync (GLib.GType gtype)
		{
			OverrideWriteAsync (gtype, WriteAsyncVMCallback);
		}

		static void OverrideWriteAsync (GLib.GType gtype, WriteAsyncNativeDelegate callback)
		{
			GOutputStreamClass class_iface = GetClassStruct (gtype, false);
			class_iface.WriteAsync = callback;
			OverrideClassStruct (gtype, class_iface);
		}

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate void WriteAsyncNativeDelegate (IntPtr inst, IntPtr buffer, UIntPtr count, int io_priority, IntPtr cancellable, GLibSharp.AsyncReadyCallbackNative cb, IntPtr user_data);

		static void WriteAsync_cb (IntPtr inst, IntPtr buffer, UIntPtr count, int io_priority, IntPtr cancellable, GLibSharp.AsyncReadyCallbackNative cb, IntPtr user_data)
		{
			try {
				OutputStream __obj = GLib.Object.GetObject (inst, false) as OutputStream;
				GLibSharp.AsyncReadyCallbackInvoker cb_invoker = new GLibSharp.AsyncReadyCallbackInvoker (cb, user_data);
				__obj.OnWriteAsync (buffer, (ulong) count, io_priority, GLib.Object.GetObject(cancellable) as GLib.Cancellable, cb_invoker.Handler);
			} catch (Exception e) {
				GLib.ExceptionManager.RaiseUnhandledException (e, false);
			}
		}

		[GLib.DefaultSignalHandler(Type=typeof(GLib.OutputStream), ConnectionMethod="OverrideWriteAsync")]
		protected virtual void OnWriteAsync (IntPtr buffer, ulong count, int io_priority, GLib.Cancellable cancellable, GLib.AsyncReadyCallback cb)
		{
			InternalWriteAsync (buffer, count, io_priority, cancellable, cb);
		}

		private void InternalWriteAsync (IntPtr buffer, ulong count, int io_priority, GLib.Cancellable cancellable, GLib.AsyncReadyCallback cb)
		{
			WriteAsyncNativeDelegate unmanaged = GetClassStruct (this.LookupGType ().GetThresholdType (), true).WriteAsync;
			if (unmanaged == null) return;

			GLibSharp.AsyncReadyCallbackWrapper cb_wrapper = new GLibSharp.AsyncReadyCallbackWrapper (cb);
			cb_wrapper.PersistUntilCalled ();
			unmanaged (this.Handle, buffer, new UIntPtr (count), io_priority, cancellable == null ? IntPtr.Zero : cancellable.Handle, cb_wrapper.NativeDelegate, IntPtr.Zero);
		}

		static WriteFinishNativeDelegate WriteFinish_cb_delegate;
		static WriteFinishNativeDelegate WriteFinishVMCallback {
			get {
				if (WriteFinish_cb_delegate == null)
					WriteFinish_cb_delegate = new WriteFinishNativeDelegate (WriteFinish_cb);
				return WriteFinish_cb_delegate;
			}
		}

		static void OverrideWriteFinish (GLib.GType gtype)
		{
			OverrideWriteFinish (gtype, WriteFinishVMCallback);
		}

		static void OverrideWriteFinish (GLib.GType gtype, WriteFinishNativeDelegate callback)
		{
			GOutputStreamClass class_iface = GetClassStruct (gtype, false);
			class_iface.WriteFinish = callback;
			OverrideClassStruct (gtype, class_iface);
		}

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate IntPtr WriteFinishNativeDelegate (IntPtr inst, IntPtr result, out IntPtr error);

		static IntPtr WriteFinish_cb (IntPtr inst, IntPtr result, out IntPtr error)
		{
			error = IntPtr.Zero;

			try {
				OutputStream __obj = GLib.Object.GetObject (inst, false) as OutputStream;
				long __result;
				__result = __obj.OnWriteFinish (GLib.AsyncResultAdapter.GetObject (result, false));
				return new IntPtr (__result);
			} catch (Exception e) {
				GLib.ExceptionManager.RaiseUnhandledException (e, true);
				// NOTREACHED: above call does not return.
				throw e;
			}
		}

		[GLib.DefaultSignalHandler(Type=typeof(GLib.OutputStream), ConnectionMethod="OverrideWriteFinish")]
		protected virtual long OnWriteFinish (GLib.IAsyncResult result)
		{
			return InternalWriteFinish (result);
		}

		private long InternalWriteFinish (GLib.IAsyncResult result)
		{
			WriteFinishNativeDelegate unmanaged = GetClassStruct (this.LookupGType ().GetThresholdType (), true).WriteFinish;
			if (unmanaged == null) throw new InvalidOperationException ("No base method to invoke");

			IntPtr error = IntPtr.Zero;
			IntPtr __result = unmanaged (this.Handle, result == null ? IntPtr.Zero : ((result is GLib.Object) ? (result as GLib.Object).Handle : (result as GLib.AsyncResultAdapter).Handle), out error);
			return (long) __result;
		}

		static SpliceAsyncNativeDelegate SpliceAsync_cb_delegate;
		static SpliceAsyncNativeDelegate SpliceAsyncVMCallback {
			get {
				if (SpliceAsync_cb_delegate == null)
					SpliceAsync_cb_delegate = new SpliceAsyncNativeDelegate (SpliceAsync_cb);
				return SpliceAsync_cb_delegate;
			}
		}

		static void OverrideSpliceAsync (GLib.GType gtype)
		{
			OverrideSpliceAsync (gtype, SpliceAsyncVMCallback);
		}

		static void OverrideSpliceAsync (GLib.GType gtype, SpliceAsyncNativeDelegate callback)
		{
			GOutputStreamClass class_iface = GetClassStruct (gtype, false);
			class_iface.SpliceAsync = callback;
			OverrideClassStruct (gtype, class_iface);
		}

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate void SpliceAsyncNativeDelegate (IntPtr inst, IntPtr source, int flags, int io_priority, IntPtr cancellable, GLibSharp.AsyncReadyCallbackNative cb, IntPtr user_data);

		static void SpliceAsync_cb (IntPtr inst, IntPtr source, int flags, int io_priority, IntPtr cancellable, GLibSharp.AsyncReadyCallbackNative cb, IntPtr user_data)
		{
			try {
				OutputStream __obj = GLib.Object.GetObject (inst, false) as OutputStream;
				GLibSharp.AsyncReadyCallbackInvoker cb_invoker = new GLibSharp.AsyncReadyCallbackInvoker (cb, user_data);
				__obj.OnSpliceAsync (GLib.Object.GetObject(source) as GLib.InputStream, (GLib.OutputStreamSpliceFlags) flags, io_priority, GLib.Object.GetObject(cancellable) as GLib.Cancellable, cb_invoker.Handler);
			} catch (Exception e) {
				GLib.ExceptionManager.RaiseUnhandledException (e, false);
			}
		}

		[GLib.DefaultSignalHandler(Type=typeof(GLib.OutputStream), ConnectionMethod="OverrideSpliceAsync")]
		protected virtual void OnSpliceAsync (GLib.InputStream source, GLib.OutputStreamSpliceFlags flags, int io_priority, GLib.Cancellable cancellable, GLib.AsyncReadyCallback cb)
		{
			InternalSpliceAsync (source, flags, io_priority, cancellable, cb);
		}

		private void InternalSpliceAsync (GLib.InputStream source, GLib.OutputStreamSpliceFlags flags, int io_priority, GLib.Cancellable cancellable, GLib.AsyncReadyCallback cb)
		{
			SpliceAsyncNativeDelegate unmanaged = GetClassStruct (this.LookupGType ().GetThresholdType (), true).SpliceAsync;
			if (unmanaged == null) return;

			GLibSharp.AsyncReadyCallbackWrapper cb_wrapper = new GLibSharp.AsyncReadyCallbackWrapper (cb);
			cb_wrapper.PersistUntilCalled ();
			unmanaged (this.Handle, source == null ? IntPtr.Zero : source.Handle, (int) flags, io_priority, cancellable == null ? IntPtr.Zero : cancellable.Handle, cb_wrapper.NativeDelegate, IntPtr.Zero);
		}

		static SpliceFinishNativeDelegate SpliceFinish_cb_delegate;
		static SpliceFinishNativeDelegate SpliceFinishVMCallback {
			get {
				if (SpliceFinish_cb_delegate == null)
					SpliceFinish_cb_delegate = new SpliceFinishNativeDelegate (SpliceFinish_cb);
				return SpliceFinish_cb_delegate;
			}
		}

		static void OverrideSpliceFinish (GLib.GType gtype)
		{
			OverrideSpliceFinish (gtype, SpliceFinishVMCallback);
		}

		static void OverrideSpliceFinish (GLib.GType gtype, SpliceFinishNativeDelegate callback)
		{
			GOutputStreamClass class_iface = GetClassStruct (gtype, false);
			class_iface.SpliceFinish = callback;
			OverrideClassStruct (gtype, class_iface);
		}

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate IntPtr SpliceFinishNativeDelegate (IntPtr inst, IntPtr result, out IntPtr error);

		static IntPtr SpliceFinish_cb (IntPtr inst, IntPtr result, out IntPtr error)
		{
			error = IntPtr.Zero;

			try {
				OutputStream __obj = GLib.Object.GetObject (inst, false) as OutputStream;
				long __result;
				__result = __obj.OnSpliceFinish (GLib.AsyncResultAdapter.GetObject (result, false));
				return new IntPtr (__result);
			} catch (Exception e) {
				GLib.ExceptionManager.RaiseUnhandledException (e, true);
				// NOTREACHED: above call does not return.
				throw e;
			}
		}

		[GLib.DefaultSignalHandler(Type=typeof(GLib.OutputStream), ConnectionMethod="OverrideSpliceFinish")]
		protected virtual long OnSpliceFinish (GLib.IAsyncResult result)
		{
			return InternalSpliceFinish (result);
		}

		private long InternalSpliceFinish (GLib.IAsyncResult result)
		{
			SpliceFinishNativeDelegate unmanaged = GetClassStruct (this.LookupGType ().GetThresholdType (), true).SpliceFinish;
			if (unmanaged == null) throw new InvalidOperationException ("No base method to invoke");

			IntPtr error = IntPtr.Zero;
			IntPtr __result = unmanaged (this.Handle, result == null ? IntPtr.Zero : ((result is GLib.Object) ? (result as GLib.Object).Handle : (result as GLib.AsyncResultAdapter).Handle), out error);
			return (long) __result;
		}

		static FlushAsyncNativeDelegate FlushAsync_cb_delegate;
		static FlushAsyncNativeDelegate FlushAsyncVMCallback {
			get {
				if (FlushAsync_cb_delegate == null)
					FlushAsync_cb_delegate = new FlushAsyncNativeDelegate (FlushAsync_cb);
				return FlushAsync_cb_delegate;
			}
		}

		static void OverrideFlushAsync (GLib.GType gtype)
		{
			OverrideFlushAsync (gtype, FlushAsyncVMCallback);
		}

		static void OverrideFlushAsync (GLib.GType gtype, FlushAsyncNativeDelegate callback)
		{
			GOutputStreamClass class_iface = GetClassStruct (gtype, false);
			class_iface.FlushAsync = callback;
			OverrideClassStruct (gtype, class_iface);
		}

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate void FlushAsyncNativeDelegate (IntPtr inst, int io_priority, IntPtr cancellable, GLibSharp.AsyncReadyCallbackNative cb, IntPtr user_data);

		static void FlushAsync_cb (IntPtr inst, int io_priority, IntPtr cancellable, GLibSharp.AsyncReadyCallbackNative cb, IntPtr user_data)
		{
			try {
				OutputStream __obj = GLib.Object.GetObject (inst, false) as OutputStream;
				GLibSharp.AsyncReadyCallbackInvoker cb_invoker = new GLibSharp.AsyncReadyCallbackInvoker (cb, user_data);
				__obj.OnFlushAsync (io_priority, GLib.Object.GetObject(cancellable) as GLib.Cancellable, cb_invoker.Handler);
			} catch (Exception e) {
				GLib.ExceptionManager.RaiseUnhandledException (e, false);
			}
		}

		[GLib.DefaultSignalHandler(Type=typeof(GLib.OutputStream), ConnectionMethod="OverrideFlushAsync")]
		protected virtual void OnFlushAsync (int io_priority, GLib.Cancellable cancellable, GLib.AsyncReadyCallback cb)
		{
			InternalFlushAsync (io_priority, cancellable, cb);
		}

		private void InternalFlushAsync (int io_priority, GLib.Cancellable cancellable, GLib.AsyncReadyCallback cb)
		{
			FlushAsyncNativeDelegate unmanaged = GetClassStruct (this.LookupGType ().GetThresholdType (), true).FlushAsync;
			if (unmanaged == null) return;

			GLibSharp.AsyncReadyCallbackWrapper cb_wrapper = new GLibSharp.AsyncReadyCallbackWrapper (cb);
			cb_wrapper.PersistUntilCalled ();
			unmanaged (this.Handle, io_priority, cancellable == null ? IntPtr.Zero : cancellable.Handle, cb_wrapper.NativeDelegate, IntPtr.Zero);
		}

		static FlushFinishNativeDelegate FlushFinish_cb_delegate;
		static FlushFinishNativeDelegate FlushFinishVMCallback {
			get {
				if (FlushFinish_cb_delegate == null)
					FlushFinish_cb_delegate = new FlushFinishNativeDelegate (FlushFinish_cb);
				return FlushFinish_cb_delegate;
			}
		}

		static void OverrideFlushFinish (GLib.GType gtype)
		{
			OverrideFlushFinish (gtype, FlushFinishVMCallback);
		}

		static void OverrideFlushFinish (GLib.GType gtype, FlushFinishNativeDelegate callback)
		{
			GOutputStreamClass class_iface = GetClassStruct (gtype, false);
			class_iface.FlushFinish = callback;
			OverrideClassStruct (gtype, class_iface);
		}

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate bool FlushFinishNativeDelegate (IntPtr inst, IntPtr result, out IntPtr error);

		static bool FlushFinish_cb (IntPtr inst, IntPtr result, out IntPtr error)
		{
			error = IntPtr.Zero;

			try {
				OutputStream __obj = GLib.Object.GetObject (inst, false) as OutputStream;
				bool __result;
				__result = __obj.OnFlushFinish (GLib.AsyncResultAdapter.GetObject (result, false));
				return __result;
			} catch (Exception e) {
				GLib.ExceptionManager.RaiseUnhandledException (e, true);
				// NOTREACHED: above call does not return.
				throw e;
			}
		}

		[GLib.DefaultSignalHandler(Type=typeof(GLib.OutputStream), ConnectionMethod="OverrideFlushFinish")]
		protected virtual bool OnFlushFinish (GLib.IAsyncResult result)
		{
			return InternalFlushFinish (result);
		}

		private bool InternalFlushFinish (GLib.IAsyncResult result)
		{
			FlushFinishNativeDelegate unmanaged = GetClassStruct (this.LookupGType ().GetThresholdType (), true).FlushFinish;
			if (unmanaged == null) throw new InvalidOperationException ("No base method to invoke");

			IntPtr error = IntPtr.Zero;
			bool __result = unmanaged (this.Handle, result == null ? IntPtr.Zero : ((result is GLib.Object) ? (result as GLib.Object).Handle : (result as GLib.AsyncResultAdapter).Handle), out error);
			return __result;
		}

		static CloseAsyncNativeDelegate CloseAsync_cb_delegate;
		static CloseAsyncNativeDelegate CloseAsyncVMCallback {
			get {
				if (CloseAsync_cb_delegate == null)
					CloseAsync_cb_delegate = new CloseAsyncNativeDelegate (CloseAsync_cb);
				return CloseAsync_cb_delegate;
			}
		}

		static void OverrideCloseAsync (GLib.GType gtype)
		{
			OverrideCloseAsync (gtype, CloseAsyncVMCallback);
		}

		static void OverrideCloseAsync (GLib.GType gtype, CloseAsyncNativeDelegate callback)
		{
			GOutputStreamClass class_iface = GetClassStruct (gtype, false);
			class_iface.CloseAsync = callback;
			OverrideClassStruct (gtype, class_iface);
		}

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate void CloseAsyncNativeDelegate (IntPtr inst, int io_priority, IntPtr cancellable, GLibSharp.AsyncReadyCallbackNative cb, IntPtr user_data);

		static void CloseAsync_cb (IntPtr inst, int io_priority, IntPtr cancellable, GLibSharp.AsyncReadyCallbackNative cb, IntPtr user_data)
		{
			try {
				OutputStream __obj = GLib.Object.GetObject (inst, false) as OutputStream;
				GLibSharp.AsyncReadyCallbackInvoker cb_invoker = new GLibSharp.AsyncReadyCallbackInvoker (cb, user_data);
				__obj.OnCloseAsync (io_priority, GLib.Object.GetObject(cancellable) as GLib.Cancellable, cb_invoker.Handler);
			} catch (Exception e) {
				GLib.ExceptionManager.RaiseUnhandledException (e, false);
			}
		}

		[GLib.DefaultSignalHandler(Type=typeof(GLib.OutputStream), ConnectionMethod="OverrideCloseAsync")]
		protected virtual void OnCloseAsync (int io_priority, GLib.Cancellable cancellable, GLib.AsyncReadyCallback cb)
		{
			InternalCloseAsync (io_priority, cancellable, cb);
		}

		private void InternalCloseAsync (int io_priority, GLib.Cancellable cancellable, GLib.AsyncReadyCallback cb)
		{
			CloseAsyncNativeDelegate unmanaged = GetClassStruct (this.LookupGType ().GetThresholdType (), true).CloseAsync;
			if (unmanaged == null) return;

			GLibSharp.AsyncReadyCallbackWrapper cb_wrapper = new GLibSharp.AsyncReadyCallbackWrapper (cb);
			cb_wrapper.PersistUntilCalled ();
			unmanaged (this.Handle, io_priority, cancellable == null ? IntPtr.Zero : cancellable.Handle, cb_wrapper.NativeDelegate, IntPtr.Zero);
		}

		static CloseFinishNativeDelegate CloseFinish_cb_delegate;
		static CloseFinishNativeDelegate CloseFinishVMCallback {
			get {
				if (CloseFinish_cb_delegate == null)
					CloseFinish_cb_delegate = new CloseFinishNativeDelegate (CloseFinish_cb);
				return CloseFinish_cb_delegate;
			}
		}

		static void OverrideCloseFinish (GLib.GType gtype)
		{
			OverrideCloseFinish (gtype, CloseFinishVMCallback);
		}

		static void OverrideCloseFinish (GLib.GType gtype, CloseFinishNativeDelegate callback)
		{
			GOutputStreamClass class_iface = GetClassStruct (gtype, false);
			class_iface.CloseFinish = callback;
			OverrideClassStruct (gtype, class_iface);
		}

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate bool CloseFinishNativeDelegate (IntPtr inst, IntPtr result, out IntPtr error);

		static bool CloseFinish_cb (IntPtr inst, IntPtr result, out IntPtr error)
		{
			error = IntPtr.Zero;

			try {
				OutputStream __obj = GLib.Object.GetObject (inst, false) as OutputStream;
				bool __result;
				__result = __obj.OnCloseFinish (GLib.AsyncResultAdapter.GetObject (result, false));
				return __result;
			} catch (Exception e) {
				GLib.ExceptionManager.RaiseUnhandledException (e, true);
				// NOTREACHED: above call does not return.
				throw e;
			}
		}

		[GLib.DefaultSignalHandler(Type=typeof(GLib.OutputStream), ConnectionMethod="OverrideCloseFinish")]
		protected virtual bool OnCloseFinish (GLib.IAsyncResult result)
		{
			return InternalCloseFinish (result);
		}

		private bool InternalCloseFinish (GLib.IAsyncResult result)
		{
			CloseFinishNativeDelegate unmanaged = GetClassStruct (this.LookupGType ().GetThresholdType (), true).CloseFinish;
			if (unmanaged == null) throw new InvalidOperationException ("No base method to invoke");

			IntPtr error = IntPtr.Zero;
			bool __result = unmanaged (this.Handle, result == null ? IntPtr.Zero : ((result is GLib.Object) ? (result as GLib.Object).Handle : (result as GLib.AsyncResultAdapter).Handle), out error);
			return __result;
		}

		[StructLayout (LayoutKind.Sequential)]
		struct GOutputStreamClass {
			public WriteFnNativeDelegate WriteFn;
			public SpliceNativeDelegate Splice;
			public FlushNativeDelegate Flush;
			public CloseFnNativeDelegate CloseFn;
			public WriteAsyncNativeDelegate WriteAsync;
			public WriteFinishNativeDelegate WriteFinish;
			public SpliceAsyncNativeDelegate SpliceAsync;
			public SpliceFinishNativeDelegate SpliceFinish;
			public FlushAsyncNativeDelegate FlushAsync;
			public FlushFinishNativeDelegate FlushFinish;
			public CloseAsyncNativeDelegate CloseAsync;
			public CloseFinishNativeDelegate CloseFinish;
			IntPtr GReserved1;
			IntPtr GReserved2;
			IntPtr GReserved3;
			IntPtr GReserved4;
			IntPtr GReserved5;
			IntPtr GReserved6;
			IntPtr GReserved7;
			IntPtr GReserved8;
		}

		static uint class_offset = ((GLib.GType) typeof (GLib.Object)).GetClassSize ();
		static Dictionary<GLib.GType, GOutputStreamClass> class_structs;

		static GOutputStreamClass GetClassStruct (GLib.GType gtype, bool use_cache)
		{
			if (class_structs == null)
				class_structs = new Dictionary<GLib.GType, GOutputStreamClass> ();

			if (use_cache && class_structs.ContainsKey (gtype))
				return class_structs [gtype];
			else {
				IntPtr class_ptr = new IntPtr (gtype.GetClassPtr ().ToInt64 () + class_offset);
				GOutputStreamClass class_struct = (GOutputStreamClass) Marshal.PtrToStructure (class_ptr, typeof (GOutputStreamClass));
				if (use_cache)
					class_structs.Add (gtype, class_struct);
				return class_struct;
			}
		}

		static void OverrideClassStruct (GLib.GType gtype, GOutputStreamClass class_struct)
		{
			IntPtr class_ptr = new IntPtr (gtype.GetClassPtr ().ToInt64 () + class_offset);
			Marshal.StructureToPtr (class_struct, class_ptr, false);
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void g_output_stream_clear_pending(IntPtr raw);

		public void ClearPending() {
			g_output_stream_clear_pending(Handle);
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern unsafe bool g_output_stream_close(IntPtr raw, IntPtr cancellable, out IntPtr error);

		public unsafe bool Close(GLib.Cancellable cancellable) {
			IntPtr error = IntPtr.Zero;
			bool raw_ret = g_output_stream_close(Handle, cancellable == null ? IntPtr.Zero : cancellable.Handle, out error);
			bool ret = raw_ret;
			if (error != IntPtr.Zero) throw new GLib.GException (error);
			return ret;
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void g_output_stream_close_async(IntPtr raw, int io_priority, IntPtr cancellable, GLibSharp.AsyncReadyCallbackNative cb, IntPtr user_data);

		public void CloseAsync(int io_priority, GLib.Cancellable cancellable, GLib.AsyncReadyCallback cb) {
			GLibSharp.AsyncReadyCallbackWrapper cb_wrapper = new GLibSharp.AsyncReadyCallbackWrapper (cb);
			cb_wrapper.PersistUntilCalled ();
			g_output_stream_close_async(Handle, io_priority, cancellable == null ? IntPtr.Zero : cancellable.Handle, cb_wrapper.NativeDelegate, IntPtr.Zero);
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern unsafe bool g_output_stream_close_finish(IntPtr raw, IntPtr result, out IntPtr error);

		public unsafe bool CloseFinish(GLib.IAsyncResult result) {
			IntPtr error = IntPtr.Zero;
			bool raw_ret = g_output_stream_close_finish(Handle, result == null ? IntPtr.Zero : ((result is GLib.Object) ? (result as GLib.Object).Handle : (result as GLib.AsyncResultAdapter).Handle), out error);
			bool ret = raw_ret;
			if (error != IntPtr.Zero) throw new GLib.GException (error);
			return ret;
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern unsafe bool g_output_stream_flush(IntPtr raw, IntPtr cancellable, out IntPtr error);

		public unsafe bool Flush(GLib.Cancellable cancellable) {
			IntPtr error = IntPtr.Zero;
			bool raw_ret = g_output_stream_flush(Handle, cancellable == null ? IntPtr.Zero : cancellable.Handle, out error);
			bool ret = raw_ret;
			if (error != IntPtr.Zero) throw new GLib.GException (error);
			return ret;
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void g_output_stream_flush_async(IntPtr raw, int io_priority, IntPtr cancellable, GLibSharp.AsyncReadyCallbackNative cb, IntPtr user_data);

		public void FlushAsync(int io_priority, GLib.Cancellable cancellable, GLib.AsyncReadyCallback cb) {
			GLibSharp.AsyncReadyCallbackWrapper cb_wrapper = new GLibSharp.AsyncReadyCallbackWrapper (cb);
			cb_wrapper.PersistUntilCalled ();
			g_output_stream_flush_async(Handle, io_priority, cancellable == null ? IntPtr.Zero : cancellable.Handle, cb_wrapper.NativeDelegate, IntPtr.Zero);
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern unsafe bool g_output_stream_flush_finish(IntPtr raw, IntPtr result, out IntPtr error);

		public unsafe bool FlushFinish(GLib.IAsyncResult result) {
			IntPtr error = IntPtr.Zero;
			bool raw_ret = g_output_stream_flush_finish(Handle, result == null ? IntPtr.Zero : ((result is GLib.Object) ? (result as GLib.Object).Handle : (result as GLib.AsyncResultAdapter).Handle), out error);
			bool ret = raw_ret;
			if (error != IntPtr.Zero) throw new GLib.GException (error);
			return ret;
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr g_output_stream_get_type();

		public static new GLib.GType GType { 
			get {
				IntPtr raw_ret = g_output_stream_get_type();
				GLib.GType ret = new GLib.GType(raw_ret);
				return ret;
			}
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool g_output_stream_has_pending(IntPtr raw);

		public bool HasPending { 
			get {
				bool raw_ret = g_output_stream_has_pending(Handle);
				bool ret = raw_ret;
				return ret;
			}
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool g_output_stream_is_closed(IntPtr raw);

		public bool IsClosed { 
			get {
				bool raw_ret = g_output_stream_is_closed(Handle);
				bool ret = raw_ret;
				return ret;
			}
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool g_output_stream_is_closing(IntPtr raw);

		public bool IsClosing { 
			get {
				bool raw_ret = g_output_stream_is_closing(Handle);
				bool ret = raw_ret;
				return ret;
			}
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern unsafe bool g_output_stream_set_pending(IntPtr raw, out IntPtr error);

		public unsafe bool SetPending() {
			IntPtr error = IntPtr.Zero;
			bool raw_ret = g_output_stream_set_pending(Handle, out error);
			bool ret = raw_ret;
			if (error != IntPtr.Zero) throw new GLib.GException (error);
			return ret;
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern unsafe IntPtr g_output_stream_splice(IntPtr raw, IntPtr source, int flags, IntPtr cancellable, out IntPtr error);

		public unsafe long Splice(GLib.InputStream source, GLib.OutputStreamSpliceFlags flags, GLib.Cancellable cancellable) {
			IntPtr error = IntPtr.Zero;
			IntPtr raw_ret = g_output_stream_splice(Handle, source == null ? IntPtr.Zero : source.Handle, (int) flags, cancellable == null ? IntPtr.Zero : cancellable.Handle, out error);
			long ret = (long) raw_ret;
			if (error != IntPtr.Zero) throw new GLib.GException (error);
			return ret;
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void g_output_stream_splice_async(IntPtr raw, IntPtr source, int flags, int io_priority, IntPtr cancellable, GLibSharp.AsyncReadyCallbackNative cb, IntPtr user_data);

		public void SpliceAsync(GLib.InputStream source, GLib.OutputStreamSpliceFlags flags, int io_priority, GLib.Cancellable cancellable, GLib.AsyncReadyCallback cb) {
			GLibSharp.AsyncReadyCallbackWrapper cb_wrapper = new GLibSharp.AsyncReadyCallbackWrapper (cb);
			cb_wrapper.PersistUntilCalled ();
			g_output_stream_splice_async(Handle, source == null ? IntPtr.Zero : source.Handle, (int) flags, io_priority, cancellable == null ? IntPtr.Zero : cancellable.Handle, cb_wrapper.NativeDelegate, IntPtr.Zero);
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern unsafe IntPtr g_output_stream_splice_finish(IntPtr raw, IntPtr result, out IntPtr error);

		public unsafe long SpliceFinish(GLib.IAsyncResult result) {
			IntPtr error = IntPtr.Zero;
			IntPtr raw_ret = g_output_stream_splice_finish(Handle, result == null ? IntPtr.Zero : ((result is GLib.Object) ? (result as GLib.Object).Handle : (result as GLib.AsyncResultAdapter).Handle), out error);
			long ret = (long) raw_ret;
			if (error != IntPtr.Zero) throw new GLib.GException (error);
			return ret;
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern unsafe IntPtr g_output_stream_write(IntPtr raw, byte[] buffer, UIntPtr count, IntPtr cancellable, out IntPtr error);

		public unsafe long Write(byte[] buffer, ulong count, GLib.Cancellable cancellable) {
			IntPtr error = IntPtr.Zero;
			IntPtr raw_ret = g_output_stream_write(Handle, buffer, new UIntPtr (count), cancellable == null ? IntPtr.Zero : cancellable.Handle, out error);
			long ret = (long) raw_ret;
			if (error != IntPtr.Zero) throw new GLib.GException (error);
			return ret;
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern unsafe bool g_output_stream_write_all(IntPtr raw, byte[] buffer, UIntPtr count, out UIntPtr bytes_written, IntPtr cancellable, out IntPtr error);

		public unsafe bool WriteAll(byte[] buffer, ulong count, out ulong bytes_written, GLib.Cancellable cancellable) {
			UIntPtr native_bytes_written;
			IntPtr error = IntPtr.Zero;
			bool raw_ret = g_output_stream_write_all(Handle, buffer, new UIntPtr (count), out native_bytes_written, cancellable == null ? IntPtr.Zero : cancellable.Handle, out error);
			bool ret = raw_ret;
			bytes_written = (ulong) native_bytes_written;
			if (error != IntPtr.Zero) throw new GLib.GException (error);
			return ret;
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void g_output_stream_write_async(IntPtr raw, byte[] buffer, UIntPtr count, int io_priority, IntPtr cancellable, GLibSharp.AsyncReadyCallbackNative cb, IntPtr user_data);

		public void WriteAsync(byte[] buffer, ulong count, int io_priority, GLib.Cancellable cancellable, GLib.AsyncReadyCallback cb) {
			GLibSharp.AsyncReadyCallbackWrapper cb_wrapper = new GLibSharp.AsyncReadyCallbackWrapper (cb);
			cb_wrapper.PersistUntilCalled ();
			g_output_stream_write_async(Handle, buffer, new UIntPtr (count), io_priority, cancellable == null ? IntPtr.Zero : cancellable.Handle, cb_wrapper.NativeDelegate, IntPtr.Zero);
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern unsafe IntPtr g_output_stream_write_finish(IntPtr raw, IntPtr result, out IntPtr error);

		public unsafe long WriteFinish(GLib.IAsyncResult result) {
			IntPtr error = IntPtr.Zero;
			IntPtr raw_ret = g_output_stream_write_finish(Handle, result == null ? IntPtr.Zero : ((result is GLib.Object) ? (result as GLib.Object).Handle : (result as GLib.AsyncResultAdapter).Handle), out error);
			long ret = (long) raw_ret;
			if (error != IntPtr.Zero) throw new GLib.GException (error);
			return ret;
		}

#endregion
	}
}
