using N5Test.Data.Models.Permissions;
using N5Test.Data.Models.PermissionTypes;
using N5Test.Data.Repository;

namespace N5Test.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<Permission> PermisionRepository { get; }
        IRepository<PermisionType> PermisionTypeRepository { get; }

        void Dispose();
        void Save();
    }
}