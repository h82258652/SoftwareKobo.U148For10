using SoftwareKobo.U148.Models;
using SoftwareKobo.UniversalToolkit.Storage;

namespace SoftwareKobo.U148.Datas
{
    public static class AppSettings
    {
        public static bool ShowDetailInNewWindow
        {
            get
            {
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
            }
        }

        public static UserInfo UserInfo
        {
            get;
            set;
        }
    }
}