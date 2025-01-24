using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Api.Common.Attributes;
using Umbraco.Cms.Api.Common.ViewModels.Pagination;
using Umbraco.Cms.Api.Management.Controllers;
using Umbraco.Cms.Api.Management.Routing;

namespace MorganasUmbraco.Controllers
{
    [MapToApi("custom-backoffice")] 
    [ApiExplorerSettings(GroupName = "Custom Management API")]
    [VersionedApiBackOfficeRoute("custom/is-ok")]
    public class BackofficeApiController : ManagementApiControllerBase
    {
        public BackofficeApiController(){

        }

        [HttpGet]
        public IActionResult IsOk(bool isOk)
        {
            if (isOk == false)
            {
                return OperationStatusResult(
                    OperationStatus.InvalidValue,
                    builder => BadRequest(
                        builder
                            .WithTitle("That was not valid")
                            .Build()
                    )
                );
            }

            return Ok();
        }
    }

    public enum OperationStatus
    {
        NotFound,
        InvalidValue,
        DuplicateValue
    }
}