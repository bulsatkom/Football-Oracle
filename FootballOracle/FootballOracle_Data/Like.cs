using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballOracle_Data
{
    public class Like
    {
        public Guid Id { get; set; }

        [Required]
        public Guid ForumPostId { get; set; }

        [Required]
        public Guid ForumAccountId { get; set; }

        [Required]
        public bool IsLike { get; set; }
    }
}