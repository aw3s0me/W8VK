using System;
using VKCore;
using VKModel.Entities;
using VKServiceLayer;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace VKViewModels
{
    public class AuthorizationViewModel : BaseViewModel
    {
        const string AppId = "3592067";

        public Uri GetUri()
        {
            var uri = new Uri(AuthorizationHelper.GetAuthorizationUrl(AppId));
            return uri;
        }

        public void ParseUri(Uri uri)
        {
            var result = AuthorizationHelper.ParseNavigatedUrl(uri.ToString());
            if (result.Status == AuthorizationStatus.Success)
            {
                GetService<IVkApi>().Initialize(result.Context);
                //(Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/MainMenu.xaml", UriKind.Relative));
                var curFrame = Window.Current.Content as Frame;
                if (curFrame!=null)
                    if (curFrame.CanGoBack)
                        curFrame.GoBack();
            }
        }

        public void RestoreContext()
        {
            GetService<IVkApi>().RestoreContext();
        }

        public bool IsAuthorized
        {
            get { return GetService<IVkApi>().IsAuthorized(); }
        }
    }
}