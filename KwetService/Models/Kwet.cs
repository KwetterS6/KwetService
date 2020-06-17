using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace KwetService.Models
{
    public class Kwet
    {
        [BsonId] 
        public Guid KwetId { get; set; }
        
        public Guid UserId { get; set; }
        public string UserName { get; set; }

        public string Message { get; set; }

        public DateTime TimeStamp { get; set; }

        public List<Likes> Likes { get; set; }

    }
    
}