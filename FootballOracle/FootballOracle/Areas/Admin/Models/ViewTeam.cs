using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FootballOracle.Areas.Admin.Models
{
    public class ViewTeam
    {
        public Guid Id { get; set; }

        public string homeTeam { get; set; }

        public string awayTeam { get; set; }

        [Range(0,15)]
        public int homeGoals { get; set; }

        [Range(0, 15)]
        public int awayGoals { get; set; }

    }
}