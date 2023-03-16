using Microsoft.AspNetCore.Mvc;
using N5Test.Models.PermissionTypes;
using N5Test.Services.PermissionTypes;

namespace N5Test.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PermisionTypesController : ControllerBase
    {
        private readonly IPermissionTypeService permissionTypeService;

        public PermisionTypesController(IPermissionTypeService permissionTypeService)
        {
            this.permissionTypeService = permissionTypeService;
        }

        [HttpGet]
        public ActionResult<IQueryable<PermissionTypeDTO>> GetAllPermisionTypes()
        {
            try
            {   
                IQueryable permissionTypes = 
                    this.permissionTypeService.RetrieveAllPermissionType();
                return Ok(permissionTypes);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async ValueTask<ActionResult<PermissionTypeDTO>> GetPermisionType(int id)
        {
            try
            {
                PermissionTypeDTO permisionType = 
                    await this.permissionTypeService.RetrievePermissionTypeByIdAsync(id);
                return Ok(permisionType);
            }
            catch (Exception ex)
            {
               return Problem(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutPermisionType(PermissionTypeDTO permisionType)
        {
            try
            {
                await this.permissionTypeService.ModifyPermissionTypeAsync(permisionType);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<PermissionTypeDTO>> PostPermisionType(PermissionTypeDTO permisionType)
        {
            try
            {
                await this.permissionTypeService.AddPermissionTypeAsync(permisionType);
                return CreatedAtAction("GetPermisionType", new { id = permisionType.Id }, permisionType);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePermisionType(int id)
        {
            try
            {
                await this.permissionTypeService.RemovePermissionTypeByIdAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
