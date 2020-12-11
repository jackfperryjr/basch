using System;

namespace Basch.Api.Core.Models  
{  
    public class Feed
    {  
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public int Like { get; set; }
        public int Dislike { get; set; }
        public int Love { get; set; }
        public DateTime TimeStamp { get; set; }
    }  
}