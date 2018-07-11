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

        public Cuisine (string name, int id = 0)
        {
            Name = name;
            Id = id;
        }

        public Cuisine ()
        {
            
        }

        public override bool Equals(System.Object otherCuisine)
        {
            if (!(otherCuisine is Cuisine))
            {
                return false;
            }
            else
            {
                Cuisine newCuisine = (Cuisine) otherCuisine;
                bool nameEquality = (this.Name == newCuisine.Name);
                return (nameEquality);
            }
        }

        public static List<Cuisine> GetAll()
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

        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM cuisines;";
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
            cmd.CommandText = @"INSERT INTO cuisines (name) VALUES (@RestaurantName);";
            cmd.Parameters.AddWithValue("@RestaurantName", this.Name);

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
            cmd.CommandText = @"UPDATE cuisines SET name = @CuisineName WHERE id = @CuisineId;";

            cmd.Parameters.AddWithValue("@CuisineName", this.Name);
            cmd.Parameters.AddWithValue("@CuisineId", this.Id);

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
            cmd.CommandText = @"DELETE FROM cuisines WHERE id = @CuisineId;";

            cmd.Parameters.AddWithValue("@CuisineId", this.Id);

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static Cuisine Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM cuisines WHERE id = @searchId;";
            cmd.Parameters.AddWithValue("@searchId", id);
            var rdr = cmd.ExecuteReader() as MySqlDataReader;

            int cuisineId = 0;
            string cuisineName = "";

            while (rdr.Read())
            {
                cuisineId = rdr.GetInt32(0);
                cuisineName = rdr.GetString(1);
            }

            Cuisine foundCuisine = new Cuisine(cuisineName, cuisineId);

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return foundCuisine;
        }
    }
}
