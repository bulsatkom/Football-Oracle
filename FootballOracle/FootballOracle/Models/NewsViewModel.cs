using FootballOracle.Models.models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FootballOracle.Models
{
    public class NewsViewModel
    {
        [Required]
        public string Content { get; set; }

        [Required]
        public string ImageSrc { get; set; }

        [Required]
        public string Title { get; set; }

        public ICollection<NewsModel> OtherNews { get; set; }

        public ICollection<NewsModel> MoreForThisArticle { get; set; }

        public ICollection<PlayedMatchModel> Matches { get; set; }

        public ICollection<CommentModel> Comments { get; set; }

        [Required]
        public int Views { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public Guid ChampionshipId { get; set; }

        [Required]
        public Guid ArticleId { get; set; }
    }
}