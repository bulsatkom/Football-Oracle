using FootballOracle_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballOracle_DataServices.Interfaces
{
    public interface IArticleService
    {
        void Add(Article article);

        ICollection<Article> GetLatestFour();

        Article GetById(Guid id);

        ICollection<Article> GetLatestNewsForChampionship(Guid championshipId, Guid ArticleId);

        ICollection<Article> GetLatestNewsForTeam(Guid teamId, Guid ArticleId);

        int GetTagsCountForArticle(Guid id);

        void updateViewsById(Guid id);
    }
}
