using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace VKViewModels.Services
{
    public class SimpleNavigationService : ISimpleNavigationService
    {
        private Frame ApplicationFrame
        {
            get { return (Window.Current.Content as Frame); }
        }

        public void NavigatToAuthorizationPage()
        {
            NavigateTo("/AuthorizationPage.xaml");
        }

        public void NavigateToProfilePage(string uid)
        {
            NavigateTo("/ProfilePage.xaml?uid=" + uid);
        }

        public void NavigateToHomePage()
        {
            NavigateTo("/HomePage.xaml");
        }

        public void NavigateToPhotoAlbumPage(string uid)
        {
            NavigateTo("/PhotoAlbumPage.xaml?uid={0}",uid);
        }

        public void NavigateToPhotosViewPage(string uid, string albumUid)
        {
            NavigateTo(String.Format("/PhotosViewPage.xaml?uid={0}&aid={1}",uid,albumUid));
        }

        public void NavigateToMessageConversationPage(string uid)
        {
            NavigateTo(String.Format("/MessageConversationPage.xaml?uid={0}",uid));
        }

        public void NavigateToMessagesPage()
        {
            NavigateTo("/MessagesPage.xaml");
        }

        public void NavigateToFriendsPage()
        {
            NavigateTo("/FriendsPage.xaml");
        }


        private void NavigateTo(string urlTemplate, params string[] values)
        {
            var url = String.Format(urlTemplate, values);
            ApplicationFrame.Navigate(Type.GetType(url));
        }
    }
}
