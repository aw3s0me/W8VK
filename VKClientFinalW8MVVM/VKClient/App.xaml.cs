using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using VKClient.Helpers;
using VKClient.Resources;
using VKClient.Services;
using VKClient.ViewModels;
using VKClient.Views;
using VkApi;

using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;
using System.IO;

// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=234227

namespace VKClient
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>

        #region Fields
        public static Vkontakte Vkontakte;

        public static Settings Settings = new Settings();
        public const string AppId = "3501149";
        
        #endregion

        public App()
        {
          
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            this.UnhandledException += OnUnhandledException;
            Settings.Load();
            ApplicationService.Instance.Settings = Settings;
            if (Settings.FontColor == "Dark")
            {
                App.Current.RequestedTheme = ApplicationTheme.Dark;

            }
            else if (Settings.FontColor == "Light")
            {
                Current.RequestedTheme = ApplicationTheme.Light;
            }
            if (!String.IsNullOrEmpty(Settings.BackGroundColor))
            {
                
            }

        }
        

       

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used when the application is launched to open a specific file, to display
        /// search results, and so forth.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            
            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();
                rootFrame.Style = Resources["RootFrameStyle"] as Style;
                if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }
           /*     SplashScreen splash = args.SplashScreen;
                Splash eSplash = new Splash(splash);
                splash.Dismissed += TypedEventHandler<SplashScreen, object>(eSplash.onSplashScreenDismissed);   */
                // Place the frame in the current Window 
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                
                try
                {
                    var vault = new Windows.Security.Credentials.PasswordVault();
                    var creds = vault.FindAllByResource("VkApp").FirstOrDefault();
                    if (creds != null)
                    {
                        ApplicationService.Instance.Settings.UserId = creds.UserName;
                        ApplicationService.Instance.Settings.AccessToken =
                            vault.Retrieve("VkApp", ApplicationService.Instance.Settings.UserId).Password;
                        //ApplicationService.Instance.Settings.Save();
                        ViewModelLocator.AuthService.SetLoginInfoVk();
                        
                        
                        
                        //Messenger.Default.Send<GoHomeMessage>(new GoHomeMessage());
                        //((LoginViewModel)DataContext).GoProfileCommand.Execute(null);
                        //Messenger.Default.Send<GoHomeMessage>(new GoHomeMessage());
                        if (!rootFrame.Navigate(typeof(ProfileViewPage), args.Arguments))
                        {
                            throw new Exception("Failed to create initial page");
                        }
                        
                        //Frame.Navigate(typeof(ProfileViewPage));
                        /* Messenger.Default.Send<NavigateToPageMessage>(new NavigateToPageMessage
                        {
                            Page = "/ProfileViewPage"
                        }); */
                    }
                }
                catch (Exception ex)
                {
                    if (!rootFrame.Navigate(typeof(LoginView), args.Arguments))
                    {
                        throw new Exception("Failed to create initial page");
                    }
                }
            }
            // Ensure the current window is active
            Window.Current.Activate();
            
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBoxHelper.Show(AppResources.Strings["ErrorCommon"]);
        }
    }
}
