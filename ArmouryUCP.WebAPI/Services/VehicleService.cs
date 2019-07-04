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

        public Vehicle GetVehicle(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from vehicles where id = '{id}'", connection);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return new Vehicle()
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Model = Convert.ToInt32(reader["Model"]),
                            Owner = reader["Owner"].ToString(),
                            Value = Convert.ToInt32(reader["Price"]),
                            KM = Convert.ToInt32(reader["KM"]),
                            Color1 = Convert.ToInt32(reader["Color1"]),
                            Color2 = Convert.ToInt32(reader["Color2"]),
                            Plate = reader["Plate"].ToString(),
                            Premium = Convert.ToInt32(reader["vMaxer"]),
                            BrokenParts = reader["vBroken"].ToString(),
                            DateOfPurchase = DateTime.Parse(reader["vLastBought"].ToString())
                        };
                    }
                }
            }
            return null;
        }
        public Vehicle GetVehicleByKM()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand($"SELECT * FROM vehicles ORDER BY KM DESC LIMIT 1", connection);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return new Vehicle()
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Model = Convert.ToInt32(reader["Model"]),
                            Owner = reader["Owner"].ToString(),
                            Value = Convert.ToInt32(reader["Price"]),
                            KM = Convert.ToInt32(reader["KM"]),
                            Color1 = Convert.ToInt32(reader["Color1"]),
                            Color2 = Convert.ToInt32(reader["Color2"]),
                            Plate = reader["Plate"].ToString(),
                            Premium = Convert.ToInt32(reader["vMaxer"]),
                            DateOfPurchase = DateTime.Parse(reader["vLastBought"].ToString())
                        };
                    }
                }
            }
            return null;
        }

        public List<Vehicle> GetVehicles(int number = 0, int start = 0)
        {
            var vehicles = new List<Vehicle>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from vehicles where id >= {start} limit {number}", connection);

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

        public GlobalVehicleInformation GetGlobalInformationForVehicles()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand($"SELECT count(*) as Total FROM vehicles", connection);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return new GlobalVehicleInformation() {
                            Total = Convert.ToInt32(reader["Total"])
                        };
                    }
                }
            }
            return null;
        }
    }
}