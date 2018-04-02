using FootballOracle_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballOracle_DataServices.Interfaces
{
    public interface IForecastService
    { 
        ICollection<Forecast> GetAllById(Guid userId);

        void Add(Forecast forecast);

        void Remove(Guid id);
    }
}
