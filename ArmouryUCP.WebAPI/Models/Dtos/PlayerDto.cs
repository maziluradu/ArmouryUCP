using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArmouryUCP.WebAPI.Models.Dtos
{
    public class PlayerDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int Cash { get; set; }
        public int Bank { get; set; }
        public int Leader { get; set; }
        public int Member { get; set; }
        public int Member2 { get; set; }
        public int AdminLevel { get; set; }
        public int DonateRank { get; set; }
        public int Respect { get; set; }
        public int Model { get; set; }
        public int ConnectedTime { get; set; }
        public int Age { get; set; }
        public int Sex { get; set; }
        public int Warnings { get; set; }
        public int Job { get; set; }
        public int SecondaryJob { get; set; }
        public DateTime LastLogin { get; set; }
        public string Faction { get; set; }
        public int FactionRank { get; set; }
        public int FactionWarnings { get; set; }
        public int FactionPunish { get; set; }
        public int FactionActivity { get; set; }
        public DateTime FactionMemberSince { get; set; }

        public int LevelProgress
        {
            get
            {
                return SharedResources.CalculateLevelProgress(Level, Respect);
            }
        }

        public string SexNice
        {
            get
            {
                return Sex == 1 ? "Male" : "Female";
            }
        }

        public string JobNice
        {
            get
            {
                return SharedResources.GetJobNameFromID(Job);
            }
        }

        public string SecondaryJobNice
        {
            get
            {
                return SharedResources.GetJobNameFromID(SecondaryJob);
            }
        }

        public string FactionMemberSinceNice
        {
            get
            {
                return FactionMemberSince.ToString(FactionMemberSince.Year < DateTime.Now.Year ? "dddd, dd MMMM \"'\"yy" : "dddd, dd MMMM");
            }
        }

        public string LastLoginNice
        {
            get
            {
                return LastLogin.ToString(LastLogin.Year < DateTime.Now.Year ? "dddd, dd MMMM \"'\"yy" : "dddd, dd MMMM");
            }
        }
    }
}