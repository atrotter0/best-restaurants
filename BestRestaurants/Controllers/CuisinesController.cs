using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BestRestaurants.Models;

namespace BestRestaurants.Controllers
{
    public class CuisinesController : Controller
    {
        [HttpGet("/cuisines")]
        public ActionResult Index()
        {
            List<Cuisine> allCuisines = Cuisine.GetAll();
            return View(allCuisines);
        }

        [HttpGet("/cuisines/new")]
        public ActionResult AddCuisine()
        {
            return View();
        }

        [HttpPost("/cuisines")]
        public ActionResult CreateCuisine(string name)
        {
            Cuisine newCuisine = new Cuisine(name);
            newCuisine.Save();
            return RedirectToAction("Index");
        }

        [HttpGet("/cuisines/{id}")]
        public ActionResult UpdateCuisineForm(int id)
        {
            Cuisine newCuisine = new Cuisine();
            newCuisine = Cuisine.Find(id);
            return View(newCuisine);
        }

        [HttpPost("/cuisines/{id}/update")]
        public ActionResult UpdateCuisine(int id, string name)
        {
            Cuisine newCuisine = Cuisine.Find(id);
            newCuisine.Name = name;
            newCuisine.Update();
            return RedirectToAction("CreateCuisine");
        }

        [HttpPost("/cuisines/{id}/delete")]
        public ActionResult UpdateCuisine(int id)
        {
            Cuisine newCuisine = Cuisine.Find(id);
            newCuisine.Delete();
            return RedirectToAction("CreateCuisine");
        }
    }
}
