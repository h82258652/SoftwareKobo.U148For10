using SoftwareKobo.U148.DataModels;
using SoftwareKobo.U148.Datas;
using SoftwareKobo.U148.Models;
using SoftwareKobo.U148.Services;
using SoftwareKobo.UniversalToolkit.Mvvm;
using System;
using System.ComponentModel.DataAnnotations;

namespace SoftwareKobo.U148.ViewModels
{
    public class LoginViewModel : VerifiableViewModelBase
    {
        private string _email;

        [Required(AllowEmptyStrings = false, ErrorMessage = "请填写邮箱。")]
        [EmailAddress(ErrorMessage = "邮件格式错误。")]
        public string Email
        {
            get
            {
                return this._email;
            }
            set
            {
                this.Set(ref this._email, value);
            }
        }

        private string _password;

        [Required(AllowEmptyStrings = false, ErrorMessage = "请填写密码。")]
        [StringLength(int.MaxValue, MinimumLength = 6, ErrorMessage = "密码最少 6 位。")]
        public string Password
        {
            get
            {
                return this._password;
            }
            set
            {
                this.Set(ref this._password, value);
            }
        }

        private readonly IUserService _service;

        public LoginViewModel(IUserService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            this._service = service;
        }

        protected override void ReceiveFromView(dynamic parameter)
        {
            Tuple<string, string> package = parameter as Tuple<string, string>;
            if (package != null)
            {
                if (package.Item1 == "password")
                {
                    this.Password = package.Item2;
                }
                else if (package.Item1 == "login")
                {
                    this.LoginAsync();
                }
            }
        }

        public async void LoginAsync()
        {
            bool isSuccess = false;
            string message = string.Empty;

            try
            {
                ResultBase<UserInfo> result = await this._service.LoginAsync(this.Email, this.Password);
                if (result.Code == 0)
                {
                    AppSettings.Instance.UserInfo = result.Data;

                    isSuccess = true;
                    message = "login success";
                }
                else
                {
                    isSuccess = false;
                    message = result.Message;
                }
            }
            catch
            {
                isSuccess = false;
                message = "网络连接失败。";
            }

            Tuple<string, bool, string> package = Tuple.Create("loginFinish", isSuccess, message);
            this.SendToView(package);
        }
    }
}