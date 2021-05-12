using System;
using System.Linq;

namespace Syncfusion.BlazorControlTesting.Components
{
    public static class TypeHelper
    {
        public static bool IsTypeNullable<T>()
        {
            Type type = typeof(T);
            if (Nullable.GetUnderlyingType(type) != null) return true; // Nullable<T>
            return false; // value-type
        }

        private static Type[]? IsTypeFloatingPointLookup { get; set; }
        public static bool IsTypeFloatingPoint<T>()
        {
            if (IsTypeFloatingPointLookup == null)
            {
                IsTypeFloatingPointLookup = new Type[] {
                    typeof(float),
                    typeof(double),
                    typeof(decimal),
                    typeof(float?),
                    typeof(double?),
                    typeof(decimal?)
                };
            }
            var isFloatingPoint = IsTypeFloatingPointLookup.Contains(typeof(T));

            return isFloatingPoint;
        }
    }
}