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
    public class InQuestionService : IInQuestionService
    {
        private readonly IFootballOracleDbContext dbContext;

        public InQuestionService(IFootballOracleDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void AddInquestion(Inquestion inquestion)
        {
            this.dbContext.Inquestion.Add(inquestion);

            this.dbContext.SaveChanges();

        }

        public void AddAnswers(ICollection<Answer> answers)
        {
            foreach (var answer in answers)
            {
                this.dbContext.Answer.Add(answer);
            }

            this.dbContext.SaveChanges();
        }

        public Inquestion GetIdOfActiveInQuestion()
        {
            return this.dbContext.Inquestion.Where(x => x.IsActive == true).FirstOrDefault();
        }

        public ICollection<Answer> GetAnswersById(Guid InQuestionId)
        {
            return this.dbContext.Answer.Where(x => x.InquestionId == InQuestionId).OrderBy(x => x.Text).ToList();
        }

        public bool CanAnswer(Guid InQuestionId, Guid UserId)
        {
            bool answer = true;

            this.dbContext.AnswerUser.ToList().ForEach(x =>
            {
                if(x.AnswerId == InQuestionId && x.UserId == UserId)
                {
                    answer = false;
                    return;
                }
            });

            return answer;
        }

        public void UpgradeVotesForInquestion(Guid InquestionId)
        {
            var inquestion = this.dbContext.Inquestion.FirstOrDefault(x => x.Id == InquestionId);
            if(inquestion != null)
            {
                inquestion.PlayersCount += 1;
                this.dbContext.SaveChanges();
            }
        }

        public void UpgradeVotesForAnswer(Guid answerId)
        {
            var answer = this.dbContext.Answer.FirstOrDefault(x => x.Id == answerId);

            if(answer != null)
            {
                answer.playedFrom += 1;
                this.dbContext.SaveChanges();
            }
        }

        public void UpgrareAnswerUserTable(Guid InquestionId, Guid userId)
        {
            var model = new AnswerUser()
            {
                Id = Guid.NewGuid(),
                AnswerId = InquestionId,
                UserId = userId
            };

            this.dbContext.AnswerUser.Add(model);

            this.dbContext.SaveChanges();
        }
    }
}
