using FootballOracle.Models.models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FootballOracle.Models
{
    public class MatchesViewModel
    {
        public ICollection<MatchModel> MatchesForToday { get; set; }

        public ICollection<MatchModel> MatchesForTomorrow { get; set; }

        public ICollection<MatchModel> OtherMatches { get; set; }

        public AddForcastViewModel Forecasts { get; set; }

        public bool IsAuthenticated { get; set; }
    }
}