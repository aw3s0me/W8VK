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

        public static Settings Settings;

        public const string AppId = "3501149";

        
        #endregion

        public App()
        {
          
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            this.UnhandledException += OnUnhandledException;
        }
        

        public static Settings AppSettings;

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
                        ApplicationService.Instance.Settings.Save();
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

    public abstract class JsonSerializable
    {

        static JsonSerializable()
        {
        }

        async public virtual void Save(string path)
        {
            StorageFile file;
            try
            {
                file = await ApplicationData.Current.LocalFolder.GetFileAsync(path);
                //no exception means file exists
            }
            catch (Exception ex)
            {
                return;
            }
            //if (!Directory.Exists(Path.GetDirectoryName(path)))
              //  return;
            using (IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                using (var streamWriter = new StreamWriter(fileStream.AsStreamForWrite()))
                {
                    using (var jsonTextWriter = new JsonTextWriter((TextWriter)streamWriter))
                    {
                        jsonTextWriter.Formatting = (Formatting) 1;
                        //((JsonWriter)jsonTextWriter).set_Formatting((Formatting)1);
                        new JsonSerializer().Serialize(jsonTextWriter, this);
                    }
                }
            }
        }

        public async static Task<T> Load<T>(string path)
        {
            StorageFile file;
            try
            {
                file = await ApplicationData.Current.LocalFolder.GetFileAsync(path);
                //no exception means file exists
            }
            catch (Exception ex)
            {
                return default(T);
            }
            //if (!File.Exists(path))
              //  return default(T);
            using (var fileStream = await file.OpenAsync(FileAccessMode.Read))
            {
                T obj;
                using (var streamReader = new StreamReader(fileStream.AsStreamForRead()))
                {
                    using (var jsonTextReader = new JsonTextReader(streamReader))
                        obj = new JsonSerializer().Deserialize<T>(jsonTextReader);
                }
                return obj;
            }
        }
    }

    public class Settings : JsonSerializable
    {
        public double Width { get; set; }

        public double Height { get; set; }

        public double Top { get; set; }

        public double Left { get; set; }

        public bool IsMaximized { get; set; }

        public string AccessToken { get; set; }

        public DateTime ExpirationDate { get; set; }

        public string UserId { get; set; }

        public double Volume { get; set; }

        public bool Shuffle { get; set; }

        public bool Repeat { get; set; }

        public string Language { get; set; }

        public string AccentColor { get; set; }

        public bool MiniTopMost { get; set; }

        public bool CheckUpdates { get; set; }

        public DateTime LastCheck { get; set; }

        public bool NeedClean { get; set; }

     //   public Privacy Privacy { get; set; }

        public bool DownloadArtistArt { get; set; }

        public bool DownloadCovers { get; set; }

        public bool ScrobbleToLastFm { get; set; }

        public string LastFmSession { get; set; }

        public string Background { get; set; }

        public string BackgroundPattern { get; set; }

        public bool UpdateVkStatus { get; set; }

        public int ScrobbleTime { get; set; }

        public bool AddTrayIcon { get; set; }

        public bool AutoMinimode { get; set; }

        public bool ShowNotification { get; set; }

        public bool SendStats { get; set; }

        public string Guid { get; set; }

     //   public VkAuthDisplayType VkAuthDisplayType { get; set; }

        public int MaxCacheLifeTime { get; set; }

        public string AudioEngine { get; set; }

        public bool UseHTTPS { get; set; }

        public bool ShowSidebar { get; set; }

        public List<string> PinnedGroups { get; set; }

        public List<string> PinnedSongs { get; set; }

        public int TrackCount { get; set; }

        public int ArtistCount { get; set; }

        public DateTime StatsLastSend { get; set; }

        /*public Keys NextHotKey { get; set; }

        public ModifierKeys NextHotKeyModifier { get; set; }

        public Keys PrevHotKey { get; set; }

        public ModifierKeys PrevHotKeyModifier { get; set; }

        public Keys PlayPauseHotKey { get; set; }

        public ModifierKeys PlayPauseHotKeyModifier { get; set; }

        public Keys ShowHideHotKey { get; set; }

        public ModifierKeys ShowHideHotKeyModifier { get; set; }

        public Keys LikeDislikeHotKey { get; set; }

        public ModifierKeys LikeDislikeHotKeyModifier { get; set; } */

        public Settings()
        {
            //if (SystemInformation.PrimaryMonitorSize.Height <= 768)
            //{
              //  this.Width = 700.0;
                //this.Height = 500.0;
            //}
           // else
           // {
                this.Width = 1024.0;
                this.Height = 650.0;
           // }
            this.Left = -100.0;
            this.Top = -100.0;
            this.Volume = 50.0;
            this.IsMaximized = false;
            this.Language = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            this.MiniTopMost = false;
            this.CheckUpdates = true;
            this.NeedClean = false;
            this.AccentColor = "Deep Blue";
            //this.Privacy = Privacy.Allow;
            this.DownloadArtistArt = true;
            this.DownloadCovers = true;
            this.ScrobbleToLastFm = false;
            this.Background = "Ice Cream";
            this.BackgroundPattern = "Circles";
            this.UpdateVkStatus = false;
            this.ScrobbleTime = 15;
            this.AutoMinimode = true;
            this.ShowNotification = true;
            this.SendStats = true;
            //this.Guid = Guid.NewGuid().ToString();
            this.PinnedGroups = new List<string>();
            this.PinnedSongs = new List<string>();
            //this.VkAuthDisplayType = (VkAuthDisplayType)1;
            this.MaxCacheLifeTime = 5760;
            //this.AudioEngine = Constants.AUDIO_ENGINE_WMP;
            this.UseHTTPS = false;
            this.ShowSidebar = true;
        }

        
    }

}
