using FootballOracle_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootballOracle.Areas.Admin.Models
{
    public class AddResultViewModel
    {
        public ICollection<ViewTeam> Matches { get; set; }
    }
}