using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace VKClient.Views
{
    
    

    public sealed partial class VideoPlayerPage : Page
    {
        private bool _isResized = false;

        public VideoPlayerPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (!_isResized)
            {
                WebGrid.Height = 644;
                WebGrid.Width = 1346;
                WebGrid.Margin = new Thickness(10);
                WebBrowser.Height = 508;
                WebBrowser.Width = 1326;
                WebBrowser.Margin = new Thickness(10, 10, 10, 0);
                BackButton.Margin = new Thickness(10, 10, 0, 0);
                MakeFull.Margin = new Thickness(1265, 27, 0, 0);
                _isResized = true;
                UpdateLayout();
            }
            else
            {
                WebGrid.Height = 432;
                WebGrid.Width = 737;
                WebGrid.Margin = new Thickness(326,112,303,120);
                WebBrowser.Height = 334;
                WebBrowser.Width = 717;
                WebBrowser.Margin = new Thickness(10, 10, 10, 0);
                BackButton.Margin = new Thickness(346,134,0,0);
                MakeFull.Margin = new Thickness(965, 134, 0, 0);
                _isResized = false;
                UpdateLayout();
            }
            
        }

        

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                var parameter = e.Parameter as string;
                WebBrowser.Source = new Uri(parameter);

            }
            catch (Exception ex)
            {
                MessageDialog msg = new MessageDialog(ex.Message);
            }
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
                Frame.GoBack();
        }
    }
}
