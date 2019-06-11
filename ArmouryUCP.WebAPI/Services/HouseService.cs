using ArmouryUCP.WebAPI.Models;
using ArmouryUCP.WebAPI.Services.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ArmouryUCP.WebAPI.Services
{
    public class HouseService : IHouseService
    {
        private readonly string connectionString = "Data Source=89.44.120.165;Initial Catalog=acevixco_samp;User ID=acevixco_sampusr;Password=xsN3m9d8UT0sK";

        public HouseService()
        {
            //this.connectionString = connectionString;
        }

        public List<House> GetHouses(string owner)
        {
            var houses = new List<House>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from houses where Owner = '{owner}' limit 3", connection);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        houses.Add(new House()
                        {
                            Id = Convert.ToInt32(reader["hID"]),
                            Level = Convert.ToInt32(reader["Level"]),
                            Name = reader["Name"].ToString(),
                            Owner = reader["Owner"].ToString(),
                            Value = Convert.ToInt32(reader["Value"]),
                            InteriorIndex = Convert.ToInt32(reader["UpgradeLock"]),
                            DateOfPurchase = DateTime.Parse(reader["DateOfPurchase"].ToString())
                        });
                    }
                }
            }
            return houses;
        }
    }
}