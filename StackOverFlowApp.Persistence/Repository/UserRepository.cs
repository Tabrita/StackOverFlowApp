using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StackOverFlowApp.Application.Persistence;
using StackOverFlowApp.Domain;

namespace StackOverFlowApp.Persistence.Repository
{
    public class UserRepository : IUsersRepository
    {
        private readonly StackOverFlowDbContext _context;
        public UserRepository(StackOverFlowDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.Where(u => u.IsAdmin == false)
                                    .OrderBy(u => u.Name)
                                    .ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            var user = await _context.Users.Where(u => u.UserID == userId).FirstOrDefaultAsync();
            return user;
        }

        public async Task<int> GetLatestUserId()
        {
            int userId = await _context.Users.Select(u => u.UserID).MaxAsync();
            return userId;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByEmailAndPasswordAsync(string email, string password)
        {
            var user = await _context.Users.Where(u => u.Email == email && u.PasswordHash == password).FirstOrDefaultAsync();
            return user;
        }

        public async void InsertUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async void UpdateUser(User user)
        {
            var userToUpdate = await _context.Users.Where(u => u.UserID == user.UserID).FirstOrDefaultAsync();
            if (userToUpdate != null)
            {
                userToUpdate.Name = user.Name;
                userToUpdate.Mobile = user.Mobile;
                await _context.SaveChangesAsync();
            }
        }

        public async void UpdateUserPassword(User user)
        {
            var userToUpdate = await _context.Users.Where(u => u.UserID == user.UserID).FirstOrDefaultAsync();
            if (userToUpdate != null)
            {
                userToUpdate.PasswordHash = user.PasswordHash;
                await _context.SaveChangesAsync();
            }
        }

        public async void DeleteUser(int userId)
        {
            var user = await _context.Users.Where(u => u.UserID == userId).FirstOrDefaultAsync();
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}