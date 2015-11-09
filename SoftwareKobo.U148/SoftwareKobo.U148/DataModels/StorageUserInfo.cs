using SoftwareKobo.U148.Datas;
using SoftwareKobo.U148.Models;
using SoftwareKobo.UniversalToolkit.Mvvm;
using System.Runtime.CompilerServices;

namespace SoftwareKobo.U148.DataModels
{
    public class StorageUserInfo : BindableBase
    {
        private string _icon;

        public string Icon
        {
            get
            {
                return this._icon;
            }
            set
            {
                this.Set(ref this._icon, value);
            }
        }

        private string _nickName;

        public string NickName
        {
            get
            {
                return this._nickName;
            }
            set
            {
                this.Set(ref this._nickName, value);
            }
        }

        private Gender _gender;

        public Gender Gender
        {
            get
            {
                return this._gender;
            }
            set
            {
                this.Set(ref this._gender, value);
            }
        }

        private string _token;

        public string Token
        {
            get
            {
                return this._token;
            }
            set
            {
                this.Set(ref this._token, value);
            }
        }

        protected override void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.RaisePropertyChanged(propertyName);

            AppSettings.Instance.UserInfo = this;
        }

        public static implicit operator StorageUserInfo(UserInfo userInfo)
        {
            return new StorageUserInfo()
            {
                Icon = userInfo.Icon,
                NickName = userInfo.NickName,
                Gender = userInfo.Gender,
                Token = userInfo.Token
            };
        }

        public static explicit operator UserInfo(StorageUserInfo userInfo)
        {
            return new UserInfo()
            {
                Icon = userInfo.Icon,
                NickName = userInfo.NickName,
                Gender = userInfo.Gender,
                Token = userInfo.Token
            };
        }
    }
}