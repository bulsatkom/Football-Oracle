using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballOracle_Data
{
    public class Tag
    {
        private ICollection<Article> articles;

        public Tag()
        {
            this.articles = new HashSet<Article>();
        }

        public Guid Id { get; set; }

        public Guid TeamId { get; set; }

        public virtual ICollection<Article> Articles
        {
            get
            {
                return this.articles;
            }
 
            set
            {
                this.articles = value;
            }
        }
    }
}
