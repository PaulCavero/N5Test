using N5Test.Data.Models.PermissionTypes;

namespace N5Test.Data.Models.Permissions;

public partial class Permission
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public string EmpleyeeForename { get; set; } = null!;

    public string EnployeeSurname { get; set; } = null!;

    public int PermissionTypeId { get; set; }

    public DateTime PermissionDate { get; set; }

    public virtual PermisionType PermissionType { get; set; } = null!;
}
