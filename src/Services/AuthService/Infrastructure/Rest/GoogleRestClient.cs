using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Kwetter.Services.AuthService.Application.Common.Interfaces;
using Kwetter.Services.AuthService.Application.Common.Models;
using Newtonsoft.Json;

namespace Kwetter.Services.AuthService.Infrastructure.Rest
{
    public class GoogleRestClient : IAuthHttpRequest
    {
        private readonly HttpClient _client;
        private readonly RequestConfig _config;

        public GoogleRestClient(HttpClient client, RequestConfig config)
        {
            _client = client;
            _config = config;
        }
        
        public async Task<Response<AuthResponseDto>> SendAuthRequest(string code)
        {
            Response<AuthResponseDto> response = new Response<AuthResponseDto>();
            
            string codeQuery =
                $"code={code}" +
                $"&client_id={_config.ClientId}" +
                $"&client_secret={_config.ClientSecret}" +
                $"&redirect_uri=postmessage" +
                $"&grant_type=authorization_code";
            
            HttpContent content = new StringContent(codeQuery, Encoding.UTF8,
                "application/x-www-form-urlencoded");
                
            HttpRequestMessage message = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new System.Uri("https://oauth2.googleapis.com/token"),
                Content = content
            };
            HttpResponseMessage httpResponseMessage = await _client.SendAsync(message);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string json = await httpResponseMessage.Content.ReadAsStringAsync();
                response.Data = JsonConvert.DeserializeObject<AuthResponseDto>(json);
                response.Success = true;
            }
            return response;
        }
    }
}