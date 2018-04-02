using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FootballOracle.Models.models
{
    public class TeamModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int Matches { get; set; }

        [Required]
        public int Points { get; set; }

        [Required]
        public int Wins { get; set; }

        [Required]
        public int Draws { get; set; }

        [Required]
        public int losses { get; set; }

        [Required]
        public string Picture { get; set; }

        [Required]
        public int GoalScored { get; set; }

        [Required]
        public int GoalConcedered { get; set; }
    }
}