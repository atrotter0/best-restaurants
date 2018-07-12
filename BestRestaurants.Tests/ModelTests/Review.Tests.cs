using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using BestRestaurants.Models;

namespace BestRestaurants.Tests
{
    [TestClass]
    public class ReviewTest : IDisposable
    {

        public void Dispose()
        {
            // Review.DeleteAll();
        }

        public ReviewTest()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=best_restaurants_test;";
        }

        [TestMethod]
        public void GetSetProperties_GetsSetsProperties_True()
        {
            Review newReview = new Review("Peter Jenkins", "Awful food", 1);
            newReview.ReviewerName = "Kevin Ahn";
            newReview.Description = "Best anime";
            newReview.Rating = 5;
            Assert.AreEqual("Kevin Ahn", newReview.ReviewerName);
            Assert.AreEqual("Best anime", newReview.Description);
            Assert.AreEqual(5, newReview.Rating);
        }

        [TestMethod]
        public void Equals_ReturnsTrueIfNamesAreTheSame_Review()
        {
            Review firstReview = new Review("Peter Jenkins", "Awful food", 1);
            Review secondReview = new Review("Peter Jenkins", "Awful food", 1);
            Assert.AreEqual(firstReview, secondReview);
        }

        [TestMethod]
        public void GetAll_DbStartsEmpty_0()
        {
            int result = Review.GetAll().Count;
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Save_SavesToDatabase_ReviewList()
        {
            Review newReview = new Review("Peter Jenkins", "Awful food", 1);
            newReview.Save();
            List<Review> result = Review.GetAll();
            List<Review> testList = new List<Review>{ newReview };
            CollectionAssert.AreEqual(testList, result);
        }

        [TestMethod]
        public void Update_UpdatesColumnInDatabase_ReviewList()
        {
            Review newReview = new Review("Peter Jenkins", "Awful food", 1);
            newReview.Save();
            newReview.ReviewerName = "Kevin";
            newReview.Description = "This is great!";
            newReview.Rating = 3;
            newReview.Update();
            List<Review> result = Review.GetAll();
            List<Review> testList = new List<Review>{ newReview };
            CollectionAssert.AreEqual(testList, result);
        }



    }
}
