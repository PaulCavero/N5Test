using N5Test.Models.PermissionTypes;

namespace N5Test.Services.PermissionTypes
{
    public interface IPermissionTypeService
    {
        Task AddPermissionTypeAsync(PermissionTypeDTO permissionTypeDTO);
        Task ModifyPermissionTypeAsync(PermissionTypeDTO permissionTypeDTO);
        Task RemovePermissionTypeByIdAsync(int permissionTypeId);
        IQueryable<PermissionTypeDTO> RetrieveAllPermissionType();
        ValueTask<PermissionTypeDTO> RetrievePermissionTypeByIdAsync(int permissionTypeId);
    }
}