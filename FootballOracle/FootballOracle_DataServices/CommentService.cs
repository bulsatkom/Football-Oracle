using FootballOracle_DataServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballOracle_Data;
using FootballOracle_Data.interfaces;

namespace FootballOracle_DataServices
{
    public class CommentService : ICommentService
    {
        private IFootballOracleDbContext dbContext;

        public CommentService(IFootballOracleDbContext dbContext)
        {
            this.dbContext = dbContext; 
        }

        public void AddComment(Comment comment)
        {
            this.dbContext.Comments.Add(comment);

            this.dbContext.SaveChanges();
        }

        public ICollection<Comment> GetCommentsForArticle(Guid id)
        {
            return this.dbContext.Comments
                .Where(x => x.ArticleId == id)
                .OrderByDescending(x => x.Date)
                .ToList();
        }
    }
}
