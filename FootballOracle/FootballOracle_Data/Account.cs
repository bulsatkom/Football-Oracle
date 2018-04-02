using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FootballOracle_Data
{
    public class Account : IdentityUser
    {
        public Account()
        {
            this.Matches = new HashSet<Match>();
        }

        [Required]
        public double Points { get; set; }

        [Required]
        public DateTime BirthDay { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public Guid FavouriteTeamId { get; set; }

        [Required]
        public Guid ForumAccountId { get; set; }

        [Required]
        public DateTime Register { get; set; }

        public virtual ICollection<Match> Matches { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Account> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
