// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace GLib {

	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Runtime.InteropServices;

#region Autogenerated code
	public partial class BufferedInputStream : GLib.FilterInputStream {

		public BufferedInputStream (IntPtr raw) : base(raw) {}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr g_buffered_input_stream_new(IntPtr base_stream);

		public BufferedInputStream (GLib.InputStream base_stream) : base (IntPtr.Zero)
		{
			if (GetType () != typeof (BufferedInputStream)) {
				var vals = new List<GLib.Value> ();
				var names = new List<string> ();
				if (base_stream != null) {
					names.Add ("base_stream");
					vals.Add (new GLib.Value (base_stream));
				}
				CreateNativeObject (names.ToArray (), vals.ToArray ());
				return;
			}
			Raw = g_buffered_input_stream_new(base_stream == null ? IntPtr.Zero : base_stream.Handle);
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr g_buffered_input_stream_new_sized(IntPtr base_stream, UIntPtr size);

		public BufferedInputStream (GLib.InputStream base_stream, ulong size) : base (IntPtr.Zero)
		{
			if (GetType () != typeof (BufferedInputStream)) {
				throw new InvalidOperationException ("Can't override this constructor.");
			}
			Raw = g_buffered_input_stream_new_sized(base_stream == null ? IntPtr.Zero : base_stream.Handle, new UIntPtr (size));
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern UIntPtr g_buffered_input_stream_get_buffer_size(IntPtr raw);

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void g_buffered_input_stream_set_buffer_size(IntPtr raw, UIntPtr size);

		[GLib.Property ("buffer-size")]
		public ulong BufferSize {
			get  {
				UIntPtr raw_ret = g_buffered_input_stream_get_buffer_size(Handle);
				ulong ret = (ulong) raw_ret;
				return ret;
			}
			set  {
				g_buffered_input_stream_set_buffer_size(Handle, new UIntPtr (value));
			}
		}

		static FillNativeDelegate Fill_cb_delegate;
		static FillNativeDelegate FillVMCallback {
			get {
				if (Fill_cb_delegate == null)
					Fill_cb_delegate = new FillNativeDelegate (Fill_cb);
				return Fill_cb_delegate;
			}
		}

		static void OverrideFill (GLib.GType gtype)
		{
			OverrideFill (gtype, FillVMCallback);
		}

		static void OverrideFill (GLib.GType gtype, FillNativeDelegate callback)
		{
			GBufferedInputStreamClass class_iface = GetClassStruct (gtype, false);
			class_iface.Fill = callback;
			OverrideClassStruct (gtype, class_iface);
		}

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate IntPtr FillNativeDelegate (IntPtr inst, IntPtr count, IntPtr cancellable, out IntPtr error);

		static IntPtr Fill_cb (IntPtr inst, IntPtr count, IntPtr cancellable, out IntPtr error)
		{
			error = IntPtr.Zero;

			try {
				BufferedInputStream __obj = GLib.Object.GetObject (inst, false) as BufferedInputStream;
				long __result;
				__result = __obj.OnFill ((long) count, GLib.Object.GetObject(cancellable) as GLib.Cancellable);
				return new IntPtr (__result);
			} catch (Exception e) {
				GLib.ExceptionManager.RaiseUnhandledException (e, true);
				// NOTREACHED: above call does not return.
				throw e;
			}
		}

		[GLib.DefaultSignalHandler(Type=typeof(GLib.BufferedInputStream), ConnectionMethod="OverrideFill")]
		protected virtual long OnFill (long count, GLib.Cancellable cancellable)
		{
			return InternalFill (count, cancellable);
		}

		private long InternalFill (long count, GLib.Cancellable cancellable)
		{
			FillNativeDelegate unmanaged = GetClassStruct (this.LookupGType ().GetThresholdType (), true).Fill;
			if (unmanaged == null) throw new InvalidOperationException ("No base method to invoke");

			IntPtr error = IntPtr.Zero;
			IntPtr __result = unmanaged (this.Handle, new IntPtr (count), cancellable == null ? IntPtr.Zero : cancellable.Handle, out error);
			return (long) __result;
		}

		static FillAsyncNativeDelegate FillAsync_cb_delegate;
		static FillAsyncNativeDelegate FillAsyncVMCallback {
			get {
				if (FillAsync_cb_delegate == null)
					FillAsync_cb_delegate = new FillAsyncNativeDelegate (FillAsync_cb);
				return FillAsync_cb_delegate;
			}
		}

		static void OverrideFillAsync (GLib.GType gtype)
		{
			OverrideFillAsync (gtype, FillAsyncVMCallback);
		}

		static void OverrideFillAsync (GLib.GType gtype, FillAsyncNativeDelegate callback)
		{
			GBufferedInputStreamClass class_iface = GetClassStruct (gtype, false);
			class_iface.FillAsync = callback;
			OverrideClassStruct (gtype, class_iface);
		}

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate void FillAsyncNativeDelegate (IntPtr inst, IntPtr count, int io_priority, IntPtr cancellable, GLibSharp.AsyncReadyCallbackNative cb, IntPtr user_data);

		static void FillAsync_cb (IntPtr inst, IntPtr count, int io_priority, IntPtr cancellable, GLibSharp.AsyncReadyCallbackNative cb, IntPtr user_data)
		{
			try {
				BufferedInputStream __obj = GLib.Object.GetObject (inst, false) as BufferedInputStream;
				GLibSharp.AsyncReadyCallbackInvoker cb_invoker = new GLibSharp.AsyncReadyCallbackInvoker (cb, user_data);
				__obj.OnFillAsync ((long) count, io_priority, GLib.Object.GetObject(cancellable) as GLib.Cancellable, cb_invoker.Handler);
			} catch (Exception e) {
				GLib.ExceptionManager.RaiseUnhandledException (e, false);
			}
		}

		[GLib.DefaultSignalHandler(Type=typeof(GLib.BufferedInputStream), ConnectionMethod="OverrideFillAsync")]
		protected virtual void OnFillAsync (long count, int io_priority, GLib.Cancellable cancellable, GLib.AsyncReadyCallback cb)
		{
			InternalFillAsync (count, io_priority, cancellable, cb);
		}

		private void InternalFillAsync (long count, int io_priority, GLib.Cancellable cancellable, GLib.AsyncReadyCallback cb)
		{
			FillAsyncNativeDelegate unmanaged = GetClassStruct (this.LookupGType ().GetThresholdType (), true).FillAsync;
			if (unmanaged == null) return;

			GLibSharp.AsyncReadyCallbackWrapper cb_wrapper = new GLibSharp.AsyncReadyCallbackWrapper (cb);
			cb_wrapper.PersistUntilCalled ();
			unmanaged (this.Handle, new IntPtr (count), io_priority, cancellable == null ? IntPtr.Zero : cancellable.Handle, cb_wrapper.NativeDelegate, IntPtr.Zero);
		}

		static FillFinishNativeDelegate FillFinish_cb_delegate;
		static FillFinishNativeDelegate FillFinishVMCallback {
			get {
				if (FillFinish_cb_delegate == null)
					FillFinish_cb_delegate = new FillFinishNativeDelegate (FillFinish_cb);
				return FillFinish_cb_delegate;
			}
		}

		static void OverrideFillFinish (GLib.GType gtype)
		{
			OverrideFillFinish (gtype, FillFinishVMCallback);
		}

		static void OverrideFillFinish (GLib.GType gtype, FillFinishNativeDelegate callback)
		{
			GBufferedInputStreamClass class_iface = GetClassStruct (gtype, false);
			class_iface.FillFinish = callback;
			OverrideClassStruct (gtype, class_iface);
		}

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate IntPtr FillFinishNativeDelegate (IntPtr inst, IntPtr result, out IntPtr error);

		static IntPtr FillFinish_cb (IntPtr inst, IntPtr result, out IntPtr error)
		{
			error = IntPtr.Zero;

			try {
				BufferedInputStream __obj = GLib.Object.GetObject (inst, false) as BufferedInputStream;
				long __result;
				__result = __obj.OnFillFinish (GLib.AsyncResultAdapter.GetObject (result, false));
				return new IntPtr (__result);
			} catch (Exception e) {
				GLib.ExceptionManager.RaiseUnhandledException (e, true);
				// NOTREACHED: above call does not return.
				throw e;
			}
		}

		[GLib.DefaultSignalHandler(Type=typeof(GLib.BufferedInputStream), ConnectionMethod="OverrideFillFinish")]
		protected virtual long OnFillFinish (GLib.IAsyncResult result)
		{
			return InternalFillFinish (result);
		}

		private long InternalFillFinish (GLib.IAsyncResult result)
		{
			FillFinishNativeDelegate unmanaged = GetClassStruct (this.LookupGType ().GetThresholdType (), true).FillFinish;
			if (unmanaged == null) throw new InvalidOperationException ("No base method to invoke");

			IntPtr error = IntPtr.Zero;
			IntPtr __result = unmanaged (this.Handle, result == null ? IntPtr.Zero : ((result is GLib.Object) ? (result as GLib.Object).Handle : (result as GLib.AsyncResultAdapter).Handle), out error);
			return (long) __result;
		}

		[StructLayout (LayoutKind.Sequential)]
		struct GBufferedInputStreamClass {
			public FillNativeDelegate Fill;
			public FillAsyncNativeDelegate FillAsync;
			public FillFinishNativeDelegate FillFinish;
			IntPtr GReserved1;
			IntPtr GReserved2;
			IntPtr GReserved3;
			IntPtr GReserved4;
			IntPtr GReserved5;
		}

		static uint class_offset = ((GLib.GType) typeof (GLib.FilterInputStream)).GetClassSize ();
		static Dictionary<GLib.GType, GBufferedInputStreamClass> class_structs;

		static GBufferedInputStreamClass GetClassStruct (GLib.GType gtype, bool use_cache)
		{
			if (class_structs == null)
				class_structs = new Dictionary<GLib.GType, GBufferedInputStreamClass> ();

			if (use_cache && class_structs.ContainsKey (gtype))
				return class_structs [gtype];
			else {
				IntPtr class_ptr = new IntPtr (gtype.GetClassPtr ().ToInt64 () + class_offset);
				GBufferedInputStreamClass class_struct = (GBufferedInputStreamClass) Marshal.PtrToStructure (class_ptr, typeof (GBufferedInputStreamClass));
				if (use_cache)
					class_structs.Add (gtype, class_struct);
				return class_struct;
			}
		}

		static void OverrideClassStruct (GLib.GType gtype, GBufferedInputStreamClass class_struct)
		{
			IntPtr class_ptr = new IntPtr (gtype.GetClassPtr ().ToInt64 () + class_offset);
			Marshal.StructureToPtr (class_struct, class_ptr, false);
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern unsafe IntPtr g_buffered_input_stream_fill(IntPtr raw, IntPtr count, IntPtr cancellable, out IntPtr error);

		public unsafe long Fill(long count, GLib.Cancellable cancellable) {
			IntPtr error = IntPtr.Zero;
			IntPtr raw_ret = g_buffered_input_stream_fill(Handle, new IntPtr (count), cancellable == null ? IntPtr.Zero : cancellable.Handle, out error);
			long ret = (long) raw_ret;
			if (error != IntPtr.Zero) throw new GLib.GException (error);
			return ret;
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void g_buffered_input_stream_fill_async(IntPtr raw, IntPtr count, int io_priority, IntPtr cancellable, GLibSharp.AsyncReadyCallbackNative cb, IntPtr user_data);

		public void FillAsync(long count, int io_priority, GLib.Cancellable cancellable, GLib.AsyncReadyCallback cb) {
			GLibSharp.AsyncReadyCallbackWrapper cb_wrapper = new GLibSharp.AsyncReadyCallbackWrapper (cb);
			cb_wrapper.PersistUntilCalled ();
			g_buffered_input_stream_fill_async(Handle, new IntPtr (count), io_priority, cancellable == null ? IntPtr.Zero : cancellable.Handle, cb_wrapper.NativeDelegate, IntPtr.Zero);
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern unsafe IntPtr g_buffered_input_stream_fill_finish(IntPtr raw, IntPtr result, out IntPtr error);

		public unsafe long FillFinish(GLib.IAsyncResult result) {
			IntPtr error = IntPtr.Zero;
			IntPtr raw_ret = g_buffered_input_stream_fill_finish(Handle, result == null ? IntPtr.Zero : ((result is GLib.Object) ? (result as GLib.Object).Handle : (result as GLib.AsyncResultAdapter).Handle), out error);
			long ret = (long) raw_ret;
			if (error != IntPtr.Zero) throw new GLib.GException (error);
			return ret;
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern UIntPtr g_buffered_input_stream_get_available(IntPtr raw);

		public ulong Available { 
			get {
				UIntPtr raw_ret = g_buffered_input_stream_get_available(Handle);
				ulong ret = (ulong) raw_ret;
				return ret;
			}
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr g_buffered_input_stream_get_type();

		public static new GLib.GType GType { 
			get {
				IntPtr raw_ret = g_buffered_input_stream_get_type();
				GLib.GType ret = new GLib.GType(raw_ret);
				return ret;
			}
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern UIntPtr g_buffered_input_stream_peek(IntPtr raw, IntPtr buffer, UIntPtr offset, UIntPtr count);

		public ulong Peek(IntPtr buffer, ulong offset, ulong count) {
			UIntPtr raw_ret = g_buffered_input_stream_peek(Handle, buffer, new UIntPtr (offset), new UIntPtr (count));
			ulong ret = (ulong) raw_ret;
			return ret;
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr g_buffered_input_stream_peek_buffer(IntPtr raw, out UIntPtr count);

		public IntPtr PeekBuffer(out ulong count) {
			UIntPtr native_count;
			IntPtr raw_ret = g_buffered_input_stream_peek_buffer(Handle, out native_count);
			IntPtr ret = raw_ret;
			count = (ulong) native_count;
			return ret;
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern unsafe int g_buffered_input_stream_read_byte(IntPtr raw, IntPtr cancellable, out IntPtr error);

		public unsafe int ReadByte(GLib.Cancellable cancellable) {
			IntPtr error = IntPtr.Zero;
			int raw_ret = g_buffered_input_stream_read_byte(Handle, cancellable == null ? IntPtr.Zero : cancellable.Handle, out error);
			int ret = raw_ret;
			if (error != IntPtr.Zero) throw new GLib.GException (error);
			return ret;
		}

#endregion
	}
}