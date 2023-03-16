using N5Test.Models.Permissions;

namespace N5Test.Models.PermissionTypes
{
    public class PermissionTypeDTO
    {
        public int Id { get; set; }

        public string Description { get; set; } = null!;

        public virtual ICollection<PermissionDTO> Permissions { get; } = new List<PermissionDTO>();
    }
}
