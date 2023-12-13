using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackOverFlowApp.Domain;

namespace StackOverFlowApp.Application.Persistence
{
    public interface IQuestionRepository
    {
        Task<IEnumerable<Question>> GetAllQuestionsAsync();
        Task<IEnumerable<Question>> GetQuestionsByCategoryAsync(int categoryId);
        Task<Question> GetQuestionByIdAsync(int questionId);
        Task<Question> GetQuestionByUserIdAsync(int userId);

        void InsertQuestion(Question question);
        void UpdateQuestionDetails(Question question);
        void UpdateQuestionVotesCount(int questionId, int value);
        void UpdateQuestionAnswersCount(int questionId, int value);
        void UpdateQuestionViews(int questionId, int value);

        void DeleteQuestion(int questionId);
    }
}