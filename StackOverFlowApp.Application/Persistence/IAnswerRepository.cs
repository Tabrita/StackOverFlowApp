using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackOverFlowApp.Domain;

namespace StackOverFlowApp.Application.Persistence
{
    public interface IAnswerRepository
    {
        void InsertAnswer(Answer answer);
        void UpdateAnswer(Answer answer);
        void UpdateAnswerVotesCount(int answerId, int value);
        void DeleteAnswer(int answerId);
        Task<IEnumerable<Answer>> GetAnswersByQuestionId(int questionId);
        Task<Answer> GetAnswerById(int answerId);
    }
}