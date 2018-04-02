using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FootballOracle.Models
{
    public class GetHotsViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string HomeTeam { get; set; }

        [Required]
        public string AwayTeam { get; set; }

        [Required]
        public double HomeCoef { get; set; }

        [Required]
        public double DrawCoef { get; set; }

        [Required]
        public double AwayCoef { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}