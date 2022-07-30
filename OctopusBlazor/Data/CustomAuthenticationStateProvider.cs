using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using OctopusBlazor.Service;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
namespace OctopusBlazor.Data
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        public IUserService _userService { get; set; }        
        private readonly ILocalStorageService _localStorageService;
        public CustomAuthenticationStateProvider(ILocalStorageService localStorageService,
            IUserService userService,
            HttpClient httpClient)
        {
            _localStorageService = localStorageService;
            _userService = userService;
            _httpClient = httpClient;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var accessToken = await _localStorageService.GetItemAsync<string>("accessToken");
            ClaimsIdentity identity;
            if (accessToken != null && accessToken != string.Empty)
            {
                User user = await _userService.GetUserByAccessTokenAsync(accessToken);
                identity = GetClaimsIdentity(user);
            }
            else
            {
                identity = new ClaimsIdentity();
            }
            var claimsPrincipal = new ClaimsPrincipal(identity);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }
        public async Task MarkUserAsAuthenticated(User user)
        {
            await _localStorageService.SetItemAsync("accessToken", user.AccessToken);
            await _localStorageService.SetItemAsync("refreshToken", user.RefreshToken);
            var identity = GetClaimsIdentity(user);
            var claimsPrincipal = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }
        public async Task MarkUserAsLoggedOut()
        {
            await _localStorageService.RemoveItemAsync("refreshToken");
            await _localStorageService.RemoveItemAsync("accessToken");
            var identity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        private ClaimsIdentity GetClaimsIdentity(User user)
        {
            var claimsIdentity = new ClaimsIdentity();

            if (user.UserID != null)
            {
                claimsIdentity = new ClaimsIdentity(new[]
                                {
                                    new Claim("UserID", user.UserID),
                                    new Claim(ClaimTypes.Role, "Member"),
                                }, "apiauth_type");
            }
            return claimsIdentity;
        }


    }
}
