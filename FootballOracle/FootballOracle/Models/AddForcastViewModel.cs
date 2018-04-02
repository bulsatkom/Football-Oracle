using FootballOracle.Models.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballOracle.Models
{
    public class AddForcastViewModel
    {
        public ICollection<ForecastModel> forcasts { get; set; }
    }
}