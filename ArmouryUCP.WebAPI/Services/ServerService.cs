using ArmouryUCP.WebAPI.Models;
using ArmouryUCP.WebAPI.Services.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ArmouryUCP.WebAPI.Services
{
    public class ServerService : IServerService
    {
        private readonly string connectionString = "Data Source=89.44.120.165;Initial Catalog=acevixco_samp;User ID=acevixco_sampusr;Password=xsN3m9d8UT0sK";

        public ServerService()
        {
            //this.connectionString = connectionString;
        }

        public ServerInformation GetServerInformation()
        {
            ServerInformation serverInformation = null;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                var selectAddition = string.Empty;
                var selectAddition2 = string.Empty;

                for (int i = 1; i < SharedResources.Jobs.Count; i++)
                {
                    selectAddition += $", x.Job{i}Count ";
                    selectAddition2 += $", count(CASE Job WHEN {i} THEN 1 ELSE NULL END) as Job{i}Count ";
                }

                connection.Open();
                MySqlCommand cmd = new MySqlCommand($"SELECT x.TotalEmployedPlayers, x.TotalPlayerMoney, x.HighestLevel, x.HighestEarnerMoney, y.UpTime, y.PreviousDayTotalMoney{selectAddition} FROM wp_users, stuff, (SELECT max(PreviousDayMoney) as HighestEarnerMoney, count(CASE WHEN Job > 0 THEN 1 ELSE NULL END) as TotalEmployedPlayers, sum(Bank) AS TotalPlayerMoney, max(Level) as HighestLevel{selectAddition2} FROM wp_users) as x, (SELECT DATEDIFF(NOW(), ServerOpening) as UpTime, PreviousDayTotalMoney as PreviousDayTotalMoney FROM stuff) as y LIMIT 1", connection);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var jobDictionary = new Dictionary<int, KeyValuePair<int, string>>();
                        foreach (var job in SharedResources.Jobs)
                        {
                            try
                            {
                                jobDictionary.Add(SharedResources.Jobs.IndexOf(job), new KeyValuePair<int, string>(Convert.ToInt32(reader[$"Job{SharedResources.Jobs.IndexOf(job)}Count"]), job));
                            }
                            catch (Exception) { }
                        }

                        jobDictionary = jobDictionary.OrderByDescending(job => job.Value.Key).ToDictionary(pair => pair.Key, pair => pair.Value);

                        serverInformation = new ServerInformation()
                        {
                            HighestLevel = Convert.ToInt32(reader["HighestLevel"]),
                            TotalPlayerMoney = Convert.ToInt64(reader["TotalPlayerMoney"]),
                            Uptime = Convert.ToInt32(reader["UpTime"]),
                            PreviousDayTotalMoney = Convert.ToInt64(reader["PreviousDayTotalMoney"]),
                            MostPopularJob = jobDictionary.First().Value.Value,
                            MostPopularJobPercentage = jobDictionary.First().Value.Key * 100 / Convert.ToSingle(reader["TotalEmployedPlayers"]),
                            HighestEarnerMoney = Convert.ToInt64(reader["HighestEarnerMoney"])
                        };
                    }
                }

                cmd = new MySqlCommand($"SELECT ID,user_login FROM wp_users WHERE PreviousDayMoney = {serverInformation.HighestEarnerMoney} LIMIT 1", connection);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        serverInformation.HighestEarner = reader["user_login"].ToString();
                        serverInformation.HighestEarnerId = Convert.ToInt32(reader["ID"]);
                    }
                }
            }
            return serverInformation;
        }

        /*public List<LogEntry> GetServerNews()
        {
            var logEntries = new List<LogEntry>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                var sql_query = "SELECT Data, Continut FROM log1";
                for(int i = 1; i < SharedResources.Factions.Count; i++)
                {
                    sql_query += $" UNION SELECT Data, Continut FROM log{i}";
                }

                connection.Open();
                MySqlCommand cmd = new MySqlCommand($"{sql_query} ORDER BY Data DESC LIMIT 10", connection);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        logEntries.Add(new LogEntry {
                            Content = reader["Continut"].ToString(),
                            Date = DateTime.Parse(reader["Data"].ToString())
                        });
                    }
                }
            }

            return logEntries;
        }*/

        public List<LogEntry> GetServerNews()
        {
            var logEntries = new List<LogEntry>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand($"SELECT Data,Continut,SkinID FROM news ORDER BY Data DESC LIMIT 10", connection);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        logEntries.Add(new LogEntry
                        {
                            Content = reader["Continut"].ToString(),
                            Date = DateTime.Parse(reader["Data"].ToString()),
                            Skin = Convert.ToInt32(reader["SkinID"]),
                        });
                    }
                }
            }

            return logEntries;
        }
    }
}