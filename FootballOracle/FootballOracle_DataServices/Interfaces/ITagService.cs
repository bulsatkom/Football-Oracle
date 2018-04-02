using FootballOracle_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballOracle_DataServices.Interfaces
{
    public interface ITagService
    {
        void add(Tag tag);

        Tag GetTagByMatchId(Guid id);
    }
}
