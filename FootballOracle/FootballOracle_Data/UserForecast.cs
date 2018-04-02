using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballOracle_Data
{
    public class UserForecast
    {
        private ICollection<Forecast> forecasts;

        public UserForecast()
        {
            this.forecasts = new HashSet<Forecast>();
        }
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid AccountId { get; set; }

        public virtual ICollection<Forecast> Forecasts
        {
            get
            {
                return this.forecasts;
            }
            set
            {
                this.forecasts = value;
            }
        }

        public double Points { get; set; }
    }
}
