using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using VKClient.Models;
using VKClient.Resources;
using VKClient.Views;
using VkApi;
using VkApi.Core.Auth;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace VKClient.Services
{
    public class AuthService : IAuthService
    {
        private readonly Vkontakte _vkontakte;
        public AuthService(Vkontakte vkontakte)
        {
            this._vkontakte = vkontakte;
        }
        public async Task LoginVk(string login, string password)
        {
            //var accessToken = await this._vkontakte.Login(login, password, VkScopeSettings.CanAccessFriends | VkScopeSettings.CanAccessAudios | VkScopeSettings.CanAccessVideos | VkScopeSettings.CanAccessStatus | VkScopeSettings.CanAccessWall | VkScopeSettings.CanAccessGroups , null, null);
            var accessToken = await this._vkontakte.Login(login, password, VkScopeSettings.CanAccessAudios, null, null);

            if (accessToken == null || accessToken.Token == null)
            {
                throw new Exception(AppResources.Strings["ErrorLoginCommon"], new ArgumentException("AccessToken is empty"));
            }
            ApplicationService.Instance.Settings.AccessToken = accessToken.Token;
            ApplicationService.Instance.Settings.UserId = accessToken.UserId;
            ApplicationService.Instance.Settings.Save();
            var vault = new Windows.Security.Credentials.PasswordVault();
            vault.Add(new Windows.Security.Credentials.PasswordCredential(
                "VkApp", ApplicationService.Instance.Settings.UserId, ApplicationService.Instance.Settings.AccessToken));
            //var vault = new PasswordVault();
            Messenger.Default.Send<LoginMessage>(new LoginMessage
            {
                Type = LoginType.LogIn,
                IsSuccess = true
            });
            Messenger.Default.Send<GoHomeMessage>(new GoHomeMessage());
        }
        public void LoginVk()
        {
            this.SetLoginInfoVk();
        }
        public void LogOutVk()
        {
            var vault = new Windows.Security.Credentials.PasswordVault();
            vault.Remove(vault.Retrieve("VkApp", ApplicationService.Instance.Settings.UserId));
            this._vkontakte.AccessToken.Token = null;
            this._vkontakte.AccessToken.UserId = null;
            ApplicationService.Instance.Settings.AccessToken = null;
            ApplicationService.Instance.Settings.UserId = null;
            ApplicationService.Instance.Settings.Save();
            
            Messenger.Default.Send<LoginMessage>(new LoginMessage
            {
                Type = LoginType.LogOut,
                IsSuccess = true
            });
        }
        public bool IsLoggedInVk(bool goToLoginIfFalse = true)
        {
            if (this._vkontakte.AccessToken != null && !String.IsNullOrEmpty(this._vkontakte.AccessToken.Token) && !String.IsNullOrEmpty(this._vkontakte.AccessToken.UserId))
            {
                return true;
            }
            if (!String.IsNullOrEmpty(ApplicationService.Instance.Settings.AccessToken))
            {
                this.SetLoginInfoVk();
                return true;
            }
            if (goToLoginIfFalse)
            {
                Frame frame = Window.Current.Content as Frame;
                if (frame == null)
                {
                    frame = new Frame();
                    frame.Style = Application.Current.Resources["RootFrameStyle"] as Style;
                    Window.Current.Content=frame;
                }
                frame.Content = new LoginView();
            }
            return false;
        }
        
        public void SetLoginInfoVk()
        {
            this._vkontakte.AccessToken.Token = ApplicationService.Instance.Settings.AccessToken;
            this._vkontakte.AccessToken.UserId = ApplicationService.Instance.Settings.UserId;
            Messenger.Default.Send<LoginMessage>(new LoginMessage
            {
                Type = LoginType.LogIn,
                IsSuccess = true
            });
        }

        

    }
}
