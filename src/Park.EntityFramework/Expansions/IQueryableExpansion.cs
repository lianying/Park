using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Park.Expansions
{
    public static class IQueryableExpansion
    {
        public static IQueryable InculdeIn(this IQueryable queryable,string path)
        {
            return queryable.Include(path);
        }

        public static IQueryable<T> InculdeIn<T, TProperty>(this IQueryable<T> query,Expression<Func<T, TProperty>> expression)
        {
            return query.Include(expression);
        }
    }
}
