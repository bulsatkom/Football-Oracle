using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballOracle_Data
{
    public class AnswerUser
    {
        public Guid Id { get; set; }

        [Required]
        public Guid AnswerId { get; set; }

        [Required]
        public Guid UserId { get; set; }
    }
}
