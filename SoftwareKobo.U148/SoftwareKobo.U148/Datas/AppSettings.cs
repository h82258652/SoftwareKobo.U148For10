using SoftwareKobo.U148.DataModels;
using SoftwareKobo.U148.Services;
using SoftwareKobo.UniversalToolkit.Helpers;
using SoftwareKobo.UniversalToolkit.Mvvm;
using SoftwareKobo.UniversalToolkit.Storage;
using System;
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

        public static AppSettings Instance => _instance ?? new AppSettings();

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
                return false;
            }
            set
            {
                ApplicationLocalSettings.Write(nameof(ShowDetailInNewWindow), value);
                RaisePropertyChanged();
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
                return SimulateDevice.Android;
            }
            set
            {
                ApplicationLocalSettings.Write(nameof(SimulateDevice), value);
                RaisePropertyChanged();
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
                return ElementTheme.Light;
            }
            set
            {
                ApplicationLocalSettings.Write(nameof(ThemeMode), value);
                Frame rootFrame = Window.Current.Content as Frame;
                if (rootFrame != null)
                {
                    rootFrame.Background = new SolidColorBrush(value == ElementTheme.Dark ? Colors.Black : Colors.White);
                }
                RaisePropertyChanged();
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
                return null;
            }
            set
            {
                ApplicationLocalSettings.Write(nameof(UserInfo), value);
                RaisePropertyChanged();
            }
        }

        public DateTime LastClickAdTime
        {
            get
            {
                if (ApplicationLocalSettings.Exists(nameof(LastClickAdTime)))
                {
                    return ApplicationLocalSettings.Read<DateTime>(nameof(LastClickAdTime));
                }
                return DateTime.MinValue;
            }
            set
            {
                ApplicationLocalSettings.Write(nameof(LastClickAdTime), value);
                RaisePropertyChanged();
            }
        }
    }
}