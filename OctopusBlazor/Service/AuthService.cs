using Newtonsoft.Json;
using OctopusBlazor.Data;
using OctopusModel;

namespace OctopusBlazor.Service
{
    public interface IAuthService
    {
        Task<User> LoginAsync(LoginModel user);
        Task Logout();
    }
    public class AuthService : IAuthService
    {
        public HttpClient _httpClient { get; }
        public AuthService(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri("https://localhost:7019");
            _httpClient = httpClient;
        }

        public async Task<User> LoginAsync(LoginModel user)
        {
            user.Password = Utility.Encrypt(user.Password);
            string serializedUser = JsonConvert.SerializeObject(user);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "Users/Login");
            requestMessage.Content = new StringContent(serializedUser);
            requestMessage.Content.Headers.ContentType
                = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var response = await _httpClient.SendAsync(requestMessage);
            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();
            var returnedUser = JsonConvert.DeserializeObject<User>(responseBody);
            if (!response.IsSuccessStatusCode)
            {
                return returnedUser;
            }

            return await Task.FromResult(returnedUser);
        }
        public async Task Logout()
        {

        }
    }
}
