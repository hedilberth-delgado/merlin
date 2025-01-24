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
    public class DocumentTypeController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<DocumentTypeController> _logger;
        private readonly IConfiguration Configuration;

        public DocumentTypeController(IHttpClientFactory httpClientFactory, ILogger<DocumentTypeController> logger, IConfiguration configuracion)
        {
            _httpClient = httpClientFactory.CreateClient("UmbracoApiClient");
            _logger = logger;
            Configuration = configuracion;
        }
          
        // POST: /DocumentType
        [HttpPost]
        public async Task<IActionResult> PostDocumentType([FromBody] DocumentTypeRequest documentTypeDto)
        {
            if (documentTypeDto == null)
            {
                return BadRequest("Document type data is required.");
            }
            HttpResponseMessage response = new();
            try
            {
                var jsonContent = JsonSerializer.Serialize(documentTypeDto);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                string endpoint = GetDocumentTypeEndpoint();
                response = await _httpClient.PostAsync(endpoint, content);

                // Handle the response from the external API
                if (response.IsSuccessStatusCode)
                {
                    return CreatedAtAction(nameof(PostDocumentType), documentTypeDto);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while posting to the service.");
            }
            return StatusCode((int)response.StatusCode, response.ReasonPhrase);
        }

        // DELETE: /DocumentType/Key
        [HttpDelete("{key}")]
        public async Task<IActionResult> DeleteDocumentType(string key)
        {
            
            HttpResponseMessage response = new();            
            try
            {
                string endpoint = GetDocumentTypeEndpoint();
                response = await _httpClient.DeleteAsync($"{endpoint}/{key}");
                
                if (response.IsSuccessStatusCode)
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while posting to the service.");
            }
            return StatusCode((int)response.StatusCode, response.ReasonPhrase);
        }

        private string GetDocumentTypeEndpoint() =>
             Configuration.GetSection("Umbraco").GetValue<string>("Endpoints:DocumentType") ?? "";
    }
}
