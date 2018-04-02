using FootballOracle.Models.models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FootballOracle.Models
{
    public class AccountViewModel
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        public string userName { get; set; }

        [Required]
        public double Points { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public DateTime RegisteredOn { get; set; }

        [Required]
        public int ForecastCount { get; set; }

        [Required]
        public int SuccessForecastCount { get; set; }

        public ICollection<PlayedBetModel> TopForcasts { get; set; }
        
        [Required]
        public Guid AccId { get; set; }
        
        [Required]
        public string FavouriteTeam { get; set; }
        
        [Required]
        public string FavouriteTeamPic { get; set; } 
    }
}