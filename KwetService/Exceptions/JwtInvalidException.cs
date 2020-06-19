using System;

namespace KwetService.Exceptions
{
    public class JwtInvalidException : Exception
    {
        public JwtInvalidException() 
            : base("The provided JWT is invalid.")
        {
        }
    }
}