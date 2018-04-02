using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FootballOracle_Data
{
    public class ApplicationUser : IdentityUser
    {
        private ICollection<Forecast> matches;

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public ApplicationUser()
        {
            this.matches = new HashSet<Forecast>();
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

        public virtual ICollection<Forecast> Matches
        {
            get
            {
                return this.matches;
            }
            set
            {
                this.matches = value;
            }

        }
    }
}
