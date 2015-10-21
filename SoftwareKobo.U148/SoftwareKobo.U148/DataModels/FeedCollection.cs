using SoftwareKobo.U148.DataSources;
using SoftwareKobo.U148.Models;
using SoftwareKobo.UniversalToolkit.Mvvm;

namespace SoftwareKobo.U148.DataModels
{
    public class FeedCollection : IncrementalLoadingCollection<Feed, FeedSource>
    {
        public FeedCollection(FeedCategory category) : base(new FeedSource(category))
        {
        }
    }
}