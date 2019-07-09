using ArmouryUCP.WebAPI.Models;
using ArmouryUCP.WebAPI.Services.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ArmouryUCP.WebAPI.Services
{
    public class BusinessService : IBusinessService
    {
        private readonly string connectionString = "Data Source=89.44.120.165;Initial Catalog=acevixco_samp;User ID=acevixco_sampusr;Password=xsN3m9d8UT0sK";

        public BusinessService()
        {
            //this.connectionString = connectionString;
        }

        public Business GetBusiness(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open(); 
                MySqlCommand cmd = new MySqlCommand($"select * from biz where bID = '{id}' ", connection);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return new Business()
                        {
                            Id = Convert.ToInt32(reader["bID"]),
                            Level = Convert.ToInt32(reader["bLevel"]),
                            Name = reader["bMessage"].ToString(),
                            Owner = reader["bOwner"].ToString(),
                            Value = Convert.ToInt32(reader["bValue"]),
                            Type = Convert.ToInt32(reader["bType"]),
                            Extortion = reader["bExtortion"].ToString(),
                            Prods = Convert.ToInt32(reader["bProds"]),
                            ComercialAd = Convert.ToInt32(reader["bMultiplier"]),
                            DateOfPurchase = DateTime.Parse(reader["DateOfPurchase"].ToString())
                        };
                    }
                }
            }
            return null;
        }

        public Business GetBusinessByTill()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand($"SELECT * FROM biz ORDER BY bTill DESC LIMIT 1", connection);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return new Business()
                        {
                            Id = Convert.ToInt32(reader["bID"]),
                            Level = Convert.ToInt32(reader["bLevel"]),
                            Name = reader["bMessage"].ToString(),
                            Owner = reader["bOwner"].ToString(),
                            Value = Convert.ToInt32(reader["bValue"]),
                            Type = Convert.ToInt32(reader["bType"]),
                            Extortion = reader["bExtortion"].ToString()
                        };
                    }
                }
            }
            return null;
        }

        public List<Business> GetBusinesses(string owner)
        {
            var businesses = new List<Business>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from biz where bOwner = '{owner}' limit 3", connection);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        businesses.Add(new Business()
                        {
                            Id = Convert.ToInt32(reader["bID"]),
                            Level = Convert.ToInt32(reader["bLevel"]),
                            Name = reader["bMessage"].ToString(),
                            Owner = reader["bOwner"].ToString(),
                            Value = Convert.ToInt32(reader["bValue"]),
                            Type = Convert.ToInt32(reader["bType"]),
                            DateOfPurchase = DateTime.Parse(reader["DateOfPurchase"].ToString())
                        });
                    }
                }
            }
            return businesses;
        }

        public List<Business> GetBusinesses(int number = 0, int start = 0)
        {
            var businesses = new List<Business>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from biz where bID >= {start} limit {number}", connection);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        businesses.Add(new Business()
                        {
                            Id = Convert.ToInt32(reader["bID"]),
                            Level = Convert.ToInt32(reader["bLevel"]),
                            Name = reader["bMessage"].ToString(),
                            Owner = reader["bOwner"].ToString(),
                            Value = Convert.ToInt32(reader["bValue"]),
                            Type = Convert.ToInt32(reader["bType"]),
                            DateOfPurchase = DateTime.Parse(reader["DateOfPurchase"].ToString())
                        });
                    }
                }
            }
            return businesses;
        }

        public GlobalBusinessInformation GetGlobalInformationForBusinesses()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand($"SELECT count(*) as Total, SUM(CASE WHEN bOwner != 'None' THEN 1 ELSE 0 END) as TotalOwned, SUM(CASE WHEN bOwner != 'None' AND bPrice > 0 THEN 1 ELSE 0 END) as TotalForSale FROM biz", connection);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return new GlobalBusinessInformation()
                        {
                            Total = Convert.ToInt32(reader["Total"]),
                            TotalOwned = Convert.ToInt32(reader["TotalOwned"]),
                            TotalForSale = Convert.ToInt32(reader["TotalForSale"])
                        };
                    }
                }
            }
            return null;
        }
    }
}