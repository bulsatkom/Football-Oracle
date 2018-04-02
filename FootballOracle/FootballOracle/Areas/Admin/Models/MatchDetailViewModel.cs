using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballOracle.Areas.Admin.Models
{
    public class MatchDetailViewModel
    {
        public int HomeGoals { get; set; }

        public int AwayGoals { get; set; }

        public string home { get; set; }

        public string away { get; set; }

    }
}