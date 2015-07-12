// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace Atk {

	using System;
	using System.Runtime.InteropServices;

#region Autogenerated code
	public partial class HyperlinkImplAdapter : GLib.GInterfaceAdapter, Atk.IHyperlinkImpl {

		[StructLayout (LayoutKind.Sequential)]
		struct AtkHyperlinkImplIface {
			public GetHyperlinkNativeDelegate GetHyperlink;
		}

		static AtkHyperlinkImplIface iface;

		static HyperlinkImplAdapter ()
		{
			GLib.GType.Register (_gtype, typeof (HyperlinkImplAdapter));
			iface.GetHyperlink = new GetHyperlinkNativeDelegate (GetHyperlink_cb);
		}

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate IntPtr GetHyperlinkNativeDelegate (IntPtr inst);

		static IntPtr GetHyperlink_cb (IntPtr inst)
		{
			try {
				IHyperlinkImplImplementor __obj = GLib.Object.GetObject (inst, false) as IHyperlinkImplImplementor;
				Atk.Hyperlink __result;
				__result = __obj.Hyperlink;
				return __result == null ? IntPtr.Zero : __result.Handle;
			} catch (Exception e) {
				GLib.ExceptionManager.RaiseUnhandledException (e, true);
				// NOTREACHED: above call does not return.
				throw e;
			}
		}

		static int class_offset = 2 * IntPtr.Size;

		static void Initialize (IntPtr ptr, IntPtr data)
		{
			IntPtr ifaceptr = new IntPtr (ptr.ToInt64 () + class_offset);
			AtkHyperlinkImplIface native_iface = (AtkHyperlinkImplIface) Marshal.PtrToStructure (ifaceptr, typeof (AtkHyperlinkImplIface));
			native_iface.GetHyperlink = iface.GetHyperlink;
			Marshal.StructureToPtr (native_iface, ifaceptr, false);
		}

		GLib.Object implementor;

		public HyperlinkImplAdapter ()
		{
			InitHandler = new GLib.GInterfaceInitHandler (Initialize);
		}

		public HyperlinkImplAdapter (IHyperlinkImplImplementor implementor)
		{
			if (implementor == null)
				throw new ArgumentNullException ("implementor");
			else if (!(implementor is GLib.Object))
				throw new ArgumentException ("implementor must be a subclass of GLib.Object");
			this.implementor = implementor as GLib.Object;
		}

		public HyperlinkImplAdapter (IntPtr handle)
		{
			if (!_gtype.IsInstance (handle))
				throw new ArgumentException ("The gobject doesn't implement the GInterface of this adapter", "handle");
			implementor = GLib.Object.GetObject (handle);
		}

		[DllImport("libatk-1.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr atk_hyperlink_impl_get_type();

		private static GLib.GType _gtype = new GLib.GType (atk_hyperlink_impl_get_type ());

		public static GLib.GType GType {
			get {
				return _gtype;
			}
		}

		public override GLib.GType GInterfaceGType {
			get {
				return _gtype;
			}
		}

		public override IntPtr Handle {
			get {
				return implementor.Handle;
			}
		}

		public IntPtr OwnedHandle {
			get {
				return implementor.OwnedHandle;
			}
		}

		public static IHyperlinkImpl GetObject (IntPtr handle, bool owned)
		{
			GLib.Object obj = GLib.Object.GetObject (handle, owned);
			return GetObject (obj);
		}

		public static IHyperlinkImpl GetObject (GLib.Object obj)
		{
			if (obj == null)
				return null;
			else if (obj is IHyperlinkImplImplementor)
				return new HyperlinkImplAdapter (obj as IHyperlinkImplImplementor);
			else if (obj as IHyperlinkImpl == null)
				return new HyperlinkImplAdapter (obj.Handle);
			else
				return obj as IHyperlinkImpl;
		}

		public IHyperlinkImplImplementor Implementor {
			get {
				return implementor as IHyperlinkImplImplementor;
			}
		}

		[DllImport("libatk-1.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr atk_hyperlink_impl_get_hyperlink(IntPtr raw);

		public Atk.Hyperlink Hyperlink { 
			get {
				IntPtr raw_ret = atk_hyperlink_impl_get_hyperlink(Handle);
				Atk.Hyperlink ret = GLib.Object.GetObject(raw_ret) as Atk.Hyperlink;
				return ret;
			}
		}

#endregion
	}
}