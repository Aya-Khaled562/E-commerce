
using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
        {
            //input query --> _context.Product
            var query = inputQuery;
            if(spec.Criteria != null)
            {
                //spec.Criteria -> p => p.Id == id
                query = query.Where(spec.Criteria); 
            }

            query = spec.Includes.Aggregate(query , (current , include)=> current.Include(include));

            return query;
        }
    }
}