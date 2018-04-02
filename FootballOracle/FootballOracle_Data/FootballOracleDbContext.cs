using FootballOracle_Data;
using FootballOracle_Data.interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballOracle_Data
{
    public class FootballOracleDbContext : DbContext, IFootballOracleDbContext
    {
        public FootballOracleDbContext()
            : base("DefaultConnection")
        {
        }

        public IDbSet<Article> Article { get; set; }

        public IDbSet<Championship> Championship { get; set; }

        public IDbSet<ForumArticle> ForumArticle { get; set; }

        public IDbSet<Comment> Comments { get; set; }

        public IDbSet<ForumAccount> ForumAccount { get; set; }

        public IDbSet<ForumPost> ForumPost { get; set; }

        public IDbSet<Like> Like { get; set; }

        public IDbSet<Match> Match { get; set; }

        public IDbSet<Team> Team { get; set; }

        public IDbSet<Theme> Theme { get; set; }

        public IDbSet<Thread> Thread { get; set; }

        public IDbSet<Forecast> Forecast { get; set; }

        public IDbSet<UserForecast> UserForecast { get; set; }

        public IDbSet<Tag> Tag { get; set; }

        public IDbSet<Inquestion> Inquestion { get; set; }

        public IDbSet<Answer> Answer { get; set; }

        public IDbSet<AnswerUser> AnswerUser { get; set; }
    }
}
