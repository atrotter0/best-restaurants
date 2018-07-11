using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using BestRestaurants.Models;

namespace BestRestaurants.Tests
{
    [TestClass]
    public class RestaurantTest : IDisposable
    {
        public void Dispose()
        {
            Restaurant.DeleteAll();
        }

        public RestaurantTest()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=best_restaurants_test;";
        }

        [TestMethod]
        public void GetSetProperties_GetsSetsProperties_True()
        {
            Restaurant newRestaurant = new Restaurant("mcdonalds", "$");
            newRestaurant.Name = "applebees";
            newRestaurant.Price = "$$";
            Assert.AreEqual("applebees", newRestaurant.Name);
            Assert.AreEqual("$$", newRestaurant.Price);
        }

        [TestMethod]
        public void Equals_ReturnsTrueIfPropertiesAreTheSame_Restaurant()
        {
            Restaurant firstRestaurant = new Restaurant("Jolly Roger", "$$");
            Restaurant secondRestaurant = new Restaurant("Jolly Roger", "$$");
            Assert.AreEqual(firstRestaurant, secondRestaurant);
        }

        [TestMethod]
        public void GetAll_DbStartsEmpty_0()
        {
            int result = Restaurant.GetAll().Count;
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Save_SavesToDatabase_RestaurantList()
        {
            Restaurant newRestaurant = new Restaurant("Jolly Roger", "$$");
            newRestaurant.Save();
            List<Restaurant> result = Restaurant.GetAll();
            List<Restaurant> testList = new List<Restaurant>{ newRestaurant };
            CollectionAssert.AreEqual(testList, result);
        }

        [TestMethod]
        public void Update_UpdatesColumnInDatabase_RestaurantList()
        {
            Restaurant newRestaurant = new Restaurant("Jolly Roger", "$$");
            newRestaurant.Id = 1;
            newRestaurant.CuisineId = 1;
            newRestaurant.Save();
            newRestaurant.Name = "Mickey Dees";
            newRestaurant.Price = "$$$$";
            newRestaurant.CuisineId = 3;
            newRestaurant.Update();
            List<Restaurant> result = Restaurant.GetAll();
            List<Restaurant> testList = new List<Restaurant>{ newRestaurant };
            CollectionAssert.AreEqual(testList, result);
        }

        [TestMethod]
        public void Delete_DeletesRecordFromDatabase_RestaurantList()
        {
            Restaurant newRestaurant = new Restaurant("Chinese Cart", "$");
            Restaurant newRestaurant2 = new Restaurant("Japanese Cart", "$");
            newRestaurant.Id = 1;
            newRestaurant2.Id = 2;
            newRestaurant.Save();
            newRestaurant2.Save();
            newRestaurant.Delete();
            List<Restaurant> result = Restaurant.GetAll();
            List<Restaurant> testList = new List<Restaurant>{ newRestaurant2 };
            CollectionAssert.AreEqual(testList, result);
        }

        [TestMethod]
        public void Find_FindsRestaurantInDatabaseById_Restaurant()
        {
            Restaurant newRestaurant = new Restaurant("Korean BBQ", "$$$$$$$$$$");
            newRestaurant.Id = 1;
            newRestaurant.Save();
            Restaurant foundRestaurant = Restaurant.Find(newRestaurant.Id);
            Assert.AreEqual(newRestaurant, foundRestaurant);
        }
    }
}
