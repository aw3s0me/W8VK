using System.Windows;
using System.Windows.Forms;
using Meridian;
using MessageBox = System.Windows.MessageBox;

namespace MeridianZ.Views
{
    /// <summary>
    /// Interaction logic for AuthView.xaml
    /// </summary>
    public partial class AuthView : Window
    {
        public AuthView()
        {
            InitializeComponent();

            Loaded += AuthPageLoaded;
        }

        void AuthPageLoaded(object sender, RoutedEventArgs e)
        {
            string authUrl = App.Vkontakte.GetAuthUrl(new[] { "audio" });
            var form = new Form();
            form.Width = 640;
            form.Height = 480;
            form.StartPosition = FormStartPosition.CenterScreen;

            var browser = new System.Windows.Forms.WebBrowser();
            browser.Dock = DockStyle.Fill;
            browser.Navigated += BrowserNavigated;

            browser.Navigate(authUrl);
            form.Controls.Add(browser);
            form.FormBorderStyle = FormBorderStyle.None;
            form.Opacity = 0;
            form.Text = Properties.Resources.AuthWindowTitle;
            form.ShowDialog();
        }

        void BrowserNavigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            //logger.Debug(e.Uri);
            VkApi.VkAuthResult authResult;
            if (VkApi.VkAuthResult.TryParse(e.Url, out authResult))
            {
                if (!authResult.IsSuccess)
                {
                    MessageBox.Show("Authorization failed!");
                    //NavigationService.Navigate(new Uri("/Views/AuthPage.xaml", UriKind.Relative));
                    return;
                }

                App.Vkontakte.AuthResult = authResult;
                //NavigationService.Navigate(new Uri("/Views/MainPage.xaml", UriKind.Relative));
            }
            else
            {
                var form = ((Form)(((System.Windows.Forms.WebBrowser)sender).Parent));
                form.FormBorderStyle = FormBorderStyle.Sizable;
                form.Opacity = 1;
            }
        }
    }
}
