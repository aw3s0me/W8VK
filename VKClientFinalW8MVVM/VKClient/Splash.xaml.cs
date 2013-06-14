using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace VKClient
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Splash : Page
    {
        public Splash()
        {
            this.InitializeComponent();
        }

        public Splash(Windows.ApplicationModel.Activation.SplashScreen splash)
        {
            // TODO: Complete member initialization
            this.InitializeComponent();
          /*  this.extendedSplashImage.SetValue(Canvas.LeftProperty, splash.ImageLocation.X);
            this.extendedSplashImage.SetValue(Canvas.TopProperty, splash.ImageLocation.Y);
            this.extendedSplashImage.Height = splash.ImageLocation.Height;
            this.extendedSplashImage.Width = splash.ImageLocation.Width; */
              // Position the extended splash screen's progress ring.
            this.ProgressRing.SetValue(Canvas.TopProperty, splash.ImageLocation.Y + splash.ImageLocation.Height + 32);
            this.ProgressRing.SetValue(Canvas.LeftProperty,
            splash.ImageLocation.X +(splash.ImageLocation.Width / 2) - 15);         
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void ProgressRing_Unloaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
