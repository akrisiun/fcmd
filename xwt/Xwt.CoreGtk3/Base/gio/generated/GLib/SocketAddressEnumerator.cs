// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace GLib {

	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Runtime.InteropServices;

#region Autogenerated code
	public partial class SocketAddressEnumerator : GLib.Object {

		public SocketAddressEnumerator (IntPtr raw) : base(raw) {}

		protected SocketAddressEnumerator() : base(IntPtr.Zero)
		{
			CreateNativeObject (new string [0], new GLib.Value [0]);
		}

		static NextNativeDelegate Next_cb_delegate;
		static NextNativeDelegate NextVMCallback {
			get {
				if (Next_cb_delegate == null)
					Next_cb_delegate = new NextNativeDelegate (Next_cb);
				return Next_cb_delegate;
			}
		}

		static void OverrideNext (GLib.GType gtype)
		{
			OverrideNext (gtype, NextVMCallback);
		}

		static void OverrideNext (GLib.GType gtype, NextNativeDelegate callback)
		{
			GSocketAddressEnumeratorClass class_iface = GetClassStruct (gtype, false);
			class_iface.Next = callback;
			OverrideClassStruct (gtype, class_iface);
		}

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate IntPtr NextNativeDelegate (IntPtr inst, IntPtr cancellable, out IntPtr error);

		static IntPtr Next_cb (IntPtr inst, IntPtr cancellable, out IntPtr error)
		{
			error = IntPtr.Zero;

			try {
				SocketAddressEnumerator __obj = GLib.Object.GetObject (inst, false) as SocketAddressEnumerator;
				GLib.SocketAddress __result;
				__result = __obj.OnNext (GLib.Object.GetObject(cancellable) as GLib.Cancellable);
				return __result == null ? IntPtr.Zero : __result.Handle;
			} catch (Exception e) {
				GLib.ExceptionManager.RaiseUnhandledException (e, true);
				// NOTREACHED: above call does not return.
				throw e;
			}
		}

		[GLib.DefaultSignalHandler(Type=typeof(GLib.SocketAddressEnumerator), ConnectionMethod="OverrideNext")]
		protected virtual GLib.SocketAddress OnNext (GLib.Cancellable cancellable)
		{
			return InternalNext (cancellable);
		}

		private GLib.SocketAddress InternalNext (GLib.Cancellable cancellable)
		{
			NextNativeDelegate unmanaged = GetClassStruct (this.LookupGType ().GetThresholdType (), true).Next;
			if (unmanaged == null) throw new InvalidOperationException ("No base method to invoke");

			IntPtr error = IntPtr.Zero;
			IntPtr __result = unmanaged (this.Handle, cancellable == null ? IntPtr.Zero : cancellable.Handle, out error);
			return GLib.Object.GetObject(__result) as GLib.SocketAddress;
		}

		static NextAsyncNativeDelegate NextAsync_cb_delegate;
		static NextAsyncNativeDelegate NextAsyncVMCallback {
			get {
				if (NextAsync_cb_delegate == null)
					NextAsync_cb_delegate = new NextAsyncNativeDelegate (NextAsync_cb);
				return NextAsync_cb_delegate;
			}
		}

		static void OverrideNextAsync (GLib.GType gtype)
		{
			OverrideNextAsync (gtype, NextAsyncVMCallback);
		}

		static void OverrideNextAsync (GLib.GType gtype, NextAsyncNativeDelegate callback)
		{
			GSocketAddressEnumeratorClass class_iface = GetClassStruct (gtype, false);
			class_iface.NextAsync = callback;
			OverrideClassStruct (gtype, class_iface);
		}

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate void NextAsyncNativeDelegate (IntPtr inst, IntPtr cancellable, GLibSharp.AsyncReadyCallbackNative cb, IntPtr user_data);

		static void NextAsync_cb (IntPtr inst, IntPtr cancellable, GLibSharp.AsyncReadyCallbackNative cb, IntPtr user_data)
		{
			try {
				SocketAddressEnumerator __obj = GLib.Object.GetObject (inst, false) as SocketAddressEnumerator;
				GLibSharp.AsyncReadyCallbackInvoker cb_invoker = new GLibSharp.AsyncReadyCallbackInvoker (cb, user_data);
				__obj.OnNextAsync (GLib.Object.GetObject(cancellable) as GLib.Cancellable, cb_invoker.Handler);
			} catch (Exception e) {
				GLib.ExceptionManager.RaiseUnhandledException (e, false);
			}
		}

		[GLib.DefaultSignalHandler(Type=typeof(GLib.SocketAddressEnumerator), ConnectionMethod="OverrideNextAsync")]
		protected virtual void OnNextAsync (GLib.Cancellable cancellable, GLib.AsyncReadyCallback cb)
		{
			InternalNextAsync (cancellable, cb);
		}

		private void InternalNextAsync (GLib.Cancellable cancellable, GLib.AsyncReadyCallback cb)
		{
			NextAsyncNativeDelegate unmanaged = GetClassStruct (this.LookupGType ().GetThresholdType (), true).NextAsync;
			if (unmanaged == null) return;

			GLibSharp.AsyncReadyCallbackWrapper cb_wrapper = new GLibSharp.AsyncReadyCallbackWrapper (cb);
			cb_wrapper.PersistUntilCalled ();
			unmanaged (this.Handle, cancellable == null ? IntPtr.Zero : cancellable.Handle, cb_wrapper.NativeDelegate, IntPtr.Zero);
		}

		static NextFinishNativeDelegate NextFinish_cb_delegate;
		static NextFinishNativeDelegate NextFinishVMCallback {
			get {
				if (NextFinish_cb_delegate == null)
					NextFinish_cb_delegate = new NextFinishNativeDelegate (NextFinish_cb);
				return NextFinish_cb_delegate;
			}
		}

		static void OverrideNextFinish (GLib.GType gtype)
		{
			OverrideNextFinish (gtype, NextFinishVMCallback);
		}

		static void OverrideNextFinish (GLib.GType gtype, NextFinishNativeDelegate callback)
		{
			GSocketAddressEnumeratorClass class_iface = GetClassStruct (gtype, false);
			class_iface.NextFinish = callback;
			OverrideClassStruct (gtype, class_iface);
		}

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate IntPtr NextFinishNativeDelegate (IntPtr inst, IntPtr result, out IntPtr error);

		static IntPtr NextFinish_cb (IntPtr inst, IntPtr result, out IntPtr error)
		{
			error = IntPtr.Zero;

			try {
				SocketAddressEnumerator __obj = GLib.Object.GetObject (inst, false) as SocketAddressEnumerator;
				GLib.SocketAddress __result;
				__result = __obj.OnNextFinish (GLib.AsyncResultAdapter.GetObject (result, false));
				return __result == null ? IntPtr.Zero : __result.Handle;
			} catch (Exception e) {
				GLib.ExceptionManager.RaiseUnhandledException (e, true);
				// NOTREACHED: above call does not return.
				throw e;
			}
		}

		[GLib.DefaultSignalHandler(Type=typeof(GLib.SocketAddressEnumerator), ConnectionMethod="OverrideNextFinish")]
		protected virtual GLib.SocketAddress OnNextFinish (GLib.IAsyncResult result)
		{
			return InternalNextFinish (result);
		}

		private GLib.SocketAddress InternalNextFinish (GLib.IAsyncResult result)
		{
			NextFinishNativeDelegate unmanaged = GetClassStruct (this.LookupGType ().GetThresholdType (), true).NextFinish;
			if (unmanaged == null) throw new InvalidOperationException ("No base method to invoke");

			IntPtr error = IntPtr.Zero;
			IntPtr __result = unmanaged (this.Handle, result == null ? IntPtr.Zero : ((result is GLib.Object) ? (result as GLib.Object).Handle : (result as GLib.AsyncResultAdapter).Handle), out error);
			return GLib.Object.GetObject(__result) as GLib.SocketAddress;
		}

		[StructLayout (LayoutKind.Sequential)]
		struct GSocketAddressEnumeratorClass {
			public NextNativeDelegate Next;
			public NextAsyncNativeDelegate NextAsync;
			public NextFinishNativeDelegate NextFinish;
		}

		static uint class_offset = ((GLib.GType) typeof (GLib.Object)).GetClassSize ();
		static Dictionary<GLib.GType, GSocketAddressEnumeratorClass> class_structs;

		static GSocketAddressEnumeratorClass GetClassStruct (GLib.GType gtype, bool use_cache)
		{
			if (class_structs == null)
				class_structs = new Dictionary<GLib.GType, GSocketAddressEnumeratorClass> ();

			if (use_cache && class_structs.ContainsKey (gtype))
				return class_structs [gtype];
			else {
				IntPtr class_ptr = new IntPtr (gtype.GetClassPtr ().ToInt64 () + class_offset);
				GSocketAddressEnumeratorClass class_struct = (GSocketAddressEnumeratorClass) Marshal.PtrToStructure (class_ptr, typeof (GSocketAddressEnumeratorClass));
				if (use_cache)
					class_structs.Add (gtype, class_struct);
				return class_struct;
			}
		}

		static void OverrideClassStruct (GLib.GType gtype, GSocketAddressEnumeratorClass class_struct)
		{
			IntPtr class_ptr = new IntPtr (gtype.GetClassPtr ().ToInt64 () + class_offset);
			Marshal.StructureToPtr (class_struct, class_ptr, false);
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr g_socket_address_enumerator_get_type();

		public static new GLib.GType GType { 
			get {
				IntPtr raw_ret = g_socket_address_enumerator_get_type();
				GLib.GType ret = new GLib.GType(raw_ret);
				return ret;
			}
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern unsafe IntPtr g_socket_address_enumerator_next(IntPtr raw, IntPtr cancellable, out IntPtr error);

		public unsafe GLib.SocketAddress Next(GLib.Cancellable cancellable) {
			IntPtr error = IntPtr.Zero;
			IntPtr raw_ret = g_socket_address_enumerator_next(Handle, cancellable == null ? IntPtr.Zero : cancellable.Handle, out error);
			GLib.SocketAddress ret = GLib.Object.GetObject(raw_ret) as GLib.SocketAddress;
			if (error != IntPtr.Zero) throw new GLib.GException (error);
			return ret;
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void g_socket_address_enumerator_next_async(IntPtr raw, IntPtr cancellable, GLibSharp.AsyncReadyCallbackNative cb, IntPtr user_data);

		public void NextAsync(GLib.Cancellable cancellable, GLib.AsyncReadyCallback cb) {
			GLibSharp.AsyncReadyCallbackWrapper cb_wrapper = new GLibSharp.AsyncReadyCallbackWrapper (cb);
			cb_wrapper.PersistUntilCalled ();
			g_socket_address_enumerator_next_async(Handle, cancellable == null ? IntPtr.Zero : cancellable.Handle, cb_wrapper.NativeDelegate, IntPtr.Zero);
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern unsafe IntPtr g_socket_address_enumerator_next_finish(IntPtr raw, IntPtr result, out IntPtr error);

		public unsafe GLib.SocketAddress NextFinish(GLib.IAsyncResult result) {
			IntPtr error = IntPtr.Zero;
			IntPtr raw_ret = g_socket_address_enumerator_next_finish(Handle, result == null ? IntPtr.Zero : ((result is GLib.Object) ? (result as GLib.Object).Handle : (result as GLib.AsyncResultAdapter).Handle), out error);
			GLib.SocketAddress ret = GLib.Object.GetObject(raw_ret) as GLib.SocketAddress;
			if (error != IntPtr.Zero) throw new GLib.GException (error);
			return ret;
		}

#endregion
	}
}
