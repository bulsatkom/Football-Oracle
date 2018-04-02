using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FootballOracle.Models
{
    public class MatchViewModel
    {
        public Guid id { get; set; }

        [Required]
        public string HomeTeam { get; set; }

        [Required]
        public string AwayTeam { get; set; }

        [Required]
        public double HomeCoeff { get; set; }

        [Required]
        public double DrawCoef { get; set; }

        [Required]
        public double AwayCoeff { get; set; }

        [Required]
        public int PlayedFrom { get; set; }

        [Required]
        public int PlayedFor1 { get; set; }

        [Required]
        public int PlayedForX { get; set; }

        [Required]
        public int PlayedFor2 { get; set; }

        [Required]
        public string HomeTeamPic { get; set; }

        [Required]
        public string AwayTeamPic { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}