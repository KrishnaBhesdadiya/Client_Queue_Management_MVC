using Frontend_Exam.Models;
using Newtonsoft.Json;
using System.Text;

namespace Frontend_Exam.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<LoginResponse?> AuthenticateUserAsync(string email, string password)
        {
            string url = "https://cmsback.sampaarsh.cloud/auth/login";

            var payload = new
            {
                email = email,
                password = password
            };

            var content = new StringContent(
                JsonConvert.SerializeObject(payload),
                Encoding.UTF8,
                "application/json"
            );

            HttpResponseMessage response = await _httpClient.PostAsync(url, content);

            string responseBody = await response.Content.ReadAsStringAsync();

            Console.WriteLine("Response: " + responseBody);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<LoginResponse>(responseBody);
        }
    }
}
