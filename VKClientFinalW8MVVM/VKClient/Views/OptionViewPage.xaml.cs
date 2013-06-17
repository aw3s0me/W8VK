using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using VKClient.Converters;
using VKClient.Services;
using VKClient.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
    public sealed partial class OptionViewPage : VKClient.Common.LayoutAwarePage
    {
        
        public OptionViewPage()
        {
            this.InitializeComponent();
            WhiteFontRButton.IsChecked = true;
            base.DataContext = new OptionViewModel();
            if (!String.IsNullOrEmpty(ApplicationService.Instance.Settings.BackGroundColor))
            {
                try
                {
                    var brush =
                        HexToColorConverter.GetColorFromHex(ApplicationService.Instance.Settings.BackGroundColor);
                    PageGrid.Background = brush;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                }
            }
        }

        private void slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            Windows.UI.Color textColor = new Windows.UI.Color();
            textColor.A = 255;
            textColor.R = (byte)redSlider.Value;
            textColor.G = (byte)greenSlider.Value;
            textColor.B = (byte)blueSlider.Value;
            SolidColorBrush textBrush = new SolidColorBrush(textColor);
            rectangle.Fill = textBrush;
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if ( rectangle.Fill is SolidColorBrush ) 
            {
                SolidColorBrush brush = rectangle.Fill as SolidColorBrush;
                ((OptionViewModel)base.DataContext).ColorName = brush.Color.ToString();
                //bool FontColor = DarkFontRButton.Checked;
                ((OptionViewModel)base.DataContext).FontColorName = DarkFontRButton.IsChecked.GetValueOrDefault() ? "Light" : "Dark";
                Debug.WriteLine(((OptionViewModel)base.DataContext).ColorName);
                Debug.WriteLine(((OptionViewModel)base.DataContext).FontColorName);
                ((OptionViewModel)base.DataContext).ChangeAppProperties.Execute(null);
            }

        }
    }
}
