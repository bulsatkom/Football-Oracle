using FootballOracle.Models.models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FootballOracle.Models
{
    public class MatchViewModel
    {
        public string championship { get; set; }

        public Guid championshipId { get; set; }

        public Guid id { get; set; }

        [Required]
        public string HomeTeamName { get; set; }

        [Required]
        public string AwayTeamName { get; set; }

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
        public double PlayedFor1Percent { get; set; }

        [Required]
        public double PlayedForXPercent { get; set; }

        [Required]
        public double PlayedFor2Percent { get; set; }

        [Required]
        public int homeGoals { get; set; }

        [Required]
        public int awayGoals { get; set; }

        [Required]
        public string HomeTeamPic { get; set; }

        [Required]
        public string AwayTeamPic { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public ICollection<TeamModel> Teams { get; set; }

        public bool isSuccess { get; set; }
    }
}