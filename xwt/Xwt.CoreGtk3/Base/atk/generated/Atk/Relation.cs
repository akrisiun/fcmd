// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace Atk {

	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Runtime.InteropServices;

#region Autogenerated code
	public partial class Relation : GLib.Object {

		public Relation (IntPtr raw) : base(raw) {}

		[DllImport("libatk-1.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr atk_relation_new(IntPtr[] targets, int n_targets, int relationship);

		public Relation (Atk.Object[] targets, Atk.RelationType relationship) : base (IntPtr.Zero)
		{
			if (GetType () != typeof (Relation)) {
				throw new InvalidOperationException ("Can't override this constructor.");
			}
			int cnt_targets = targets == null ? 0 : targets.Length;
			IntPtr[] native_targets = new IntPtr [cnt_targets];
			for (int i = 0; i < cnt_targets; i++)
				native_targets [i] = targets[i] == null ? IntPtr.Zero : targets[i].Handle;
			Raw = atk_relation_new(native_targets, (targets == null ? 0 : targets.Length), (int) relationship);
		}

		[DllImport("libatk-1.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern int atk_relation_get_relation_type(IntPtr raw);

		[GLib.Property ("relation_type")]
		public Atk.RelationType RelationType {
			get  {
				int raw_ret = atk_relation_get_relation_type(Handle);
				Atk.RelationType ret = (Atk.RelationType) raw_ret;
				return ret;
			}
			set {
				GLib.Value val = new GLib.Value((Enum) value);
				SetProperty("relation_type", val);
				val.Dispose ();
			}
		}

		[DllImport("libatk-1.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr atk_relation_get_target(IntPtr raw);

		public Atk.Object[] Target {
			get  {
				IntPtr raw_ret = atk_relation_get_target(Handle);
				Atk.Object[] ret = (Atk.Object[]) GLib.Marshaller.PtrArrayToArray (raw_ret, false, false, typeof(Atk.Object));
				return ret;
			}
		}

		[StructLayout (LayoutKind.Sequential)]
		struct AtkRelationClass {
		}

		static uint class_offset = ((GLib.GType) typeof (GLib.Object)).GetClassSize ();
		static Dictionary<GLib.GType, AtkRelationClass> class_structs;

		static AtkRelationClass GetClassStruct (GLib.GType gtype, bool use_cache)
		{
			if (class_structs == null)
				class_structs = new Dictionary<GLib.GType, AtkRelationClass> ();

			if (use_cache && class_structs.ContainsKey (gtype))
				return class_structs [gtype];
			else {
				IntPtr class_ptr = new IntPtr (gtype.GetClassPtr ().ToInt64 () + class_offset);
				AtkRelationClass class_struct = (AtkRelationClass) Marshal.PtrToStructure (class_ptr, typeof (AtkRelationClass));
				if (use_cache)
					class_structs.Add (gtype, class_struct);
				return class_struct;
			}
		}

		static void OverrideClassStruct (GLib.GType gtype, AtkRelationClass class_struct)
		{
			IntPtr class_ptr = new IntPtr (gtype.GetClassPtr ().ToInt64 () + class_offset);
			Marshal.StructureToPtr (class_struct, class_ptr, false);
		}

		[DllImport("libatk-1.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void atk_relation_add_target(IntPtr raw, IntPtr target);

		public void AddTarget(Atk.Object target) {
			atk_relation_add_target(Handle, target == null ? IntPtr.Zero : target.Handle);
		}

		[DllImport("libatk-1.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr atk_relation_get_type();

		public static new GLib.GType GType { 
			get {
				IntPtr raw_ret = atk_relation_get_type();
				GLib.GType ret = new GLib.GType(raw_ret);
				return ret;
			}
		}

		[DllImport("libatk-1.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool atk_relation_remove_target(IntPtr raw, IntPtr target);

		public bool RemoveTarget(Atk.Object target) {
			bool raw_ret = atk_relation_remove_target(Handle, target == null ? IntPtr.Zero : target.Handle);
			bool ret = raw_ret;
			return ret;
		}

		[DllImport("libatk-1.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern int atk_relation_type_for_name(IntPtr name);

		public static Atk.RelationType TypeForName(string name) {
			IntPtr native_name = GLib.Marshaller.StringToPtrGStrdup (name);
			int raw_ret = atk_relation_type_for_name(native_name);
			Atk.RelationType ret = (Atk.RelationType) raw_ret;
			GLib.Marshaller.Free (native_name);
			return ret;
		}

		[DllImport("libatk-1.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr atk_relation_type_get_name(int type);

		public static string TypeGetName(Atk.RelationType type) {
			IntPtr raw_ret = atk_relation_type_get_name((int) type);
			string ret = GLib.Marshaller.Utf8PtrToString (raw_ret);
			return ret;
		}

		[DllImport("libatk-1.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern int atk_relation_type_register(IntPtr name);

		public static Atk.RelationType TypeRegister(string name) {
			IntPtr native_name = GLib.Marshaller.StringToPtrGStrdup (name);
			int raw_ret = atk_relation_type_register(native_name);
			Atk.RelationType ret = (Atk.RelationType) raw_ret;
			GLib.Marshaller.Free (native_name);
			return ret;
		}

#endregion
	}
}
