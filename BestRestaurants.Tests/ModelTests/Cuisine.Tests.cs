using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using BestRestaurants.Models;

namespace BestRestaurants.Tests
{
    [TestClass]
    public class CuisineTest
    {
        [TestMethod]
        public void GetSetProperties_GetsSetsProperties_True()
        {
            Cuisine newCuisine = new Cuisine("Mexican");
            newCuisine.Name = "Italian";
            Assert.AreEqual("Italian", newCuisine.Name);
        }

        [TestMethod]
        public void Equals_ReturnsTrueIfNamesAreTheSame_Cuisine()
        {
            Cuisine firstCuisine = new Cuisine("Italian");
            Cuisine secondCuisine = new Cuisine("Italian");
            Assert.AreEqual(firstCuisine, secondCuisine);
        }
    }
}
