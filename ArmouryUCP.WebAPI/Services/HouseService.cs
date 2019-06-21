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

        public List<House> GetHouses(int number = 10, int start = 0)
        {
            var houses = new List<House>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from houses where hID >= {start} limit {number}", connection);

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
                            Price = Convert.ToInt32(reader["Price"]),
                            InteriorIndex = Convert.ToInt32(reader["UpgradeLock"]),
                            DateOfPurchase = DateTime.Parse(reader["DateOfPurchase"].ToString())
                        });
                    }
                }
            }
            return houses;
        }

        public GlobalHouseInformation GetGlobalInformationForHouses()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand($"SELECT x.Total, x.TotalOwned, x.TotalForSale, y.TotalTenants FROM wp_users, houses, (SELECT count(*) as Total, SUM(CASE WHEN Owner != 'Unbought' THEN 1 ELSE 0 END) as TotalOwned, SUM(CASE WHEN Owner != 'Unbought' AND Price > 0 THEN 1 ELSE 0 END) as TotalForSale FROM houses) as x, (SELECT SUM(CASE WHEN House != 255 THEN 1 ELSE 0 END) as TotalTenants FROM wp_users) as y", connection);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return new GlobalHouseInformation() {
                            Total = Convert.ToInt32(reader["Total"]),
                            TotalOwned = Convert.ToInt32(reader["TotalOwned"]),
                            TotalForSale = Convert.ToInt32(reader["TotalForSale"]),
                            TotalTenants = Convert.ToInt32(reader["TotalTenants"]) - Convert.ToInt32(reader["TotalOwned"])
                        };
                    }
                }
            }
            return null;
        }
    }
}