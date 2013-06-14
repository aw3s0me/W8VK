using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VKClient.Models.Entities;
using VKClient.ViewModels;
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
    public sealed partial class AudioViewPage : VKClient.Common.LayoutAwarePage
    {
        private string _userId;

        public string UserId { get; set; }

        public AudioViewPage()
        {
            this.InitializeComponent();
            DataContext = new AudioViewModel();
            //UserId = Frame.GetNavigationState();
            //((AudioViewModel) DataContext).User.Uid = UserId;
            //((AudioViewModel)DataContext).AddAudiosCommand.Execute(null);
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
                if (DataContext != null )
                {
                    ((AudioViewModel)DataContext).User = new UserProfile {Uid = parameter};
                    ((AudioViewModel)DataContext).AddAudiosCommand.Execute(null);
                }
            }
            catch (Exception ex)
            {
                MessageDialog msg = new MessageDialog(ex.Message);
            }
            
        }

        private void VolumeSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
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

        private void MyMessagesClicked(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MessagesViewPage));
        }

        private void MyNewsClicked(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NewsViewPage));
        }

        private void MyOptionButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(OptionViewPage));
        }

        private void MySwitchButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModelLocator.AuthService.LogOutVk();
            Frame.Navigate(typeof(LoginView));
        }

        private void MyExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }
    }
}
