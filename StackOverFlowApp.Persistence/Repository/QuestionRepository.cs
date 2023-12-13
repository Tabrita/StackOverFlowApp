using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StackOverFlowApp.Application.Persistence;
using StackOverFlowApp.Domain;

namespace StackOverFlowApp.Persistence.Repository
{
    public class QuestionRepository : IQuestionRepository
    {

        private readonly StackOverFlowDbContext _context;
        public QuestionRepository(StackOverFlowDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Question>> GetAllQuestionsAsync()
        {
            return await _context.Questions.OrderBy(q => q.QuestionID).ToListAsync();
        }

        public async Task<Question> GetQuestionByIdAsync(int questionId)
        {
            return await _context.Questions.Where(q => q.QuestionID == questionId).FirstOrDefaultAsync();
        }

        public async Task<Question> GetQuestionByUserIdAsync(int userId)
        {
            return await _context.Questions.Include(u => u.User)
                                    .Where(q => q.UserID == userId)
                                    .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Question>> GetQuestionsByCategoryAsync(int categoryId)
        {
            return await _context.Questions.Where(q => q.CategoryID == categoryId).ToListAsync();
        }

        public async void InsertQuestion(Question question)
        {
            if (question != null)
            {
                await _context.Questions.AddAsync(question);
                await _context.SaveChangesAsync();
            }
        }

        public async void UpdateQuestionAnswersCount(int questionId, int value)
        {
            var question = await _context.Questions.Where(q => q.QuestionID == questionId).FirstOrDefaultAsync();
            if (question != null)
            {
                question.AnswerCount += value;
            }
        }

        public async void UpdateQuestionDetails(Question question)
        {
            var questionToUpdate = await _context.Questions.Where(q => q.QuestionID == question.QuestionID).FirstOrDefaultAsync();
            if (questionToUpdate != null)
            {
                questionToUpdate.QuestionTitle = question.QuestionTitle;
                questionToUpdate.QuestionDescription = question.QuestionDescription;

                await _context.SaveChangesAsync();
            }
        }

        public async void UpdateQuestionViews(int questionId, int value)
        {
            var question = await _context.Questions.Where(q => q.QuestionID == questionId).FirstOrDefaultAsync();
            if (question != null)
            {
                question.ViewsCount += value;
                await _context.SaveChangesAsync();
            }
        }

        public async void UpdateQuestionVotesCount(int questionId, int value)
        {
            var question = await _context.Questions.Where(q => q.QuestionID == questionId).FirstOrDefaultAsync();
            if (question != null)
            {
                question.VoteCount += value;
                await _context.SaveChangesAsync();
            }
        }

        public async void DeleteQuestion(int questionId)
        {
            var question = await _context.Questions.Where(q => q.QuestionID == questionId).FirstOrDefaultAsync();
            if (question != null)
            {
                _context.Questions.Remove(question);
                await _context.SaveChangesAsync();
            }
        }
    }
}