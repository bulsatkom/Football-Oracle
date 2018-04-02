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
    public class ChampionshipService : IChampionshipService
    {
        private IFootballOracleDbContext dbContext;

        public ChampionshipService(IFootballOracleDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(Championship championship)
        {
            this.dbContext.Championship.Add(championship);
            this.dbContext.SaveChanges();
        }

        public ICollection<Championship> GetAll()
        {
            return this.dbContext.Championship.ToList();
        }

        public ICollection<Article> GetArticlesForChampionship(Guid id)
        {
            return this.dbContext.Article
                .Where(x => x.ChampionshipId == id)
                .OrderByDescending(x => x.Date)
                .ToList();
        }

        public string GetNameById(Guid id)
        {
            return this.dbContext.Championship
                .Where(x => x.Id == id)
                .Select(x => x.Name)
                .FirstOrDefault();
        }

        public ICollection<Match> GetPlayedMatchByChampionshipId(Guid id)
        {
            return this.dbContext.Match
                .Where(x => x.ChampionshipId == id && x.IsOpen == false)
                .OrderByDescending(x => x.Date)
                .Take(10)
                .ToList();
        }

        public ICollection<Team> GetTeamsForChampionship(Guid id)
        {
            return this.dbContext.Team
                .Where(x => x.ChampionshipId == id)
                .OrderByDescending(x => x.Points)
                .ThenByDescending(x => x.GoalScored - x.GoalConcedered)
                .ThenByDescending(x => x.GoalScored)
                .ToList();
        }

        public ICollection<Match> GetUpcamingMatchByChampionshipId(Guid id)
        {
            return this.dbContext.Match
                .Where(x => x.ChampionshipId == id && x.IsOpen == true)
                .OrderBy(x => x.Date)
                .Take(10)
                .ToList();
        }
    }
}
