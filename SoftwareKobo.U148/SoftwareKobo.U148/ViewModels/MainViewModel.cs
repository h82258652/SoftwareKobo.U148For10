using SoftwareKobo.U148.DataModels;
using SoftwareKobo.U148.Models;
using SoftwareKobo.U148.Services;
using SoftwareKobo.UniversalToolkit.Extensions;
using SoftwareKobo.UniversalToolkit.Mvvm;
using System.Collections.Generic;

namespace SoftwareKobo.U148.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private Dictionary<FeedCategory, FeedCollection> _categories = new Dictionary<FeedCategory, FeedCollection>();

        private DelegateCommand<Feed> _detailCommand;

        private DelegateCommand<FeedCollection> _refreshCommand;

        public MainViewModel(IFeedService feedService)
        {
            foreach (FeedCategory category in EnumExtensions.GetValues<FeedCategory>())
            {
                this._categories[category] = new FeedCollection(feedService, category);
            }
        }

        public Dictionary<FeedCategory, FeedCollection> Categories
        {
            get
            {
                return this._categories;
            }
        }

        public DelegateCommand<Feed> DetailCommand
        {
            get
            {
                if (this._detailCommand == null)
                {
                    this._detailCommand = new DelegateCommand<Feed>(feed =>
                    {
                        if (feed != null)
                        {
                            this.SendToView(feed);
                        }
                    });
                }
                return this._detailCommand;
            }
        }

        public DelegateCommand<FeedCollection> RefreshCommand
        {
            get
            {
                if (this._refreshCommand == null)
                {
                    this._refreshCommand = new DelegateCommand<FeedCollection>(collection =>
                    {
                        if (collection != null)
                        {
                            collection.Refresh();
                        }
                    });
                }
                return this._refreshCommand;
            }
        }
    }
}