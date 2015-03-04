using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHQ.Models
{
    public class Topic
    {
   
        public string ID { get; set; }
      
    }

    public class Topics
    {
        public IEnumerable<Topic> AllTopics { get; set; }
      
    }
}
   