using Jobeer.Models.Base;

namespace Jobeer.Models
{
    public class SearchModel: BaseEntity
    {
        public string Name { get; set; } = "";
        public string Url {  get; set; }
        public SearchModelType Type { get; set; } = SearchModelType.HHru;
        public DateTime LastParse { get; set; } = DateTime.MinValue;

    }

    public enum SearchModelType
    {
        HHru
    }
}
