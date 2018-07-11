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

        [TestMethod]
        public void Save_SavesToDatabase_CuisineList()
        {
            Cuisine newCuisine = new Cuisine("Chinese Street Food");
            newCuisine.Save();
            List<Cuisine> result = Cuisine.GetAll();
            List<Cuisine> testList = new List<Cuisine>{ newCuisine };
            CollectionAssert.AreEqual(testList, result);
        }

        [TestMethod]
        public void Update_UpdatesColumnInDatabase_CuisineList()
        {
            Cuisine newCuisine = new Cuisine("Chinese Street Food");
            newCuisine.Id = 1;
            newCuisine.Save();
            newCuisine.Name = "Chinese";
            newCuisine.Update();
            List<Cuisine> result = Cuisine.GetAll();
            List<Cuisine> testList = new List<Cuisine>{ newCuisine };
            CollectionAssert.AreEqual(testList, result);
        }

        [TestMethod]
        public void Delete_DeletesRecordFromDatabase_CuisineList()
        {
            Cuisine newCuisine = new Cuisine("Chinese Street Food");
            Cuisine newCuisine2 = new Cuisine("Chinese Street Garbage");
            newCuisine.Id = 1;
            newCuisine2.Id = 2;
            newCuisine.Save();
            newCuisine2.Save();
            newCuisine.Delete();
            List<Cuisine> result = Cuisine.GetAll();
            List<Cuisine> testList = new List<Cuisine>{ newCuisine2 };
            CollectionAssert.AreEqual(testList, result);
        }

        [TestMethod]
        public void Find_FindsCuisineInDatabaseById_Cuisine()
        {
            Cuisine newCuisine = new Cuisine("Korean BBQ");
            newCuisine.Id = 1;
            newCuisine.Save();
            Cuisine foundCuisine = Cuisine.Find(newCuisine.Id);
            Assert.AreEqual(newCuisine, foundCuisine);
        }
    }
}
