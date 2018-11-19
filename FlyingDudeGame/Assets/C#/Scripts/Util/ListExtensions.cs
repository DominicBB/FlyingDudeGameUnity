using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ListExtensions {
    public static class ListExtensions
    {
        public static T RemoveAndGet<T>(this IList<T> list, int index)
        {
            lock (list)
            {
                T value = list[index];
                list.RemoveAt(index);
                return value;
            }
        }
    }
}
