using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using VKClient.Helpers;
using VKClient.Models;
using VKClient.Models.Entities;
using VKClient.Resources;
using VKClient.Services;
using VKClient.Views;
using VKClient.VkControls;
using VkApi.Error;
using Windows.Media;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace VKClient.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private UserProfile _userProfile;
        private Audio _selectedAudio;
        private PhotoItemToPhotoGridView _selectedPhoto;
        private bool _statusUpdated;
        private bool _nowPlayingUpdated;
        public Audio SelectedAudio
        {
            get
            {
                return this._selectedAudio;
            }
            set
            {
                if (this._selectedAudio == value)
                {
                    return;
                }
                this._selectedAudio = value;
                this.IsAppbarOpen = (value != null);
                this.RaisePropertyChanged("SelectedAudio");
            }
        }

        public double CurrentAudioPositionSeconds
        {
            get
            {
                if (base.IsInDesignMode)
                {
                    return 0.0;
                }
                return ViewModelLocator.AudioService.CurrentAudioPosition.TotalSeconds;
            }
            set
            {
                ViewModelLocator.AudioService.CurrentAudioPosition = TimeSpan.FromSeconds(value);
            }
        }
        public TimeSpan CurrentAudioPosition
        {
            get
            {
                if (base.IsInDesignMode)
                {
                    return TimeSpan.Zero;
                }
                return ViewModelLocator.AudioService.CurrentAudioPosition;
            }
        }
        public TimeSpan CurrentAudioDuration
        {
            get
            {
                return ViewModelLocator.AudioService.CurrentAudioDuration;
            }
        }
        private bool _isAppbarOpen;
        public bool IsAppbarOpen
        {
            get
            {
                return this._isAppbarOpen;
            }
            set
            {
                if (this._isAppbarOpen == value)
                {
                    return;
                }
                this._isAppbarOpen = value;
                this.RaisePropertyChanged("IsAppbarOpen");
            }
        }
        public UserProfile UserProfile
        {
            get
            {
                return this._userProfile;
            }
            set
            {
                if (this._userProfile == value)
                {
                    return;
                }
                this._userProfile = value;
                this.RaisePropertyChanged("UserProfile");
            }
        }
        public Audio CurrentAudio
        {
            get
            {
                if (!base.IsInDesignMode)
                {
                    return ViewModelLocator.AudioService.CurrentAudio;
                }
                Audio audio = new Audio
                {
                    Artist = "Artist",
                    Title = "Title"
                };
                audio.Info.Album = "Album";
                return audio;
            }
        }
        private async void LoadUserInfo()
        {
            try
            {
                this.UserProfile = await ViewModelLocator.DataService.GetUserProfile();
            }
            catch (Exception)
            {
                MessageBoxHelper.Show(AppResources.Strings["MainErrorLoadProfile"]);
                base.MessengerInstance.Send<LoginMessage>(new LoginMessage
                {
                    IsSuccess = true,
                    Type = LoginType.LogOut
                });
            }
        }

        private async void LoadUserInfoByUserId(string userId)
        {
            try
            {
                this.UserProfile = await ViewModelLocator.DataService.GetUserProfileByUserId(userId);
            }
            catch (Exception)
            {
                MessageBoxHelper.Show(AppResources.Strings["MainErrorLoadProfile"]);
                base.MessengerInstance.Send<LoginMessage>(new LoginMessage
                {
                    IsSuccess = true,
                    Type = LoginType.LogOut
                });
            }
        }

        private void OnLogin(LoginMessage message)
        {
            if (message.Type == LoginType.LogIn && message.IsSuccess)
            {
                this.LoadUserInfo();
                return;
            }
            /*base.MessengerInstance.Send<NavigateToPageMessage>(new NavigateToPageMessage
            {
                Page = "/LoginView"
            }); */
            if (ViewModelLocator.AudioService.IsPlaying)
            {
                ViewModelLocator.AudioService.Stop();
            }
            ViewModelLocator.AudioService.CurrentAudio = null; 
        }

        public RelayCommand<string> GoToPageCommand
        {
            get;
            private set;
        }
        public RelayCommand<Audio> PlayAudioCommand
        {
            get;
            private set;
        }
        public RelayCommand<Audio> PlayTopAudioCommand
        {
            get;
            private set;
        }
        public RelayCommand NextAudioCommand
        {
            get;
            private set;
        }
        public RelayCommand PrevAudioCommand
        {
            get;
            private set;
        }
        public RelayCommand PlayPauseCommand
        {
            get;
            private set;
        }
        public RelayCommand<Audio> AddAudioToPlaylistCommand
        {
            get;
            private set;
        }
        public RelayCommand SignOutCommand
        {
            get;
            private set;
        }
        public RelayCommand AboutCommand
        {
            get;
            private set;
        }


        public MainViewModel()
        {
            this.InitializeMessageInterception();
            this.InitializeCommands();
            
        }

        private void InitializeCommands()
        {
            this.GoToPageCommand = new RelayCommand<string>(
                page => base.MessengerInstance.Send<NavigateToPageMessage>(new NavigateToPageMessage
                    {
                        Page = page
                    }));
            this.PlayAudioCommand = new RelayCommand<Audio>(audio => ViewModelLocator.AudioService.Play(audio));
            
            
            this.PrevAudioCommand = new RelayCommand(() => ViewModelLocator.AudioService.Prev());
            this.NextAudioCommand = new RelayCommand(() => ViewModelLocator.AudioService.Next());
            this.PlayPauseCommand = new RelayCommand(() =>
                {
                    if (ViewModelLocator.AudioService.IsPlaying)
                    {
                        ViewModelLocator.AudioService.Pause();
                        return;
                    }
                    ViewModelLocator.AudioService.Play();
                });
            
            this.SignOutCommand = new RelayCommand(() => ViewModelLocator.AuthService.LogOutVk());
            
            
            
        }
        private void InitializeMessageInterception()
        {
            base.MessengerInstance.Register<NavigateToPageMessage>(this, new Action<NavigateToPageMessage>(this.OnNavigateToPage));
            base.MessengerInstance.Register<GoBackMessage>(this, new Action<GoBackMessage>(this.OnGoBack));
            base.MessengerInstance.Register<LoginMessage>(this, new Action<LoginMessage>(this.OnLogin));
            base.MessengerInstance.Register<AudioChangedMessage>(this, new Action<AudioChangedMessage>(this.OnAudioChanged));
            base.MessengerInstance.Register<PlayerPositionChangedMessage>(this, new Action<PlayerPositionChangedMessage>(this.OnPlayerPositionChanged));
           // base.MessengerInstance.Register<GoHomeMessage>(this, new Action<GoHomeMessage>(this.OnGoHome));
        }

        private void OnNavigateToPage(NavigateToPageMessage message)
        {
            string[] parameters = message.Page.Split(new Char[] { '|' });
            message.Page = parameters[0];
            Type type;
            if (message.Page.StartsWith("/MainView"))
            {
                string TotalPath = "VKClientFinalW8MVVM." + message.Page.Substring(1);
                type = Type.GetType(TotalPath, false);
            }
            else
            {
                string TotalPath = "VKClient.Views." + message.Page.Substring(1);
                //string TotalPath = "VKClientFinalW8MVVM.Views." + message.Page.Substring(1);
                type = Type.GetType(TotalPath, false);
            }
            if (type == null)
            {
                if (Debugger.IsAttached)
                {
                    Debugger.Break();
                }
                return;
            }
            if (typeof(Page).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo()))
            {
                Frame frame = (Frame)Window.Current.Content;
                frame.Navigate(type, parameters);
                return;
            }
            //DialogService.DisplayPopup(type, message.Parameters, null);
        }
     /*   private void OnGoHome(GoHomeMessage message)
        {
            Frame frame = new Frame();
            frame.Style = (Application.Current.Resources["RootFrameStyle"] as Style);
            Window.Current.Content = (frame);
            frame.Content = (new ProfileViewPage());
        } */
        private void OnGoBack(GoBackMessage message)
        {
            ((Frame)Window.Current.Content).GoBack();
        }

        private void OnAudioChanged(AudioChangedMessage message)
        {
            this.RaisePropertyChanged("CurrentAudio");
            //this._artRequested = false;
            this._statusUpdated = false;
            this._nowPlayingUpdated = false;
       /*     if (message.NewAudio != null)
            {
                MetaData info = ViewModelLocator.AudioService.CurrentAudio.Info;
                if (info != null && info.AlbumArtUri != null && info.AlbumArtUri.OriginalString.StartsWith("ms-appdata://"))
                {
                    MediaControl.AlbumArt = (info.AlbumArtUri);
                    return;
                }
                MediaControl.AlbumArt = (null);
            } */
        }

        private async void OnPlayerPositionChanged(PlayerPositionChangedMessage message)
        {
            this.RaisePropertyChanged("CurrentAudioPosition");
            this.RaisePropertyChanged("CurrentAudioPositionSeconds");
            this.RaisePropertyChanged("CurrentAudioDuration");
           
            
        }

    }
}
