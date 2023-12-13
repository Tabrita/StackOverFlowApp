using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StackOverFlowApp.Application.Persistence;
using StackOverFlowApp.Domain;

namespace StackOverFlowApp.Persistence.Repository
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly StackOverFlowDbContext _context;
        public AnswerRepository(StackOverFlowDbContext context)
        {
            _context = context;
        }

        public async Task<Answer> GetAnswerById(int answerId)
        {
            var answer = await _context.Answers.Where(a => a.AnswerID == answerId).FirstOrDefaultAsync();
            return answer;
        }

        public async Task<IEnumerable<Answer>> GetAnswersByQuestionId(int questionId)
        {
            return await _context.Answers.Where(a => a.QuestionID == questionId).ToListAsync();
        }

        public async void InsertAnswer(Answer answer)
        {
            await _context.Answers.AddRangeAsync(answer);
            await _context.SaveChangesAsync();
        }

        public async void UpdateAnswer(Answer answer)
        {
            var answerToUpdate = await _context.Answers.Where(a => a.AnswerID == answer.AnswerID).FirstOrDefaultAsync();
            if (answerToUpdate != null)
            {
                answerToUpdate.AnswerText = answer.AnswerText;
                await _context.SaveChangesAsync();
            }
        }

        public void UpdateAnswerVotesCount(int answerId, int value)
        {
            throw new NotImplementedException();
        }

        public async void DeleteAnswer(int answerId)
        {
            var answer = await _context.Answers.Where(a => a.AnswerID == answerId).FirstOrDefaultAsync();
            if (answer != null)
            {
                _context.Answers.Remove(answer);
                await _context.SaveChangesAsync();
            }
        }

    }
}