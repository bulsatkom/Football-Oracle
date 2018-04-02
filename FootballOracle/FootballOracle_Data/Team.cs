using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballOracle_Data
{
    public class Team
    {
        public Guid Id { get; set; }

        [Required]
        public Guid ChampionshipId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public int Matches { get; set; }

        [Required]
        public int Wins { get; set; }

        [Required]
        public int Draws { get; set; }

        [Required]
        public int Losses { get; set; }

        [Required]
        public int Points { get; set; }

        [Required]
        public string Picture { get; set; }

        public int GoalScored { get; set; }

        public int GoalConcedered { get; set; }
    }
}
