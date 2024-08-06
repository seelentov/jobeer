using Jobeer.Models.Base;

namespace Jobeer.Models
{
    public class SearchModel: BaseEntity
    {
        public string? Name { get; set; } = null!;
        public string Url {  get; set; }
        public SearchModelType Type { get; set; } = SearchModelType.HHru;
        public DateTime? LastParse { get; set; } = null!;

    }

    public enum SearchModelType
    {
        HHru
    }
}
