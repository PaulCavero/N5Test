using N5Test.Models.Permissions;

namespace N5Test.Services.Permissions
{
    public interface IPermissionService
    {
        Task AddPermissionAsync(PermissionDTO permissionDTO);
        Task ModifyPermissionAsync(PermissionDTO permissionDTO);
        Task RemovePermissionByIdAsync(int permissionId);
        IQueryable<PermissionDTO> RetrieveAllPermission();
        ValueTask<PermissionDTO> RetrievePermissionByIdAsync(int permissionId);
    }
}