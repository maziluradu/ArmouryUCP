using System;
using System.Collections.Generic;
using System.Linq;

namespace ArmouryUCP.WebAPI
{
    public static class SharedResources
    {
        public static List<string> Factions = new List<string>()
        {
            "Civilian",
            "Los Santos Police Department",
            "Federal Bureau of Investigations",
            "Las Venturas Police Department",
            "Firemen & Paramedics",
            "Falcone Famiglia",
            "Dallas Crime Family",
            "Government",
            "Hitman Agency",
            "News Reporters",
            "Taxi Cab Company",
            "School Instructors",
            "Bonanno Famiglia",
            "Orleans Families",
            "15th Avenue Families",
            "Sacra Corona Unita",
            "Tow Car Company"
        };

        public static List<string> Jobs = new List<string>()
        {
            "Unknown",
            "Detective",
            "Lawyer",
            "Prostitute",
            "Drugs Dealer",
            "Car Jacker",
            "Reporter",
            "Unknown",
            "Bodyguard",
            "Arms Dealer",
            "Car Dealer",
            "Unknown",
            "Boxer",
            "Unknown",
            "Bus Driver",
            "Paper Boy",
            "Truck Driver",
            "Unknown",
            "Unknown",
            "Unknown",
            "Unknown",
            "Unknown",
            "Unknown",
            "Truck Driver",
            "Courier",
            "Garbage Man",
            "Duneride Driver",
            "Farmer",
            "Street Sweeper"
        };

        public static int CalculateLevelProgress(int currentLevel, int respectPoints)
        {
            var progress = Convert.ToInt32((float)respectPoints / ((float)(currentLevel - 1) * 4 + 1) * 100.0);
            if (progress >= 0 && progress <= 100)
                return progress;
            else if (progress < 0)
                return 0;
            return 100;
        }

        public static string GetJobNameFromID(int job)
        {
            return Jobs.ElementAtOrDefault(job);
        }
    }
}