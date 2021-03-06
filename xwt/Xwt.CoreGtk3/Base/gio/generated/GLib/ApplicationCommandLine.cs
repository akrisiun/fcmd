// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace GLib {

	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Runtime.InteropServices;

#region Autogenerated code
	public partial class ApplicationCommandLine : GLib.Object {

		public ApplicationCommandLine (IntPtr raw) : base(raw) {}

		protected ApplicationCommandLine() : base(IntPtr.Zero)
		{
			CreateNativeObject (new string [0], new GLib.Value [0]);
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool g_application_command_line_get_is_remote(IntPtr raw);

		[GLib.Property ("is-remote")]
		public bool IsRemote {
			get  {
				bool raw_ret = g_application_command_line_get_is_remote(Handle);
				bool ret = raw_ret;
				return ret;
			}
		}

		static PrintLiteralNativeDelegate PrintLiteral_cb_delegate;
		static PrintLiteralNativeDelegate PrintLiteralVMCallback {
			get {
				if (PrintLiteral_cb_delegate == null)
					PrintLiteral_cb_delegate = new PrintLiteralNativeDelegate (PrintLiteral_cb);
				return PrintLiteral_cb_delegate;
			}
		}

		static void OverridePrintLiteral (GLib.GType gtype)
		{
			OverridePrintLiteral (gtype, PrintLiteralVMCallback);
		}

		static void OverridePrintLiteral (GLib.GType gtype, PrintLiteralNativeDelegate callback)
		{
			GApplicationCommandLineClass class_iface = GetClassStruct (gtype, false);
			class_iface.PrintLiteral = callback;
			OverrideClassStruct (gtype, class_iface);
		}

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate void PrintLiteralNativeDelegate (IntPtr inst, IntPtr message);

		static void PrintLiteral_cb (IntPtr inst, IntPtr message)
		{
			try {
				ApplicationCommandLine __obj = GLib.Object.GetObject (inst, false) as ApplicationCommandLine;
				__obj.OnPrintLiteral (GLib.Marshaller.Utf8PtrToString (message));
			} catch (Exception e) {
				GLib.ExceptionManager.RaiseUnhandledException (e, false);
			}
		}

		[GLib.DefaultSignalHandler(Type=typeof(GLib.ApplicationCommandLine), ConnectionMethod="OverridePrintLiteral")]
		protected virtual void OnPrintLiteral (string message)
		{
			InternalPrintLiteral (message);
		}

		private void InternalPrintLiteral (string message)
		{
			PrintLiteralNativeDelegate unmanaged = GetClassStruct (this.LookupGType ().GetThresholdType (), true).PrintLiteral;
			if (unmanaged == null) return;

			IntPtr native_message = GLib.Marshaller.StringToPtrGStrdup (message);
			unmanaged (this.Handle, native_message);
			GLib.Marshaller.Free (native_message);
		}

		static PrinterrLiteralNativeDelegate PrinterrLiteral_cb_delegate;
		static PrinterrLiteralNativeDelegate PrinterrLiteralVMCallback {
			get {
				if (PrinterrLiteral_cb_delegate == null)
					PrinterrLiteral_cb_delegate = new PrinterrLiteralNativeDelegate (PrinterrLiteral_cb);
				return PrinterrLiteral_cb_delegate;
			}
		}

		static void OverridePrinterrLiteral (GLib.GType gtype)
		{
			OverridePrinterrLiteral (gtype, PrinterrLiteralVMCallback);
		}

		static void OverridePrinterrLiteral (GLib.GType gtype, PrinterrLiteralNativeDelegate callback)
		{
			GApplicationCommandLineClass class_iface = GetClassStruct (gtype, false);
			class_iface.PrinterrLiteral = callback;
			OverrideClassStruct (gtype, class_iface);
		}

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate void PrinterrLiteralNativeDelegate (IntPtr inst, IntPtr message);

		static void PrinterrLiteral_cb (IntPtr inst, IntPtr message)
		{
			try {
				ApplicationCommandLine __obj = GLib.Object.GetObject (inst, false) as ApplicationCommandLine;
				__obj.OnPrinterrLiteral (GLib.Marshaller.Utf8PtrToString (message));
			} catch (Exception e) {
				GLib.ExceptionManager.RaiseUnhandledException (e, false);
			}
		}

		[GLib.DefaultSignalHandler(Type=typeof(GLib.ApplicationCommandLine), ConnectionMethod="OverridePrinterrLiteral")]
		protected virtual void OnPrinterrLiteral (string message)
		{
			InternalPrinterrLiteral (message);
		}

		private void InternalPrinterrLiteral (string message)
		{
			PrinterrLiteralNativeDelegate unmanaged = GetClassStruct (this.LookupGType ().GetThresholdType (), true).PrinterrLiteral;
			if (unmanaged == null) return;

			IntPtr native_message = GLib.Marshaller.StringToPtrGStrdup (message);
			unmanaged (this.Handle, native_message);
			GLib.Marshaller.Free (native_message);
		}

		[StructLayout (LayoutKind.Sequential)]
		struct GApplicationCommandLineClass {
			public PrintLiteralNativeDelegate PrintLiteral;
			public PrinterrLiteralNativeDelegate PrinterrLiteral;
			[MarshalAs (UnmanagedType.ByValArray, SizeConst=12)]
			private IntPtr[] Padding;
		}

		static uint class_offset = ((GLib.GType) typeof (GLib.Object)).GetClassSize ();
		static Dictionary<GLib.GType, GApplicationCommandLineClass> class_structs;

		static GApplicationCommandLineClass GetClassStruct (GLib.GType gtype, bool use_cache)
		{
			if (class_structs == null)
				class_structs = new Dictionary<GLib.GType, GApplicationCommandLineClass> ();

			if (use_cache && class_structs.ContainsKey (gtype))
				return class_structs [gtype];
			else {
				IntPtr class_ptr = new IntPtr (gtype.GetClassPtr ().ToInt64 () + class_offset);
				GApplicationCommandLineClass class_struct = (GApplicationCommandLineClass) Marshal.PtrToStructure (class_ptr, typeof (GApplicationCommandLineClass));
				if (use_cache)
					class_structs.Add (gtype, class_struct);
				return class_struct;
			}
		}

		static void OverrideClassStruct (GLib.GType gtype, GApplicationCommandLineClass class_struct)
		{
			IntPtr class_ptr = new IntPtr (gtype.GetClassPtr ().ToInt64 () + class_offset);
			Marshal.StructureToPtr (class_struct, class_ptr, false);
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr g_application_command_line_get_arguments(IntPtr raw, out int argc);

		public string GetArguments(out int argc) {
			IntPtr raw_ret = g_application_command_line_get_arguments(Handle, out argc);
			string ret = GLib.Marshaller.PtrToStringGFree(raw_ret);
			return ret;
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr g_application_command_line_get_cwd(IntPtr raw);

		public string Cwd { 
			get {
				IntPtr raw_ret = g_application_command_line_get_cwd(Handle);
				string ret = GLib.Marshaller.Utf8PtrToString (raw_ret);
				return ret;
			}
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr g_application_command_line_get_environ(IntPtr raw);

		public string Environ { 
			get {
				IntPtr raw_ret = g_application_command_line_get_environ(Handle);
				string ret = GLib.Marshaller.Utf8PtrToString (raw_ret);
				return ret;
			}
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern int g_application_command_line_get_exit_status(IntPtr raw);

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void g_application_command_line_set_exit_status(IntPtr raw, int exit_status);

		public int ExitStatus { 
			get {
				int raw_ret = g_application_command_line_get_exit_status(Handle);
				int ret = raw_ret;
				return ret;
			}
			set {
				g_application_command_line_set_exit_status(Handle, value);
			}
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr g_application_command_line_get_platform_data(IntPtr raw);

		public GLib.Variant PlatformData { 
			get {
				IntPtr raw_ret = g_application_command_line_get_platform_data(Handle);
				GLib.Variant ret = new GLib.Variant(raw_ret);
				return ret;
			}
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr g_application_command_line_get_type();

		public static new GLib.GType GType { 
			get {
				IntPtr raw_ret = g_application_command_line_get_type();
				GLib.GType ret = new GLib.GType(raw_ret);
				return ret;
			}
		}

		[DllImport("libgio-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr g_application_command_line_getenv(IntPtr raw, IntPtr name);

		public string Getenv(string name) {
			IntPtr native_name = GLib.Marshaller.StringToPtrGStrdup (name);
			IntPtr raw_ret = g_application_command_line_getenv(Handle, native_name);
			string ret = GLib.Marshaller.Utf8PtrToString (raw_ret);
			GLib.Marshaller.Free (native_name);
			return ret;
		}

#endregion
	}
}
