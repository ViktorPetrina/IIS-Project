namespace MobilePhoneSpecsApi.Models
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public DateTime Expires { get; set; }
        public bool Revoked { get; set; }
    }
}
