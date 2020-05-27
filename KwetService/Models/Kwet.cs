using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace KwetService.Models
{
    public class Kwet
    {
        [BsonId] 
        
        public Guid Id { get; set; }

        public string Message { get; set; }

        public DateTime TimeStamp { get; set; }
        
    }
    
}