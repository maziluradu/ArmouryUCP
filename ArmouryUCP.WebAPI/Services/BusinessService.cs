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
    }
}