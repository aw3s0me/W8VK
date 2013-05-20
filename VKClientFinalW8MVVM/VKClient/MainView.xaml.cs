using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VkApi;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Authentication.Web;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace VKClient
{

    public sealed partial class MainView : Page
    {
        
        private Vkontakte vk;
        

        async private void Auth()
        {
            var VkUrl = "https://oauth.vk.com/authorize?client_id="+App.AppId+"&scope=wall&redirect_uri=http://oauth.vk.com/blank.html&display=touch&response_type=token";

            var requestUri = new Uri(VkUrl);
            var callbackUri = new Uri("http://oauth.vk.com/blank.html");

            WebAuthenticationResult webAuthResult = await WebAuthenticationBroker.AuthenticateAsync(
                                                    WebAuthenticationOptions.None,
                                                    requestUri,
                                                    callbackUri);

               if (webAuthResult.ResponseStatus == WebAuthenticationStatus.Success)
               {
                   var responseString = webAuthResult.ResponseData.ToString();
                   char[] separators = { '=', '&' };
                   string[] responseContent = responseString.Split(separators);
                   string AccessToken = responseContent[1];
                   int UserId = Int32.Parse(responseContent[5]);
               } 
        }

        public MainView()
        {
            this.InitializeComponent();
            //Auth();
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
    }
}
