using System.Linq.Expressions;

namespace FullSharedCore.Extensions.Predicates
{
    //object result = Activator.CreateInstance(inputType);
    //PropertyInfo prop = inputType.GetProperty(propName);
    //prop.SetValue(result, itemFirst);
    public static class FilterPredicate
    {
        public static Expression<Predicate<T>> PredicateGetIds<T, R>(this List<int> list, Expression<Func<T, R>> selector)
        {

            var itemFirst = list.FirstOrDefault();
            list.RemoveAt(0);
            var parameterFirst = selector.Parameters[0]; //u
            var leftFirst = selector.Body;//u.Id
            var rightFirst = Expression.Constant(itemFirst); //10
            var binaryExpressionFirst = Expression.MakeBinary(ExpressionType.Equal, leftFirst, rightFirst);  //u.Id==10


            BinaryExpression resultBinary = default;
            bool firstRow = true;
            foreach (int i in list)
            {
                var parameter = selector.Parameters[0];
                var left = selector.Body;
                var right = Expression.Constant(i);
                var binaryExpression = Expression.MakeBinary(ExpressionType.Equal, left, right);
                if (firstRow)
                {
                    firstRow = false;
                    resultBinary = Expression.Or(binaryExpressionFirst, binaryExpression);
                }
                else
                {
                    resultBinary = Expression.Or(resultBinary, binaryExpression);
                }
            }

            //var inputType = typeof(int);
            //var left = Expression.Parameter(inputType, "left");
            //var rightLeft = Expression.Parameter(inputType, "rightLeft");
            //var rightRight = Expression.Parameter(inputType, "rightRight");
            //var multiply = Expression.Multiply(rightLeft, rightRight);
            //var add = Expression.Add(left, multiply);

            //var lambda = Expression.Lambda<Func<int, int, int, int>>(add, left, rightLeft, rightRight);
            //Console.WriteLine(lambda.ToString()); // will print "(left, rightLeft, rightRight) => (left + (rightLeft * rightRight))"
            //var result = lambda.Compile().Invoke(1, 2, 3);


            return Expression.Lambda<Predicate<T>>(resultBinary, parameterFirst);
        }


        public static Expression<Func<T, bool>> PredicateGetIdsTest<T>(this List<int> list, string propertyName)
        {
            var propertyValue = list.FirstOrDefault();
            list.RemoveAt(0);

            var propType = typeof(int);
            var parameter = Expression.Parameter(typeof(T));
            //var left = Expression.Property(expression, propertyName);
            var left = Expression.Parameter(propType, propertyName);
            var right = Expression.Constant(propertyValue);
            var body = Expression.Equal(left, right);
            var predicate = Expression.Lambda<Func<T, bool>>(body, new ParameterExpression[] { parameter });
            string ff = predicate.ToString();
            return predicate;


            /*
            var inputType = typeof(T);
            var leftFisrst = Expression.Parameter(inputType);

          

            
            var propType = typeof(int); 
            var leftFirst = Expression.Parameter(propType, $"u.{propertyName}"); 
            var binaryExpressionFirst = Expression.MakeBinary(ExpressionType.Equal, leftFirst, Expression.Constant(propertyValue, propType));
           


            BinaryExpression resultBinary = default;
            BinaryExpression BinaryFirst = default;
            bool firstRow = true;
            foreach (int i in list)
            { 
                BinaryFirst = Expression.MakeBinary(ExpressionType.Equal, leftFirst, Expression.Constant(i, propType));
                if (firstRow)
                {
                    firstRow = false;
                    resultBinary = Expression.Or(binaryExpressionFirst, BinaryFirst);
                }
                else
                {
                    resultBinary = Expression.Or(resultBinary, BinaryFirst);
                }
            }




            return Expression.Lambda<Func<T, bool>>(resultBinary, leftFisrst);
             */
        }
        //static Predicate<T> ConvertToPredicate<T>(Func<T, bool> func)
        //{
        //    return new Predicate<T>(func);
        //}
        //public static IQueryable<T> WhereX<T>(this IQueryable<T> source, Predicate<T> predicate)
        //{
        //    return source.Where(x => predicate(x));
        //}

        //public static IQueryable<T> Filter<T>(this IQueryable<T> query, string propertyName, object propertyValue)
        //{
        //    var parameter = Expression.Parameter(typeof(T));
        //    var left = Expression.Property(expression, propertyName);
        //    var right = Expression.Constant(propertyValue);
        //    var body = Expression.Equal(left, right);
        //    var predicate = Expression.Lambda<Func<T, bool>>(body, new ParameterExpression[] { parameter });

        //    return query.Where(predicate);
        //}



        private static Expression<Predicate<T>> PredicateGetFilter<T, R>(Expression<Func<T, R>> selector, FilterOps operand, R value)
        {
            var parameter = selector.Parameters[0];
            var left = selector.Body;
            var right = Expression.Constant(value);
            var binaryExpression = Expression.MakeBinary(operand.ToExpressionType(), left, right);
            return Expression.Lambda<Predicate<T>>(binaryExpression, parameter);
        }




        public enum FilterOps
        {
            GreaterThan, LessThan, Equal, NotEqual, LessThanOrEqual, GreaterThanOrEqual
        }

        public static ExpressionType ToExpressionType(this FilterOps operand)
        {

            switch (operand)
            {
                case FilterOps.GreaterThan: return ExpressionType.GreaterThan;
                case FilterOps.LessThan: return ExpressionType.LessThan;
                case FilterOps.Equal: return ExpressionType.Equal;
                case FilterOps.NotEqual: return ExpressionType.NotEqual;
                case FilterOps.LessThanOrEqual: return ExpressionType.LessThanOrEqual;
                case FilterOps.GreaterThanOrEqual: return ExpressionType.GreaterThanOrEqual;
                default: throw new NotSupportedException();
            }
        }
    }

}
