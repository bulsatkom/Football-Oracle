using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballOracle_Data
{
    public class Forecast
    {
        public Guid Id { get; set; }

        [Required]
        public Guid MatchId { get; set; }

        [Required]
        public Guid AccountId { get; set; }

        [Required]
        public string Forcast { get; set; }

        [Required]
        public double Coefficient { get; set; }

        public int homeGoals { get; set; }

        public int AwayGoals { get; set; }

        [Required]
        [Range(0, 10)]
        public int PointsPlayed { get; set; }

        [Required]
        public bool IsOpen { get; set; }

        [Required]
        public bool IsSuccess { get; set; }

        [Required]
        public bool IsPlayed { get; set; }
    }
}
