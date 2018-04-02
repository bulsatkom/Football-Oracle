using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballOracle_Data
{
    public class ForumPost
    {
        private ICollection<Like> likes;

        public ForumPost()
        {
            this.likes = new HashSet<Like>();
        }

        public Guid Id { get; set; }
        

        [Required]
        public Guid ThemeId { get; set; }
        
        [Required]
        public Guid ForumAccountId { get; set; }
        
        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public virtual ICollection<Like> Likes
        {
            get
            {
                return this.likes;
            }
            set
            {
                this.likes = value;
            }
        }
    }
}