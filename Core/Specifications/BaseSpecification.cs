
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
        
        protected void AddIncludes(Expression<Func<T, object>> includeExpression){
            Includes.Add(includeExpression);
        }
    }
}