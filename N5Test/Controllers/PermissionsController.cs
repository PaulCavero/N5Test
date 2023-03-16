using Microsoft.AspNetCore.Mvc;
using N5Test.Models.Permissions;
using N5Test.Services.Permissions;

namespace N5Test.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private readonly IPermissionService permissionService;

        public PermissionsController(IPermissionService permissionService)
        {
            this.permissionService = permissionService;
        }
        [HttpGet]
        public ActionResult<IQueryable<PermissionDTO>> GetAllPermissions()
        {
            try
            {
                IQueryable permissions =
                    this.permissionService.RetrieveAllPermission();
                return Ok(permissions);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async ValueTask<ActionResult<PermissionDTO>> GetPermission(int id)
        {
            try
            {
                PermissionDTO Permission =
                    await this.permissionService.RetrievePermissionByIdAsync(id);
                return Ok(Permission);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutPermission(PermissionDTO Permission)
        {
            try
            {
                await this.permissionService.ModifyPermissionAsync(Permission);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<PermissionDTO>> PostPermission(PermissionDTO Permission)
        {
            try
            {
                await this.permissionService.AddPermissionAsync(Permission);
                return CreatedAtAction("GetPermission", new { id = Permission.Id }, Permission);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePermission(int id)
        {
            try
            {
                await this.permissionService.RemovePermissionByIdAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
