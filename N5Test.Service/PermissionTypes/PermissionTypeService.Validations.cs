using N5Test.Data.Models.PermissionTypes;
using N5Test.Models.PermissionTypes;

namespace N5Test.Service.PermissionTypes
{
    public partial class PermissionTypeService
    {
        private static void ValidatePermissionTypeIsNull(PermissionTypeDTO permissionTypeDTO) {
            if (permissionTypeDTO is null) {
                throw new ArgumentNullException
                            ("PermissionType", "The Instance of PermissionType cannot be null.");
            }
        }

        private static void ValidatePermissionTypeExist(PermisionType permissionType)
        {
            if (permissionType is null)
            {
                throw new ArgumentNullException
                            ("PermissionType", "The Instance of PermissionType cannot befound on DataBase.");
            }
        }

        private static void ValidatePermissionTypeId(int permissionTypeId) {
            if (permissionTypeId == 0) {
                throw new ArgumentNullException
                        ("PermissionType", "The ID of PermissionType cannot be default.");
            }
        }
    }
}
