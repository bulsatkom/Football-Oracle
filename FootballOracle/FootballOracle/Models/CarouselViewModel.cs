using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FootballOracle.Models
{
    public class CarouselViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string ImageSrc { get; set; }

        [Required]
        public string Title { get; set; }
    }
}