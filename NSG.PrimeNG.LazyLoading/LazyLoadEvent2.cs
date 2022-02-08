// ===========================================================================
using System;
using System.Text;
using System.Collections.Generic;
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
    /// {"first":0,"rows":5,"sortOrder":1,"filters":{
    ///  "NoteTypeDesc":[
    ///   {"value":"F","matchMode":"startsWith","operator":"and"},
    ///   { "value":"1","matchMode":"contains","operator":"and"}],
    ///  "NoteTypeShortDesc":[
    ///   {"value":"S","matchMode":"startsWith","operator":"and"}]
    /// },"globalFilter":null}
    /// </example>
    /// </summary>
    public class LazyLoadEvent2
    {
        // {"first":0,"rows":3,"sortOrder":1,
        // "filters":{"ServerId":[{"value":1,"matchMode":"eq"}],"Mailed":[{"value":"false","matchMode":"eq"}],"Closed":[{"value":"false","matchMode":"eq"}],"Special":[{"value":"false","matchMode":"eq"}]},
        // "globalFilter":null}
        /// <summary>
        /// First record #.
        /// </summary>
        public long first { set; get; }
        /// <summary>
        /// # of rows to return (page size).
        /// </summary>
        public long rows { set; get; }
        /// <summary>
        /// Sort field.
        /// </summary>
        public string sortField { set; get; }
        /// <summary>
        /// Ascending or desending sort order.
        /// </summary>
        /// <value> 1 = asc, -1 = desc</value>
        public int sortOrder { set; get; }
        /// <summary>
        /// multiSortMeta, not implemented.
        /// </summary>
        public object multiSortMeta { set; get; }
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
        public Dictionary<string, FilterMetadata[]> filters { set; get; }
        /// <summary>
        /// globalFilter, not implemented.
        /// </summary>
        public object globalFilter { set; get; }
        //
        /// <summary>
        /// Returns a string that represents of the current object.
        /// This method overrides the default 'to string' method.
        /// </summary>
        /// <example>
        /// LazyLoadEvent2:[first: 0, rows: 5, sortField: NoteTypeId, sortOrder: -1, multiSortMeta: /null/,
        /// filters: NoteTypeDesc-startsWith:F:and, NoteTypeDesc-contains:1:and, NoteTypeShortDesc-startsWith:S:and, globalFilter: /null/]
        /// </example>
        /// <returns>
        /// A formatted string of the object's values.
        /// </returns>
        public override string ToString()
        {
            StringBuilder _return = new StringBuilder("LazyLoadEvent2:[");
            _return.AppendFormat("first: {0}, rows: {1}, ", first, rows);
            _return.AppendFormat("sortField: {0}, sortOrder: {1}, ", sortField, sortOrder);
            _return.AppendFormat("multiSortMeta: {0}, ", multiSortMeta != null ? multiSortMeta.ToString() : "/null/");
            _return.AppendFormat("filters: ");
            foreach (KeyValuePair<string, FilterMetadata[]> metakey in filters)
            {
                foreach (FilterMetadata metadata in metakey.Value)
                {
                    _return.AppendFormat("{0}-{1}:{2}:{3}, ", metakey.Key,
                        metadata.matchMode, (metadata.value != null ? metadata.value : "/null/"), metadata.@operator != null ? metadata.@operator : "/null/");
                }
            }
            _return.AppendFormat("globalFilter: {0}]", globalFilter != null ? globalFilter.ToString() : "/null/");
            return _return.ToString();
        }
    }
}
// ===========================================================================
