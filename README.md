# NSG.PrimeNG
## Overview
This solution contains .Net Standard projects as follows:
- A collection of support functions for PrimeNG (NSG.Library.PrimeNG)

## The libraries
### NSG.PrimeNG.LazyLoading
NSG.PrimeNG.LazyLoading is a project in support of TurboTable lazy and filter features.  This allows for remote pagination, filtering and sorting using a .Net MVC API.

Example from original LazyLoadEvent unit tests:
```
    string _jsonString =
            "{\"first\":0,\"rows\":3," +
            "\"sortOrder\":-1,\"sortField\":\"NoteTypeSortOrder\"," +
            "\"filters\":{\"NoteTypeDesc\":{\"value\":\"SO\",\"matchMode\":\"StartsWith\"}}}";
    JavaScriptSerializer _js_slzr = new JavaScriptSerializer();
    LazyLoadEvent _loadEvent = (LazyLoadEvent)_js_slzr.Deserialize(_jsonString, typeof(LazyLoadEvent));
    List<NoteType> _rows = NoteTypes.AsQueryable()
            .LazyOrderBy(_loadEvent)
            .LazyFilters(_loadEvent)
            .LazySkipTake(_loadEvent).ToList();
```

Example of corrected LazyLoadEvent2 interface test:
```
    string _pagination = "{\"first\":0,\"rows\":3," +
        "\"sortOrder\":-1,\"sortField\":\"NoteTypeSortOrder\"," +
        "{\"filters\":{\"NoteTypeDesc\":[" +
        "{\"value\":\"SO\",\"matchMode\":\"startsWith\",\"operator\":\"or\"}," +
        "{\"value\":\"6\",\"matchMode\":\"contains\",\"operator\":\"or\"}]}}";
    LazyLoadEvent2 _loadEvent = JsonConvert.DeserializeObject<LazyLoadEvent2>(_pagination);
    List<NoteType> _rows = NoteTypes.AsQueryable()
        .LazyOrderBy2(_loadEvent)
        .LazyFilters2(_loadEvent)
        .LazySkipTake2(_loadEvent).ToList();
```

### NSG.PrimeNG.LazyLoading_Tests
NSG.PrimeNG.LazyLoading_Tests is a .Net Core project of unit tests for the LazyLoading library.

### Docs
The Sandcastle project files for creating HTML compiled help files.

### Wiki
The Wiki pages were initialy created by the CS2Wiki.awk scripts with the library's XML file.
The AWK scripts is a hack and got me 70% of the way to creating the MediaWiki file. Make sure the text is flush left except for **code**. **Code** is indented by two spaces.
The mediwiki scripts have been moved [here](https://github.com/PHuhn/py-media-wiki).

Check the Wiki pages for more information:
- [NSG.PrimeNG](https://github.com/PHuhn/NSG.PrimeNG/wiki/Home),
- [NSG.PrimeNG.LazyLoading](https://github.com/PHuhn/NSG.PrimeNG/wiki/NSG.PrimeNG.LazyLoading).

### Primefaces
PrimeNG links:
- [PRIMENG](https://www.primefaces.org/primeng/#/),
- [primeng github](https://github.com/primefaces/primeng).
