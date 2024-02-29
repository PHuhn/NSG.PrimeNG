# NSG.PrimeNG.LazyLoading Namespace
 

The NSG.PrimeNG.LazyLoading namespace contains a class used by lazy loading feature and filter features. The lazy loading feature allows one to return a page of data and combined with the filtering and sorting features gives a rich feature of transferring large set of data efficiently.


## Classes
&nbsp;<table><tr><th></th><th>Class</th><th>Description</th></tr><tr><td>![Public class](media/pubclass.gif "Public class")</td><td><a href="49f42670-211f-4806-5d61-22fa57ea9333">FilterMetadata</a></td><td>
Json of class: {"value":"S","matchMode":"startsWith","operator":"and"}</td></tr><tr><td>![Public class](media/pubclass.gif "Public class")</td><td><a href="52bd568b-6e32-15c3-9eb2-39baac962d10">Helpers</a></td><td>
Set of static helper methods, meant to be used as extension methods.</td></tr><tr><td>![Public class](media/pubclass.gif "Public class")![Code example](media/CodeExample.png "Code example")</td><td><a href="f5f3ed8c-72eb-3292-5e30-9b3b771141fd">LazyLoadEvent</a></td><td>
PrimeNG structure, used by lazy loading feature. Class LazyLoadEvent ported from PrimeNG to this library. Generally, populated by the PrimeNG filter feature. 

## Examples
An example of the JSON: {"first":0,"rows":3,"sortOrder":1, "filters":{"ServerId":{"value":1,"matchMode":"eq"}, "Mailed":{"value":"false","matchMode":"eq"}, "Closed":{"value":"false","matchMode":"eq"}, "Special":{"value":"false","matchMode":"eq"}}, "globalFilter":null}</td></tr><tr><td>![Public class](media/pubclass.gif "Public class")![Code example](media/CodeExample.png "Code example")</td><td><a href="7dac6be9-9bab-726a-a0a3-067a4be6363c">LazyLoadEvent2</a></td><td>
PrimeNG structure, used by lazy loading feature. Class LazyLoadEvent ported from PrimeNG to this library. Generally, populated by the PrimeNG filter feature. 

## Examples
An example of the JSON: {"first":0,"rows":5,"sortOrder":1,"filters":{ "NoteTypeDesc":[ {"value":"F","matchMode":"startsWith","operator":"and"}, { "value":"1","matchMode":"contains","operator":"and"}], "NoteTypeShortDesc":[ {"value":"S","matchMode":"startsWith","operator":"and"}] },"globalFilter":null}</td></tr><tr><td>![Public class](media/pubclass.gif "Public class")</td><td><a href="f101f97e-d69b-1cf5-cb00-3a5121639da7">SortMeta</a></td><td>
a list sorting item, can be sorted on multiple fields</td></tr></table>

## Interfaces
&nbsp;<table><tr><th></th><th>Interface</th><th>Description</th></tr><tr><td>![Public interface](media/pubinterface.gif "Public interface")</td><td><a href="712b62aa-7afd-8eda-95ee-2845ca8e8f44">IFilterMetadata</a></td><td>
Json of interface: {"value":"S","matchMode":"startsWith","operator":"and"}</td></tr><tr><td>![Public interface](media/pubinterface.gif "Public interface")</td><td><a href="1650a0bc-51b3-4184-8f4e-70f8dc0462d6">ISortMeta</a></td><td>
a list sorting item, can be sorted on multiple fields</td></tr></table>

## Examples
A full example as follows: 
```
string _jsonString =
    "{\"first\":0,\"rows\":3," +
    "\"sortOrder\":-1,\"sortField\":\"NoteTypeSortOrder\"," +
    "\"filters\":{\"NoteTypeDesc\":{\"value\":\"SO\",\"matchMode\":\"StartsWith\"}}}";
JavaScriptSerializer _js_slzr = new JavaScriptSerializer();
LazyLoadEvent _loadEvent = (LazyLoadEvent)_js_slzr.Deserialize(_jsonString, typeof(LazyLoadEvent));
List<NoteType> _rows = NoteTypes.AsQueryable()
    .LazyOrderBy<NoteType>(_loadEvent)
    .LazyFilters<NoteType>(_loadEvent)
    .LazySkipTake<NoteType>(_loadEvent).ToList();
```


## Examples
A full example of the of corrected LazyLoadEvent2 as follows: 
```
string _pagination = "{\"first\":0,\"rows\":3," +
    "\"sortOrder\":-1,\"sortField\":\"NoteTypeSortOrder\"," +
    "{\"filters\":{\"NoteTypeDesc\":[" +
    "{\"value\":\"SO\",\"matchMode\":\"startsWith\",\"operator\":\"or\"}," +
    "{\"value\":\"6\",\"matchMode\":\"contains\",\"operator\":\"or\"}]}}";
LazyLoadEvent2 _loadEvent = JsonConvert.DeserializeObject<LazyLoadEvent2>(_pagination);
List<NoteType> _rows = NoteTypes.AsQueryable()
    .LazyOrderBy2<NoteType>(_loadEvent)
    .LazyFilters2<NoteType>(_loadEvent)
    .LazySkipTake2<NoteType>(_loadEvent).ToList();
```
