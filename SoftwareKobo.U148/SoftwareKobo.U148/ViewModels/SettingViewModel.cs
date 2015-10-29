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
                return AppSettings.ShowDetailInNewWindow;
            }
            set
            {
                AppSettings.ShowDetailInNewWindow = value;
                this.RaisePropertyChanged(nameof(ShowDetailInNewWindow));
            }
        }
    }
}