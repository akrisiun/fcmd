using System;
using System.Collections.Generic;
using System.Dynamic;

namespace AiLib.Reflection
{
    public static class ExpandoUtils
    {
        public static object Data(this object[] obj, int index)
        {
            return obj[index];
        }

        public static ICollection<string> Keys(this ExpandoObject obj)
        {
            return (obj as IDictionary<string, object>).Keys;
        }

        // Unsafe
        public static object Val(this ExpandoObject obj, string key)
        {
            return (obj as IDictionary<string, object>)[key];
        }

        //public static SqlField[] DynFields(this ExpandoObject obj, Type type = null)
        //{
        //    var fields = new Collection<SqlField>();
        //    foreach (string item in obj.Keys())
        //    {
        //        var field = new SqlField
        //        {
        //            Ordinal = fields.Count,
        //            Name = item,
        //            Caption = StringExt.Proper(item)
        //        };
        //        fields.Add(field);
        //    }

        //    return System.Linq.Enumerable.ToArray(fields);
        //}

        public static T ValConvert<T>(this ExpandoObject obj, string key) where T : IConvertible
        {
            return (T)(obj as IDictionary<string, object>)[key];
        }

        public static T ValObj<T>(this ExpandoObject obj, string key) where T : class
        {
            return (T)(obj as IDictionary<string, object>)[key];
        }

        public static object ValAt(this ExpandoObject obj, int index)
        {
            return System.Linq.Enumerable.ElementAt((obj as IDictionary<string, object>).Values, index);
        }
    }
}