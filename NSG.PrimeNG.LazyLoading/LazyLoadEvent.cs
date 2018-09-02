// ===========================================================================
using System;
using System.Collections.Generic;
//
namespace NSG.PrimeNG.LazyLoading
{
    //
    /// <summary>
    /// PrimeNG structure
    /// </summary>
    public class LazyLoadEvent
    {
        // {"first":0,"rows":3,"sortOrder":1,
        // "filters":{"ServerId":{"value":1,"matchMode":"eq"},"Mailed":{"value":"false","matchMode":"eq"},"Closed":{"value":"false","matchMode":"eq"},"Special":{"value":"false","matchMode":"eq"}},
        // "globalFilter":null}
        public long first;
        public long rows;
        public string sortField;
        public int sortOrder;
        public object multiSortMeta;
        public Dictionary<string, Object> filters;
        public object globalFilter;
    }
}
// ===========================================================================
