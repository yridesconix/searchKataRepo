using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchKataApp;
using SearchKataApp.Entities;

namespace SearchKataUnitTests
{
    [TestClass]
    public class LoaderUnitTests
    {
        Loader loader;

        [TestInitialize]
        public void setup()
        {
            loader = new Loader();
        }

        //- get search terms 
        [TestMethod]
        public void whenLoaderIsPassedAFilePathItReturnsTrue()
        {
            Assert.AreEqual(true, loader.LoadFile(@"C:\puzzle.txt"));
        }

        [TestMethod]
        public void whenLoaderIsPassedAFilePathItReturnsTrueAndSearchTermsAreLoaded()
        {
            var searchTerms = new List<string> { "BONES", "KHAN", "KIRK", "SCOTTY", "SPOCK", "SULU", "UHURA" };
            loader.LoadFile(@"C:\puzzle.txt");
            Assert.IsNotNull(loader.SearchTerms);
            Assert.AreEqual(7, loader.SearchTerms.Count);

            foreach(var term in loader.SearchTerms)
            {
                Assert.IsTrue(searchTerms.Contains(term));
            }
        }

        //- load fields to be searched 
        [TestMethod]
        public void whenLoaderIsPassedAFilePathItReturnsTrueAndSearchableDataIsLoaded()
        {
            loader.LoadFile(@"C:\puzzle.txt");
            Assert.IsNotNull(loader.Data);
            Assert.IsTrue(loader.Data.Count > 0);
            Assert.AreEqual(14, loader.Data.Max(x => x.XPosition));
            Assert.AreEqual(14, loader.Data.Max(x => x.YPosition));
        }
    }
}