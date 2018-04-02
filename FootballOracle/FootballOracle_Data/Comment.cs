using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballOracle_Data
{
    public class Comment
    {
        public Guid Id { get; set; }

        [Required]
        public Guid ArticleId { get; set; }

        [Required]
        public Guid AccountId { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
        
        [Required]
        public DateTime Date { get; set; } 
    }
}