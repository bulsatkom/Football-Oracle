using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FootballOracle.Models.models
{
    public class CommentModel
    {
        public DateTime Date { get; set; }

        [Required]
        [StringLength(500, MinimumLength =3)]
        public string Content { get; set; }

        public string Author { get; set; }

        public Guid articleId { get; set; }
    }
}