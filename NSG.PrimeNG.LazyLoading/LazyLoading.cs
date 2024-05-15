// ===========================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Linq.Expressions;
//
namespace NSG.PrimeNG.LazyLoading
{
    /// <summary>
    /// The <see cref="NSG.PrimeNG.LazyLoading"/> namespace contains a class
    /// used by lazy loading feature and filter features.
    /// 
    /// The lazy loading feature allows one to return a page of data
    /// and combined with the filtering and sorting features gives
    /// a rich feature of transferring large set of data efficiently.
    /// </summary>
    /// <example>
    /// A full example as follows:
    /// <code>
    /// string _jsonString =
    ///     "{\"first\":0,\"rows\":3," +
    ///     "\"sortOrder\":-1,\"sortField\":\"NoteTypeSortOrder\"," +
    ///     "\"filters\":{\"NoteTypeDesc\":{\"value\":\"SO\",\"matchMode\":\"StartsWith\"}}}";
    /// JavaScriptSerializer _js_slzr = new JavaScriptSerializer();
    /// LazyLoadEvent _loadEvent = (LazyLoadEvent)_js_slzr.Deserialize(_jsonString, typeof(LazyLoadEvent));
    /// List&lt;NoteType&gt; _rows = NoteTypes.AsQueryable()
    ///     .LazyOrderBy&lt;NoteType&gt;(_loadEvent)
    ///     .LazyFilters&lt;NoteType&gt;(_loadEvent)
    ///     .LazySkipTake&lt;NoteType&gt;(_loadEvent).ToList();
    /// </code>
    /// </example>
    /// <example>
    /// A full example of the of corrected LazyLoadEvent2 as follows:
    /// <code>
    /// string _pagination = "{\"first\":0,\"rows\":3," +
    ///     "\"sortOrder\":-1,\"sortField\":\"NoteTypeSortOrder\"," +
    ///     "{\"filters\":{\"NoteTypeDesc\":[" +
    ///     "{\"value\":\"SO\",\"matchMode\":\"startsWith\",\"operator\":\"or\"}," +
    ///     "{\"value\":\"6\",\"matchMode\":\"contains\",\"operator\":\"or\"}]}}";
    /// LazyLoadEvent2 _loadEvent = JsonConvert.DeserializeObject&lt;LazyLoadEvent2&gt;(_pagination);
    /// List&lt;NoteType&gt; _rows = NoteTypes.AsQueryable()
    ///     .LazyOrderBy2&lt;NoteType&gt;(_loadEvent)
    ///     .LazyFilters2&lt;NoteType&gt;(_loadEvent)
    ///     .LazySkipTake2&lt;NoteType&gt;(_loadEvent).ToList();
    /// </code>
    /// </example>
    [System.Runtime.CompilerServices.CompilerGenerated]
    class NamespaceDoc
    {
    }
    //
    /// <summary>
    /// Set of static helper methods, meant to be used as extension methods.
    /// </summary>
    public static partial class Helpers
    {
        //
        /// <summary>
        /// Sort this IQueryable, with:
        ///  sortField and
        ///  sortOrder 1=ascending
        ///           -1=descending
        /// <example> 
        /// This sample shows how to call this method, where _incidentQuery
        /// is IQueryable of Incident:
        /// <code>
        ///   JavaScriptSerializer _jsSlzr = new JavaScriptSerializer();
        ///   _loadEvent = (LazyLoadEvent) _jsSlzr.Deserialize( jsonString, typeof(LazyLoadEvent) );
        ///   _incidentQuery = _incidentQuery.LazyOrderBy( _loadEvent );
        /// </code>
        /// </example>
        /// <note type="note">
        ///  'OrderBy' must be called before the method 'Skip'.
        /// </note>
        /// </summary>
        /// <typeparam name="T">Some class (database)</typeparam>
        /// <param name="qry">IQueryable query of T (above class)</param>
        /// <param name="lle">PrimeNG lazy loading event (LazyLoadEvent2) structure</param>
        /// <returns>IQueryable query of T (with ascending or descending sort applied)</returns>
        public static IQueryable<T> LazyOrderBy2<T>(
                this IQueryable<T> qry, LazyLoadEvent2 lle)
        {
            return qry.OrderBy<T>(lle.sortField, lle.sortOrder);
        }
        public static IQueryable<T> LazyOrderBy<T>(
                this IQueryable<T> qry, LazyLoadEvent lle)
        {
            return qry.OrderBy<T>(lle.sortField, lle.sortOrder);
        }
        private static IQueryable<T> OrderBy<T>(
                this IQueryable<T> qry, string sortField, int sortOrder)
        {
            ParameterExpression _parm = Expression.Parameter(typeof(T));
            MemberExpression memberAccess = Expression.PropertyOrField(_parm, sortField);
            LambdaExpression keySelector = Expression.Lambda(memberAccess, _parm);
            //
            MethodCallExpression orderBy = Expression.Call(
                typeof(Queryable),
                (sortOrder == 1 ? "OrderBy" : "OrderByDescending"),
                new Type[] { typeof(T), memberAccess.Type },
                qry.Expression,
                Expression.Quote(keySelector));
            //
            return qry.Provider.CreateQuery<T>(orderBy);
        }
        //
        /// <summary>
        ///  Skip forward in the database and take n # of rows
        /// <example> 
        /// This sample shows how to call this method, where _incidentQuery
        /// is IQueryable of Incident:
        /// <code>
        ///   JavaScriptSerializer _jsSlzr = new JavaScriptSerializer();
        ///   _loadEvent = (LazyLoadEvent) _jsSlzr.Deserialize( jsonString, typeof(LazyLoadEvent) );
        ///   _incidentQuery = _incidentQuery.LazySkipTake( _loadEvent );
        /// </code>
        /// </example>
        /// <note type="note">
        ///  'OrderBy' must be called before the method 'Skip'.
        /// </note>
        /// </summary>
        /// <typeparam name="T">Some class (database)</typeparam>
        /// <param name="qry">IQueryable query of T (above class)</param>
        /// <param name="lle">PrimeNG lazy loading event (LazyLoadEvent) structure</param>
        /// <returns>IQueryable query of T (with skip/take applied)</returns>
        public static IQueryable<T> LazySkipTake2<T>(this IQueryable<T> qry, LazyLoadEvent2 lle)
        {
            return qry.SkipTake<T>((int)lle.first, (int)lle.rows);
        }
        public static IQueryable<T> LazySkipTake<T>( this IQueryable<T> qry, LazyLoadEvent lle)
        {
            return qry.SkipTake<T>((int)lle.first, (int)lle.rows);
        }
        private static IQueryable<T> SkipTake<T>(this IQueryable<T> qry, int first, int rows)
        {
            if (rows > 0)
            {
                qry = qry.Skip((int)first).Take(rows);
            }
            return qry;
        }
        //
        /// <summary>
        ///  Apply filter to an IQueryable from PrimeNG request.
        ///  Filter:
        ///   key of the dictionary is the field name,
        ///   object is value(s) and match mode
        /// <example> 
        /// This sample shows how to call this method, where _incidentQuery
        /// is IQueryable of Incident:
        /// <code>
        ///   JavaScriptSerializer _jsSlzr = new JavaScriptSerializer();
        ///   _loadEvent = (LazyLoadEvent) _jsSlzr.Deserialize( jsonString, typeof(LazyLoadEvent) );
        ///   _incidentQuery = _incidentQuery.LazyFilters( _loadEvent );
        /// </code>
        /// </example>
        /// </summary>
        /// <typeparam name="T">Some class (database)</typeparam>
        /// <param name="qry">IQueryable query of T (above class)</param>
        /// <param name="lle">PrimeNG lazy loading event (LazyLoadEvent) structure</param>
        /// <returns>IQueryable query of T (with where filters applied)</returns>
        public static IQueryable<T> LazyFilters<T>(
                this IQueryable<T> qry, LazyLoadEvent lle)
        {
            if (lle.filters != null)
            {
                foreach (var _o in lle.filters)
                {
                    PropertyInfo _propertyInfo = typeof(T).GetProperty(_o.Key);
                    Type _type = _propertyInfo.PropertyType;
                    FilterMetadata _value = _o.Value;
                    if(_value != null)
                    {
                        if(_value.value != null )
                        {
                            var whereClause = LazyDynamicFilterExpression<T>(_o.Key,
                                    (string)_value.matchMode, _value.value.ToString(), _type, "");
                            qry = qry.Where(whereClause);
                        }
                    }
                }
            }
            return qry;
        }
        public static IQueryable<T> LazyFilters2<T>(
                this IQueryable<T> qry, LazyLoadEvent2 lle)
        {
            if (lle.filters != null)
            {
                foreach (var _f in lle.filters)
                {
                    PropertyInfo _propertyInfo = typeof(T).GetProperty(_f.Key);
                    Type _type = _propertyInfo.PropertyType;
                    FilterMetadata[] _values = _f.Value.Where(f => f.value != null).ToArray();
                    if( _values != null && _values.Length != 0)
                    {
                        var count = 1;
                        Expression<Func<T, bool>> whereClause = null;
                        foreach (FilterMetadata _m in _values)
                        {
                            var whereTemp = LazyDynamicFilterExpression<T>(_f.Key,
                                    (string)_m.matchMode, _m.value.ToString(), _type, _m.@operator);
                            if (count == 1)
                            {
                                whereClause = whereTemp;
                            }
                            else
                            {
                                if (_m.@operator.ToLower() == "or")
                                {
                                    whereClause = whereClause.OrExpression(whereTemp);
                                }
                                else
                                {
                                    whereClause = whereClause.AndExpression(whereTemp);
                                }
                            }
                            count++;
                        }
                        qry = qry.Where(whereClause);
                    }
                }
            }
            return qry;
        }
        /// <summary>
        /// Implement an Or conditional
        /// </summary>
        /// <typeparam name="T">Some class (database)</typeparam>
        /// <param name="left">The left (this) expression</param>
        /// <param name="right">The right expression</param>
        /// <returns>An expression that can be used for querying data sets</returns>
        public static Expression<Func<T, bool>> OrExpression<T>(this Expression<Func<T, bool>> left,
                                                            Expression<Func<T, bool>> right)
        {
            var invokedExpr = Expression.Invoke(right, left.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.OrElse(left.Body, invokedExpr), left.Parameters);
        }
        //
        /// <summary>
        /// Implement an And conditional
        /// </summary>
        /// <typeparam name="T">Some class (database)</typeparam>
        /// <param name="left">The left (this) expression</param>
        /// <param name="right">The right expression</param>
        /// <returns>An expression that can be used for querying data sets</returns>
        public static Expression<Func<T, bool>> AndExpression<T>(this Expression<Func<T, bool>> left,
                                                                      Expression<Func<T, bool>> right)
        {
            var invokedExpr = Expression.Invoke(right, left.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.AndAlso(left.Body, invokedExpr), left.Parameters);
        }
        //
        // PrimeNG:
        //  "contains", "startsWith", "endsWith", "equals", "notEquals", "in", "lt", "lte", "gt" and "gte".
        /// <summary>
        ///  A method to create an expression dynamically given a generic entity,
        ///  and a propertyName, operator and value.
        ///  <list type="bullet">
        ///   <listheader><description>Operators</description></listheader>
        ///   <item><description>contains</description></item>
        ///   <item><description>startsWith</description></item>
        ///   <item><description>endsWith</description></item>
        ///   <item><description>equals</description></item>
        ///   <item><description>notEquals</description></item>
        ///   <item><description>lt</description></item>
        ///   <item><description>lte</description></item>
        ///   <item><description>gt</description></item>
        ///   <item><description>gte</description></item>
        /// </list>
        ///  <note type="note">
        ///   The following code mostly come from:
        ///   https://stackoverflow.com/questions/2497303/how-to-specify-dynamic-field-names-in-a-linq-where-clause
        ///  </note>
        ///  <note type="note">
        ///   The 'in' operator is not handled and will throw an exception:
        ///   <exception cref="ArgumentOutOfRangeException">Unhandled or invalid operators</exception>
        ///  </note>
        /// </summary>
        /// <typeparam name="TEntity">
        ///  The class to create the expression for. Most commonly an entity framework
        ///  entity that is used for a DbSet.
        /// </typeparam>
        /// <param name="propertyName">A string value of the property name.</param>
        /// <param name="match">
        ///  A string representing an operator (see above list of operators).
        /// </param>
        /// <param name="value">A string representation of the value.</param>
        /// <param name="valueType">The underlying type of the value</param>
        /// <param name="op">operator and/or</param>
        /// <returns>
        ///  An expression that can be used for querying data sets
        ///  (Expression&lt;Func&lt;TEntity, bool&gt;&gt;)
        /// </returns>
        private static Expression<Func<TEntity, bool>>
            LazyDynamicFilterExpression<TEntity>(
                string propertyName, string match, string value, Type valueType, string op)
        {
            Type type = typeof(TEntity);
            object asType = AsType(value, valueType);
            ParameterExpression p = Expression.Parameter(type, type.Name);
            MemberExpression member = Expression.Property(p, propertyName);
            string _stringValue = asType.ToString();
            ConstantExpression valueExpression = Expression.Constant(asType);
            //
            MethodInfo method;
            Expression q;
            //
            switch (match.ToLower())
            {
                case "gt":
                    q = Expression.GreaterThan(member, valueExpression);
                    break;
                case "lt":
                    q = Expression.LessThan(member, valueExpression);
                    break;
                case "equals":
                    q = Expression.Equal(member, valueExpression);
                    break;
                case "lte":
                    q = Expression.LessThanOrEqual(member, valueExpression);
                    break;
                case "gte":
                    q = Expression.GreaterThanOrEqual(member, valueExpression);
                    break;
                case "notequals":
                    q = Expression.NotEqual(member, valueExpression);
                    break;
                case "contains":
                    method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                    q = Expression.Call(member, method ?? throw new InvalidOperationException(),
                        Expression.Constant(_stringValue, typeof(string)));
                    break;
                case "startswith":
                    method = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });
                    q = Expression.Call(
                        member, 
                        method ?? throw new InvalidOperationException(),
                        Expression.Constant(_stringValue, typeof(string)));
                    break;
                case "endswith":
                    method = typeof(string).GetMethod("EndsWith", new[] { typeof(string) });
                    q = Expression.Call(member, method ?? throw new InvalidOperationException(),
                        Expression.Constant(_stringValue, typeof(string)));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(match), $"filter matchMode of: '{match}', not gt/lt/equals/lte/gte/notequals/contains/startswith/endswith");
            }
            //
            return Expression.Lambda<Func<TEntity, bool>>(q, p);
        }
        //
        /// <summary>
        ///  Extract this string value as the passed in object type (convert/cast).
        /// </summary>
        /// <param name="value">The value, as a string</param>
        /// <param name="type">The desired type</param>
        /// <returns>The value, as the specified type</returns>
        private static object AsType(string value, Type type)
        {
            //TODO: This method needs to be expanded to include all appropriate use cases
            string v = value;
            if (value.StartsWith("'") && value.EndsWith("'"))
                v = value.Substring(1, value.Length - 2);
            if (value.StartsWith("\"") && value.EndsWith("\""))
                v = value.Substring(1, value.Length - 2);
            //
            if (type == typeof(string)) return v;
            if (type == typeof(DateTime)) return DateTime.Parse(v);
            if (type == typeof(DateTime?)) return DateTime.Parse(v);
            if (type == typeof(int)) return int.Parse(v);
            if (type == typeof(int?)) return int.Parse(v);
            if (type == typeof(long) || type == typeof(long?)) return long.Parse(v);
            if (type == typeof(short) || type == typeof(short?)) return short.Parse(v);
            if (type == typeof(byte) || type == typeof(byte?)) return byte.Parse(v);
            if (type == typeof(bool) || type == typeof(bool?)) return bool.Parse(v);
            //
            throw new ArgumentException("NSG.PrimeNG.LazyLoading.Helpers.AsType: " +
                "A filter was attempted for a field with value '" + value + "' and type '" +
                type + "' however this type is not currently supported");
        }
    }
}
// ===========================================================================
