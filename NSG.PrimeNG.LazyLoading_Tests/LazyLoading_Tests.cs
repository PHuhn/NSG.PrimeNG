using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Newtonsoft.Json;
//
using NSG.PrimeNG.LazyLoading;
//
namespace NSG.PrimeNG.LazyLoading_Tests
{
    /// <summary>
    /// Summary description for LazyLoading
    /// </summary>
    [TestClass]
    public class LazyLoading_Tests
    {
        public LazyLoading_Tests()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        //
        //
        IEnumerable<NoteType> NoteTypes = new List<NoteType>()
        {
            new NoteType() { NoteTypeId = 1, NoteTypeSortOrder = 200, NoteTypeShortDesc = "Id:1", NoteTypeDesc = "Id:1"},
            new NoteType() { NoteTypeId = 2, NoteTypeSortOrder = 104, NoteTypeShortDesc = "Id:2", NoteTypeDesc = "SO:104"},
            new NoteType() { NoteTypeId = 3, NoteTypeSortOrder = 103, NoteTypeShortDesc = "Id:3", NoteTypeDesc = "SO:103"},
            new NoteType() { NoteTypeId = 4, NoteTypeSortOrder = 102, NoteTypeShortDesc = "Id:4", NoteTypeDesc = "SO:102"},
            new NoteType() { NoteTypeId = 5, NoteTypeSortOrder = 101, NoteTypeShortDesc = "Id:5", NoteTypeDesc = "SO:101"},
            new NoteType() { NoteTypeId = 6, NoteTypeSortOrder = 100, NoteTypeShortDesc = "Id:6", NoteTypeDesc = "Id:6"},
            new NoteType() { NoteTypeId = 7, NoteTypeSortOrder = 100, NoteTypeShortDesc = "Id:6", NoteTypeDesc = "Id:7"}
        };
        //
        private TestContext testContextInstance;
        //
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion
        //
        // Use TestInitialize to run code before running each test 
        [TestInitialize()]
        public void MyTestInitialize()
        {
        }
        //
        [TestMethod(), TestCategory("EF_Native")]
        public void LazyLoading_LazySkipTake01_Test()
        {
            string _pagination = "{\"first\":0,\"rows\":5}";
            IQueryable<NoteType> noteTypeQueryable =
                (from _r in NoteTypes select _r).AsQueryable();
            LazyLoadEvent loadEvent = JsonConvert.DeserializeObject<LazyLoadEvent>(_pagination);
            List<NoteType> _rows = noteTypeQueryable.LazySkipTake(loadEvent).ToList();
            //
            foreach (var _row in _rows)
                System.Diagnostics.Debug.WriteLine(_row.ToString());
            Assert.IsTrue(_rows.Count == 5);
            NoteType _row0 = _rows[0];
            Assert.AreEqual(_row0.NoteTypeId, 1);
        }
        //
        [TestMethod(), TestCategory("EF_Native")]
        public void LazyLoading_LazySkipTake02_Test()
        {
            string _pagination = "{\"first\":5,\"rows\":5}";
            IQueryable<NoteType> noteTypeQueryable =
                (from _r in NoteTypes select _r).AsQueryable();
            LazyLoadEvent loadEvent = JsonConvert.DeserializeObject<LazyLoadEvent>(_pagination);
            List<NoteType> _rows = noteTypeQueryable.LazySkipTake(loadEvent).ToList();
            //
            foreach (var _row in _rows)
                System.Diagnostics.Debug.WriteLine(_row.ToString());
            Assert.IsTrue(_rows.Count == 2);
            NoteType _row0 = _rows[0];
            Assert.AreEqual(_row0.NoteTypeId, 6);
        }
        //
        [TestMethod(), TestCategory("EF_Native")]
        public void LazyLoading_LazyFilter01_IntEquals_Test()
        {
            string _pagination = "{\"first\":0,\"rows\":3,\"sortOrder\":-1," +
                "\"filters\":{\"NoteTypeSortOrder\":{\"value\":\"100\",\"matchMode\":\"equals\"}},\"multiSortMeta\": {},\"globalFilter\": {}}";
            IQueryable<NoteType> noteTypeQueryable =
                (from _r in NoteTypes select _r).AsQueryable();
            Console.WriteLine(_pagination);
            LazyLoadEvent loadEvent = JsonConvert.DeserializeObject<LazyLoadEvent>(_pagination);
            Console.WriteLine(loadEvent);
            List<NoteType> _rows = noteTypeQueryable.LazyFilters(loadEvent).ToList();
            //
            foreach (var _row in _rows)
                System.Diagnostics.Debug.WriteLine(_row.ToString());
            Assert.IsTrue(_rows.Count == 2);
            NoteType _row0 = _rows[0];
            Assert.AreEqual(_row0.NoteTypeSortOrder, 100);
        }
        //
        [TestMethod(), TestCategory("EF_Native")]
        public void LazyLoading_LazyFilter02_StringEquals_Test()
        {
            // string _pagination = "{\"filters\":{\"NoteTypeSortOrder\":{\"value\":100,\"matchMode\":\"equals\"},\"Mailed\":{\"value\":false,\"matchMode\":\"equals\"},\"Closed\":{\"value\":false,\"matchMode\":\"equals\"},\"Special\":{\"value\":false,\"matchMode\":\"equals\"}},\"globalFilter\":null}";
            string _pagination = "{\"filters\":{\"NoteTypeDesc\":{\"value\":\"Id:7\",\"matchMode\":\"equals\"}}}";
            IQueryable<NoteType> noteTypeQueryable =
                (from _r in NoteTypes select _r).AsQueryable();
            LazyLoadEvent loadEvent = JsonConvert.DeserializeObject<LazyLoadEvent>(_pagination);
            List<NoteType> _rows = noteTypeQueryable.LazyFilters(loadEvent).ToList();
            //
            foreach (var _row in _rows)
                System.Diagnostics.Debug.WriteLine(_row.ToString());
            Assert.IsTrue(_rows.Count == 1);
            NoteType _row0 = _rows[0];
            Assert.IsTrue(_row0.NoteTypeDesc == "Id:7");
        }
        //
        [TestMethod(), TestCategory("EF_Native")]
        public void LazyLoading_LazyFilter03_GT_Test()
        {
            string _pagination = "{\"filters\":{\"NoteTypeSortOrder\":{\"value\":100,\"matchMode\":\"gt\"}}}";
            IQueryable<NoteType> noteTypeQueryable =
                (from _r in NoteTypes select _r).AsQueryable();
            LazyLoadEvent loadEvent = JsonConvert.DeserializeObject<LazyLoadEvent>(_pagination);
            List<NoteType> _rows = noteTypeQueryable.LazyFilters(loadEvent).ToList();
            //
            foreach (var _row in _rows)
                System.Diagnostics.Debug.WriteLine(_row.ToString());
            Assert.IsTrue(_rows.Count == 5);
            NoteType _row0 = _rows[0];
            Assert.IsTrue(_row0.NoteTypeSortOrder > 100);
        }
        //
        [TestMethod(), TestCategory("EF_Native")]
        public void LazyLoading_LazyFilter04_GTE_Test()
        {
            string _pagination = "{\"filters\":{\"NoteTypeSortOrder\":{\"value\":101,\"matchMode\":\"gte\"}}}";
            IQueryable<NoteType> noteTypeQueryable =
                (from _r in NoteTypes select _r).AsQueryable();
            LazyLoadEvent loadEvent = JsonConvert.DeserializeObject<LazyLoadEvent>(_pagination);
            List<NoteType> _rows = noteTypeQueryable.LazyFilters(loadEvent).ToList();
            //
            foreach (var _row in _rows)
                System.Diagnostics.Debug.WriteLine(_row.ToString());
            Assert.IsTrue(_rows.Count == 5);
            NoteType _row0 = _rows[0];
            Assert.IsTrue(_row0.NoteTypeSortOrder >= 101);
        }
        //
        [TestMethod(), TestCategory("EF_Native")]
        public void LazyLoading_LazyFilter05_LT_Test()
        {
            // string _pagination = "{\"filters\":{\"NoteTypeSortOrder\":{\"value\":100,\"matchMode\":\"equals\"},\"Mailed\":{\"value\":false,\"matchMode\":\"equals\"},\"Closed\":{\"value\":false,\"matchMode\":\"equals\"},\"Special\":{\"value\":false,\"matchMode\":\"equals\"}},\"globalFilter\":null}";
            string _pagination = "{\"filters\":{\"NoteTypeSortOrder\":{\"value\":103,\"matchMode\":\"lt\"}}}";
            IQueryable<NoteType> noteTypeQueryable =
                (from _r in NoteTypes select _r).AsQueryable();
            LazyLoadEvent loadEvent = JsonConvert.DeserializeObject<LazyLoadEvent>(_pagination);
            List<NoteType> _rows = noteTypeQueryable.LazyFilters(loadEvent).ToList();
            //
            foreach (var _row in _rows)
                System.Diagnostics.Debug.WriteLine(_row.ToString());
            Assert.IsTrue(_rows.Count == 4);
            NoteType _row0 = _rows[0];
            Assert.IsTrue(_row0.NoteTypeSortOrder < 103);
        }
        //
        [TestMethod(), TestCategory("EF_Native")]
        public void LazyLoading_LazyFilter06_LTE_Test()
        {
            string _pagination = "{\"filters\":{\"NoteTypeSortOrder\":{\"value\":103,\"matchMode\":\"lte\"}}}";
            IQueryable<NoteType> noteTypeQueryable =
                (from _r in NoteTypes select _r).AsQueryable();
            LazyLoadEvent loadEvent = JsonConvert.DeserializeObject<LazyLoadEvent>(_pagination);
            List<NoteType> _rows = noteTypeQueryable.LazyFilters(loadEvent).ToList();
            //
            foreach (var _row in _rows)
                System.Diagnostics.Debug.WriteLine(_row.ToString());
            Assert.IsTrue(_rows.Count == 5);
            NoteType _row0 = _rows[0];
            Assert.IsTrue(_row0.NoteTypeSortOrder <= 103);
        }
        //
        [TestMethod(), TestCategory("EF_Native")]
        public void LazyLoading_LazyFilter07_NotEquals_Test()
        {
            string _pagination = "{\"filters\":{\"NoteTypeSortOrder\":{\"value\":100,\"matchMode\":\"NotEquals\"}}}";
            IQueryable<NoteType> noteTypeQueryable =
                (from _r in NoteTypes select _r).AsQueryable();
            LazyLoadEvent loadEvent = JsonConvert.DeserializeObject<LazyLoadEvent>(_pagination);
            List<NoteType> _rows = noteTypeQueryable.LazyFilters(loadEvent).ToList();
            //
            foreach (var _row in _rows)
                System.Diagnostics.Debug.WriteLine(_row.ToString());
            Assert.IsTrue(_rows.Count == 5);
            NoteType _row0 = _rows[0];
            Assert.IsTrue(_row0.NoteTypeSortOrder != 100);
        }
        //
        [TestMethod(), TestCategory("EF_Native")]
        public void LazyLoading_LazyFilter08_Contains_Test()
        {
            string _pagination = "{\"filters\":{\"NoteTypeDesc\":{\"value\":\"SO\",\"matchMode\":\"Contains\"}}}";
            IQueryable<NoteType> noteTypeQueryable =
                (from _r in NoteTypes select _r).AsQueryable();
            LazyLoadEvent loadEvent = JsonConvert.DeserializeObject<LazyLoadEvent>(_pagination);
            List<NoteType> _rows = noteTypeQueryable.LazyFilters(loadEvent).ToList();
            //
            foreach (var _row in _rows)
                System.Diagnostics.Debug.WriteLine(_row.ToString());
            Assert.IsTrue(_rows.Count == 4);
            NoteType _row0 = _rows[0];
            Assert.IsTrue(_row0.NoteTypeDesc.Substring(0, 2) == "SO");
        }
        //
        [TestMethod(), TestCategory("EF_Native")]
        public void LazyLoading_LazyFilter09_StartsWith_Test()
        {
            string _pagination = "{\"filters\":{\"NoteTypeDesc\":{\"value\":\"SO\",\"matchMode\":\"StartsWith\"}}}";
            IQueryable<NoteType> noteTypeQueryable =
                (from _r in NoteTypes select _r).AsQueryable();
            LazyLoadEvent loadEvent = JsonConvert.DeserializeObject<LazyLoadEvent>(_pagination);
            List<NoteType> _rows = noteTypeQueryable.LazyFilters(loadEvent).ToList();
            //
            foreach (var _row in _rows)
                System.Diagnostics.Debug.WriteLine(_row.ToString());
            Assert.IsTrue(_rows.Count == 4);
            NoteType _row0 = _rows[0];
            Assert.IsTrue(_row0.NoteTypeDesc.Substring(0, 2) == "SO");
        }
        //
        [TestMethod(), TestCategory("EF_Native")]
        public void LazyLoading_LazyFilter10_EndsWith_Test()
        {
            string _pagination = "{\"filters\":{\"NoteTypeDesc\":{\"value\":\"1\",\"matchMode\":\"EndsWith\"}}}";
            IQueryable<NoteType> noteTypeQueryable =
                (from _r in NoteTypes select _r).AsQueryable();
            LazyLoadEvent loadEvent = JsonConvert.DeserializeObject<LazyLoadEvent>(_pagination);
            List<NoteType> _rows = noteTypeQueryable.LazyFilters(loadEvent).ToList();
            //
            foreach (var _row in _rows)
                System.Diagnostics.Debug.WriteLine(_row.ToString());
            Assert.IsTrue(_rows.Count == 2);
            NoteType _row0 = _rows[0];
            Assert.IsTrue(_row0.NoteTypeDesc.Substring(_row0.NoteTypeDesc.Length - 1, 1) == "1");
        }
        //
        [TestMethod(), TestCategory("EF_Native")]
        public void LazyLoading_LazyOrderBy01_Test()
        {
            // sortField: "NoteTypeSortOrder"
            // string _pagination = "{\"sortOrder\":1,\"sortField\":\"NoteTypeSortOrder\",\"filters\":{\"ServerId\":{\"value\":1,\"matchMode\":\"equals\"},\"Mailed\":{\"value\":false,\"matchMode\":\"equals\"},\"Closed\":{\"value\":false,\"matchMode\":\"equals\"},\"Special\":{\"value\":false,\"matchMode\":\"equals\"}},\"globalFilter\":null}";
            string _pagination = "{\"sortOrder\":1,\"sortField\":\"NoteTypeSortOrder\"}";
            IQueryable<NoteType> noteTypeQueryable =
                (from _r in NoteTypes select _r).AsQueryable();
            LazyLoadEvent loadEvent = JsonConvert.DeserializeObject<LazyLoadEvent>(_pagination);
            List<NoteType> _rows = noteTypeQueryable.LazyOrderBy(loadEvent).ToList();
            //
            foreach (var _row in _rows)
                System.Diagnostics.Debug.WriteLine(_row.ToString());
            Assert.IsTrue(_rows.Count == 7);
            NoteType _row0 = _rows[0];
            Assert.AreEqual(_row0.NoteTypeSortOrder, 100);
            NoteType _row7 = _rows[6];
            Assert.AreEqual(_row7.NoteTypeSortOrder, 200);
        }
        //
        [TestMethod(), TestCategory("EF_Native")]
        public void LazyLoading_LazyOrderBy02_Test()
        {
            // sortField: "NoteTypeSortOrder"
            // string _pagination = "{\"sortOrder\":1,\"sortField\":\"NoteTypeSortOrder\",\"filters\":{\"ServerId\":{\"value\":1,\"matchMode\":\"equals\"},\"Mailed\":{\"value\":false,\"matchMode\":\"equals\"},\"Closed\":{\"value\":false,\"matchMode\":\"equals\"},\"Special\":{\"value\":false,\"matchMode\":\"equals\"}},\"globalFilter\":null}";
            string _pagination = "{\"sortOrder\":-1,\"sortField\":\"NoteTypeSortOrder\"}";
            IQueryable<NoteType> noteTypeQueryable =
                (from _r in NoteTypes select _r).AsQueryable();
            LazyLoadEvent loadEvent = JsonConvert.DeserializeObject<LazyLoadEvent>(_pagination);
            List<NoteType> _rows = noteTypeQueryable.LazyOrderBy(loadEvent).ToList();
            //
            foreach (var _row in _rows)
                System.Diagnostics.Debug.WriteLine(_row.ToString());
            Assert.IsTrue(_rows.Count == 7);
            NoteType _row0 = _rows[0];
            Assert.AreEqual(_row0.NoteTypeSortOrder, 200);
            NoteType _row7 = _rows[6];
            Assert.AreEqual(_row7.NoteTypeSortOrder, 100);
        }
        //
        [TestMethod(), TestCategory("EF_Native")]
        public void LazyLoading_Lazy_All_01_Test()
        {
            string _jsonString = "{\"first\":0,\"rows\":3," +
                        "\"sortOrder\":-1,\"sortField\":\"NoteTypeSortOrder\"," +
                        "\"filters\":{\"NoteTypeDesc\":{\"value\":\"SO\",\"matchMode\":\"StartsWith\"}}}";
            LazyLoadEvent _loadEvent = JsonConvert.DeserializeObject<LazyLoadEvent>(_jsonString);
            List<NoteType> _rows = NoteTypes.AsQueryable()
                        .LazyOrderBy(_loadEvent)
                        .LazyFilters(_loadEvent)
                        .LazySkipTake(_loadEvent).ToList();
            //
            foreach (var _row in _rows)
                System.Diagnostics.Debug.WriteLine(_row.ToString());
            Assert.IsTrue(_rows.Count == 3);
            NoteType _row0 = _rows[0];
            Assert.IsTrue(_row0.NoteTypeDesc.Substring(0, 2) == "SO");
            Assert.AreEqual(_row0.NoteTypeSortOrder, 104);
            Assert.AreEqual(_row0.NoteTypeId, 2);
        }
        //
    }
}
