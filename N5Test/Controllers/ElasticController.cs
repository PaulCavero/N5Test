using Microsoft.AspNetCore.Mvc;
using N5Test.Models.Elastic;
using N5Test.Models.Permissions;
using N5Test.Services.ElasticProvider;

namespace N5Test.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ElasticController : ControllerBase
    {
        private readonly IElasticService elasticService;

        public ElasticController(IElasticService elasticService)
        {
            this.elasticService = elasticService;
        }
        [HttpGet("{empleyeeName}")]
        public ActionResult<List<PermissionDTO>> GetPermissionsByName(string empleyeeName)
        {
            try
            {
                var response = this.elasticService.SearchPermission(empleyeeName);
                var model = new SearchResultModel { Results = response.Documents.ToList() };
                return Ok(model);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
