using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using N5Test.Controllers;
using N5Test.Data.Models.Permissions;
using N5Test.Data.Repository;
using N5Test.Models.Permissions;
using N5Test.Models.PermissionTypes;
using N5Test.Services.KafkaProvider;
using N5Test.Services.Permissions;
using RESTFulSense.Clients;


namespace N5Test.IntegrationTest
{
    public class BaseTest
    {
  
        protected readonly Mock<IRepository<PermisionType>> repositoryPermissionType;
        protected readonly Mock<IRepository<Permission>> repositoryPermission;
        protected readonly Mock<IUnitOfWork> unitOfWorkMock;
        protected readonly Mock<ILoggingBroker> loggingBrokerMock;
        protected readonly IPermissionTypeService permissionTypeService;
        protected readonly IPermissionService permissionService;
        protected readonly Mock<IPermissionTypeService> permissionTypeServiceMock;
        protected readonly Mock<IPermissionService> iPermissionServiceMock;
        protected readonly Mock<PermissionService> permissionServiceMock;
        protected readonly Mock<PermisionTypesController> permisionTypesControllerMock;
        protected readonly Mock<IKafkaService> kafkaMock;

        public BaseTest()
        {
            this.repositoryPermissionType = new Mock<IRepository<PermisionType>>();
            this.repositoryPermission = new Mock<IRepository<Permission>>();
            this.unitOfWorkMock = new Mock<IUnitOfWork>();
            this.unitOfWorkMock.Setup(x => x.PermisionTypeRepository).
                Returns(this.repositoryPermissionType.Object);
            this.unitOfWorkMock.Setup(x => x.PermisionRepository).
                Returns(this.repositoryPermission.Object);
            this.loggingBrokerMock = new Mock<ILoggingBroker>();
            this.kafkaMock = new Mock<IKafkaService>();
            this.permissionTypeService =
                new PermissionTypeService(this.unitOfWorkMock.Object, this.loggingBrokerMock.Object, this.kafkaMock.Object);

            this.permissionService =
                new PermissionService(this.unitOfWorkMock.Object, this.loggingBrokerMock.Object, this.kafkaMock.Object);
            this.permissionTypeServiceMock = new Mock<IPermissionTypeService>();
            this.permissionServiceMock = new Mock<PermissionService>();
            this.iPermissionServiceMock = new Mock<IPermissionService>();
            this.permisionTypesControllerMock = new Mock<PermisionTypesController>();
        }
        protected  static Filler<PermissionTypeDTO> CreatePermissionTypeDTOFiller()
        {
            var filler = new Filler<PermissionTypeDTO>();

            return filler;
        }

        protected static Filler<PermisionType> CreatePermissionTypeFiller()
        {
            var filler = new Filler<PermisionType>();

            return filler;
        }

        protected static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        protected static Filler<PermissionDTO> CreatePermissionDTOFiller()
        {
            var filler = new Filler<PermissionDTO>();

            return filler;
        }

        protected static Filler<Permission> CreatePermissionFiller()
        {
            var filler = new Filler<Permission>();

            return filler;
        }

        protected static T GetObjectResultContent<T>(ActionResult<T> result)
        {
            return (T)((ObjectResult)result.Result).Value;
        }
    }
}
