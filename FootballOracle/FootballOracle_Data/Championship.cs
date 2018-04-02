using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FootballOracle_Data
{
    public class Championship
    {
        private ICollection<Team> teams;
        public Championship()
        {
            this.teams = new HashSet<Team>();
        }

        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Team> Teams
        {
            get
            {
                return this.teams;
            }
            set
            {
                this.teams = value;
            }
        }
    }
}
