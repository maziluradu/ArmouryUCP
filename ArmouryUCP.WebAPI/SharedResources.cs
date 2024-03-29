﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace ArmouryUCP.WebAPI
{
    public static class SharedResources
    {
        public static int MaxUsernameLength = 32;
        public static int MaxPasswordLength = 32;

        public static List<string> Skills = new List<string> {
            "DetSkill",
            "SexSkill",
            "LawSkill",
            "MechSkill",
            "JackSkill",
            "NewsSkill",
            "DrugsSkill",
            "FishSkill",
            "TruckerSkill",
            "BoxingSkill",
            "KarateSkill",
            "GrabkickSkill",
            "MatsSkill",
            "ArmsSkill",
            "RepairSkill"
        };

        public static List<string> SkillNiceNames = new List<string> {
            "Detective",
            "Prostitute",
            "Lawyer",
            "Mechanic",
            "Car Jacker",
            "News Reporter",
            "Drugs Dealer",
            "Fisher",
            "Trucker",
            "Boxer",
            "Karate",
            "Grabkick",
            "Materials",
            "Arms Dealer",
            "Repair"
        };

        public static List<string> SkillIcons = new List<string> {
            "fa-user-secret",
            "fa-glass-martini-alt",
            "fa-gavel",
            "fa-tools",
            "fa-screwdriver",
            "fa-bullhorn",
            "fa-cannabis",
            "fa-fish",
            "fa-truck-moving",
            "fa-fist-raised",
            "fa-praying-hands",
            "fa-sign-language",
            "fa-box-open",
            "fa-bomb",
            "fa-wrench"
        };

        public static List<string> VehicleNames = new List<string> {
	        "Landstalker",
	        "Bravura",
	        "Buffalo",
	        "Linerunner",
	        "Perrenial",
	        "Sentinel",
	        "Dumper",
	        "Firetruck",
	        "Trashmaster",
	        "Stretch",
	        "Manana",
	        "Infernus",
	        "Voodoo",
	        "Pony",
	        "Mule",
	        "Cheetah",
	        "Ambulance",
	        "Leviathan",
	        "Moonbeam",
	        "Esperanto",
	        "Taxi",
	        "Washington",
	        "Bobcat",
	        "Mr Whoopee",
	        "BF Injection",
	        "Hunter",
	        "Premier",
	        "Enforcer",
	        "Securicar",
	        "Banshee",
	        "Predator",
	        "Bus",
	        "Rhino",
	        "Barracks",
	        "Hotknife",
	        "Trailer 1", //artict1
	        "Previon",
	        "Coach",
	        "Cabbie",
	        "Stallion",
	        "Rumpo",
	        "RC Bandit",
	        "Romero",
	        "Packer",
	        "Monster",
	        "Admiral",
	        "Squalo",
	        "Seasparrow",
	        "Pizzaboy",
	        "Tram",
	        "Trailer 2", //artict2
	        "Turismo",
	        "Speeder",
	        "Reefer",
	        "Tropic",
	        "Flatbed",
	        "Yankee",
	        "Caddy",
	        "Solair",
	        "Berkley's RC Van",
	        "Skimmer",
	        "PCJ-600",
	        "Faggio",
	        "Freeway",
	        "RC Baron",
	        "RC Raider",
	        "Glendale",
	        "Oceanic",
	        "Sanchez",
	        "Sparrow",
	        "Patriot",
	        "Quad",
	        "Coastguard",
	        "Dinghy",
	        "Hermes",
	        "Sabre",
	        "Rustler",
	        "ZR-350",
	        "Walton",
	        "Regina",
	        "Comet",
	        "BMX",
	        "Burrito",
	        "Camper",
	        "Marquis",
	        "Baggage",
	        "Dozer",
	        "Maverick",
	        "News Chopper",
	        "Rancher",
	        "FBI-Rancher",
	        "Virgo",
	        "Greenwood",
	        "Jetmax",
	        "Hotring",
	        "Sandking",
	        "Blista Compact",
	        "Police Maverick",
	        "Boxville",
	        "Benson",
	        "Mesa",
	        "RC Goblin",
	        "Hotring Racer A", //hotrina
	        "Hotring Racer B", //hotrinb
	        "Bloodring Banger",
	        "Rancher",
	        "Super GT",
	        "Elegant",
	        "Journey",
	        "Bike",
	        "Mountain Bike",
	        "Beagle",
	        "Cropdust",
	        "Stunt",
	        "Tanker", //petro
	        "Roadtrain",
	        "Nebula",
	        "Majestic",
	        "Buccaneer",
	        "Shamal",
	        "Hydra",
	        "FCR-900",
	        "NRG-500",
	        "HPV1000",
	        "Cement Truck",
	        "Tow Truck",
	        "Fortune",
	        "Cadrona",
	        "FBI-Truck",
	        "Willard",
	        "Forklift",
	        "Tractor",
	        "Combine",
	        "Feltzer",
	        "Remington",
	        "Slamvan",
	        "Blade",
	        "Freight",
	        "Streak",
	        "Vortex",
	        "Vincent",
	        "Bullet",
	        "Clover",
	        "Sadler",
	        "Firetruck LA", //firela
	        "Hustler",
	        "Intruder",
	        "Primo",
	        "Cargobob",
	        "Tampa",
	        "Sunrise",
	        "Merit",
	        "Utility",
	        "Nevada",
	        "Yosemite",
	        "Windsor",
	        "Monster A", //monstera
	        "Monster B", //monsterb
	        "Uranus",
	        "Jester",
	        "Sultan",
	        "Stratum",
	        "Elegy",
	        "Raindance",
	        "RC Tiger",
	        "Flash",
	        "Tahoma",
	        "Savanna",
	        "Bandito",
	        "Freight Flat", //freiflat
	        "Streak Carriage", //streakc
	        "Kart",
	        "Mower",
	        "Duneride",
	        "Sweeper",
	        "Broadway",
	        "Tornado",
	        "AT-400",
	        "DFT-30",
	        "Huntley",
	        "Stafford",
	        "BF-400",
	        "Newsvan",
	        "Tug",
	        "Trailer 3", //petrotr
	        "Emperor",
	        "Wayfarer",
	        "Euros",
	        "Hotdog",
	        "Club",
	        "Freight Carriage", //freibox
	        "Trailer 3", //artict3
	        "Andromada",
	        "Dodo",
	        "RC Cam",
	        "Launch",
	        "Police-LSPD",
	        "Police-SFPD",
	        "Police-LVPD",
	        "Police-Ranger",
	        "Picador",
	        "S.W.A.T. Van",
	        "Alpha",
	        "Phoenix",
	        "Glendale",
	        "Sadler",
	        "Luggage Trailer A", //bagboxa
	        "Luggage Trailer B", //bagboxb
	        "Stair Trailer", //tugstair
	        "Boxville",
	        "Farm Plow", //farmtr1
	        "Utility Trailer" //utiltr1
        };

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
            "Miner",
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