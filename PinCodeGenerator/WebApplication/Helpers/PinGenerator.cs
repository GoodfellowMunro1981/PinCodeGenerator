using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace WebApplication
{
    public static class PinGenerator
    {
        public static IEnumerable<int> GetNumberEnumerable(int min, int max)
        {
            if (!min.IsNegative() && !max.IsNegative() && max > min)
            {
                var count = max - min + 1;
                return Enumerable.Range(min, count);
            }

            return Enumerable.Empty<int>();
        }
    }
}
