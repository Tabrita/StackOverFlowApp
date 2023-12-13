using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverFlowApp.Domain
{
    public class Answer
    {
        [Key]
        public int AnswerID { get; set; }
        public string AnswerText { get; set; }
        public DateTime AnswerDateAndTime { get; set; }
        public int UserID { get; set; }
        public int QuestionID { get; set; }
        public int VoteCount { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }
        [ForeignKey("QuestionID")]
        public Question Question { get; set; }

        public List<Vote> Votes { get; set; }
    }
}