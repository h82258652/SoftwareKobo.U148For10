using SoftwareKobo.U148.DataSources;
using SoftwareKobo.U148.Models;
using SoftwareKobo.U148.Services;
using SoftwareKobo.UniversalToolkit.Mvvm;

namespace SoftwareKobo.U148.DataModels
{
    public class FeedCollection : IncrementalLoadingCollection<Feed, IncrementalItemSourceBase<Feed>>
    {
        public FeedCollection(IFeedService service, FeedCategory category) : base(new FeedSource(service, category))
        {
        }

        public FeedCollection(ISearchService service, string keyword) : base(new SearchedFeedSource(service, keyword))
        {
        }
    }
}