using N5Test.Data.Models.Permissions;
using N5Test.Models.Permissions;
using Nest;
namespace N5Test.Services.ElasticProvider
{
    public class ElasticService : IElasticService
    {
        private readonly IConfiguration configuration;
        private readonly ElasticClient client;

        public ElasticService(IConfiguration configuration)
        {
            this.configuration = configuration;
            client = new ElasticClient(
                new ConnectionSettings(new Uri(configuration.GetValue<string>("ElasticCloud:Endpoint")))
                    .DefaultIndex("permission")
                    .BasicAuthentication(configuration.GetValue<string>("ElasticCloud:BasicAuthUser"),
                        configuration.GetValue<string>("ElasticCloud:BasicAuthPassword")));
        }

        public ISearchResponse<Permission> SearchPermission(string searchText)
        {
            return client.Search<Permission>(s => s
                .From(0)
                .Size(10)
                .Query(q => q
                    .Match(m => m
                        .Field(f => f.EmpleyeeForename)
                        .Query(searchText)
                    )
                ));
        }

        public void UploadPermission(PermissionDTO permission)
        {
            var result = client.IndexDocument<PermissionDTO>(permission);
        }
    }
}
