using FootballOracle_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballOracle_DataServices.Interfaces
{
    public interface IInQuestionService
    {
        void AddInquestion(Inquestion inquestion);

        void AddAnswers(ICollection<Answer> answers);

        Inquestion GetIdOfActiveInQuestion();

        ICollection<Answer> GetAnswersById(Guid InQuestionId);

        bool CanAnswer(Guid InQuestionId, Guid UserId);

        void UpgradeVotesForAnswer(Guid answerId);

        void UpgradeVotesForInquestion(Guid InquestionId);

        void UpgrareAnswerUserTable(Guid InquestionId, Guid userId);
    }
}
