using FootballOracle_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballOracle_DataServices.Interfaces
{
    public interface IUserForecastService
    {
        ICollection<Forecast> GetAllById(Guid userId);

        ICollection<Forecast> GetAllByIdAndPlayed(Guid id);

        void Add(Forecast forecast);

        void Remove(Guid id);

        Guid GetIdByAccountId(Guid accId);

        void Create(UserForecast userForecast);

        void UpdateForecast(Guid forecast, int points);

        ICollection<Guid> GetAllForUser(Guid userId);

        int GetSuccessfullForecastCount(Guid userId);

        ICollection<Forecast> GetTopForcast(Guid userId);

        void UpgradeUserPoints(Guid user, int points);

        double GetPointsbyUserId(Guid Id);

        void UpgradeUserPointsAfterMatch(Guid user, double points);

        Forecast DeleteBet(Guid bet);
    }
}
