using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StackOverFlowApp.Domain;

namespace StackOverFlowApp.Persistence
{
    public class StackOverFlowDbContext : DbContext
    {
        public StackOverFlowDbContext(DbContextOptions<StackOverFlowDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Vote> Votes { get; set; }
    }
}