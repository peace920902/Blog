using System.Collections.Generic;
using System.Linq;

namespace Lazcat.Blog.Infrastructure
{
    public static class LinqExtension
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> input)
        {
            return input == null || input.Any();
        }
    }
}