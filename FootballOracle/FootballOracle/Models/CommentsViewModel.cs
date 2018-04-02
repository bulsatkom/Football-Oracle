using FootballOracle.Models.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballOracle.Models
{
    public class CommentsViewModel
    {
        public ICollection<CommentPartialModel> Comment { get; set; }
    }
}