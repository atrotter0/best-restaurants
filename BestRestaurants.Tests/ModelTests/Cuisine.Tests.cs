using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using BestRestaurants.Models;

namespace BestRestaurants.Tests
{
    [TestClass]
    public class CuisineTest : IDisposable
    {

        public void Dispose()
        {
            Cuisine.DeleteAll();
        }

        public CuisineTest()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=best_restaurants_test;";
        }

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

        [TestMethod]
        public void GetAll_DbStartsEmpty_0()
        {
            int result = Cuisine.GetAll().Count;
            Assert.AreEqual(0, result);
        }
    }
}
