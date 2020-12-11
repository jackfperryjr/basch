using System;

namespace Basch.Api.Core.Models  
{  
    public class Notification
    {  
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Image { get; set; }
        public int Active { get; set; }
    }  
}