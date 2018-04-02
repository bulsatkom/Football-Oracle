using System;
using System.ComponentModel.DataAnnotations;


namespace FootballOracle_Data
{
    public class Match
    {
        public Guid Id { get; set; }

        [Required]
        public Guid HomeTeam { get; set; }
        
        [Required]
        public Guid AwayTeam { get; set; }

        [Required]
        public Guid ChampionshipId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Required]
        public bool IsOpen { get; set; }

        [Required]
        public int HomeGoals { get; set; }

        [Required]
        public int AwayGoals { get; set; }
        
        [Required]
        public double HomeCoefficient { get; set; }

        [Required]
        public double DrawCoefficient { get; set; }

        [Required]
        public double AwayCoefficient { get; set; }

        [Required]
        public int PlayedFrom { get; set; }

        public int? PlayedFor1 { get; set; }

        public int? PlayedForX { get; set; }

        public int? PlayedFor2 { get; set; }

        [Required]
        public int SuccessMatch { get; set; }

        [Required]
        public int SuccessResult { get; set; }
    }
}
