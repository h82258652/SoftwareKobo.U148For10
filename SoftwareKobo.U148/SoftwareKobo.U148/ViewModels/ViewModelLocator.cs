using SoftwareKobo.U148.Services;
using SoftwareKobo.UniversalToolkit.Mvvm;

namespace SoftwareKobo.U148.ViewModels
{
    public class ViewModelLocator : ViewModelLocatorBase
    {
        private static ViewModelLocator _instance;

        public ViewModelLocator()
        {
            #region 注册服务。

            this.Register<IFeedService, FeedService>();
            this.Register<ICommentService, CommentService>();
            this.Register<ISearchService, SearchService>();
            this.Register<IUserService, UserService>();

            #endregion 注册服务。

            #region 注册 ViewModel。

            this.Register<MainViewModel>();
            this.Register<DetailViewModel>();
            this.Register<CommentViewModel>();
            this.Register<SettingViewModel>();
            this.Register<SearchViewModel>();
            this.Register<LoginViewModel>();
            this.Register<AboutViewModel>();
            this.Register<FavouriteViewModel>();

            #endregion 注册 ViewModel。

            _instance = this;
        }

        public static ViewModelLocator Intance
        {
            get
            {
                return _instance;
            }
        }

        public CommentViewModel Comment
        {
            get
            {
                return this.GetInstance<CommentViewModel>();
            }
        }

        public DetailViewModel Detail
        {
            get
            {
                return this.GetInstance<DetailViewModel>();
            }
        }

        public LoginViewModel Login
        {
            get
            {
                return this.GetInstance<LoginViewModel>();
            }
        }

        public FavouriteViewModel Favourite
        {
            get
            {
                return this.GetInstance<FavouriteViewModel>();
            }
        }

        public AboutViewModel About
        {
            get
            {
                return this.GetInstance<AboutViewModel>();
            }
        }

        public MainViewModel Main
        {
            get
            {
                return this.GetInstance<MainViewModel>();
            }
        }

        public SearchViewModel Search
        {
            get
            {
                return this.GetInstance<SearchViewModel>();
            }
        }

        public SettingViewModel Setting
        {
            get
            {
                return this.GetInstance<SettingViewModel>();
            }
        }
    }
}