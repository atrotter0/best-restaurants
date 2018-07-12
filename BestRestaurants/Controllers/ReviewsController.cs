using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BestRestaurants.Models;

namespace BestRestaurants.Controllers
{
    public class ReviewsController : Controller
    {
        [HttpGet("/reviews")]
        public ActionResult Index()
        {
            List<Review> allReviews = Review.GetAll();
            return View(allReviews);
        }

        [HttpGet("/reviews/new")]
        public ActionResult CreateReviewForm()
        {
            List<Restaurant> allRestaurants = Restaurant.GetAll();
            return View(allRestaurants);
        }

        [HttpPost("/reviews")]
        public ActionResult CreateReview(string reviewerName, string description, int rating, int restaurantId)
        {
            Review newReview = new Review(reviewerName, description, rating, 0, restaurantId);
            newReview.Save();
            return RedirectToAction("Index");
        }

        [HttpGet("/reviews/{id}")]
        public ActionResult ShowReview(int id)
        {
            Review newReview = Review.Find(id);
            return View(newReview);
        }

        [HttpGet("/reviews/{id}/update")]
        public ActionResult UpdateReviewForm(int id)
        {
            Review newReview = Review.Find(id);
            return View(newReview);
        }

        [HttpPost("/reviews/{id}/delete")]
        public ActionResult UpdateReview(int id)
        {
            Review review = Review.Find(id);
            review.Delete();
            return RedirectToAction("Index");
        }
    }
}
