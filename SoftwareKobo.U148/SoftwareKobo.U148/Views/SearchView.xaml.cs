using SoftwareKobo.U148.Datas;
using SoftwareKobo.U148.Models;
using SoftwareKobo.UniversalToolkit;
using SoftwareKobo.UniversalToolkit.Helpers;
using SoftwareKobo.UniversalToolkit.Mvvm;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace SoftwareKobo.U148.Views
{
    public sealed partial class SearchView : IView
    {
        public SearchView()
        {
            InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Required;
        }

        public void ReceiveFromViewModel(dynamic parameter)
        {
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            Messenger.Register(this);

            NavigationHelper.Register(Frame);

            if (e.NavigationMode != NavigationMode.Back)
            {
                string query = e.Parameter as string;
                if (string.IsNullOrEmpty(query) == false)
                {
                    txtSearch.Text = query;
                    this.SendToViewModel(query);
                }
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            Messenger.Unregister(this);

            NavigationHelper.Unregister(Frame);
        }

        private async void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Feed feed = e.ClickedItem as Feed;
            if (feed != null)
            {
                if (AppSettings.Instance.ShowDetailInNewWindow == false)
                {
                    Frame.Navigate(typeof(DetailView), feed);
                }
                else
                {
                    await Bootstrapper.Current.ShowNewWindowAsync(typeof(DetailView), feed);
                }
            }
        }
    }
}