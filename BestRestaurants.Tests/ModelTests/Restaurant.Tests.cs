using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using BestRestaurants.Models;

namespace BestRestaurants.Tests
{
    [TestClass]
    public class RestaurantTest
    {
        [TestMethod]
        public void GetSetProperties_GetsSetsProperties_True()
        {
            Restaurant newRestaurant = new Restaurant("mcdonalds", "$");
            newRestaurant.Name = "applebees";
            newRestaurant.Price = "$$";
            Assert.AreEqual("applebees", newRestaurant.Name);
            Assert.AreEqual("$$", newRestaurant.Price);
        }
    }
}
