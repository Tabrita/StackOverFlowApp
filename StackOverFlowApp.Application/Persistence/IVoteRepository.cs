using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverFlowApp.Application.Persistence
{
    public interface IVoteRepository
    {
        void UpdateVote(int answerId, int userId, int value);
    }
}