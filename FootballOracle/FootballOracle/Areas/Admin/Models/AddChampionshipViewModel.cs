using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FootballOracle.Areas.Admin.Models
{
    public class AddChampionshipViewModel
    {
        [Required]
        [Display(Name = "Име")]
        [StringLength(15,MinimumLength = 4)]
        public string name { get; set; }
    }
}