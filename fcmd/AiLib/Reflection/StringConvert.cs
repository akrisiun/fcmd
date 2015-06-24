using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AiLib.Reflection
{
    public static class StringConvert
    {
        public static string StrLeft(this string str, int left)
        {
            if (str == null || str.Length < left)
                return str ?? String.Empty;
            return str.Substring(0, left);
        }

        public static string StrLeft(object obj, int left)
        {
            if (obj == null)
                return String.Empty;
            var str  = obj is string ? obj as string : obj.ToString();
            if (str.Length < left)
                return str ?? String.Empty;
            return str.Substring(0, left);
        }

        public static bool ContainsCase(this string str, string substring, 
            StringComparison comparisonType = StringComparison.InvariantCultureIgnoreCase)
        {
            return str.IndexOf(substring, comparisonType: comparisonType) >= 0;
        }

        public static bool EqualsCase(this string str, object obj, 
            StringComparison comparisonType = StringComparison.InvariantCultureIgnoreCase)
        {
            if (str == null && obj == null)
                return false;

            var objStr = obj is string ? obj as string : obj.ToString();
            return String.Equals(str, objStr, comparisonType: comparisonType);
        }

        public static bool StrEquals<T>(this T objA, T objB,
          StringComparison comparisonType = StringComparison.InvariantCulture) where T : class
        {
            if (objA == null || objB == null)
                return false;
            if (Object.ReferenceEquals(objA, objB))
                return true;

            var strA = objA.ToString();
            var strB = objB.ToString();
            if (string.IsNullOrWhiteSpace(strA) && string.IsNullOrWhiteSpace(strB))
                return true;

            return string.Equals(strA, strB, comparisonType: comparisonType);
        }

        public static bool IsArrayEmpty(this object[] data)
        {
            if (data == null || data.Length == 0)
                return true;
            int index = 0;
            while (index < data.Length)
            {
                if (data[index] == null || string.IsNullOrWhiteSpace(data[index].ToString()) )
                    return true;
                index++;
            }

            return false;
        }

        public static string[] NewLines = new string[] { System.Environment.NewLine };
        public static char[] NewChar = new char[] { '\n' };
        public static string NewCharRemove = "\r";

        // Safe null
        public static string[] SplitNewLines(this string str)
        {
            return str == null ? null : str.Split(NewLines, options: StringSplitOptions.RemoveEmptyEntries);
        }

        public static string[] SplitLines(this string str)
        {
            if (str == null) return new string[1] { null };

            var fixStr = str.Replace(NewCharRemove, string.Empty);
            return fixStr.Split(NewChar, options: StringSplitOptions.None);
        }

        // Safe null
        public static string PadRight(this string str, int len, char paddingChar = ' ')
        {
            return str == null ? null : str.PadRight(len, paddingChar);
        }
    }
}
