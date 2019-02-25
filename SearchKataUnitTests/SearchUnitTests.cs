using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchKataApp;
using SearchKataApp.Entities;

namespace SearchKataUnitTests
{
    [TestClass]
    public class SearchUnitTests
    {
        Loader loader;
        Search search;

        [TestInitialize]
        public void setup()
        {
            loader = new Loader();
            loader.LoadFile(@"C:\puzzle.txt");
            search = new Search(loader.Data, loader.SearchTerms);
        }

        //- fields to be searched will be letter with coordinates: a, 1, 1 (list of objects - letter, x pos, y pos) 
        //return x,y coordinates for each word found 
        [TestMethod]
        public void whenSearchIsRunListofStringsAreReturnedAndSevenMatchesAreFound()
        {
            var output = new List<string> {
                "BONES: (0,6),(0,7),(0,8),(0,9),(0,10)",
                "KHAN: (5,9),(5,8),(5,7),(5,6)",
                "KIRK: (4,7),(3,7),(2,7),(1,7)",
                "SCOTTY: (0,5),(1,5),(2,5),(3,5),(4,5),(5,5)",
                "SPOCK: (2,1),(3,2),(4,3),(5,4),(6,5)",
                "SULU: (3,3),(2,2),(1,1),(0,0)",
                "UHURA: (4,0),(3,1),(2,2),(1,3),(0,4)"
            };
            var matches = search.ExecuteSearch();
            Assert.IsInstanceOfType(matches, typeof(List<string>));
            foreach(var match in matches)
            {
                Assert.IsTrue(output.Contains(match));
            }
        }
    }
}