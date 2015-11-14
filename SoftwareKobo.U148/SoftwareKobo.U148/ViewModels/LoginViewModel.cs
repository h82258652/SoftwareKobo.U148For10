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
        private readonly IUserService _service;

        private string _email;

        private bool _isLogining;

        private DelegateCommand _loginCommand;

        private string _password;

        public LoginViewModel(IUserService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            this._service = service;
        }

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

        public bool IsLogining
        {
            get
            {
                return this._isLogining;
            }
            set
            {
                this.Set(ref this._isLogining, value);
            }
        }

        public DelegateCommand LoginCommand
        {
            get
            {
                if (this._loginCommand == null)
                {
                    this._loginCommand = new DelegateCommand(async () =>
                    {
                        this.IsLogining = true;

                        bool isSuccess = false;
                        string message = string.Empty;

                        DataResultBase<UserInfo> result = null;
                        try
                        {
                            result = await this._service.LoginAsync(this.Email, this.Password);
                        }
                        catch
                        {
                        }

                        if (result !=null)
                        {
                            if (result.Code == 0)
                            {
                                AppSettings.Instance.UserInfo = result.Data;

                                isSuccess = true;
                                message = "登录成功";
                            }
                            else
                            {
                                isSuccess = false;
                                message = result.Message;
                            }
                        }
                        else
                        {
                            isSuccess = false;
                            message = "网络连接失败";
                        }
                        
                        Tuple<string, bool, string> package = Tuple.Create("loginFinish", isSuccess, message);
                        this.SendToView(package);

                        this.IsLogining = false;
                    });
                }
                return this._loginCommand;
            }
        }

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
    }
}