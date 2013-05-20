using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VKClient.Models.Entities;
using VKClient.ViewModels;
using VKClient.VkControls;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace VKClient.Views
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class PhotoAlbumsViewPage : VKClient.Common.LayoutAwarePage
    {
        public PhotoAlbumsViewPage()
        {
            this.InitializeComponent();
            DataContext = new PhotoAlbumsViewModel();  
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {

        }

        private void MyProfileClicked(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ProfileViewPage), ViewModelLocator.Vkontakte.AccessToken.UserId);
        }

        private void MyFriendsClicked(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(FriendsViewPage), ViewModelLocator.Vkontakte.AccessToken.UserId);
        }

        private void MyPhotoClicked(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NewPhotoAlbumsViewPage), ViewModelLocator.Vkontakte.AccessToken.UserId);
        }

        private void MyVideoesClicked(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(VideoViewPage), ViewModelLocator.Vkontakte.AccessToken.UserId);
        }

        private void MyAudioClicked(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AudioViewPage), ViewModelLocator.Vkontakte.AccessToken.UserId);
        }

        private void MyGroupsClicked(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(GroupsViewPage), ViewModelLocator.Vkontakte.AccessToken.UserId);
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                var fields = new string[2];
                var parameter = e.Parameter as string;
                if (parameter != null)
                {
                    fields = parameter.Split('|');
                }
                if (fields[1] == null||fields[1]==String.Empty)
                {
                    if (DataContext != null)
                    {
                        ((PhotoAlbumsViewModel)DataContext).User = new UserProfile { Uid = parameter };
                        ((PhotoAlbumsViewModel)DataContext).AddPhotoAlbumsCommand.Execute(null);
                    }
                }
                else
                {
                    if (DataContext != null)
                    {
                        ((PhotoAlbumsViewModel)DataContext).User = new UserProfile { Uid = fields[0] };
                        ((PhotoAlbumsViewModel)DataContext).AddPhotoAlbumsCommand.Execute(null);
                        IEnumerable<PhotoItemToListView> photos=
                            ((PhotoAlbumsViewModel) DataContext).PhotoAlbums.Where(s => s.Aid==fields[1]);
                        ((PhotoAlbumsViewModel) DataContext).SelectedPhoto = photos.First();
                        ((PhotoAlbumsViewModel) DataContext).LoadPhotosFromAlbumCommand.Execute(null);

                    }
                }
                
                
            }
            catch (Exception ex)
            {
                MessageDialog msg = new MessageDialog(ex.Message);
            }
        }
    }
}
