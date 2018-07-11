using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using BestRestaurants;

namespace BestRestaurants.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public int CuisineId { get; set; }
        public int ReviewId { get; set; }

        public Restaurant (string name, string price)
        {
            Name = name;
            Price = price;
        }
    }
}
