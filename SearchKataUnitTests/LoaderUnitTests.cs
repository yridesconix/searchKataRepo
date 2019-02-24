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

        //- fields to be searched will be letter with coordinates: a, 1, 1 (list of objects - letter, x pos, y pos) 

        //load forwards vertical arrays/collections 

        //load backwards vertical arrays/collections 

        //load forwards horizontal arrays/collections 

        //load backwards horizontal arrays/collections 

        //load forwards upward diagonal arrays/collections 

        //load backwards upward diagonal arrays/collections 

        //load forwards downward diagonal arrays/collections 

        //load backwards downward diagonal arrays/collections 

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

        //return x,y coordinates for each word found 
        [TestMethod]
        public void ReturnMatches()
        {
            //throw new NotImplementedException();
            Assert.IsTrue(1 == 1);
        }
    }
}