using ArmouryUCP.WebAPI.Models;
using ArmouryUCP.WebAPI.Services.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ArmouryUCP.WebAPI.Services
{
    public class ComplaintService : IComplaintService
    {
        private readonly string connectionString = "Data Source = 193.203.39.226; Initial Catalog = armoury_samp; User ID = armoury_sampuser; Password=)s}35@e]8J-2eST[";

        public ComplaintService()
        {
            //this.connectionString = connectionString;
        }

        public Complaint GetComplaint(int complaint)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from complaints where ComplaintID = {complaint} LIMIT 1", connection);

                Complaint result = null;

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = new Complaint()
                        {
                            ID = Convert.ToInt32(reader["ComplaintID"]),
                            ReporterID = Convert.ToInt32(reader["ReporterID"]),
                            ReportedID = Convert.ToInt32(reader["ReportedID"]),
                            ReporterSkin = Convert.ToInt32(reader["ReporterSkin"]),
                            ReportedSkin = Convert.ToInt32(reader["ReportedSkin"]),
                            ReporterName = reader["ReporterName"].ToString(),
                            ReportedName = reader["ReportedName"].ToString(),
                            CreationDate = DateTime.Parse(reader["CreationDate"].ToString()),
                            Reason = reader["Reason"].ToString(),
                            Status = reader["Status"].ToString(),
                            Description = reader["Description"].ToString(),
                            ReporterLevel = Convert.ToInt32(reader["ReporterLevel"]),
                            ReportedLevel = Convert.ToInt32(reader["ReportedLevel"]),
                            ReporterFaction = reader["ReporterFaction"].ToString(),
                            ReportedFaction = reader["ReportedFaction"].ToString(),
                            ReporterLevelProgress = Convert.ToInt32(reader["ReporterLevelProgress"]),
                            ReportedLevelProgress = Convert.ToInt32(reader["ReportedLevelProgress"])
                        };
                    }
                }

                if (result != null)
                {
                    cmd = new MySqlCommand($"select * from complaints_comments where ComplaintID = {complaint} ORDER BY CreationDate ASC", connection);
                    result.Comments = new List<Comment>();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Comments.Add(new Comment {
                                Creator = reader["Creator"].ToString(),
                                Content = reader["Content"].ToString(),
                                CreationDate = DateTime.Parse(reader["CreationDate"].ToString()),
                                Score = Convert.ToInt32(reader["Score"]),
                                CreatorAdminLevel = Convert.ToInt32(reader["CreatorAdminLevel"])
                            });
                        }
                    }
                }

                return result;
            }
        }

        public List<Complaint> GetComplaints(int number = 10, int start = 0)
        {
            var complaints = new List<Complaint>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from complaints where ComplaintID >= {start} limit {number}", connection);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        complaints.Add(new Complaint()
                        {
                            ID = Convert.ToInt32(reader["ComplaintID"]),
                            ReporterID = Convert.ToInt32(reader["ReporterID"]),
                            ReportedID = Convert.ToInt32(reader["ReportedID"]),
                            ReporterSkin = Convert.ToInt32(reader["ReporterSkin"]),
                            ReportedSkin = Convert.ToInt32(reader["ReportedSkin"]),
                            ReporterName = reader["ReporterName"].ToString(),
                            ReportedName = reader["ReportedName"].ToString(),
                            CreationDate = DateTime.Parse(reader["CreationDate"].ToString()),
                            Reason = reader["Reason"].ToString(),
                            Status = reader["Status"].ToString()
                        });
                    }
                }
            }
            return complaints;
        }

        public GlobalComplaintInformation GetGlobalInformationForComplaints(int playerID = -1)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var playerComplaintInformationString = playerID == -1 ? "" : $", SUM(CASE WHEN ReporterID = {playerID} THEN 1 ELSE 0 END) as PlayerComplaints, SUM(CASE WHEN ReportedID = {playerID} THEN 1 ELSE 0 END) as ComplaintsAgainstPlayer, SUM(CASE WHEN ReporterID = {playerID} AND Status = 'Closed' THEN 1 ELSE 0 END) as SuccessfulPlayerComplaints, SUM(CASE WHEN ReportedID = {playerID} AND Status = 'Closed' THEN 1 ELSE 0 END) as PlayerSanctionsReceived";
                MySqlCommand cmd = new MySqlCommand($"SELECT count(*) AS Total, SUM(CASE WHEN Status != 'Closed' THEN 1 ELSE 0 END) as TotalOpen{playerComplaintInformationString} FROM complaints", connection);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return new GlobalComplaintInformation() {
                            Total = Convert.ToInt32(reader["Total"]),
                            TotalOpen = Convert.ToInt32(reader["TotalOpen"]),
                            PlayerComplaintInformation = playerID == -1 ? 
                                new PlayerComplaintInformation() : 
                                new PlayerComplaintInformation()
                                {
                                    ComplaintsCreated = Convert.ToInt32(reader["PlayerComplaints"]),
                                    SuccessfulComplaints = Convert.ToInt32(reader["SuccessfulPlayerComplaints"]),
                                    ComplaintsAgainst = Convert.ToInt32(reader["ComplaintsAgainstPlayer"]),
                                    SanctionsReceived = Convert.ToInt32(reader["PlayerSanctionsReceived"])
                                }
                        };
                    }
                }
            }
            return null;
        }
    }
}