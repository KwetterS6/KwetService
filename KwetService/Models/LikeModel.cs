using System;

namespace KwetService.Models
{
    public class LikeModel
    {
        public Guid Id { get; set; }
        
        public string UserName { get; set; }
        public Guid KwetId { get; set; }
    }
}