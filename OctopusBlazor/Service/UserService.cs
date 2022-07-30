using Newtonsoft.Json;
using OctopusBlazor.Data;
using OctopusModel;
using System.Net.Http.Json;

namespace OctopusBlazor.Service
{
    public interface IUserService
    {
        Task<User> LoginAsync(LoginModel user);
        Task Logout();
        Task<User> GetUserByAccessTokenAsync(string accessToken);
    }
    public class UserService : IUserService
    {
        public HttpClient _httpClient { get; }
        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<User> GetUserByAccessTokenAsync(string accessToken)
        {
            string serializedRefreshRequest = JsonConvert.SerializeObject(accessToken);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "Users/GetUserByAccessToken");
            requestMessage.Content = new StringContent(serializedRefreshRequest);

            requestMessage.Content.Headers.ContentType
                = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();

            var returnedUser = JsonConvert.DeserializeObject<User>(responseBody);

            return await Task.FromResult(returnedUser);
        }


        public async Task<User> LoginAsync(LoginModel user)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/User/Login", user);
            var content = await response.Content.ReadAsStringAsync();

            var responseBody = await response.Content.ReadAsStringAsync();
            var returnedUser = JsonConvert.DeserializeObject<User>(responseBody);
            return await Task.FromResult(returnedUser);
        }
        public async Task Logout()
        {

        }
    }
}
