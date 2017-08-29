using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnkietaMVC.Models
{
    public class UserAnswer
    {
        public string QuestionId { get; set; }
        public string Answer { get; set; }
        public string Question { get; set; }
    }
}