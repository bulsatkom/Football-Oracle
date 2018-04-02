using System;
using System.Collections.Generic;

namespace FootballOracle_Data
{
    public class ForumAccount
    {
        private ICollection<ForumPost> forumPosts;
        public ForumAccount()
        {
            this.forumPosts = new HashSet<ForumPost>();
        }

        public Guid Id { get; set; }

        public int ForumPoints { get; set; }

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
