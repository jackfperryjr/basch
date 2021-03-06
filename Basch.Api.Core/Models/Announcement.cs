using System;

namespace Basch.Api.Core.Models  
{  
    public class Announcement
    {  
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string ContentImage { get; set; }
        public string Image { get; set; }
        public int Like { get; set; }
        public int Dislike { get; set; }
        public int Love { get; set; }
        public DateTime TimeStamp { get; set; }
    }  
}