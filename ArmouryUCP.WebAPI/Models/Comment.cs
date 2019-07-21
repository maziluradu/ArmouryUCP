using System;

namespace ArmouryUCP.WebAPI.Models
{
    public class Comment
    {
        public string Creator { get; set; }
        public string Content { get; set; }
        public int Score { get; set; }
        public int CreatorAdminLevel { get; set; }
        public DateTime CreationDate { get; set; }
    }
}