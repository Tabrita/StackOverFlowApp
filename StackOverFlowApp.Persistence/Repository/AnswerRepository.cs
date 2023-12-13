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
        private readonly IQuestionRepository _questionRepository;
        private readonly IVoteRepository _voteRepository;
        public AnswerRepository(StackOverFlowDbContext context, IQuestionRepository questionRepository, IVoteRepository voteRepository)
        {
            _context = context;
            _questionRepository = questionRepository;
            _voteRepository = voteRepository;
        }

        public async Task<Answer> GetAnswerById(int answerId)
        {
            var answer = await _context.Answers.Where(a => a.AnswerID == answerId).FirstOrDefaultAsync();
            return answer;
        }

        public async Task<IEnumerable<Answer>> GetAnswersByQuestionId(int questionId)
        {
            return await _context.Answers.Where(a => a.QuestionID == questionId)
                                        .OrderByDescending(q => q.AnswerDateAndTime)
                                        .ToListAsync();
        }

        public async void InsertAnswer(Answer answer)
        {
            await _context.Answers.AddRangeAsync(answer);
            await _context.SaveChangesAsync();
            //Update answer count
            _questionRepository.UpdateQuestionAnswersCount(answer.QuestionID, 1);
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

        public async void UpdateAnswerVotesCount(int answerId, int userId, int value)
        {
            var answer = await _context.Answers.Where(a => a.AnswerID == answerId).FirstOrDefaultAsync();
            if (answer != null)
            {
                answer.VoteCount += value;
                await _context.SaveChangesAsync();

                //Update the total vote count on the question
                _questionRepository.UpdateQuestionVotesCount(answer.AnswerID, value);
                _voteRepository.UpdateVote(answerId, userId, value);
            }
        }

        public async void DeleteAnswer(int answerId)
        {
            var answer = await _context.Answers.Where(a => a.AnswerID == answerId).FirstOrDefaultAsync();
            if (answer != null)
            {
                _context.Answers.Remove(answer);
                await _context.SaveChangesAsync();

                //Update the answer count in question table
                _questionRepository.UpdateQuestionAnswersCount(answer.AnswerID, -1);
            }
        }

    }
}