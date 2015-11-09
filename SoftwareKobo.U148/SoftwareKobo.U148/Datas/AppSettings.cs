using SoftwareKobo.U148.DataModels;
using SoftwareKobo.U148.Services;
using SoftwareKobo.UniversalToolkit.Helpers;
using SoftwareKobo.UniversalToolkit.Mvvm;
using SoftwareKobo.UniversalToolkit.Storage;

namespace SoftwareKobo.U148.Datas
{
    public class AppSettings : BindableBase
    {
        private static AppSettings _instance;

        private AppSettings()
        {
        }

        public static AppSettings Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AppSettings();
                }
                return _instance;
            }
        }

        public bool ShowDetailInNewWindow
        {
            get
            {
                if (ApplicationLocalSettings.Exists(nameof(ShowDetailInNewWindow)))
                {
                    return ApplicationLocalSettings.Read<bool>(nameof(ShowDetailInNewWindow));
                }
                else
                {
                    return DeviceFamilyHelper.IsDesktop ? true : false;
                }
            }
            set
            {
                ApplicationLocalSettings.Write(nameof(ShowDetailInNewWindow), value);
                this.RaisePropertyChanged();
            }
        }

        public SimulateDevice SimulateDevice
        {
            get
            {
                if (ApplicationLocalSettings.Exists(nameof(SimulateDevice)))
                {
                    return ApplicationLocalSettings.Read<SimulateDevice>(nameof(SimulateDevice));
                }
                else
                {
                    return Services.SimulateDevice.Android;
                }
            }
            set
            {
                ApplicationLocalSettings.Write(nameof(SimulateDevice), value);
            }
        }

        public StorageUserInfo UserInfo
        {
            get
            {
                if (ApplicationLocalSettings.Exists(nameof(UserInfo)))
                {
                    return ApplicationLocalSettings.Read<StorageUserInfo>(nameof(UserInfo));
                }
                else
                {
                    return null;
                }
            }
            set
            {
                ApplicationLocalSettings.Write(nameof(UserInfo), value);
                this.RaisePropertyChanged();
            }
        }
    }
}