using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballOracle.Models.models
{
    public class NewsModel
    {
        public Guid Id { get; set; }

        public string ImageSrc { get; set; }

        public string Title { get; set; }
    }
}