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

        public Restaurant (string name, string price, int id = 0, int cuisineId = 0, int reviewId = 0)
        {
            Name = name;
            Price = price;
            Id = id;
            CuisineId = cuisineId;
            ReviewId = reviewId;
        }

        public override bool Equals(System.Object otherRestaurant)
        {
            if (!(otherRestaurant is Restaurant))
            {
                return false;
            }
            else
            {
                Restaurant newRestaurant = (Restaurant) otherRestaurant;
                bool nameEquality = (this.Name == newRestaurant.Name);
                bool priceEquality = (this.Price == newRestaurant.Price);
                bool cuisineIdEquality = (this.CuisineId == newRestaurant.CuisineId);
                bool reviewIdEquality = (this.ReviewId == newRestaurant.ReviewId);
                return (nameEquality && priceEquality && cuisineIdEquality && reviewIdEquality);
            }
        }

        public List<Cuisine> GetAllCuisines()
        {
            List<Cuisine> allCuisines = new List<Cuisine>() {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM cuisines;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int cuisineId = rdr.GetInt32(0);
                string cuisineName = rdr.GetString(1);
                Cuisine newCuisine = new Cuisine(cuisineName);
                newCuisine.Id = cuisineId;
                allCuisines.Add(newCuisine);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allCuisines;
        }

        public List<Cuisine> GetCuisines()
        {
            List<Cuisine> allCuisines = new List<Cuisine>() {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM cuisines WHERE id = " + this.CuisineId + ";";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int cuisineId = rdr.GetInt32(0);
                string cuisineName = rdr.GetString(1);
                Cuisine newCuisine = new Cuisine(cuisineName);
                newCuisine.Id = cuisineId;
                allCuisines.Add(newCuisine);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allCuisines;
        }

        public static List<Restaurant> GetAll()
        {
            List<Restaurant> allRestaurants = new List<Restaurant>() {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM restaurants;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                string price = rdr.GetString(2);
                int cuisineId = rdr.GetInt32(3);
                int reviewId = rdr.GetInt32(4);

                Restaurant newRestaurant = new Restaurant(name, price);
                newRestaurant.Id = id;
                newRestaurant.CuisineId = cuisineId;
                newRestaurant.ReviewId = reviewId;
                allRestaurants.Add(newRestaurant);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allRestaurants;
        }

        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM restaurants;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
               conn.Dispose();
            }
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO restaurants (name, price, cuisine_id, review_id) VALUES (@RestaurantName, @RestaurantPrice, @RestaurantCuisineId, @RestaurantReviewId);";
            cmd.Parameters.AddWithValue("@RestaurantName", this.Name);
            cmd.Parameters.AddWithValue("@RestaurantCuisineId", this.CuisineId);
            cmd.Parameters.AddWithValue("@RestaurantReviewId", this.ReviewId);
            cmd.Parameters.AddWithValue("@RestaurantPrice", this.Price);

            cmd.ExecuteNonQuery();
            this.Id = (int) cmd.LastInsertedId;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void Update()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE restaurants SET name = @RestaurantName, price = @RestaurantPrice, cuisine_id = @RestaurantCuisineId WHERE id = @RestaurantId;";

            cmd.Parameters.AddWithValue("@RestaurantName", this.Name);
            cmd.Parameters.AddWithValue("@RestaurantPrice", this.Price);
            cmd.Parameters.AddWithValue("@RestaurantId", this.Id);
            cmd.Parameters.AddWithValue("@RestaurantCuisineId", this.CuisineId);

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void Delete()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM restaurants WHERE id = @RestaurantId;";

            cmd.Parameters.AddWithValue("@RestaurantId", this.Id);

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static Restaurant Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM restaurants WHERE id = @searchId;";
            cmd.Parameters.AddWithValue("@searchId", id);
            var rdr = cmd.ExecuteReader() as MySqlDataReader;

            int restaurantId = 0;
            string name = "";
            string price = "";
            int cuisineId = 0;
            int reviewId = 0;

            while (rdr.Read())
            {
                 restaurantId = rdr.GetInt32(0);
                 name = rdr.GetString(1);
                 price = rdr.GetString(2);
                 cuisineId = rdr.GetInt32(3);
                 reviewId = rdr.GetInt32(4);
            }

            Restaurant foundRestaurant = new Restaurant(name, price, restaurantId, cuisineId, reviewId);

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return foundRestaurant;
        }
    }
}
