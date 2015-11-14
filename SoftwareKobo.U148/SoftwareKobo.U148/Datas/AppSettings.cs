using SoftwareKobo.U148.DataModels;
using SoftwareKobo.U148.Services;
using SoftwareKobo.UniversalToolkit.Helpers;
using SoftwareKobo.UniversalToolkit.Mvvm;
using SoftwareKobo.UniversalToolkit.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace SoftwareKobo.U148.Datas
{
    public class AppSettings : BindableBase
    {
        private static AppSettings _instance;

        public AppSettings()
        {
            _instance = this;
        }

        public static AppSettings Instance
        {
            get
            {
                return _instance ?? new AppSettings();
            }
        }

        public bool ShowDetailInNewWindow
        {
            get
            {
                if (HardwareButtonsHelper.IsUseable)
                {
                    return false;
                }

                if (ApplicationLocalSettings.Exists(nameof(ShowDetailInNewWindow)))
                {
                    return ApplicationLocalSettings.Read<bool>(nameof(ShowDetailInNewWindow));
                }
                else
                {
                    return false;
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
                    return SimulateDevice.Android;
                }
            }
            set
            {
                ApplicationLocalSettings.Write(nameof(SimulateDevice), value);
                this.RaisePropertyChanged();
            }
        }

        public ElementTheme ThemeMode
        {
            get
            {
                if (ApplicationLocalSettings.Exists(nameof(ThemeMode)))
                {
                    return ApplicationLocalSettings.Read<ElementTheme>(nameof(ThemeMode));
                }
                else
                {
                    return ElementTheme.Light;
                }
            }
            set
            {
                ApplicationLocalSettings.Write(nameof(ThemeMode), value);
                Frame rootFrame = Window.Current.Content as Frame;
                if (rootFrame != null)
                {
                    rootFrame.Background = new SolidColorBrush(value == ElementTheme.Dark ? Colors.Black : Colors.White);
                }
                this.RaisePropertyChanged();
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