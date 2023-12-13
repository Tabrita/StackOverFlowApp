using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverFlowApp.Domain
{
    public class Question
    {
        [Key]
        public int QuestionID { get; set; }
        public string QuestionTitle { get; set; }
        public string QuestionDescription { get; set; }
        public DateTime QuestionDateAndTime { get; set; }
        public int UserID { get; set; }
        public int CategoryID { get; set; }
        public int VoteCount { get; set; }
        public int AnswerCount { get; set; }
        public int ViewsCount { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }
        [ForeignKey("CategoryID")]
        public Category Category { get; set; }

        public List<Answer> Answers { get; set; }
    }
}