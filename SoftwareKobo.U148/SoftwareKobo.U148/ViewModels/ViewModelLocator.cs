using SoftwareKobo.U148.Services;
using SoftwareKobo.UniversalToolkit.Mvvm;

namespace SoftwareKobo.U148.ViewModels
{
    public class ViewModelLocator : ViewModelLocatorBase
    {
        public ViewModelLocator()
        {
            #region 注册服务。

            this.Register<IFeedService, FeedService>();
            this.Register<ICommentService, CommentService>();

            #endregion 注册服务。

            #region 注册 ViewModel。

            this.Register<MainViewModel>();
            this.Register<DetailViewModel>();
            this.Register<CommentViewModel>();

            #endregion 注册 ViewModel。
        }

        public MainViewModel Main
        {
            get
            {
                return this.GetInstance<MainViewModel>();
            }
        }

        public DetailViewModel Detail
        {
            get
            {
                return this.GetInstance<DetailViewModel>();
            }
        }

        public CommentViewModel Comment
        {
            get
            {
                return this.GetInstance<CommentViewModel>();
            }
        }
    }
}