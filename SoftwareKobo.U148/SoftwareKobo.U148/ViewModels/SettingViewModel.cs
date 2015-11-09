using SoftwareKobo.U148.Datas;
using SoftwareKobo.U148.Services;
using SoftwareKobo.UniversalToolkit.Extensions;
using SoftwareKobo.UniversalToolkit.Mvvm;
using System.Collections.Generic;
using System.Linq;

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

        public SimulateDevice SimulateDevice
        {
            get
            {
                return AppSettings.Instance.SimulateDevice;
            }
            set
            {
                AppSettings.Instance.SimulateDevice = value;
                this.RaisePropertyChanged(nameof(SimulateDevice));
            }
        }

        public IReadOnlyList<SimulateDevice> SimulateDevices
        {
            get
            {
                return EnumExtensions.GetValues<SimulateDevice>().ToList();
            }
        }
    }
}