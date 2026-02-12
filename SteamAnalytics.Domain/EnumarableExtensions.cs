using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamAnalytics.Domain {
    public static class EnumerableExtensions {
        public static IEnumerable<List<T>> Batch<T>(
            this IEnumerable<T> source,
            int size) {

            var batch = new List<T>(size);

            foreach (var item in source) {
                batch.Add(item);
                if (batch.Count == size) {
                    yield return batch;
                    batch = new List<T>(size);
                }
            }

            if (batch.Count > 0)
                yield return batch;
        }
    }

}
