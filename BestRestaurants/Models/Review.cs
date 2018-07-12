using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using BestRestaurants;

namespace BestRestaurants.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string ReviewerName { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public int RestaurantId { get; set; }

        public Review (string reviewerName, string description, int rating, int id = 0, int restaurantId = 0)
        {
            ReviewerName = reviewerName;
            Description = description;
            Rating = rating;
            Id = id;
            RestaurantId = restaurantId;
        }

        public override bool Equals(System.Object otherReview)
        {
            if (!(otherReview is Review))
            {
                return false;
            }
            else
            {
                Review newReview = (Review) otherReview;
                bool reviewerNameEquality = (this.ReviewerName == newReview.ReviewerName);
                bool descriptionEquality = (this.Description == newReview.Description);
                bool ratingEquality = (this.Rating == newReview.Rating);
                return (reviewerNameEquality && descriptionEquality && ratingEquality);
            }
        }

        public static List<Review> GetAll()
        {
            List<Review> allReviews = new List<Review>() {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM reviews;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string reviewerName = rdr.GetString(1);
                string description = rdr.GetString(2);
                int rating = rdr.GetInt32(3);
                int restaurantId = rdr.GetInt32(4);
                Review newReview = new Review(reviewerName, description, rating, id, restaurantId);
                allReviews.Add(newReview);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allReviews;
        }

        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM reviews;";
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
            cmd.CommandText = @"INSERT INTO reviews (reviewer_name, description, rating, restaurant_id) VALUES (@ReviewerName, @Description, @Rating, @RestaurantId);";
            cmd.Parameters.AddWithValue("@ReviewerName", this.ReviewerName);
            cmd.Parameters.AddWithValue("@Description", this.Description);
            cmd.Parameters.AddWithValue("@Rating", this.Rating);
            cmd.Parameters.AddWithValue("@RestaurantId", this.RestaurantId);
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
            cmd.CommandText = @"UPDATE reviews SET reviewer_name = @ReviewerName, description = @Description, rating = @Rating, restaurant_id = @RestaurantId  WHERE id = @Id;";
            cmd.Parameters.AddWithValue("@ReviewerName", this.ReviewerName);
            cmd.Parameters.AddWithValue("@Description", this.Description);
            cmd.Parameters.AddWithValue("@Rating", this.Rating);
            cmd.Parameters.AddWithValue("@RestaurantId", this.RestaurantId);
            cmd.Parameters.AddWithValue("@Id", this.Id);
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
            cmd.CommandText = @"DELETE FROM reviews WHERE id = @Id;";
            cmd.Parameters.AddWithValue("@Id", this.Id);
            cmd.ExecuteNonQuery();


            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static Review Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM reviews WHERE id = @searchId;";
            cmd.Parameters.AddWithValue("@searchId", id);
            var rdr = cmd.ExecuteReader() as MySqlDataReader;

            int reviewId = 0;
            string reviewerName = "";
            string description = "";
            int rating = 0;
            int restaurantId = 0;

            while (rdr.Read())
            {
                reviewId = rdr.GetInt32(0);
                reviewerName = rdr.GetString(1);
                description = rdr.GetString(2);
                rating = rdr.GetInt32(3);
                restaurantId = rdr.GetInt32(4);
            }

            Review foundReview = new Review(reviewerName, description, rating, reviewId, restaurantId);

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return foundReview;
        }

        public Restaurant GetRestaurant()
        {
            Restaurant newRestaurant = new Restaurant("",  "");

            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM restaurants WHERE id = " + this.RestaurantId + ";";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                string price = rdr.GetString(2);
                int cuisineId = rdr.GetInt32(3);

                newRestaurant.Name = name;
                newRestaurant.Price = price;
                newRestaurant.Id = id;
                newRestaurant.CuisineId = cuisineId;
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return newRestaurant;
        }

    }
}
