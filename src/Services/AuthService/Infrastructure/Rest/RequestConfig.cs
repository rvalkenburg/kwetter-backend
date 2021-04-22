namespace Kwetter.Services.AuthService.Infrastructure.Rest
{
    public class RequestConfig
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string RedirectUri { get; set;  }
        public string Code { get; set; }
        
    }
}