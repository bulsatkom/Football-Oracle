using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballOracle_Data
{
    public class Answer
    {
        public Guid Id { get; set; }

        [Required]
        public Guid InquestionId { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public int playedFrom { get; set; }
    }
}
