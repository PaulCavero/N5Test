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
        [HttpGet("{name}")]
        public ActionResult<List<PermissionDTO>> GetPermissionsByName(string name)
        {
            try
            {
                var response = this.elasticService.SearchPermission(name);
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
