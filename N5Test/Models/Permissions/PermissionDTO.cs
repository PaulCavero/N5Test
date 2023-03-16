using N5Test.Models.PermissionTypes;

namespace N5Test.Models.Permissions
{
    public class PermissionDTO
    {
        public int Id { get; set; }

        public string? Description { get; set; }

        public string EmpleyeeForename { get; set; } = null!;

        public string EnployeeSurname { get; set; } = null!;

        public int PermissionTypeId { get; set; }

        public DateTime PermissionDate { get; set; }

        public virtual PermissionTypeDTO PermissionType { get; set; } = null!;
    }
}
