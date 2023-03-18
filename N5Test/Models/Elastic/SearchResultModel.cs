using N5Test.Data.Models.Permissions;

namespace N5Test.Models.Elastic
{
    public class SearchResultModel
    {
        public string SearchText { get; set; }

        public List<Permission> Results { get; set; }
    }
}
