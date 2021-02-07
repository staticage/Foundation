using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Example.Applications.Api.Common
{
    public static class JwtSecurityKey
    {
        static SymmetricSecurityKey Create(string secret)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
        }

        public static SymmetricSecurityKey Get() => Create("Th!sIs@Very$tr0ngKeyFor$un$et");

    }
}
