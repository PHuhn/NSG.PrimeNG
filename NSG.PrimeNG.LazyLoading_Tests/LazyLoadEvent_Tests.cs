using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
//
using NSG.PrimeNG.LazyLoading;
//
namespace NSG.PrimeNG.LazyLoading_Tests
{
    [TestClass]
    public class LazyLoadEvent_Tests
    {
        //
        [TestMethod]
        public void NoteType01_Test()
        {
            NoteType _sut = new NoteType() { NoteTypeId = 1, NoteTypeSortOrder = 200, NoteTypeShortDesc = "Id:1", NoteTypeDesc = "Id:1" };
            Assert.AreEqual(1, _sut.NoteTypeId);
            Assert.AreEqual(200, _sut.NoteTypeSortOrder);
            Assert.AreEqual("Id:1", _sut.NoteTypeDesc);
        }
        //
        [TestMethod]
        public void LazyLoadEvent_JsonString_Test()
        {
            // given
            string lazyLoadJSON = "{\"first\":0,\"rows\":5,\"sortOrder\":1,\"filters\":{" +
                "\"ServerId\":{\"value\":1,\"matchMode\":\"equals\"}," +
                "\"Mailed\":{\"value\":false,\"matchMode\":\"equals\"}," +
                "\"Closed\":{\"value\":false,\"matchMode\":\"equals\"}," +
                "\"Special\":{\"value\":false,\"matchMode\":\"equals\"}},\"globalFilter\":null}";
            // when
            LazyLoadEvent loadEvent = JsonConvert.DeserializeObject<LazyLoadEvent>(lazyLoadJSON);
            // then
            Assert.IsNotNull(loadEvent);
            // when
            string eventString = loadEvent.ToString();
            // then
            Assert.AreEqual(eventString, "LazyLoadEvent:[first: 0, rows: 5, sortField: , sortOrder: 1, multiSortMeta: /null/, filters: ServerId-equals:1:/null/, Mailed-equals:False:/null/, Closed-equals:False:/null/, Special-equals:False:/null/, globalFilter: /null/]");
            //
        }
        //
        [TestMethod]
        public void LazyLoadEvent_ToString_Test()
        {
            // given
            LazyLoadEvent loadEvent = new LazyLoadEvent();
            // when
            string eventString = loadEvent.ToString();
            // then
            Assert.AreEqual(eventString, "LazyLoadEvent:[first: 0, rows: 0, sortField: /null/, sortOrder: 0, multiSortMeta: /null/, filters: /null/, globalFilter: /null/]");
            //
        }
        //
        [TestMethod]
        public void LazyLoadEvent2_JsonString_Test()
        {
            // given
            string lazyLoadJSON = "{\"first\":0,\"rows\":5,\"sortField\":\"NoteTypeId\",\"sortOrder\":-1,\"filters\":{" +
                "\"NoteTypeDesc\":[{\"value\":\"F\",\"matchMode\":\"startsWith\",\"operator\":\"and\"},{\"value\":\"1\",\"matchMode\":\"contains\",\"operator\":\"and\"}]," +
                "\"NoteTypeShortDesc\":[{\"value\":\"S\",\"matchMode\":\"startsWith\",\"operator\":\"and\"}]},\"globalFilter\":null}";
            // when
            LazyLoadEvent2 loadEvent = JsonConvert.DeserializeObject<LazyLoadEvent2>(lazyLoadJSON);
            // then
            Assert.IsNotNull(loadEvent);
            Assert.AreEqual(loadEvent.filters.Count, 2);
            // when
            string eventString = loadEvent.ToString();
            // then
            Assert.AreEqual(eventString, "LazyLoadEvent2:[first: 0, rows: 5, sortField: NoteTypeId, sortOrder: -1, multiSortMeta: /null/, filters: NoteTypeDesc-startsWith:F:and, NoteTypeDesc-contains:1:and, NoteTypeShortDesc-startsWith:S:and, globalFilter: /null/]");
            //
        }
        //
        [TestMethod]
        public void LazyLoadEvent2_ToString_Test()
        {
            // given
            LazyLoadEvent2 loadEvent = new LazyLoadEvent2();
            // when
            string eventString = loadEvent.ToString();
            // then
            Assert.AreEqual(eventString, "LazyLoadEvent2:[first: 0, rows: 0, sortField: /null/, sortOrder: 0, multiSortMeta: /null/, filters: /null/, globalFilter: /null/]");
            //
        }
        //
        [TestMethod]
        public void FilterMetadata_1_Prop_Test()
        {
            // given
            // when
            IFilterMetadata filterData = new FilterMetadata();
            filterData.value = 1;
            // then
            Assert.IsNotNull(filterData);
            //
        }
        //
        [TestMethod]
        public void FilterMetadata_2_Prop_Test()
        {
            // given
            // when
            IFilterMetadata filterData = new FilterMetadata();
            filterData.value = 1;
            filterData.matchMode = "equal";
            // then
            Assert.IsNotNull(filterData);
            //
        }
        //
    }
}
