namespace AuthServer.Core.Models
{
    public class UserRefreshToken
    {
        public string Userid { get; set; }
        public string Code { get; set; }
        public DateTime Expiration { get; set; }
    }
}