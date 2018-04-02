using FootballOracle.Models.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballOracle.Models
{
    public class ChampDetailsViewModel
    {
        public ICollection<UpCommingMatchModel> UpcommingMatches { get; set; }

        public ICollection<PlayedMatchModel> PlayedMatches { get; set; }

        public ICollection<TeamModel> Table { get; set; }
         
        public ICollection<NewsModel> News { get; set; }

        public string Name { get; set; }
    }
}