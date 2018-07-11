using Microsoft.AspNetCore.Mvc;
using BestRestaurants.Models;

namespace BestRestaurants.Controllers
{
    public class CuisinesController : Controller
    {
        [HttpGet("/cuisines")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
