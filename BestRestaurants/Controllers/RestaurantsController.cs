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
            Restaurant newRestaurant = new Restaurant(name, price, 0, cuisineId);
            newRestaurant.Save();
            return RedirectToAction("Index");
        }

        [HttpGet("/restaurants/{id}")]
        public ActionResult ShowRestaurant(int id)
        {
            Restaurant newRestaurant = Restaurant.Find(id);
            return View(newRestaurant);
        }

        [HttpGet("/restaurants/{id}/update")]
        public ActionResult UpdateRestaurantForm(int id)
        {
            Restaurant newRestaurant = Restaurant.Find(id);
            return View(newRestaurant);
        }

        [HttpPost("/restaurants/{id}/update")]
        public ActionResult UpdateRestaurant(int id, string name, string price, int cuisineId)
        {
            Restaurant newRestaurant = Restaurant.Find(id);
            newRestaurant.Name = name;
            newRestaurant.Price = price;
            newRestaurant.CuisineId = cuisineId;
            newRestaurant.Update();
            return RedirectToAction("ShowRestaurant");
        }

        [HttpPost("/restaurants/{id}/delete")]
        public ActionResult UpdateRestaurant(int id)
        {
            Restaurant newRestaurant = Restaurant.Find(id);
            newRestaurant.Delete();
            return RedirectToAction("Index");
        }
    }
}
