using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FootballOracle.Models.models
{
    public class MatchModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string HomeTeam { get; set; }

        [Required]
        public string AwayTeam { get; set; }

        [Required]
        public double HomeCoeff { get; set; }

        [Required]
        public double AwayCoeff { get; set; }

        [Required]
        public double DrawCoeff { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public bool IsDisabled { get; set; }
    }
}