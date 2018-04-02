using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootballOracle.Areas.Admin.Models
{
    public class AddArticleViewModel
    {
        [Required]
        [StringLength(150, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name ="Заглавие")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Снимка")]
        public string Image { get; set; }

        [Required]
        [AllowHtml]
        [Display(Name = "Текст")]
        public string Content { get; set; }

        [Required]
        [Display(Name = "Първенство")]
        public Guid ChampionshipId { get; set; }

        public virtual ICollection<Guid> TeamsId { get; set; }

        [Display(Name = "Тагове")]
        public ICollection<SelectListItem> Teams { get; set; }

        public ICollection<SelectListItem> Championships { get; set; }
    }
}