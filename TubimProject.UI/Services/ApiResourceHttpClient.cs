using Microsoft.Extensions.Configuration;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Collections.Generic;

using System.Threading.Tasks;
using TubimProject.UI.Models;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using TubimProject.Application.DTOs.Identity.User;
using TubimProject.Application.Response;
using TubimProject.Application.Constants;

namespace TubimProject.UI.Services
{
    public class ApiResourceHttpClient : IApiResourceHttpClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private HttpClient _client;

        public ApiResourceHttpClient(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor=httpContextAccessor;
            _configuration=configuration;
            _client=new HttpClient();
        }

        public async Task<HttpClient> GetHttpClient()
        {
            var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

            _client.SetBearerToken(accessToken);

            return _client;
        }
        public async Task<TokenResponse?> GetToken()
        {
            var disco = await _client.GetDiscoveryDocumentAsync(_configuration["AuthServerUrl"]);

            if (disco.IsError)
            {
                //loglama yap
            }

            var clientCredentialsTokenRequest = new ClientCredentialsTokenRequest();

            clientCredentialsTokenRequest.ClientId = _configuration["ClientResourceOwner:ClientId"];
            clientCredentialsTokenRequest.ClientSecret = _configuration["ClientResourceOwner:ClientSecret"];
            clientCredentialsTokenRequest.Address = disco.TokenEndpoint;

            var token = await _client.RequestClientCredentialsTokenAsync(clientCredentialsTokenRequest);

            if (token.IsError)
            {
                return null;
            }
            return token;
        }

