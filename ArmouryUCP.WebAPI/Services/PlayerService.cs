using ArmouryUCP.WebAPI.Models;
using ArmouryUCP.WebAPI.Services.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ArmouryUCP.WebAPI.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly string connectionString = "Data Source=89.44.120.165;Initial Catalog=acevixco_samp;User ID=acevixco_sampusr;Password=xsN3m9d8UT0sK";

        public PlayerService()
        {
            //this.connectionString = connectionString;
        }

        public List<Player> GetPlayers(int playersToReturn = 10)
        {
            var players = new List<Player>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from wp_users where id > 0 limit {playersToReturn}", connection);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        players.Add(new Player()
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            Name = reader["user_login"].ToString()
                        });
                    }
                }
            }
            return players;
        }

        public Player GetPlayer(int id)
        {
            Player player = null;
            List<Skill> skills = new List<Skill>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand($"SELECT * FROM wp_users WHERE ID = {id}", connection);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        foreach (var skill in SharedResources.Skills)
                        {
                            if (Convert.ToInt32(reader[skill]) >= 100)
                                skills.Add(new Skill {
                                    Name = skill,
                                    Progress = Convert.ToInt32(reader[skill]) > 500 ? 500 : Convert.ToInt32(reader[skill]),
                                    NameNice = SharedResources.SkillNiceNames[SharedResources.Skills.IndexOf(skill)],
                                    Icon = SharedResources.SkillIcons[SharedResources.Skills.IndexOf(skill)]
                                });
                        }

                        player = new Player()
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            Name = reader["user_login"].ToString(),
                            Level = Convert.ToInt32(reader["Level"]),
                            Member = Convert.ToInt32(reader["Faction"]),
                            Member2 = Convert.ToInt32(reader["Member2"]),
                            Cash = Convert.ToInt32(reader["Money"]),
                            Bank = Convert.ToInt32(reader["Bank"]),
                            DonateRank = Convert.ToInt32(reader["DonateRank"]),
                            AdminLevel = Convert.ToInt32(reader["AdminLevel"]),
                            Respect = Convert.ToInt32(reader["Respect"]),
                            LastLogin = DateTime.Parse(reader["LastLogin"].ToString()),
                            Model = Convert.ToInt32(reader["Model"]),
                            ConnectedTime = Convert.ToInt32(reader["ConnectedTime"]),
                            Age = Convert.ToInt32(reader["Age"]),
                            Sex = Convert.ToInt32(reader["Sex"]),
                            Warnings = Convert.ToInt32(reader["Warnings"]),
                            Job = Convert.ToInt32(reader["Job"]),
                            SecondaryJob = Convert.ToInt32(reader["Job1"]),
                            FactionRank = Convert.ToInt32(reader["iRank"]),
                            Leader = Convert.ToInt32(reader["iRank"]) >= 7 ? Convert.ToInt32(reader["Faction"]) : 0,
                            FactionWarnings = Convert.ToInt32(reader["Fwarn"]),
                            FactionPunish = Convert.ToInt32(reader["Punish"]),
                            FactionActivity = 0,
                            FactionMemberSince = DateTime.Parse(reader["LastLogin"].ToString()),
                            Skills = skills
                        };
                    }
                }
            }
            return player;
        }

        public Player GetPlayer(string name)
        {
            Player player = null;
            List<Skill> skills = new List<Skill>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand($"SELECT * FROM wp_users WHERE user_login = '{name}'", connection);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        foreach (var skill in SharedResources.Skills)
                        {
                            if (Convert.ToInt32(reader[skill]) >= 100)
                                skills.Add(new Skill
                                {
                                    Name = skill,
                                    Progress = Convert.ToInt32(reader[skill]) > 500 ? 500 : Convert.ToInt32(reader[skill]),
                                    NameNice = SharedResources.SkillNiceNames[SharedResources.Skills.IndexOf(skill)],
                                    Icon = SharedResources.SkillIcons[SharedResources.Skills.IndexOf(skill)]
                                });
                        }

                        player = new Player()
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            Name = reader["user_login"].ToString(),
                            Level = Convert.ToInt32(reader["Level"]),
                            Member = Convert.ToInt32(reader["Faction"]),
                            Member2 = Convert.ToInt32(reader["Member2"]),
                            Cash = Convert.ToInt32(reader["Money"]),
                            Bank = Convert.ToInt32(reader["Bank"]),
                            DonateRank = Convert.ToInt32(reader["DonateRank"]),
                            AdminLevel = Convert.ToInt32(reader["AdminLevel"]),
                            Respect = Convert.ToInt32(reader["Respect"]),
                            LastLogin = DateTime.Parse(reader["LastLogin"].ToString()),
                            Model = Convert.ToInt32(reader["Model"]),
                            ConnectedTime = Convert.ToInt32(reader["ConnectedTime"]),
                            Age = Convert.ToInt32(reader["Age"]),
                            Sex = Convert.ToInt32(reader["Sex"]),
                            Warnings = Convert.ToInt32(reader["Warnings"]),
                            Job = Convert.ToInt32(reader["Job"]),
                            SecondaryJob = Convert.ToInt32(reader["Job1"]),
                            FactionRank = Convert.ToInt32(reader["iRank"]),
                            Leader = Convert.ToInt32(reader["iRank"]) >= 7 ? Convert.ToInt32(reader["Faction"]) : 0,
                            FactionWarnings = Convert.ToInt32(reader["Fwarn"]),
                            FactionPunish = Convert.ToInt32(reader["Punish"]),
                            FactionActivity = 0,
                            FactionMemberSince = DateTime.Parse(reader["LastLogin"].ToString()),
                            Skills = skills
                        };
                    }
                }
            }
            return player;
        }

        public List<FactionHistory> GetFactionHistory(int id)
        {
            var factionHistory = new List<FactionHistory>();
            var sql = string.Empty;
            for (int i = 1; i < SharedResources.Factions.Count; i++)
            {
                if (i > 1)
                    sql += " UNION ";
                sql += $"SELECT * FROM log{i} WHERE Continut LIKE '%/{id}>%'";
            }

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand($"{sql} ORDER BY Data DESC LIMIT 20", connection);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        factionHistory.Add(new FactionHistory()
                        {
                            Content = reader["Continut"].ToString(),
                            Date = DateTime.Parse(reader["Data"].ToString())
                        });
                    }
                }
            }
            return factionHistory;
        }

        public Player LoginUser(string username, string password)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand($"SELECT ID FROM wp_users WHERE user_login='{username}' AND user_pass='6E6D898C6392E28F4A03A93CC14A0D095FC46793B2D8FE03E14A711B3508249B401D0478AACAD6874D237B82F0D21310E3276F7327E18EE74677D71E164E7179' LIMIT 1", connection);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return GetPlayer(Convert.ToInt32(reader["ID"]));
                    }
                }
            }

            return null;
        }

        public List<Player> GetOnlinePlayers(bool showFull = false)
        {
            var players = new List<Player>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand($"SELECT x.countt, ID, Model, user_login FROM wp_users, (select count(*) as countt FROM wp_users WHERE Connected=1) as x WHERE Connected=1 ORDER BY AdminLevel DESC LIMIT 4", connection);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        players.Add(new Player()
                        {
                            Model = Convert.ToInt32(reader["Model"]),
                            ID = Convert.ToInt32(reader["ID"]),
                            Name = reader["user_login"].ToString(),
                            TotalPlayers = Convert.ToInt32(reader["countt"])
                        });
                    }
                }
            }

            return players;
        }
    }
}