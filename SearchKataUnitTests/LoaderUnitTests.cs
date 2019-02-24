using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchKataApp;
using SearchKataApp.Entities;

namespace SearchKataUnitTests
{
    [TestClass]
    public class LoaderUnitTests
    {
        //- get search terms 
        [TestMethod]
        public void LoadFile()
        {
            var loader = new Loader();
            Assert.AreEqual(true, loader.LoadFile(@"C:\puzzle.txt"));
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
        public void GetSearchTerms()
        {
            //throw new NotImplementedException();
            Assert.IsTrue(1 == 1);
        }

        //return x,y coordinates for each word found 
        [TestMethod]
        public void ReturnMatches()
        {
            throw new NotImplementedException();
            Assert.IsTrue(1 == 1);
        }
    }
}