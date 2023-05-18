
using System.Linq.Expressions;

namespace Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        //I use this ctor with queries that don't have conditions 
        // I use it with GetListOfProduct for example 
        public BaseSpecification()
        {
        }

        //I use it with queries that contain conditions
        // Like GetProductById 
        //Cririria is p=>p.Id 
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        
        public Expression<Func<T, bool>> Criteria {get;}

        public List<Expression<Func<T, object>>> Includes {get;} = 
            new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderBy {get; private set;}

        public Expression<Func<T, object>> OrderByDescending {get; private set;}

        public int Take {get; private set;}
        public int Skip {get; private set;}
        public bool IsPagingEnable {get; private set;}

        protected void AddIncludes(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
        {
            OrderByDescending = orderByDescExpression;
        }

        protected void ApplyPagging(int skip, int take )
        {
            Skip = skip;
            Take = take;
            IsPagingEnable = true;
        }
    }
}