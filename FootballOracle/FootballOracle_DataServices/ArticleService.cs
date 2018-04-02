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
    public class ArticleService : IArticleService
    {
        private IFootballOracleDbContext dbContext;

        public ArticleService(IFootballOracleDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(Article article)
        {
            this.dbContext.Article.Add(article);
            this.dbContext.SaveChanges();
        }

        public Article GetById(Guid id)
        {
            return this.dbContext.Article.FirstOrDefault(x => x.Id == id);
        }

        public ICollection<Article> GetLatestFour()
        {
            return this.dbContext.Article
                .OrderByDescending(x => x.Date)
                .Take(4)
                .ToList();
        }

        public ICollection<Article> GetLatestNewsForChampionship(Guid championshipId, Guid ArticleId)
        {
            return this.dbContext.Article
                .Where(x => x.ChampionshipId == championshipId && x.Id != ArticleId)
                .OrderByDescending(x => x.Date)
                .Take(6)
                .ToList();
        }

        public ICollection<Article> GetLatestNewsForTeam(Guid teamId, Guid ArticleId)
        {
            return this.dbContext.Tag
                .Where(x => x.TeamId == teamId)
                .Select(x => x.Articles)
                .FirstOrDefault()
                .ToList();
        }

        public int GetTagsCountForArticle(Guid id)
        {
            return this.dbContext.Article
                .Where(x => x.Id == id)
                .Select(x => x.Tags.Count)
                .FirstOrDefault();
        }

        public void updateViewsById(Guid id)
        {
            var article = this.dbContext.Article.FirstOrDefault(x => x.Id == id);

            if (article != null)
            {
                article.Viewing += 1;

                this.dbContext.SaveChanges();
            }
        }
    }
}
