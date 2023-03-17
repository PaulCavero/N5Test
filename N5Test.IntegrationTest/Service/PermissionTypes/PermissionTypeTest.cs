using N5Test.Models.PermissionTypes;

namespace N5Test.IntegrationTest.Service.PermissionTypes
{
    [TestClass]
    public partial class PermissionTypeTest : BaseTest
    {
        [TestMethod]
        public async Task ShouldCreatePermissionTypesWhenDataIsValid()
        {
            //given
            PermissionTypeDTO permisionDTO = CreatePermissionTypeDTOFiller().Create();
            
            //when
            await this.permissionTypeService.AddPermissionTypeAsync(permisionDTO);

            //then
            this.unitOfWorkMock.Verify(x => x.Save());
        }

        [TestMethod]
        public async Task ShouldUpdatePermissionTypesWhenDataIsValid()
        {
            //given
            PermissionTypeDTO permisionDTO = CreatePermissionTypeDTOFiller().Create();

            //when
            await this.permissionTypeService.ModifyPermissionTypeAsync(permisionDTO);
            

            //then
            this.unitOfWorkMock.Verify(x => x.Save());
        }

        [TestMethod]
        public async Task ShouldDeletePermissionTypesWhenDataIsValid()
        {
            //given
            PermissionTypeDTO permisionDTO = CreatePermissionTypeDTOFiller().Create();
            PermisionType permision = CreatePermissionTypeFiller().Create();
            this.unitOfWorkMock.Setup(x => x.PermisionTypeRepository.GetByID(permisionDTO.Id))
                .Returns(permision);
            //when
            await this.permissionTypeService.RemovePermissionTypeByIdAsync(permisionDTO.Id);

            //then
            this.unitOfWorkMock.Verify(x => x.Save());
        }

        [TestMethod]
        public async Task ShouldGetPermissionTypesById()
        {
            //given
            PermissionTypeDTO permisionDTO = CreatePermissionTypeDTOFiller().Create();
            PermisionType permision = CreatePermissionTypeFiller().Create();
            this.unitOfWorkMock.Setup(x => x.PermisionTypeRepository.GetByID(permision.Id))
                .Returns(permision);
            //when
            PermissionTypeDTO expectedPermissionType = await this.permissionTypeService.RetrievePermissionTypeByIdAsync(permision.Id);

            //then
            Assert.IsTrue(expectedPermissionType.Id == permision.Id);
        }
    }
}
