using FootballOracle_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballOracle_DataServices.Interfaces
{
    public interface ITeamService
    {
        void Add(Team team);

        ICollection<Team> GetAll();

        string FindTeamNameById(Guid id);

        string GetTeamAvatarById(Guid id);

        void UpdateTeams(Guid HomeTeam, Guid AwayTeam, int homeGoals, int awayGoals);
    }
}
