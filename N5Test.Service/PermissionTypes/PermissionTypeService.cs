﻿using N5Test.Data.Models.PermissionTypes;
using N5Test.Data.UnitOfWork;
using N5Test.Models.PermissionTypes;

namespace N5Test.Service.PermissionTypes
{
    public partial class PermissionTypeService : IPermissionTypeService
    {
        private readonly IUnitOfWork unitOfWork;

        public PermissionTypeService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public Task AddPermissionTypeAsync(PermissionTypeDTO permissionTypeDTO)
        {
            try
            {
                ValidatePermissionTypeIsNull(permissionTypeDTO);

                unitOfWork.PermisionTypeRepository.
                    Insert(ToPermissionType(permissionTypeDTO));
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

        public IQueryable<PermissionTypeDTO> RetrieveAllPermissionType()
        {
            try
            {
                IEnumerable<PermisionType> Permissions = unitOfWork.PermisionTypeRepository.Get();

                return Permissions.Select(x => ToPermissionTypeDTO(x)).AsQueryable<PermissionTypeDTO>();
            }
            catch (Exception ex)
            {
                string errorMessage = $"An error occurred using RetrieveAllPermissionType.";
                //_logger.Error($"[ContactService].[InsertItemAsync] " + errorMessage, ex);
                throw new ArgumentException(errorMessage, ex);
            }
        }

        public ValueTask<PermissionTypeDTO> RetrievePermissionTypeByIdAsync(int permissionTypeId)
        {
            try
            {
                ValidatePermissionTypeId(permissionTypeId);
                PermisionType permisionType = unitOfWork.PermisionTypeRepository.GetByID(permissionTypeId);
                return new ValueTask<PermissionTypeDTO>(ToPermissionTypeDTO(permisionType));
            }
            catch (Exception ex)
            {
                string errorMessage = $"An error occurred using RetrieveAllPermissionType.";
                //_logger.Error($"[ContactService].[InsertItemAsync] " + errorMessage, ex);
                throw new ArgumentException(errorMessage, ex);
            }
        }

        public Task ModifyPermissionTypeAsync(PermissionTypeDTO permissionTypeDTO)
        {
            try
            {
                ValidatePermissionTypeIsNull(permissionTypeDTO);
              
                unitOfWork.PermisionTypeRepository.Update(ToPermissionType(permissionTypeDTO));
                unitOfWork.Save();
            }
            catch (Exception ex)
            {
                string errorMessage = $"An error occurred using ModifyPermissionTypeAsync.";
                //_logger.Error($"[ContactService].[InsertItemAsync] " + errorMessage, ex);
                throw new ArgumentException(errorMessage, ex);
            }
            return Task.CompletedTask;
        }

        public Task RemovePermissionTypeByIdAsync(int permissionTypeId)
        {
            try
            {
                ValidatePermissionTypeId(permissionTypeId);
                PermisionType permisionType = unitOfWork.PermisionTypeRepository.GetByID(permissionTypeId);

                ValidatePermissionTypeExist(permisionType);
                unitOfWork.PermisionTypeRepository.Delete(permisionType);
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

        public static PermissionTypeDTO ToPermissionTypeDTO(PermisionType permisionType)
        {
            return new PermissionTypeDTO
            {
                Id = permisionType.Id,
                Description = permisionType.Description
            };
        }

        public static PermisionType ToPermissionType(PermissionTypeDTO permisionTypeDTO)
        {
            return new PermisionType
            {
                Id = permisionTypeDTO.Id,
                Description = permisionTypeDTO.Description
            };
        }

    }
}
