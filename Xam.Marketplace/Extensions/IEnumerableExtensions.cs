using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xam.Marketplace.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> Foreach<T>(this IEnumerable<T> source, Func<T,T> function) {
            if (!(source?.Count() > 0)) yield return default(T);

            foreach (var item in source)
            {
                yield return function.Invoke(item);
            }
        }
    }
}
