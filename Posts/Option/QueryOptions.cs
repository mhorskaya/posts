using System;
using System.Collections.Generic;

namespace Posts.Option
{
    public class QueryOptions : IQueryOptions
    {
        public bool QueryAll { get; set; }
        public IEnumerable<int> Ids { get; set; }

        public static IQueryOptions GetQueryOptions(IOptions options)
        {
            if (options.Query == "all")
            {
                return new QueryOptions { QueryAll = true };
            }

            var ids = new List<int>();

            foreach (var str in options.Query.Split(","))
            {
                if (int.TryParse(str, out var id))
                {
                    ids.Add(id);
                }
                else
                {
                    throw new ArgumentException("Invalid query argument. Expected (all | <comma_separated_numbers>)");
                }
            }

            return new QueryOptions { Ids = ids };
        }
    }
}