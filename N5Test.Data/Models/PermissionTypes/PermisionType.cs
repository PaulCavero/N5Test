using N5Test.Data.Models.Permissions;

namespace N5Test.Data.Models.PermissionTypes;

public partial class PermisionType
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<Permission> Permissions { get; } = new List<Permission>();
}
