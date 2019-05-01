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
    ///     .LazyOrderBy(_loadEvent)
    ///     .LazyFilters(_loadEvent)
    ///     .LazySkipTake(_loadEvent).ToList();
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
        /// <param name="lle">PrimeNG lazy loading event (LazyLoadEvent) structure</param>
        /// <returns>IQueryable query of T (with ascending or descending sort applied)</returns>
        public static IQueryable<T> LazyOrderBy<T>(
                this IQueryable<T> qry, LazyLoadEvent lle)
        {
            ParameterExpression _parm = Expression.Parameter(typeof(T));
            MemberExpression memberAccess = Expression.PropertyOrField(_parm, lle.sortField);
            LambdaExpression keySelector = Expression.Lambda(memberAccess, _parm);
            //
            MethodCallExpression orderBy = Expression.Call(
                typeof(Queryable),
                (lle.sortOrder == 1 ? "OrderBy" : "OrderByDescending" ),
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
        public static IQueryable<T> LazySkipTake<T>(
                this IQueryable<T> qry, LazyLoadEvent lle)
        {
            if (lle.rows > 0)
            {
                qry = qry.Skip((int)lle.first).Take((int)lle.rows);
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
                    Dictionary<string, Object> _value =
                            ((Dictionary<string, Object>)_o.Value);
                    var whereClause = LazyDynamicFilterExpression<T>(_o.Key,
                            (string)_value["matchMode"], _value["value"].ToString(), _type);
                    qry = qry.Where(whereClause);
                }
            }
            return qry;
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
        /// <param name="op">
        ///  A string representing an operator (see above list of operators).
        /// </param>
        /// <param name="value">A string representation of the value.</param>
        /// <param name="valueType">The underlying type of the value</param>
        /// <returns>
        ///  An expression that can be used for querying data sets
        ///  (Expression&lt;Func&lt;TEntity, bool&gt;&gt;)
        /// </returns>
        private static Expression<Func<TEntity, bool>>
            LazyDynamicFilterExpression<TEntity>(
                string propertyName, string op, string value, Type valueType)
        {
            Type type = typeof(TEntity);
            object asType = AsType(value, valueType);
            ParameterExpression p = Expression.Parameter(type, "x");
            MemberExpression member = Expression.Property(p, propertyName);
            string _stringValue = asType.ToString();
            ConstantExpression valueExpression = Expression.Constant(asType);
            //
            MethodInfo method;
            Expression q;
            //
            switch (op.ToLower())
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
                    throw new ArgumentOutOfRangeException(nameof(op), op, null);
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
