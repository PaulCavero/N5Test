using N5Test.Data.Models.Permissions;
using N5Test.Data.Models.PermissionTypes;

namespace N5Test.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        GenericRepository<Permission> PermisionRepository { get; }
        GenericRepository<PermisionType> PermisionTypeRepository { get; }

        void Dispose();
        void Save();
    }
}