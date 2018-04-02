using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballOracle_Data
{
    public class Inquestion
    {
        private ICollection<Answer> answers;

        public Inquestion()
        {
            this.answers = new HashSet<Answer>();
        }

        public Guid Id { get; set; }

        [Required]
        public string Question { get; set; }

        [Required]
        public int PlayersCount { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public virtual ICollection<Answer> Answers
        {
            get
            {
                return this.answers;
            }
            set
            {
                this.answers = value;
            }
        }
    }
}
