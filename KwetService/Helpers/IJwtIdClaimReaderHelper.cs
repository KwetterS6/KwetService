using System;

namespace KwetService.Helpers
{
    public interface IJwtIdClaimReaderHelper
    {
        public Guid getUserIdFromToken(string jwt);
    }
}
