using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using GalaSoft.MvvmLight.Messaging;
using VKClient.Models;
using VKClient.Services;
using VKClient.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginView : Page
    {
        public LoginView()
        {
            this.InitializeComponent();
            base.DataContext=new LoginViewModel();
            ((LoginViewModel)DataContext).LoginCommand.Execute(null);
        /*    if (ViewModelLocator.AuthService.IsLoggedInVk(true))
            //if (String.IsNullOrEmpty(ApplicationService.Instance.Settings.AccessToken) || String.IsNullOrEmpty(ApplicationService.Instance.Settings.UserId))
                
            else
            {
                ViewModelLocator.AuthService.SetLoginInfoVk();
                Frame frame = (Frame)Window.Current.Content;
                frame.Navigate(typeof(Views.ProfileViewPage));
            } */
            //отслеживает все изменения и обрабатывает запросы для БД
            //также хорош для их обработки
            //DataContext = new LoginViewModel();
        }
        
        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {

        }
        
     /*   private void LoginTextBox_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            
            if (e.Key != VirtualKey.Enter)
            {
                return;
            }
            if (string.IsNullOrEmpty(this.LoginTextBox.Text))
            {
                this.PasswordTextBox.Focus((FocusState)2);
                return;
            }
            ((LoginViewModel)DataContext).LoginCommand.Execute(null);
        }

        private void PasswordTextBox_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key != VirtualKey.Enter)
            {
                return;
            }
            if (string.IsNullOrEmpty(this.PasswordTextBox.Password))
            {
                this.PasswordTextBox.Focus((FocusState)2);
                return;
            }
            ((LoginViewModel)base.DataContext).LoginCommand.Execute(null);
        } */
    }
}
