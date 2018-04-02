using FootballOracle_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballOracle_DataServices.Interfaces
{
    public interface IChampionshipService
    {
        ICollection<Championship> GetAll();

        string GetNameById(Guid id);

        void Add(Championship championship);

        ICollection<Match> GetUpcamingMatchByChampionshipId(Guid id);

        ICollection<Match> GetPlayedMatchByChampionshipId(Guid id);

        ICollection<Team> GetTeamsForChampionship(Guid id);

        ICollection<Article> GetArticlesForChampionship(Guid id);
    }
}
