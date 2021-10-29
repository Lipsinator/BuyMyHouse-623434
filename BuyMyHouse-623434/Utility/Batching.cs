using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class Batching
    {
        public static IEnumerable<IEnumerable<T>> CreateBatch<T>(IEnumerable<T> lists, int size)
        {
            return lists
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / size)
                .Select(x => x.Select(v => v.Value));
        }
    }
}
