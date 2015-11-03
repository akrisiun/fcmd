// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace GLib {

	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Runtime.InteropServices;

#region Autogenerated code
	public partial class InetAddress : GLib.Object {

		public InetAddress (IntPtr raw) : base(raw) {}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr g_inet_address_new_any(int family);

		public InetAddress (GLib.SocketFamily family) : base (IntPtr.Zero)
		{
			if (GetType () != typeof (InetAddress)) {
				var vals = new List<GLib.Value> ();
				var names = new List<string> ();
				names.Add ("family");
				vals.Add (new GLib.Value (family));
				CreateNativeObject (names.ToArray (), vals.ToArray ());
				return;
			}
			Raw = g_inet_address_new_any((int) family);
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr g_inet_address_new_from_bytes(byte[] bytes, int family);

		public InetAddress (byte[] bytes, GLib.SocketFamily family) : base (IntPtr.Zero)
		{
			if (GetType () != typeof (InetAddress)) {
				var vals = new List<GLib.Value> ();
				var names = new List<string> ();
				names.Add ("bytes");
				vals.Add (new GLib.Value (bytes));
				names.Add ("family");
				vals.Add (new GLib.Value (family));
				CreateNativeObject (names.ToArray (), vals.ToArray ());
				return;
			}
			Raw = g_inet_address_new_from_bytes(bytes, (int) family);
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr g_inet_address_new_from_string(IntPtr str1ng);

		public InetAddress (string str1ng) : base (IntPtr.Zero)
		{
			if (GetType () != typeof (InetAddress)) {
				throw new InvalidOperationException ("Can't override this constructor.");
			}
			IntPtr native_str1ng = GLib.Marshaller.StringToPtrGStrdup (str1ng);
			Raw = g_inet_address_new_from_string(native_str1ng);
			GLib.Marshaller.Free (native_str1ng);
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr g_inet_address_new_loopback(int family);

		public static InetAddress NewLoopback(GLib.SocketFamily family)
		{
			InetAddress result = new InetAddress (g_inet_address_new_loopback((int) family));
			return result;
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern int g_inet_address_get_family(IntPtr raw);

		[GLib.Property ("family")]
		public GLib.SocketFamily Family {
			get  {
				int raw_ret = g_inet_address_get_family(Handle);
				GLib.SocketFamily ret = (GLib.SocketFamily) raw_ret;
				return ret;
			}
		}

		[GLib.Property ("bytes")]
		public IntPtr Bytes {
			get {
				GLib.Value val = GetProperty ("bytes");
				IntPtr ret = (IntPtr) val;
				val.Dispose ();
				return ret;
			}
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool g_inet_address_get_is_any(IntPtr raw);

		[GLib.Property ("is-any")]
		public bool IsAny {
			get  {
				bool raw_ret = g_inet_address_get_is_any(Handle);
				bool ret = raw_ret;
				return ret;
			}
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool g_inet_address_get_is_link_local(IntPtr raw);

		[GLib.Property ("is-link-local")]
		public bool IsLinkLocal {
			get  {
				bool raw_ret = g_inet_address_get_is_link_local(Handle);
				bool ret = raw_ret;
				return ret;
			}
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool g_inet_address_get_is_loopback(IntPtr raw);

		[GLib.Property ("is-loopback")]
		public bool IsLoopback {
			get  {
				bool raw_ret = g_inet_address_get_is_loopback(Handle);
				bool ret = raw_ret;
				return ret;
			}
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool g_inet_address_get_is_site_local(IntPtr raw);

		[GLib.Property ("is-site-local")]
		public bool IsSiteLocal {
			get  {
				bool raw_ret = g_inet_address_get_is_site_local(Handle);
				bool ret = raw_ret;
				return ret;
			}
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool g_inet_address_get_is_multicast(IntPtr raw);

		[GLib.Property ("is-multicast")]
		public bool IsMulticast {
			get  {
				bool raw_ret = g_inet_address_get_is_multicast(Handle);
				bool ret = raw_ret;
				return ret;
			}
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool g_inet_address_get_is_mc_global(IntPtr raw);

		[GLib.Property ("is-mc-global")]
		public bool IsMcGlobal {
			get  {
				bool raw_ret = g_inet_address_get_is_mc_global(Handle);
				bool ret = raw_ret;
				return ret;
			}
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool g_inet_address_get_is_mc_link_local(IntPtr raw);

		[GLib.Property ("is-mc-link-local")]
		public bool IsMcLinkLocal {
			get  {
				bool raw_ret = g_inet_address_get_is_mc_link_local(Handle);
				bool ret = raw_ret;
				return ret;
			}
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool g_inet_address_get_is_mc_node_local(IntPtr raw);

		[GLib.Property ("is-mc-node-local")]
		public bool IsMcNodeLocal {
			get  {
				bool raw_ret = g_inet_address_get_is_mc_node_local(Handle);
				bool ret = raw_ret;
				return ret;
			}
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool g_inet_address_get_is_mc_org_local(IntPtr raw);

		[GLib.Property ("is-mc-org-local")]
		public bool IsMcOrgLocal {
			get  {
				bool raw_ret = g_inet_address_get_is_mc_org_local(Handle);
				bool ret = raw_ret;
				return ret;
			}
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool g_inet_address_get_is_mc_site_local(IntPtr raw);

		[GLib.Property ("is-mc-site-local")]
		public bool IsMcSiteLocal {
			get  {
				bool raw_ret = g_inet_address_get_is_mc_site_local(Handle);
				bool ret = raw_ret;
				return ret;
			}
		}

		static ToStringNativeDelegate ToString_cb_delegate;
		static ToStringNativeDelegate ToStringVMCallback {
			get {
				if (ToString_cb_delegate == null)
					ToString_cb_delegate = new ToStringNativeDelegate (ToString_cb);
				return ToString_cb_delegate;
			}
		}

		static void OverrideToString (GLib.GType gtype)
		{
			OverrideToString (gtype, ToStringVMCallback);
		}

		static void OverrideToString (GLib.GType gtype, ToStringNativeDelegate callback)
		{
			GInetAddressClass class_iface = GetClassStruct (gtype, false);
			class_iface.ToString = callback;
			OverrideClassStruct (gtype, class_iface);
		}

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate IntPtr ToStringNativeDelegate (IntPtr inst);

		static IntPtr ToString_cb (IntPtr inst)
		{
			try {
				InetAddress __obj = GLib.Object.GetObject (inst, false) as InetAddress;
				string __result;
				__result = __obj.OnToString ();
				return GLib.Marshaller.StringToPtrGStrdup(__result);
			} catch (Exception e) {
				GLib.ExceptionManager.RaiseUnhandledException (e, true);
				// NOTREACHED: above call does not return.
				throw e;
			}
		}

		[GLib.DefaultSignalHandler(Type=typeof(GLib.InetAddress), ConnectionMethod="OverrideToString")]
		protected virtual string OnToString ()
		{
			return InternalToString ();
		}

		private string InternalToString ()
		{
			ToStringNativeDelegate unmanaged = GetClassStruct (this.LookupGType ().GetThresholdType (), true).ToString;
			if (unmanaged == null) return null;

			IntPtr __result = unmanaged (this.Handle);
			return GLib.Marshaller.PtrToStringGFree(__result);
		}

		static ToBytesNativeDelegate ToBytes_cb_delegate;
		static ToBytesNativeDelegate ToBytesVMCallback {
			get {
				if (ToBytes_cb_delegate == null)
					ToBytes_cb_delegate = new ToBytesNativeDelegate (ToBytes_cb);
				return ToBytes_cb_delegate;
			}
		}

		static void OverrideToBytes (GLib.GType gtype)
		{
			OverrideToBytes (gtype, ToBytesVMCallback);
		}

		static void OverrideToBytes (GLib.GType gtype, ToBytesNativeDelegate callback)
		{
			GInetAddressClass class_iface = GetClassStruct (gtype, false);
			class_iface.ToBytes = callback;
			OverrideClassStruct (gtype, class_iface);
		}

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate byte ToBytesNativeDelegate (IntPtr inst);

		static byte ToBytes_cb (IntPtr inst)
		{
			try {
				InetAddress __obj = GLib.Object.GetObject (inst, false) as InetAddress;
				byte __result;
				__result = __obj.OnToBytes ();
				return __result;
			} catch (Exception e) {
				GLib.ExceptionManager.RaiseUnhandledException (e, true);
				// NOTREACHED: above call does not return.
				throw e;
			}
		}

		[GLib.DefaultSignalHandler(Type=typeof(GLib.InetAddress), ConnectionMethod="OverrideToBytes")]
		protected virtual byte OnToBytes ()
		{
			return InternalToBytes ();
		}

		private byte InternalToBytes ()
		{
			ToBytesNativeDelegate unmanaged = GetClassStruct (this.LookupGType ().GetThresholdType (), true).ToBytes;
			if (unmanaged == null) return 0;

			byte __result = unmanaged (this.Handle);
			return __result;
		}

		[StructLayout (LayoutKind.Sequential)]
		struct GInetAddressClass {
			public new ToStringNativeDelegate ToString;
			public ToBytesNativeDelegate ToBytes;
		}

		static uint class_offset = ((GLib.GType) typeof (GLib.Object)).GetClassSize ();
		static Dictionary<GLib.GType, GInetAddressClass> class_structs;

		static GInetAddressClass GetClassStruct (GLib.GType gtype, bool use_cache)
		{
			if (class_structs == null)
				class_structs = new Dictionary<GLib.GType, GInetAddressClass> ();

			if (use_cache && class_structs.ContainsKey (gtype))
				return class_structs [gtype];
			else {
				IntPtr class_ptr = new IntPtr (gtype.GetClassPtr ().ToInt64 () + class_offset);
				GInetAddressClass class_struct = (GInetAddressClass) Marshal.PtrToStructure (class_ptr, typeof (GInetAddressClass));
				if (use_cache)
					class_structs.Add (gtype, class_struct);
				return class_struct;
			}
		}

		static void OverrideClassStruct (GLib.GType gtype, GInetAddressClass class_struct)
		{
			IntPtr class_ptr = new IntPtr (gtype.GetClassPtr ().ToInt64 () + class_offset);
			Marshal.StructureToPtr (class_struct, class_ptr, false);
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern UIntPtr g_inet_address_get_native_size(IntPtr raw);

		public ulong NativeSize { 
			get {
				UIntPtr raw_ret = g_inet_address_get_native_size(Handle);
				ulong ret = (ulong) raw_ret;
				return ret;
			}
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr g_inet_address_get_type();

		public static new GLib.GType GType { 
			get {
				IntPtr raw_ret = g_inet_address_get_type();
				GLib.GType ret = new GLib.GType(raw_ret);
				return ret;
			}
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern byte g_inet_address_to_bytes(IntPtr raw);

		public byte ToBytes() {
			byte raw_ret = g_inet_address_to_bytes(Handle);
			byte ret = raw_ret;
			return ret;
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr g_inet_address_to_string(IntPtr raw);

		public override string ToString() {
			IntPtr raw_ret = g_inet_address_to_string(Handle);
			string ret = GLib.Marshaller.PtrToStringGFree(raw_ret);
			return ret;
		}

#endregion
	}
}
