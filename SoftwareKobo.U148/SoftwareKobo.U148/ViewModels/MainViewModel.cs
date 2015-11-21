using SoftwareKobo.U148.DataModels;
using SoftwareKobo.U148.Datas;
using SoftwareKobo.U148.Models;
using SoftwareKobo.U148.Services;
using SoftwareKobo.UniversalToolkit.Extensions;
using SoftwareKobo.UniversalToolkit.Mvvm;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace SoftwareKobo.U148.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly Dictionary<FeedCategory, FeedCollection> _categories = new Dictionary<FeedCategory, FeedCollection>();

        private DelegateCommand<Feed> _detailCommand;

        private DelegateCommand _logoutCommand;

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

        public bool IsLogined
        {
            get
            {
                return this.UserInfo != null;
            }
        }

        public DelegateCommand LogoutCommand
        {
            get
            {
                if (this._logoutCommand == null)
                {
                    this._logoutCommand = new DelegateCommand(async () =>
                    {
                        AppSettings.Instance.UserInfo = null;
                        this.RaisePropertyChanged(nameof(UserInfo));
                        this.RaisePropertyChanged(nameof(IsLogined));
                        await new MessageDialog("登出成功！").ShowAsync();
                    });
                }
                return this._logoutCommand;
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

        public StorageUserInfo UserInfo
        {
            get
            {
                return AppSettings.Instance.UserInfo;
            }
        }

        protected override void ReceiveFromView(dynamic parameter)
        {
            if (parameter == "navigated")
            {
                this.RaisePropertyChanged(nameof(UserInfo));
                this.RaisePropertyChanged(nameof(IsLogined));
            }
        }
    }
}