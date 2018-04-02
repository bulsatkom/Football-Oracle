using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballOracle_Data
{
    public class Theme
    {
        private ICollection<ForumPost> forumPosts;

        public Theme()
        {
            this.forumPosts = new HashSet<ForumPost>();
        }

        public Guid Id { get; set; }

        [Required]
        public Guid ForumAccountId { get; set; }

        [Required]
        public Guid TitleForumPost { get; set; }

        public virtual ICollection<ForumPost> ForumPosts
        {
            get
            {
                return this.forumPosts;
            }
            set
            {
                this.forumPosts = value;
            }
        }
    }
}