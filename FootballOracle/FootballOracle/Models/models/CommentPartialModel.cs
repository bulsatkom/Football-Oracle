using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FootballOracle.Models.models
{
    public class CommentPartialModel
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public Guid AuthorId { get; set; }
    }
}