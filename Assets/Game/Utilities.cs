using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public static class Utilities
    {
        public static void Apply<T>(this IEnumerable<T> enumerable, Action<T> func, Predicate<T> predicate = null)
        {
            predicate ??= _ => true;
            
            foreach (var element in enumerable)
            {
                if (predicate(element))
                    func(element);
            }
        }
        public static float Abs(this float value)
        {
            return Mathf.Abs(value);
        }
        public static float Clamp(this float value, float minValue, float maxValue)
        {
            return Mathf.Clamp(value, minValue, maxValue);
        }
    }
}