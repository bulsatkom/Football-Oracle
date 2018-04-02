using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballOracle.Models
{
    public class MenuForChampionshipsViewModel
    {
        public IDictionary<Guid, string> ChampionShips { get; set; }

        public Guid? Selected { get; set; }
    }
}