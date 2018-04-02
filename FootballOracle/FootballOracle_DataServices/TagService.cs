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
    public class TagService : ITagService
    {
        private IFootballOracleDbContext dbContext;

        public TagService(IFootballOracleDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void add(Tag tag)
        {
            this.dbContext.Tag.Add(tag);

            this.dbContext.SaveChanges();
        }

        public Tag GetTagByMatchId(Guid id)
        {
            return this.dbContext.Tag.FirstOrDefault(x => x.TeamId == id);
        }
    }
}
