using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Morganas.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using IdentityModel.Client;
   
namespace Morganas.Controllers
{   
    [ApiController]
    [Route("[controller]")]
    public class HealthcheckController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<HealthcheckController> _logger;
        private readonly IConfiguration Configuration;

        public HealthcheckController(IHttpClientFactory httpClientFactory, ILogger<HealthcheckController> logger, IConfiguration configuracion)
        {
            _httpClient = httpClientFactory.CreateClient("UmbracoApiClient");
            _logger = logger;
            Configuration = configuracion;
        }

        [HttpGet]
        public async Task<IActionResult> GetHealthchecks()
        {
            HttpResponseMessage apiResponse = new();
            try
            {
                var endpoint = GetHealthcheckEndpoint(); 
                apiResponse = await _httpClient.GetAsync(endpoint);
 
                if (apiResponse.IsSuccessStatusCode)
                {
                    var content = await apiResponse.Content.ReadAsStringAsync();
                    var documentTypes = JsonSerializer.Deserialize<HealthCheckGroupsResponse>(content);

                    return Ok(documentTypes); 
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred accessing the requested service.");
            }
            
            return StatusCode((int)apiResponse.StatusCode, apiResponse.ReasonPhrase);
        }

        private string GetHealthcheckEndpoint() =>
             Configuration.GetSection("Umbraco").GetValue<string>("Endpoints:Healthcheck") ?? "";
    }
}