        public async Task<Response> Register(UserSaveModel userSaveViewModel)
        {
            Response returnModel = new Response();
            var stringConent = new StringContent(JsonConvert.SerializeObject(userSaveViewModel), Encoding.UTF8, "application/json");

            var token = await GetToken();

            if (token is not null)
            {
                _client.SetBearerToken(token.AccessToken);
                var response = await _client.PostAsync($"{TubimProject.Application.Constants.UTILS.AUTH_SERVER_BASE_URL}user/signup", stringConent);
                if (!response.IsSuccessStatusCode)
                {
                    returnModel.isSuccess=false;
                    returnModel.Errors=JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());

                    return returnModel;
                }
                returnModel.isSuccess=true;
                return returnModel;
            }
            returnModel.isSuccess=false;
            returnModel.Errors="Token Doğrulanamadı";
            return returnModel;
        }

        public async Task<Response> CreateRole(AddNewRole roleName)
        {
            Response returnModel = new Response();
            var token = await GetToken();
            if (token is not null)
            {
                var stringConent = new StringContent(JsonConvert.SerializeObject(roleName), Encoding.UTF8, "application/json");
                _client.SetBearerToken(token.AccessToken);
                var response = await _client.PostAsync($"{UTILS.AUTH_SERVER_BASE_URL}user/CreateRole", stringConent);
                if (!response.IsSuccessStatusCode)
                {
                    returnModel.isSuccess=false;
                    returnModel.Errors= JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
                    return returnModel;
                }
                returnModel.isSuccess=true;
                return returnModel;
            }
            returnModel.isSuccess=false;
            returnModel.Errors="Token Doğrulanamadı";
            return returnModel;




        }

        public async Task<Response> GetRoles()
        {
            Response returnModel = new Response();
            var token = await GetToken();
            if (token is not null)
            {
                _client.SetBearerToken(token.AccessToken);

                var response = await _client.GetAsync($"{UTILS.AUTH_SERVER_BASE_URL}user/GetRoles");
                if (response.IsSuccessStatusCode)
                {
                    returnModel.isSuccess=true;
                    returnModel.Value=JsonConvert.DeserializeObject<List<RolesViewModel>>(response.Content.ReadAsStringAsync().Result);
                    return returnModel;
                }
                returnModel.isSuccess=false;
                return returnModel;
            }
            returnModel.isSuccess=false;
            returnModel.Errors="Token Doğrulanamadı";
            return returnModel;
        }

        public async Task<Response> CreateUser(AddNewUser model)
        {
            Response returnModel = new Response();
            var token = await GetToken();
            if (token is not null)
            {
                var stringConent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                _client.SetBearerToken(token.AccessToken);

                var response = await _client.PostAsync($"{UTILS.AUTH_SERVER_BASE_URL}user/CreateUser", stringConent);
                if (!response.IsSuccessStatusCode)
                {
                    returnModel.isSuccess=false;
                    returnModel.Errors=JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
                    return returnModel;
                }
                returnModel.isSuccess=true;
                return returnModel;
            }
            returnModel.isSuccess=false;
            returnModel.Errors="Token Doğrulanamadı";
            return returnModel;
        }

        public async Task<Response> GetUsers()
        {
            Response returnModel = new Response();
            var token = await GetToken();
            if (token is not null)
            {
                _client.SetBearerToken(token.AccessToken);
                var response = await _client.GetAsync($"{UTILS.AUTH_SERVER_BASE_URL}user/GetUsers");
                if (response.IsSuccessStatusCode)
                {
                    returnModel.isSuccess=true;
                    returnModel.Value= JsonConvert.DeserializeObject<List<AddNewUser>>(response.Content.ReadAsStringAsync().Result);
                    return returnModel;
                }
                returnModel.isSuccess=false;
                return returnModel;
            }
            returnModel.isSuccess=false;
            returnModel.Errors="Token Doğrulanamadı";
            return returnModel;
        }

        public async Task<Response> GetUserRoles(string userName)
        { 
            Response returnModel = new Response();
            var token = await GetToken();
            if (token is not null)
            {
                _client.SetBearerToken(token.AccessToken);
                var response = await _client.GetAsync($"{UTILS.AUTH_SERVER_BASE_URL}user/GetUserRoles?userName={userName}");
                if (response.IsSuccessStatusCode)
                {
                    returnModel.isSuccess=true;
                    returnModel.Value= JsonConvert.DeserializeObject<IEnumerable<string>>(response.Content.ReadAsStringAsync().Result);
                    return returnModel;
                }
                returnModel.isSuccess=false;
                return returnModel;
            }
            returnModel.isSuccess=false;
            returnModel.Errors="Token Doğrulanamadı";
            return returnModel; 
        }

        public async Task<Response> AddRoleToUserAsync(string userName, string roleName)
        {
            Response returnModel = new Response();
            var token = await GetToken();
            if (token is not null)
            { 
                var model = new AddRoleToUser
                {
                    UserName=   userName,
                    RoleName=    roleName
                }; 
                var stringConent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"); 
                _client.SetBearerToken(token.AccessToken); 
                var response = await _client.PostAsync($"{UTILS.AUTH_SERVER_BASE_URL}user/AddRoleToUser", stringConent);
                if (response.IsSuccessStatusCode)
                {
                    returnModel.isSuccess=true;
                    returnModel.Value= JsonConvert.DeserializeObject<IEnumerable<string>>(response.Content.ReadAsStringAsync().Result);
                    return returnModel;
                }
                returnModel.isSuccess=false;
                return returnModel;
            }
            returnModel.isSuccess=false;
            returnModel.Errors="Token Doğrulanamadı";
            return returnModel;

        }

        public async Task<Response> RemoveRoleOfUser(string userName, string roleName)
        {
            Response returnModel = new Response();
            var token = await GetToken();
            if (token is not null)
            {
                var model = new AddRoleToUser
                {
                    UserName=   userName,
                    RoleName=    roleName
                };
                var stringConent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                _client.SetBearerToken(token.AccessToken);
                var response = await _client.PostAsync($"{UTILS.AUTH_SERVER_BASE_URL}user/RemoveRoleOfUser", stringConent);
                if (response.IsSuccessStatusCode)
                {
                    returnModel.isSuccess=true;
                    returnModel.Value= JsonConvert.DeserializeObject<string>(response.Content.ReadAsStringAsync().Result);
                    return returnModel;
                }
                returnModel.isSuccess=false;
                returnModel.Errors=JsonConvert.DeserializeObject<string>(response.Content.ReadAsStringAsync().Result);
                return returnModel;
            }
            returnModel.isSuccess=false;
            returnModel.Errors="Token Doğrulanamadı";
            return returnModel;
        }

        public async Task<Response> GetUserInformation(string userName)
        {
            Response returnModel = new Response();
            var token = await GetToken();
            if (token is not null)
            {
                
                var stringConent = new StringContent(JsonConvert.SerializeObject(userName), Encoding.UTF8, "application/json");
                _client.SetBearerToken(token.AccessToken);
                var response = await _client.GetAsync($"{UTILS.AUTH_SERVER_BASE_URL}user/getUserInformation?userName={userName}");
                if (response.IsSuccessStatusCode)
                {
                    returnModel.isSuccess=true;
                    returnModel.Value= JsonConvert.DeserializeObject<UserInfo>(response.Content.ReadAsStringAsync().Result);
                    return returnModel;
                }
                returnModel.isSuccess=false;
                returnModel.Errors=JsonConvert.DeserializeObject<string>(response.Content.ReadAsStringAsync().Result);
                return returnModel;
            }
            returnModel.isSuccess=false;
            returnModel.Errors="Token Doğrulanamadı";
            return returnModel;
        }
    }
}
