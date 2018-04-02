using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootballOracle.Areas.Admin.Models
{
    public class AddTeamViewModel
    {
        public AddTeamViewModel()
        {
            this.Championships = new List<SelectListItem>();
        }

        [Required]
        [Display(Name = "Първенство")]
        public Guid ChampionshipId { get; set; }

        [Required]
        [Display(Name = "Аватар")]
        public string Picture { get; set; }

        [Required]
        [Display(Name = "Име")]
        public string Name { get; set; }

        public ICollection<SelectListItem> Championships { get; set; }
    }
}