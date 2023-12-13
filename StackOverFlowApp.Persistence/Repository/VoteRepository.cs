using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StackOverFlowApp.Application.Persistence;
using StackOverFlowApp.Domain;

namespace StackOverFlowApp.Persistence.Repository
{
    public class VoteRepository : IVoteRepository
    {
        private readonly StackOverFlowDbContext _context;
        public VoteRepository(StackOverFlowDbContext context)
        {
            _context = context;
        }
        public async void UpdateVote(int answerId, int userId, int value)
        {
            int updateValue;
            if (value > 0) updateValue = 1;
            else if (value < 0) updateValue = -1;
            else updateValue = 0;

            var vote = await _context.Votes.Where(v => v.AnswerID == answerId).FirstOrDefaultAsync();
            if (vote != null)
            {
                vote.VoteValue = updateValue;
            }
            else
            {
                Vote newVote = new Vote() { AnswerID = answerId, UserID = userId, VoteValue = updateValue };
                await _context.Votes.AddAsync(newVote);
            }

            await _context.SaveChangesAsync();
        }
    }
}