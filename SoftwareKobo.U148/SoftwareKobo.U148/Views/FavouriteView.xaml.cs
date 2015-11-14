using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using SoftwareKobo.UniversalToolkit.Helpers;
using Windows.UI.Xaml.Navigation;
using SoftwareKobo.U148.Services;
using SoftwareKobo.U148.Datas;
using SoftwareKobo.U148.Models;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace SoftwareKobo.U148.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class FavouriteView : Page
    {
        public FavouriteView()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.Frame.RegisterNavigateBack();

            UserService service = new UserService();
            var s = await service.AddFavouriteAsync((UserInfo)AppSettings.Instance.UserInfo, new Feed() { Id = 134511 });
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            this.Frame.UnregisterNavigateBack();
        }
    }
}
