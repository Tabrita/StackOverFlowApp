using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackOverFlowApp.Domain;

namespace StackOverFlowApp.Application.Persistence
{
    public interface IUsersRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int userId);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByEmailAndPasswordAsync(string email, string password);
        Task<int> GetLatestUserId();
        void InsertUser(User user);
        void UpdateUser(User user);
        void UpdateUserPassword(User user);
        void DeleteUser(int userId);
    }
}