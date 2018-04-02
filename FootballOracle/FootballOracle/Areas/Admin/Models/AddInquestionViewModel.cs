using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FootballOracle.Areas.Admin.Models
{
    public class AddInquestionViewModel
    {
        [Required]
        public string Question { get; set; }

        public ICollection<string> Answers { get; set; }

        [Range(3,7)]
        public int AnswersCount { get; set; }
    }
}