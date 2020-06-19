using System;

namespace KwetService.Exceptions
{
    public class LikeNotFoundException : Exception
    {
        public LikeNotFoundException() 
            : base("A like by this user could not be found in the kwet.")
        {
        }
    }
}