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

        //load forwards vertical arrays/collections 
        //load backwards vertical arrays/collections 
        //load forwards horizontal arrays/collections 
        //load backwards horizontal arrays/collections 

        [TestMethod]
        public void whenSearchIsPassedARowOrColumnNumberTheLettersOfThatRowOrColumnIsReturned()
        {
            var horizontal = "KYLBQQPMDFCKEAB";
            var horizontalBackwards = "BAEKCFDMPQQBLYK";
            var vertical = "IWJERZEMMJBECUM";
            var verticalBackwards = "MUCEBJMMEZREJWI";
            Assert.AreEqual(horizontal, search.ReturnHorizontal(14));
            Assert.AreEqual(horizontalBackwards, search.ReturnHorizontalBackwards(14));
            Assert.AreEqual(vertical, search.ReturnVertical(7));
            Assert.AreEqual(verticalBackwards, search.ReturnVerticalBackwards(7));
        }

        [TestMethod]
        public void whenReturnVerticalForwardsAndBackwardsIsRunListOfStringsIsReturned()
        {
            var searchables = search.ReturnVerticalForwardsAndBackwards();
            var vertical = "IWJERZEMMJBECUM";
            var verticalBackwards = "MUCEBJMMEZREJWI";
            Assert.IsNotNull(searchables);
            Assert.IsTrue(searchables.Contains(vertical));
            Assert.IsTrue(searchables.Contains(verticalBackwards));
        }

        //load forwards upward diagonal arrays/collections 
        //load backwards upward diagonal arrays/collections 
        //load forwards downward diagonal arrays/collections 
        //load backwards downward diagonal arrays/collections 

        [TestMethod]
        public void whenReturnDiagonalUpwardsForwardsAndBackwardsIsRunListOfStringsIsReturned()
        {
            var searchables = search.ReturnDiagonalUpwardsForwardsAndBackwards();
            var diagonal = "KUUK";
            Assert.IsNotNull(searchables);
            Assert.IsTrue(searchables.Contains(diagonal));
        }

        [TestMethod]
        public void whenReturnDiagonalDownwardsForwardsAndBackwardsIsRunListOfStringsIsReturned()
        {
            var searchables = search.ReturnDiagonalDownwardsForwardsAndBackwards();
            var diagonal = "OUTE";
            Assert.IsNotNull(searchables);
            Assert.IsTrue(searchables.Contains(diagonal));
        }

        //- fields to be searched will be letter with coordinates: a, 1, 1 (list of objects - letter, x pos, y pos) 

        //return x,y coordinates for each word found 
        [TestMethod]
        public void whenSearchIsRunListofStringsAreReturned()
        {
            //throw new NotImplementedException();
            Assert.IsInstanceOfType(search.ExecuteSearch(), typeof(List<string>));
        }
    }
}