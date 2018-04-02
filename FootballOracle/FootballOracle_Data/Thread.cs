using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FootballOracle_Data
{
    public class Thread
    {
        private ICollection<ForumArticle> forumArticles;
        public Thread()
        {
            this.forumArticles = new HashSet<ForumArticle>();
        }

        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<ForumArticle> ForumArticles
        {
            get
            {
                return this.forumArticles;
            }
            set
            {
                this.forumArticles = value;
            }
        }
    }
}
