using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance
{
    internal static class SpecificationEvaluator
    {
        public static IQueryable<T> GetQuery<T>(IQueryable<T> inputQuery, Specifications<T> spec) where T : class
        {
            var query = inputQuery;
            if(spec.Criteria is not null) query = query.Where(spec.Criteria);
            query = spec.IncludeExpressions.Aggregate(query, (current, include) => current.Include(include));
            if (spec.OrderBy is not null) query = query.OrderBy(spec.OrderBy);
            else if (spec.OrderByDescending is not null) query = query.OrderByDescending(spec.OrderByDescending);
            if (spec.IsPaginated)
                query = query.Skip(spec.Skip).Take(spec.Take);
            return query;
        }
    }
}
