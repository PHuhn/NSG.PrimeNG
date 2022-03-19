// ===========================================================================
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
//
namespace NSG.PrimeNG.LazyLoading
{
    //
    /// <summary>
    /// PrimeNG structure, used by lazy loading feature.
    /// Class LazyLoadEvent ported from PrimeNG to this library.
    /// Generally, populated by the PrimeNG filter feature.
    /// <example>
    /// An example of the JSON:
    /// {"first":0,"rows":3,"sortOrder":1,
    ///   "filters":{"ServerId":{"value":1,"matchMode":"eq"},
    ///     "Mailed":{"value":"false","matchMode":"eq"},
    ///     "Closed":{"value":"false","matchMode":"eq"},
    ///     "Special":{"value":"false","matchMode":"eq"}},
    ///    "globalFilter":null}
    /// </example>
    /// </summary>
    public class LazyLoadEvent
    {
        // {"first":0,"rows":3,"sortOrder":1,
        // "filters":{"ServerId":{"value":1,"matchMode":"eq"},"Mailed":{"value":"false","matchMode":"eq"},"Closed":{"value":"false","matchMode":"eq"},"Special":{"value":"false","matchMode":"eq"}},
        // "globalFilter":null}
        /// <summary>
        /// First record #.
        /// </summary>
        public long first;
        /// <summary>
        /// # of rows to return (page size).
        /// </summary>
        public long rows;
        /// <summary>
        /// Sort field.
        /// </summary>
        public string sortField;
        /// <summary>
        /// Ascending or desending sort order.
        /// </summary>
        /// <value> 1 = asc, -1 = desc</value>
        public int sortOrder;
        /// <summary>
        /// multiSortMeta, not implemented.
        /// </summary>
        public object multiSortMeta;
        /// <summary>
        /// A dictionary of filters.
        /// Key of the dictionary is the field name, object is value(s)
        /// and match mode.
        /// </summary>
        /// <example>
        /// "filters":{"ServerId":{"value":1,"matchMode":"eq"},
        ///     "Mailed":{"value":"false","matchMode":"eq"},
        ///     "Closed":{"value":"false","matchMode":"eq"},
        ///     "Special":{"value":"false","matchMode":"eq"}},
        /// </example>
        public Dictionary<string, FilterMetadata> filters;
        /// <summary>
        /// globalFilter, not implemented.
        /// </summary>
        public object globalFilter;
        //
        /// <summary>
        /// Returns a string that represents of the current object.
        /// This method overrides the default 'to string' method.
        /// </summary>
        /// <returns>
        /// A formatted string of the object's values.
        /// </returns>
        public override string ToString()
        {
            string _null = "/null/";
            StringBuilder _return = new StringBuilder("LazyLoadEvent:[");
            _return.AppendFormat("first: {0}, rows: {1}, ", first, rows);
            _return.AppendFormat("sortField: {0}, sortOrder: {1}, ", sortField != null ? sortField : _null, sortOrder);
            _return.AppendFormat("multiSortMeta: {0}, ", multiSortMeta != null ? multiSortMeta.ToString() : _null);
            _return.AppendFormat("filters: ");
            if (filters != null)
            {
                foreach (KeyValuePair<string, FilterMetadata> metadata in filters)
                {
                    _return.AppendFormat("{0}-{1}:{2}:{3}, ", metadata.Key, 
                        metadata.Value.matchMode, metadata.Value.value, metadata.Value.@operator != null ? metadata.Value.@operator : "/null/");
                }
            }
            else
            {
                _return.AppendFormat("{0}, ", _null);
            }
            _return.AppendFormat("globalFilter: {0}]", globalFilter != null ? globalFilter.ToString() : "/null/");
            return _return.ToString();
        }
    }
}
// ===========================================================================
