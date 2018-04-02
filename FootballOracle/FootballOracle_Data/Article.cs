using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FootballOracle_Data
{
    public class Article
    {
        private ICollection<Comment> comments;
        private ICollection<Tag> tags;


        public Article()
        {
            this.comments = new HashSet<Comment>();
            this.tags = new HashSet<Tag>();
        }

        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        //[UIHint("Html")]
        //[AllowHtml]
        [DataType(DataType.Html)]
        public string Content { get; set; }
        
        [Required]
        public Guid ChampionshipId { get; set; }

        [Required]
        public int Viewing { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
