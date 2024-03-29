﻿using System;
using System.Collections.Generic;

namespace ArmouryUCP.WebAPI.Models
{
    public class Player
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int Leader { get; set; }
        public int Member { get; set; }
        public int Member2 { get; set; }
        public int Cash { get; set; }
        public int Bank { get; set; }
        public int DonateRank { get; set; }
        public int AdminLevel { get; set; }
        public int Respect { get; set; }
        public int Model { get; set; }
        public int ConnectedTime { get; set; }
        public int Age { get; set; }
        public int Sex { get; set; }
        public int Warnings { get; set; }
        public int Job { get; set; }
        public int SecondaryJob { get; set; }
        public int FactionRank { get; set; }
        public int FactionWarnings { get; set; }
        public int FactionPunish { get; set; }
        public int FactionActivity { get; set; }
        public DateTime FactionMemberSince { get; set; }
        public DateTime LastLogin { get; set; }
        public List<Skill> Skills { get; set; }
        public bool Connected { get; set; }
        public int TotalShots { get; set; }
        public int TotalHits { get; set; }
        public int TorsoHits { get; set; }
        public int GroinHits { get; set; }
        public int LeftArmHits { get; set; }
        public int RightArmHits { get; set; }
        public int LeftLegHits { get; set; }
        public int RightLegHits { get; set; }
        public int HeadHits { get; set; }
        public int TotalKills { get; set; }
        public List<WeaponData> WeaponHitInformation { get; set; }
        public List<WeaponData> WeaponKillInformation { get; set; }
        public List<WeaponData> WeaponShotInformation { get; set; }

        public int TotalPlayers { get; set; }
    }
}