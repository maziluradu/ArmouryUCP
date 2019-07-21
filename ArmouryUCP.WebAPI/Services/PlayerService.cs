using ArmouryUCP.WebAPI.Models;
using ArmouryUCP.WebAPI.Services.Interfaces;
using MySql.Data.MySqlClient;
using OC.Core.Crypto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ArmouryUCP.WebAPI.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly string connectionString = "Data Source = 193.203.39.226; Initial Catalog = armoury_samp; User ID = armoury_sampuser; Password=)s}35@e]8J-2eST[";

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

        public List<Player> SearchPlayers(string name, bool incomplete = false)
        {
            var players = new List<Player>();
            var cleanUsername = name.Substring(0, SharedResources.MaxUsernameLength > name.Length ? name.Length : SharedResources.MaxUsernameLength);

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var playersToReturn = incomplete ? 20 : 1;
                MySqlCommand cmd = new MySqlCommand($"SELECT * FROM wp_users WHERE user_login {(incomplete ? "LIKE" : "=")} '{(incomplete ? "%" : "")}@username{(incomplete ? "%" : "")}' ORDER BY `Level` DESC LIMIT {playersToReturn}", connection);
                cmd.Parameters.AddWithValue("@username", cleanUsername);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        players.Add(new Player()
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            Name = reader["user_login"].ToString(),
                            Model = Convert.ToInt32(reader["Model"]),
                            LastLogin = DateTime.Parse(reader["LastLogin"].ToString()),
                            Level = Convert.ToInt32(reader["Level"]),
                            Member = Convert.ToInt32(reader["Faction"]),
                            Leader = Convert.ToInt32(reader["iRank"]) >= 7 ? Convert.ToInt32(reader["Faction"]) : 0,
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
                MySqlCommand cmd = new MySqlCommand($"SELECT * FROM wp_users WHERE ID = @id", connection);
                cmd.Parameters.AddWithValue("@id", id);

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
                            Skills = skills,
                            Connected = Convert.ToInt32(reader["Connected"]) == 1,
                            TotalShots = Convert.ToInt32(reader["totalShots"]),
                            TotalHits = Convert.ToInt32(reader["totalHits"]),
                            TorsoHits = Convert.ToInt32(reader["hitsTorso"]),
                            GroinHits = Convert.ToInt32(reader["hitsGroin"]),
                            LeftArmHits = Convert.ToInt32(reader["hitsLeftArm"]),
                            RightArmHits = Convert.ToInt32(reader["hitsRightArm"]),
                            LeftLegHits = Convert.ToInt32(reader["hitsLeftLeg"]),
                            RightLegHits = Convert.ToInt32(reader["hitsRightLeg"]),
                            HeadHits = Convert.ToInt32(reader["hitsHead"]),
                            TotalKills = Convert.ToInt32(reader["totalKills"]),
                            WeaponHitInformation = reader["weaponHits"].ToString().Split('/').Select((item, index) => new WeaponData() { Weapon = index, Amount = !string.IsNullOrWhiteSpace(item) ? Convert.ToInt32(item) : 0 }).ToList(),
                            WeaponShotInformation = reader["weaponShots"].ToString().Split('/').Select((item, index) => new WeaponData() { Weapon = index, Amount = !string.IsNullOrWhiteSpace(item) ? Convert.ToInt32(item) : 0 }).ToList(),
                            WeaponKillInformation = reader["weaponKills"].ToString().Split('/').Select((item, index) => new WeaponData() { Weapon = index, Amount = !string.IsNullOrWhiteSpace(item) ? Convert.ToInt32(item) : 0 }).ToList()
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
            var cleanUsername = name.Substring(0, SharedResources.MaxUsernameLength > name.Length ? name.Length : SharedResources.MaxUsernameLength);
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand($"SELECT * FROM wp_users WHERE user_login = @username", connection);
                cmd.Parameters.AddWithValue("@username", cleanUsername);

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
                            Skills = skills,
                            Connected = Convert.ToInt32(reader["Connected"]) == 1
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

        public Player LoginPlayer(string username, string password)
        {
            var cleanUsername = username.Substring(0, username.Length >= SharedResources.MaxUsernameLength ? SharedResources.MaxUsernameLength : username.Length);
            var cleanPassword = password.Substring(0, password.Length >= SharedResources.MaxPasswordLength ? SharedResources.MaxPasswordLength : password.Length);
            Player player = null;
            List<Skill> skills = new List<Skill>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand($"SELECT * FROM wp_users WHERE user_login = @username AND user_pass = @password", connection);
                cmd.Parameters.AddWithValue("@username", cleanUsername);
                cmd.Parameters.AddWithValue("@password", new Hash().Whirlpool(cleanPassword));

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