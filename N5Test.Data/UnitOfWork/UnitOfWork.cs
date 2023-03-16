using Microsoft.EntityFrameworkCore;
using N5Test.Data.Models.Permissions;
using N5Test.Data.Models.PermissionTypes;

namespace N5Test.Data.UnitOfWork
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private GenericRepository<PermisionType> permisionTypeRepository;
        private GenericRepository<Permission> permissionRepository;
        private readonly N5testContext context;

        public UnitOfWork(N5testContext context)
        {
            this.context = context;
        }

        public GenericRepository<PermisionType> PermisionTypeRepository
        {
            get
            {

                if (permisionTypeRepository == null)
                {
                    permisionTypeRepository = new GenericRepository<PermisionType>(context);
                }
                return permisionTypeRepository;
            }
        }

        public GenericRepository<Permission> PermisionRepository
        {
            get
            {

                if (permissionRepository == null)
                {
                    permissionRepository = new GenericRepository<Permission>(context);
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
