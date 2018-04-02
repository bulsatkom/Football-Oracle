using FootballOracle_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballOracle_DataServices.Interfaces
{
    public interface IMatchService
    {
        void Add(Match match);

        ICollection<Match> GetAllOpen();

        Match GetById(Guid id);

        void AddResult(Guid id, int homeGoals, int awayGoals);

        ICollection<Match> GetLatest();

        ICollection<Match> GetHots();

        ICollection<Match> GetMatchesForForcast();

        void UpdatePlayedMatchCount(Guid id);

        void UpdatePlayedForForcastCount(Guid MatchId, string forcast);

        ICollection<Tuple<Guid, double, int>> UpdateAllForecasts(Guid MatchId, int homeGoals, int awayGoals);

        void UpgradeUserPointsForSuccessMatch(Guid UserId, int points, double coeff);

        void RemovePlayedMatchCount(Guid id);

        void RemovePlayedForForcastCount(Guid MatchId, string forcast);
    }
}
