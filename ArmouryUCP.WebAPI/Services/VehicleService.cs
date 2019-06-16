using ArmouryUCP.WebAPI.Models;
using ArmouryUCP.WebAPI.Services.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ArmouryUCP.WebAPI.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly string connectionString = "Data Source=89.44.120.165;Initial Catalog=acevixco_samp;User ID=acevixco_sampusr;Password=xsN3m9d8UT0sK";

        public VehicleService()
        {
            //this.connectionString = connectionString;
        }

        public List<Vehicle> GetVehicles(string owner)
        {
            var vehicles = new List<Vehicle>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from vehicles where Owner = '{owner}' limit 10", connection);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        vehicles.Add(new Vehicle()
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Model = Convert.ToInt32(reader["Model"]),
                            Owner = reader["Owner"].ToString(),
                            Value = Convert.ToInt32(reader["Price"]),
                            DateOfPurchase = DateTime.Parse(reader["vLastBought"].ToString())
                        });
                    }
                }
            }
            return vehicles;
        }
    }
}