using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BestRestaurants.Models;

namespace BestRestaurants.Controllers
{
    public class RestaurantsController : Controller
    {
        [HttpGet("/restaurants")]
        public ActionResult Index()
        {
            List<Restaurant> allRestaurants = Restaurant.GetAll();
            return View(allRestaurants);
        }

        [HttpGet("/restaurants/new")]
        public ActionResult AddRestaurant()
        {
            List<Cuisine> allCuisines = Cuisine.GetAll();
            return View(allCuisines);
        }

        [HttpPost("/restaurants")]
        public ActionResult CreateRestaurant(string name, string price, int cuisineId)
        {
            Restaurant newRestaurant = new Restaurant(name, price, 0, cuisineId, 0);
            newRestaurant.Save();
            return RedirectToAction("Index");
        }
    }
}
