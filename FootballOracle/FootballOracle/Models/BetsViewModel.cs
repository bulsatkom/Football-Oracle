using FootballOracle.Models.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballOracle.Models
{
    public class BetsViewModel
    {
        public ICollection<PlayedBetModel> PlayedMatches { get; set; }

        public ICollection<NotPlayedBetModel> NotPlayedMatches { get; set; }

        public Guid UserId { get; set; }
    }
}