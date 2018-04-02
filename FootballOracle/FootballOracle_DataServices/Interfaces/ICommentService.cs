using FootballOracle_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballOracle_DataServices.Interfaces
{
    public interface ICommentService
    {
        ICollection<Comment> GetCommentsForArticle(Guid id);

        void AddComment(Comment comment);
    }
}
