using N5Test.Broker.Loggings;
using N5Test.Data.Models.Permissions;
using N5Test.Data.UnitOfWork;
using N5Test.Models.Kafka;
using N5Test.Models.Permissions;
using N5Test.Services.ElasticProvider;
using N5Test.Services.KafkaProvider;

namespace N5Test.Services.Permissions
{
    public partial class PermissionService : IPermissionService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILoggingBroker loggingBroker;
        private readonly IKafkaService kafkaService;
        private readonly IElasticService elasticService;

        public PermissionService(IUnitOfWork unitOfWork, 
            ILoggingBroker loggingBroker, IKafkaService kafkaService,
            IElasticService elasticService)
        {
            this.unitOfWork = unitOfWork;
            this.loggingBroker = loggingBroker;
            this.kafkaService = kafkaService;
            this.elasticService = elasticService;
        }

        public Task AddPermissionAsync(PermissionDTO permissionDTO)
        {
            try
            {
                ValidatePermissionIsNull(permissionDTO);

                unitOfWork.PermisionRepository.
                    Insert(ToPermission(permissionDTO));
                unitOfWork.Save();
                kafkaService.SendKafkaMessage(new KafkaOperation() 
                {Id = Guid.NewGuid(), NameOperation= "PermissionResquest" });
                elasticService.UploadPermission(permissionDTO);
            }
            catch (Exception ex)
            {
                string errorMessage = $"An error occurred using insert a Permission Type.";
                this.loggingBroker.LogError(ex);
                throw new ArgumentException(errorMessage, ex);
            }

            return Task.CompletedTask;
        }

        public IQueryable<PermissionDTO> RetrieveAllPermission()
        {
            try
            {
                IEnumerable<Permission> Permissions = unitOfWork.PermisionRepository.Get();
                kafkaService.SendKafkaMessage(new KafkaOperation()
                { Id = Guid.NewGuid(), NameOperation = "PermissionGetAll" });
                return Permissions.Select(x => ToPermissionDTO(x)).AsQueryable<PermissionDTO>();
            }
            catch (Exception ex)
            {
                string errorMessage = $"An error occurred using RetrieveAllPermissionType.";
                this.loggingBroker.LogError(ex);
                throw new ArgumentException(errorMessage, ex);
            }
        }

        public ValueTask<PermissionDTO> RetrievePermissionByIdAsync(int permissionId)
        {
            try
            {
                ValidatePermissionId(permissionId);
                Permission permission = unitOfWork.PermisionRepository.GetByID(permissionId);
                kafkaService.SendKafkaMessage(new KafkaOperation()
                { Id = Guid.NewGuid(), NameOperation = "PermissionGetById" });
                return new ValueTask<PermissionDTO>(ToPermissionDTO(permission));
            }
            catch (Exception ex)
            {
                string errorMessage = $"An error occurred using RetrieveAllPermissionType.";
                this.loggingBroker.LogError(ex);
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
                kafkaService.SendKafkaMessage(new KafkaOperation()
                { Id = Guid.NewGuid(), NameOperation = "PermissionModify" });
            }
            catch (Exception ex)
            {
                string errorMessage = $"An error occurred using ModifyPermissionAsync.";
                this.loggingBroker.LogError(ex);
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
                this.loggingBroker.LogError(ex);
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

        private static void ValidatePermissionIsNull(PermissionDTO permissionDTO)
        {
            if (permissionDTO is null)
            {
                throw new ArgumentNullException
                            ("Permission", "The Instance of Permission cannot be null.");
            }
        }

        private static void ValidatePermissionExist(Permission permission)
        {
            if (permission is null)
            {
                throw new ArgumentNullException
                            ("Permission", "The Instance of Permission cannot befound on DataBase.");
            }
        }

        private static void ValidatePermissionId(int permissionId)
        {
            if (permissionId == 0)
            {
                throw new ArgumentNullException
                        ("Permission", "The ID of Permission cannot be default.");
            }
        }
    }
}
