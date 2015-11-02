using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pluginner
{
    public static class EnumExtensions
    {
        public static ICollection<T> AddRange<T>(this ICollection<T> collection, IEnumerable<T> list)
        {
            foreach (T item in list)
            {
                collection.Add(item);
            }
            return collection;
        }

        public static ICollection<T> AddRange<T>(this ICollection<T> collection, T[] list) where T : class
        {
            foreach (T item in list)
            {
                collection.Add(item);
            }
            return collection;
        }
    }
}
