using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Morganas.Models;
using IdentityModel.Client;
   
namespace Morganas.Middleware
{
    public class AuthenticationDelegatingHandler: DelegatingHandler
    {
        private readonly SecurityCMS _securityCMSOptions;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<AuthenticationDelegatingHandler> _logger;

        public AuthenticationDelegatingHandler(
            IOptions<SecurityCMS> securityCMSOptions,
            IHttpClientFactory httpClientFactory,
            ILogger<AuthenticationDelegatingHandler> logger)
        {
            _securityCMSOptions = securityCMSOptions.Value;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            try
            {
                var accessToken = await GetBearerToken();
                request.Headers.Add("Authorization", "Bearer " + accessToken);

                return await base.SendAsync(request, cancellationToken);
            }
            catch (HttpRequestException httpRequestException)
            {
                _logger.LogError(httpRequestException, "An error occurred while sending the request.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in the AuthenticationDelegatingHandler.");
                throw;
            }
        }

        private async Task<string> GetBearerToken()
        {
            using HttpClient client = _httpClientFactory.CreateClient("UmbracoAuthClient");
                var tokenResponse = await client.RequestClientCredentialsTokenAsync(
                    new ClientCredentialsTokenRequest
                    {
                        Address = $"{_securityCMSOptions?.AuthEndpoint}",
                        ClientId = _securityCMSOptions?.ClientId!,
                        ClientSecret = _securityCMSOptions?.ClientSecret
                    }
                );
                if (tokenResponse.IsError || tokenResponse.AccessToken is null)
                {
                    Console.WriteLine($"Error obtaining a token: {tokenResponse.Exception}");
                    return string.Empty;
                }

                return tokenResponse.AccessToken;
        }
    }
}