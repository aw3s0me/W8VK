using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Callisto.Controls;
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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace VKClient.Views
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class VideoViewPage : VKClient.Common.LayoutAwarePage
    {
        private string currentUri = String.Empty;

        public VideoViewPage()
        {
            this.InitializeComponent();
            DataContext = new VideoPageVIewModel();
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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                var parameter = e.Parameter as string;
                if (DataContext != null)
                {
                    ((VideoPageVIewModel)DataContext).User = new UserProfile { Uid = parameter };
                    ((VideoPageVIewModel)DataContext).AddVideosCommand.Execute(null);
                }
            }
            catch (Exception ex)
            {
                MessageDialog msg = new MessageDialog(ex.Message);
            }
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


        private void GridViewForVideos_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                var item = ((VideoItemToGridView) e.ClickedItem);
                string param = item.PlayerUri.OriginalString;
                Frame.Navigate(typeof (VideoPlayerPage), param );
            }
            catch (Exception ex)
            {
                var msgDlg = new MessageDialog(ex.Message);
            }
            
            /*   Popup popup = new Popup();

            Grid panel = new Grid();

            panel.Height = 250;
            panel.Width = 250;
            
            panel.Transitions = new TransitionCollection();
            panel.Transitions.Add(new PopupThemeTransition());


            WebView web = new WebView();
            //web.HorizontalAlignment = HorizontalAlignment.Center;
            //web.VerticalAlignment = VerticalAlignment.Center;
            web.Navigate(item.PlayerUri);
            popup.Child = panel;
            panel.Children.Add(web);
            popup.HorizontalAlignment = HorizontalAlignment.Center;
            popup.VerticalAlignment = VerticalAlignment.Center;


            popup.HorizontalOffset = (Window.Current.Bounds.Width / 2 - panel.Width / 2);
            popup.VerticalOffset = (Window.Current.Bounds.Height / 2 - panel.Height / 2);

            popup.IsOpen = true;
            UpdateLayout(); 
            //((VideosViewModel)DataContext)
            //((VideosViewModel)DataContext).LoadPhotosFromAlbumCommand.Execute(null); */
        }
    }
}
