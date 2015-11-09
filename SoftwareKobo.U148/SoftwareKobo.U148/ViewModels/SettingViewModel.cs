using SoftwareKobo.U148.Datas;
using SoftwareKobo.UniversalToolkit.Mvvm;

namespace SoftwareKobo.U148.ViewModels
{
    public class SettingViewModel : ViewModelBase
    {
        public bool ShowDetailInNewWindow
        {
            get
            {
                return AppSettings.Instance.ShowDetailInNewWindow;
            }
            set
            {
                AppSettings.Instance.ShowDetailInNewWindow = value;
                this.RaisePropertyChanged(nameof(ShowDetailInNewWindow));
            }
        }
    }
}