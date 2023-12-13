using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverFlowApp.Domain
{
    public class Vote
    {
        [Key]
        public int VoteID { get; set; }
        public int UserID { get; set; }
        public int AnswerID { get; set; }
        public int VoteValue { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }
        [ForeignKey("AnswerID")]
        public Answer Answer { get; set; }
    }
}