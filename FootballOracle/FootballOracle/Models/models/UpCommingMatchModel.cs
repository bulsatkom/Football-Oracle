using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FootballOracle.Models.models
{
    public class UpCommingMatchModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string homeTeam { get; set; }

        [Required]
        public string awayTeam { get; set; }

        [Required]
        public double homeCoeff { get; set; }

        [Required]
        public double drawCoeff { get; set; }

        [Required]
        public double awayCoeff { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}