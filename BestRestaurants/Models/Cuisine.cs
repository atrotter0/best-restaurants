using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using BestRestaurants;

namespace BestRestaurants.Models
{
    public class Cuisine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RestaurantId { get; set; }

        public Cuisine (string name)
        {
            Name = name;
        }
    }
}
