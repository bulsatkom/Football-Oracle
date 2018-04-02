using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballOracle_Data.interfaces
{
    public interface IFootballOracleDbContext
    {
         IDbSet<Article> Article { get; set; }

         IDbSet<Championship> Championship { get; set; }

         IDbSet<ForumArticle> ForumArticle { get; set; }

         IDbSet<Comment> Comments { get; set; }

         IDbSet<ForumAccount> ForumAccount { get; set; }

         IDbSet<ForumPost> ForumPost { get; set; }

         IDbSet<Like> Like { get; set; }

         IDbSet<Match> Match { get; set; }

         IDbSet<Team> Team { get; set; }

         IDbSet<Theme> Theme { get; set; }

         IDbSet<Thread> Thread { get; set; }

        IDbSet<Forecast> Forecast { get; set; }

        IDbSet<UserForecast> UserForecast { get; set; }

        IDbSet<Tag> Tag { get; set; }

        IDbSet<Inquestion> Inquestion { get; set; }

        IDbSet<Answer> Answer { get; set; }

        IDbSet<AnswerUser> AnswerUser { get; set; }

        int SaveChanges();
    }
}
