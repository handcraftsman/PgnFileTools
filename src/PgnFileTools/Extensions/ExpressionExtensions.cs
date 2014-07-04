using System;
using System.Linq.Expressions;

namespace PgnFileTools.Extensions
{
    public static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> AndWith<T>(this Expression<Func<T, bool>> expression1, Expression<Func<T, bool>> expression2)
        {
            if (expression1 == null)
            {
                return expression2;
            }
            // http://stackoverflow.com/questions/13967523/andalso-between-several-expressionfunct-bool-referenced-from-scope
            var param = expression1.Parameters[0];
            if (ReferenceEquals(param, expression2.Parameters[0]))
            {
                // simple version
                return Expression.Lambda<Func<T, bool>>(
                    Expression.AndAlso(expression1.Body, expression2.Body), param);
            }
            // otherwise, keep expr1 "as is" and invoke expr2
            return Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(
                    expression1.Body,
                    Expression.Invoke(expression2, param)), param);
        }
    }
}