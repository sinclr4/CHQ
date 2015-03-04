using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHQ.Models
{
    public class Question
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string Topic { get; set; }
    }

    public class Questions
    {
        public IEnumerable<Question> AllQuestions { get; set; }

    }
}