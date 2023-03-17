using Microsoft.EntityFrameworkCore;
using N5Test.Data.Models.Permissions;
using N5Test.Data.Models.PermissionTypes;
using N5Test.Data.Repository;

namespace N5Test.Data.UnitOfWork
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private IRepository<PermisionType> permisionTypeRepository;
        private IRepository<Permission> permissionRepository;
        private N5testContext context = null;

        public UnitOfWork(N5testContext context)
        {
            this.context = context;
        }

        public IRepository<PermisionType> PermisionTypeRepository
        {
            get
            {

                if (permisionTypeRepository == null)
                {
                    permisionTypeRepository = new Repository<PermisionType>(context);
                }
                return permisionTypeRepository;
            }
        }

        public IRepository<Permission> PermisionRepository
        {
            get
            {

                if (permissionRepository == null)
                {
                    permissionRepository = new Repository<Permission>(context);
                }
                return permissionRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;


        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
