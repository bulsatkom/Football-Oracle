using FootballOracle.Models.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballOracle.Models
{
    public class ChampionshipPartialViewModel
    {
        public ICollection<NewsModel> news { get; set; }

        public int page { get; set; }

        public int pages { get; set; }
    }
}