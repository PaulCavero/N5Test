using N5Test.Data.Models.Permissions;
using N5Test.Models.Permissions;
using Nest;

namespace N5Test.Services.ElasticProvider
{
    public interface IElasticService
    {
        ISearchResponse<Permission> SearchPermission(string searchText);
        void UploadPermission(PermissionDTO permission);
    }
}