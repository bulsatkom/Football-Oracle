using FootballOracle_Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootballOracle.Areas.Admin.Models
{
    public class AddMatchViewModel
    {
        [Required]
        [Display(Name = "Домакин")]
        public Guid HomeTeam { get; set; }

        [Required]
        [Display(Name = "Гост")]
        public Guid AwayTeam { get; set; }

        [Required]
        [Display(Name = "Дата")]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Първенство")]
        public Guid ChampionshipId { get; set; }

        [Required]
        [Range(1,30)]
        public double HomeCoefficient { get; set; }

        [Required]
        [Range(1, 30)]
        public double DrawCoefficient { get; set; }

        [Required]
        [Range(1, 30)]
        public double AwayCoefficient { get; set; }

        public ICollection<SelectListItem> Teams { get; set; }

        public ICollection<SelectListItem> Championships { get; set; }

    }
}