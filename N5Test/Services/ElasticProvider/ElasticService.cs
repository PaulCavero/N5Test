using N5Test.Broker.Loggings;
using N5Test.Data.Models.Permissions;
using N5Test.Models.Permissions;
using Nest;
namespace N5Test.Services.ElasticProvider
{
    public class ElasticService : IElasticService
    {
        private readonly IConfiguration configuration;
        private readonly ILoggingBroker loggingBroker;
        private readonly ElasticClient client;

        public ElasticService(IConfiguration configuration, ILoggingBroker loggingBroker)
        {
            this.configuration = configuration;
            this.loggingBroker = loggingBroker;
            client = new ElasticClient(
                new ConnectionSettings(new Uri(configuration.GetValue<string>("ElasticCloud:Endpoint")))
                    .DefaultIndex("permission")
                    .BasicAuthentication(configuration.GetValue<string>("ElasticCloud:BasicAuthUser"),
                        configuration.GetValue<string>("ElasticCloud:BasicAuthPassword")));
        }

        public ISearchResponse<Permission> SearchPermission(string searchText)
        {
            try
            {
                if (searchText == string.Empty) { throw new ArgumentException("The parameter cant be null"); };

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
            catch (Exception ex)
            {
                string errorMessage = $"An error occurred using SearchPermission.";
                this.loggingBroker.LogError(ex);
                throw new ArgumentException(errorMessage, ex);
            }
            
        }

        public void UploadPermission(PermissionDTO permission)
        {
            try
            {
                if (permission == null) { throw new ArgumentException("The parameter cant be null"); }
                var result = client.IndexDocument<PermissionDTO>(permission);
            }
            catch (Exception ex)
            {
                string errorMessage = $"An error occurred using UploadPermission.";
                this.loggingBroker.LogError(ex);
                throw new ArgumentException(errorMessage, ex);
            }
            
        }
    }
}
