using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    }
}
