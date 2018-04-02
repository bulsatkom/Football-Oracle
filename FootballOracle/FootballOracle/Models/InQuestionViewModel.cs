using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootballOracle.Models
{
    public class InQuestionViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        public ICollection<SelectListItem> Answers { get; set; }

        [Required]
        public Guid Answer { get; set; }

        public bool CanAnswer { get; set; }

        public int AllPlayedCount { get; set; }

        public ICollection<int> PlayedForEach { get; set; }
    }
}