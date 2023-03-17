using N5Test.Data.Models.Permissions;
using N5Test.Models.Permissions;
using N5Test.Models.PermissionTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N5Test.IntegrationTest.Service.Permissions
{
    [TestClass]
    public partial class PermissionTest : BaseTest
    {
        [TestMethod]
        public async Task ShouldCreatePermissionWhenDataIsValid()
        {
            //given
            PermissionDTO permisionDTO = CreatePermissionDTOFiller().Create();

            //when
            await this.permissionService.AddPermissionAsync(permisionDTO);

            //then
            this.unitOfWorkMock.Verify(x => x.Save());
        }

        [TestMethod]
        public async Task ShouldUpdatePermissionWhenDataIsValid()
        {
            //given
            PermissionDTO permisionDTO = CreatePermissionDTOFiller().Create();

            //when
            await this.permissionService.ModifyPermissionAsync(permisionDTO);


            //then
            this.unitOfWorkMock.Verify(x => x.Save());
        }

        [TestMethod]
        public async Task ShouldDeletePermissionWhenDataIsValid()
        {
            //given
            PermissionDTO permisionDTO = CreatePermissionDTOFiller().Create();
            Permission permision = CreatePermissionFiller().Create();
            this.unitOfWorkMock.Setup(x => x.PermisionRepository.GetByID(permisionDTO.Id))
                .Returns(permision);
            //when
            await this.permissionService.RemovePermissionByIdAsync(permisionDTO.Id);

            //then
            this.unitOfWorkMock.Verify(x => x.Save());
        }

        [TestMethod]
        public async Task ShouldGetPermissionById()
        {
            //given
            PermissionDTO permisionDTO = CreatePermissionDTOFiller().Create();
            Permission permision = CreatePermissionFiller().Create();
            this.unitOfWorkMock.Setup(x => x.PermisionRepository.GetByID(permision.Id))
                .Returns(permision);
            //when
            PermissionDTO expectedPermissionType = await this.permissionService.RetrievePermissionByIdAsync(permision.Id);

            //then
            Assert.IsTrue(expectedPermissionType.Id == permision.Id);
        }
    }
}
