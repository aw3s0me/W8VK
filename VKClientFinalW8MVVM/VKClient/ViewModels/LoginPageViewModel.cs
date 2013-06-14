using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using VKClient.Helpers;
using VKClient.Models;
using VKClient.Resources;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace VKClient.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _login;
        private string _password;
        private bool _canLogin;
        private bool _isLoginFormVisible;
        public RelayCommand LoginCommand
        {
            get;
            private set;
        }
        public RelayCommand SignUpVkCommand
        {
            get;
            private set;
        }
        public RelayCommand LoginVkCommand
        {
            get;
            private set;
        }
        public RelayCommand GoProfileCommand
        {
            get;
            private set;
        }

        public RelayCommand CancelLoginVkCommand
        {
            get;
            private set;
        }
        public string Login
        {
            get
            {
                return this._login;
            }
            set
            {
                if (this._login == value)
                {
                    return;
                }
                this._login = value;
                this.UpdateCanLogin();
                this.RaisePropertyChanged("Login");
            }
        }
        public string Password
        {
            get
            {
                return this._password;
            }
            set
            {
                if (this._password == value)
                {
                    return;
                }
                this._password = value;
                this.UpdateCanLogin();
                this.RaisePropertyChanged("Password");
            }
        }
        public bool CanLogin
        {
            get
            {
                return this._canLogin;
            }
            set
            {
                if (this._canLogin == value)
                {
                    return;
                }
                this._canLogin = value;
                this.RaisePropertyChanged("CanLogin");
            }
        }
        public bool IsLoginFormVisible
        {
            get
            {
                return this._isLoginFormVisible;
            }
            set
            {
                if (this._isLoginFormVisible == value)
                {
                    return;
                }
                this._isLoginFormVisible = value;
                this.RaisePropertyChanged("IsLoginFormVisible");
            }
        }
        private void UpdateCanLogin()
        {
            if (string.IsNullOrEmpty(this.Login) || string.IsNullOrEmpty(this.Password))
            {
                this.CanLogin = false;
                return;
            }
            this.CanLogin = true;
        }
        public LoginViewModel()
        {
            this.InitializeCommands();
        }
        private void InitializeCommands()
        {
            this.LoginCommand = new RelayCommand(new Action(this.DoLogin));
            this.SignUpVkCommand = new RelayCommand(new Action(this.DoSignUp));
            GoProfileCommand = new RelayCommand(delegate
            {
                    Frame frame = (Frame)Window.Current.Content;
                    frame.Navigate(typeof(Views.ProfileViewPage));
            });
            this.LoginVkCommand = new RelayCommand(delegate
            {
                this.IsLoginFormVisible = true;
            });
            this.CancelLoginVkCommand = new RelayCommand(delegate
            {
                this.IsLoginFormVisible = false;
            });
        }
        private async void DoLogin()
        {
        //    if (!string.IsNullOrWhiteSpace(this.Login) && !string.IsNullOrWhiteSpace(this.Password))
        //    {
                base.IsWorking = true;
                try
                {
             //       await ViewModelLocator.AuthService.LoginVk(this.Login, this.Password);
                    await ViewModelLocator.AuthService.LoginVk("blabla", "blabla");
                    Frame frame = (Frame)Window.Current.Content;
                    frame.Navigate(typeof(Views.ProfileViewPage));

                   /* base.MessengerInstance.Send<NavigateToPageMessage>(new NavigateToPageMessage
                    {
                        //Page = "/ProfileViewPage"
                        Page = "/MainView"
                    }); */
                }
                catch (Exception ex)
                {
                    //LoggingService.Log(ex.ToString());
                    string messageBoxText = AppResources.Strings["ErrorLoginCommon"];
                    MessageBoxHelper.Show(messageBoxText+" "+ex.ToString());

                    base.IsWorking = false;
                }
        }
        private async void DoSignUp()
        {
            Uri uri = new Uri("http://vk.com");
            await Launcher.LaunchUriAsync(uri);
        }
    }
}
