using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FootballOracle.Models.models
{
    public class ForecastModel
    {
        public Guid Id { get; set; }

        public string homeTeam { get; set; }

        public string AwayTeam { get; set; }

        [Display(Name = "Коефициент")]
        public double Coeff { get; set; }

        [Display(Name = "Tочки")]
        [Range(0, 10)]
        public int points { get; set; }

        [Display(Name ="Прогноза")]
        public string Forecast { get; set; }
    }
}