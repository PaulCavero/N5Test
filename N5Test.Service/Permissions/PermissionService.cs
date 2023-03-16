using N5Test.Data.Models.Permissions;
using N5Test.Data.UnitOfWork;
using N5Test.Models.Permissions;

namespace N5Test.Service.Permissions
{
    public partial class PermissionService : IPermissionService
    {
        private readonly IUnitOfWork unitOfWork;

        public PermissionService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task AddPermissionAsync(PermissionDTO permissionDTO)
        {
            try
            {
                ValidatePermissionIsNull(permissionDTO);

                unitOfWork.PermisionRepository.
                    Insert(ToPermission(permissionDTO));
                unitOfWork.Save();
            }
            catch (Exception ex)
            {
                string errorMessage = $"An error occurred using insert a Permission Type.";
                //_logger.Error($"[ContactService].[InsertItemAsync] " + errorMessage, ex);
                throw new ArgumentException(errorMessage, ex);
            }

            return Task.CompletedTask;
        }

        public IQueryable<PermissionDTO> RetrieveAllPermission()
        {
            try
            {
                IEnumerable<Permission> Permissions = unitOfWork.PermisionRepository.Get();

                return Permissions.Select(x => ToPermissionDTO(x)).AsQueryable<PermissionDTO>();
            }
            catch (Exception ex)
            {
                string errorMessage = $"An error occurred using RetrieveAllPermissionType.";
                //_logger.Error($"[ContactService].[InsertItemAsync] " + errorMessage, ex);
                throw new ArgumentException(errorMessage, ex);
            }
        }

        public ValueTask<PermissionDTO> RetrievePermissionByIdAsync(int permissionId)
        {
            try
            {
                ValidatePermissionId(permissionId);
                Permission permission = unitOfWork.PermisionRepository.GetByID(permissionId);
                return new ValueTask<PermissionDTO>(ToPermissionDTO(permission));
            }
            catch (Exception ex)
            {
                string errorMessage = $"An error occurred using RetrieveAllPermissionType.";
                //_logger.Error($"[ContactService].[InsertItemAsync] " + errorMessage, ex);
                throw new ArgumentException(errorMessage, ex);
            }
        }

        public Task ModifyPermissionAsync(PermissionDTO permissionDTO)
        {
            try
            {
                ValidatePermissionIsNull(permissionDTO);

                unitOfWork.PermisionRepository.Update(ToPermission(permissionDTO));
                unitOfWork.Save();
            }
            catch (Exception ex)
            {
                string errorMessage = $"An error occurred using ModifyPermissionAsync.";
                //_logger.Error($"[ContactService].[InsertItemAsync] " + errorMessage, ex);
                throw new ArgumentException(errorMessage, ex);
            }
            return Task.CompletedTask;
        }

        public Task RemovePermissionByIdAsync(int permissionId)
        {
            try
            {
                ValidatePermissionId(permissionId);
                Permission permission = unitOfWork.PermisionRepository.GetByID(permissionId);

                ValidatePermissionExist(permission);
                unitOfWork.PermisionRepository.Delete(permission);
                unitOfWork.Save();
            }
            catch (Exception ex)
            {
                string errorMessage = $"An error occurred using RemovePermissionTypeByIdAsync.";
                //_logger.Error($"[ContactService].[InsertItemAsync] " + errorMessage, ex);
                throw new ArgumentException(errorMessage, ex);
            }
            return Task.CompletedTask;
        }

        public static PermissionDTO ToPermissionDTO(Permission permission)
        {
            return new PermissionDTO
            {
                Id = permission.Id,
                Description = permission.Description,
                EnployeeSurname = permission.EnployeeSurname,
                EmpleyeeForename = permission.EmpleyeeForename,
                PermissionDate = permission.PermissionDate,
                PermissionTypeId = permission.PermissionTypeId,
            };
        }

        public static Permission ToPermission(PermissionDTO permissionDTO)
        {
            return new Permission
            {
                Id = permissionDTO.Id,
                Description = permissionDTO.Description,
                EnployeeSurname = permissionDTO.EnployeeSurname,
                EmpleyeeForename = permissionDTO.EmpleyeeForename,
                PermissionDate = permissionDTO.PermissionDate,
                PermissionTypeId = permissionDTO.PermissionTypeId
            };
        }
    }
}
