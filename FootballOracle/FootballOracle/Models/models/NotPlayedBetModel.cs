using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FootballOracle.Models.models
{
    public class NotPlayedBetModel
    {
        [Required]
        public Guid MatchId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string HomeTeam { get; set; }

        [Required]
        public string AwayTeam { get; set; }

        [Required]
        public string HomePic { get; set; }

        [Required]
        public string AwayPic { get; set; }

        [Required]
        public int PlayedPoints { get; set; }

        [Required]
        public double Coeff { get; set; }

        [Required]
        public string Forcast { get; set; }

        [Required]
        public bool IsStarted { get; set; }

        [Required]
        public Guid ForecastId { get; set; }
    }
}