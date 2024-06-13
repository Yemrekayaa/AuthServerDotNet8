using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace AuthServer.Service.Services
{
    public static class SignService
    {
        public static SecurityKey GetSymmetricSecurityKey(string SecurityKey){
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey));
        }
    }
}