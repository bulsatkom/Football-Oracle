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
    public class TeamService : ITeamService
    {
        private readonly IFootballOracleDbContext dbContext;

        public TeamService(IFootballOracleDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(Team team)
        {
            this.dbContext.Team.Add(team);
            this.dbContext.SaveChanges();
        }

        public string FindTeamNameById(Guid id)
        {
            return this.dbContext.Team
                .Where(x => x.Id == id)
                .Select(x => x.Name)
                .FirstOrDefault();
        }

        public ICollection<Team> GetAll()
        {
            return this.dbContext.Team.OrderBy(x => x.Name).ToList();
        }

        public string GetTeamAvatarById(Guid id)
        {
            return this.dbContext.Team
                .Where(x => x.Id == id)
                .Select(x => x.Picture)
                .FirstOrDefault();
        }

        public void UpdateTeams(Guid HomeTeam, Guid AwayTeam, int homeGoals, int awayGoals)
        {
            var homeTeam = this.dbContext.Team.FirstOrDefault(x => x.Id == HomeTeam);
            var awayTeam = this.dbContext.Team.FirstOrDefault(x => x.Id == AwayTeam);

            if(homeGoals > awayGoals)
            {
                homeTeam.Wins += 1;
                homeTeam.Points += 3;

                awayTeam.Losses += 1;
            }
            else if(homeGoals == awayGoals)
            {
                homeTeam.Draws += 1;
                homeTeam.Points += 1;

                awayTeam.Draws += 1;
                awayTeam.Points += 1;
            }
            else
            {
                homeTeam.Losses += 1;

                awayTeam.Wins += 1;
                awayTeam.Points += 3;
            }

            homeTeam.GoalScored += homeGoals;
            homeTeam.Matches += 1;
            homeTeam.GoalConcedered += awayGoals;

            awayTeam.GoalScored += awayGoals;
            awayTeam.Matches += 1;
            awayTeam.GoalConcedered += homeGoals;

            this.dbContext.SaveChanges();
        }
    }
}
