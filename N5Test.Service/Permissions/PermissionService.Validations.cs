using N5Test.Data.Models.Permissions;
using N5Test.Models.Permissions;

namespace N5Test.Service.Permissions
{
    public partial class PermissionService
    {
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
