using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballOracle_Data
{
    public class ForumArticle
    {
        private ICollection<Theme> themes;

        public ForumArticle()
        {
            this.themes = new HashSet<Theme>();
        }
        
        public Guid Id { get; set; }

        [Required]
        public Guid ThreadId { get; set; }

        [Required]
        public string Title { get; set; }

        public virtual ICollection<Theme> Themes
        {
            get
            {
                return this.themes;
            }
            set
            {
                this.themes = value;
            }
        }
    }
}
