using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using VKClient.Models;
using VKClient.Resources;
using VKClient.ViewModels;
using Windows.Media;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace VKClient.Services
{
    public class AudioService : IAudioService
    {
        private MediaElement _mediaPlayer;
        private IList<Audio> _playlist;
        private Audio _currentAudio;
        private readonly DispatcherTimer _positionTimer;
        private CoreDispatcher _dispatcher;
        
        private T FindFirstElementInVisualTree<T>(DependencyObject parentElement) where T : DependencyObject
        {
            var count = VisualTreeHelper.GetChildrenCount(parentElement);
            if (count == 0)
                return null;

            for (int i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(parentElement, i);

                if (child != null && child is T)
                {
                    return (T)child;
                }
                else
                {
                    var result = FindFirstElementInVisualTree<T>(child);
                    if (result != null)
                        return result;
                }
            }
            return null;
        }

        public MediaElement MediaPlayer
        {
            get
            {
                if (this._mediaPlayer == null)
                {
                    try
                    {
                        DependencyObject child = VisualTreeHelper.GetChild(Window.Current.Content, 0);
                        if (child == null)
                        {
                            return null;
                        }
                        this._mediaPlayer = (MediaElement) VisualTreeHelper.GetChild(child, 0);
                        this._mediaPlayer.UpdateLayout();
                        MediaElement mediaPlayer = this._mediaPlayer;
                        mediaPlayer.MediaEnded += this.MediaPlayerOnMediaEnded;
                        //     WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(mediaPlayer.add_MediaEnded), new Action<EventRegistrationToken>(mediaPlayer.remove_MediaEnded), new RoutedEventHandler(this.MediaPlayerOnMediaEnded));
                        //MediaElement mediaPlayer2 = this._mediaPlayer;
                        mediaPlayer.MediaFailed += this.MediaPlayerOnMediaFailed;
                        //   WindowsRuntimeMarshal.AddEventHandler<ExceptionRoutedEventHandler>(new Func<ExceptionRoutedEventHandler, EventRegistrationToken>(mediaPlayer2.add_MediaFailed), new Action<EventRegistrationToken>(mediaPlayer2.remove_MediaFailed), new ExceptionRoutedEventHandler(this.MediaPlayerOnMediaFailed));
                        //MediaElement mediaPlayer3 = this._mediaPlayer;
                        mediaPlayer.MediaOpened += this.MediaPlayerOnMediaOpened;
                        //   WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(mediaPlayer3.add_MediaOpened), new Action<EventRegistrationToken>(mediaPlayer3.remove_MediaOpened), new RoutedEventHandler(this.MediaPlayerOnMediaOpened));
                        //MediaElement mediaPlayer4 = this._mediaPlayer;
                        mediaPlayer.CurrentStateChanged += this.MediaPlayerCurrentStateChanged;
                        //   WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(mediaPlayer4.add_CurrentStateChanged), new Action<EventRegistrationToken>(mediaPlayer4.remove_CurrentStateChanged), new RoutedEventHandler(this.MediaPlayerCurrentStateChanged));
                        this._mediaPlayer.Volume = (ApplicationService.Instance.Settings.Volume/100.0);
                        this._mediaPlayer.IsMuted = (ApplicationService.Instance.Settings.IsMuted);

                        return this._mediaPlayer;
                    }
                    catch (Exception ex)
                    {
                        var msgDlg = new MessageDialog(ex.Message);
                    }
                }
                return _mediaPlayer;
            }
        }
        public Audio CurrentAudio
        {
            get
            {
                return this._currentAudio;
            }
            set
            {
                Audio currentAudio = this._currentAudio;
                this._currentAudio = value;
                this.NotifyAudioChanged(currentAudio);
            }
        }
        public bool IsPlaying
        {
            get
            {
                if (this._mediaPlayer != null)
                {
                    return this._mediaPlayer.CurrentState == (MediaElementState)3;
                }
                return MediaControl.IsPlaying;
            }
        }
        public bool Shuffle
        {
            get
            {
                return ApplicationService.Instance != null && ApplicationService.Instance.Settings != null && ApplicationService.Instance.Settings.Shuffle;
            }
            set
            {
                ApplicationService.Instance.Settings.Shuffle = value;
                ApplicationService.Instance.Settings.Save();
            }
        }
        public bool Repeat
        {
            get
            {
                return ApplicationService.Instance.Settings.Repeat;
            }
            set
            {
                ApplicationService.Instance.Settings.Repeat = value;
                ApplicationService.Instance.Settings.Save();
            }
        }
        public TimeSpan CurrentAudioPosition
        {
            get
            {
                if (this.MediaPlayer == null)
                {
                    return TimeSpan.Zero;
                }
                return this.MediaPlayer.Position;
            }
            set
            {
                if (this.MediaPlayer == null)
                {
                    return;
                }
                if (this.MediaPlayer.Position.TotalSeconds == value.TotalSeconds)
                {
                    return;
                }
                this.MediaPlayer.Position = (value);
            }
        }
        public TimeSpan CurrentAudioDuration
        {
            get
            {
                if (this.MediaPlayer != null && this.MediaPlayer.NaturalDuration.HasTimeSpan)
                {
                    return this.MediaPlayer.NaturalDuration.TimeSpan;
                }
                return TimeSpan.Zero;
            }
        }
        public IList<Audio> Playlist
        {
            get
            {
                return this._playlist;
            }
            set
            {
                this._playlist = value;
                Messenger.Default.Send<CurrentPlaylistChangedMessage>(new CurrentPlaylistChangedMessage());
            }
        }
        public double Volume
        {
            get
            {
                try
                {
                    if (this.MediaPlayer != null)
                    {
                        return this.MediaPlayer.Volume*100.0;
                    }
                    return ApplicationService.Instance.Settings.Volume;
                }
                catch (Exception ex)
                {
                    var msgDlg = new MessageDialog(ex.Message);
                    return ApplicationService.Instance.Settings.Volume;
                }
                
            }
            set
            {
                try
                {
                    if (this.MediaPlayer != null)
                    {
                        this.MediaPlayer.Volume = (value / 100.0);
                    }
                    ApplicationService.Instance.Settings.Volume = value;
                }
                catch (Exception ex)
                {
                    var msgDlg = new MessageDialog(ex.Message);
                }
                
            }
        }
        public bool IsMuted
        {
            get
            {
                if (this.MediaPlayer != null)
                {
                    return this.MediaPlayer.IsMuted;
                }
                return ApplicationService.Instance.Settings.IsMuted;
            }
            set
            {
                if (this.MediaPlayer != null)
                {
                    this.MediaPlayer.IsMuted = (value);
                }
                ApplicationService.Instance.Settings.IsMuted = value;
                ApplicationService.Instance.Settings.Save();
            }
        }
        public AudioService()
		{
			if (Window.Current == null || Window.Current.Content == null)
			{
				throw new Exception("Can't get media player.");
			}
			this._dispatcher = Window.Current.Dispatcher;
            _dispatcher.RunAsync(0, new DispatchedHandler(Stop));
            //MediaControl.StopPressed += Stop;
            //MediaControl.PlayPressed + = Play();
            MediaControl.StopPressed += async delegate(object sender, object o)
                {
                    await this._dispatcher.RunAsync(0, new DispatchedHandler(this.Stop));
                };
		/*	WindowsRuntimeMarshal.AddEventHandler<EventHandler<object>>(new Func<EventHandler<object>, EventRegistrationToken>(MediaControl.add_StopPressed), new Action<EventRegistrationToken>(MediaControl.remove_StopPressed), delegate(object sender, object o)
			{
				await this._dispatcher.RunAsync(0, new DispatchedHandler(this.Stop));
			}); */
            MediaControl.PlayPressed += async delegate(object sender, object o)
			{
				await this._dispatcher.RunAsync(0, new DispatchedHandler(this.Play));
			};
		/*	WindowsRuntimeMarshal.AddEventHandler<EventHandler<object>>(new Func<EventHandler<object>, EventRegistrationToken>(MediaControl.add_PlayPressed), new Action<EventRegistrationToken>(MediaControl.remove_PlayPressed), delegate(object sender, object o)
			{
				await this._dispatcher.RunAsync(0, new DispatchedHandler(this.Play));
			}); */
            MediaControl.PausePressed += async delegate(object sender, object o)
                {
                    await this._dispatcher.RunAsync(0, new DispatchedHandler(this.Pause));
                };
		/*	WindowsRuntimeMarshal.AddEventHandler<EventHandler<object>>(new Func<EventHandler<object>, EventRegistrationToken>(MediaControl.add_PausePressed), new Action<EventRegistrationToken>(MediaControl.remove_PausePressed), delegate(object sender, object o)
			{
				await this._dispatcher.RunAsync(0, new DispatchedHandler(this.Pause));
			}); */
            MediaControl.PlayPauseTogglePressed += async delegate(object sender, object o)
                {
                    await this._dispatcher.RunAsync(0, delegate
                        {
                            if (!MediaControl.IsPlaying)
                            {
                                this.Play();
                                return;
                            }
                            this.Pause();
                        });
                };
		/*	WindowsRuntimeMarshal.AddEventHandler<EventHandler<object>>(new Func<EventHandler<object>, EventRegistrationToken>(MediaControl.add_PlayPauseTogglePressed), new Action<EventRegistrationToken>(MediaControl.remove_PlayPauseTogglePressed), delegate(object sender, object o)
			{
				await this._dispatcher.RunAsync(0, delegate
				{
					if (!MediaControl.get_IsPlaying())
					{
						this.Play();
						return;
					}
					this.Pause();
				});
			});
			WindowsRuntimeMarshal.AddEventHandler<EventHandler<object>>(new Func<EventHandler<object>, EventRegistrationToken>(MediaControl.add_NextTrackPressed), new Action<EventRegistrationToken>(MediaControl.remove_NextTrackPressed), delegate(object sender, object o)
			{
				await this._dispatcher.RunAsync(0, new DispatchedHandler(this.Next));
			});

			WindowsRuntimeMarshal.AddEventHandler<EventHandler<object>>(new Func<EventHandler<object>, EventRegistrationToken>(MediaControl.add_PreviousTrackPressed), new Action<EventRegistrationToken>(MediaControl.remove_PreviousTrackPressed), delegate(object sender, object o)
			{
				await this._dispatcher.RunAsync(0, new DispatchedHandler(this.Prev));
			}); */
            MediaControl.NextTrackPressed += async delegate(object sender, object o)
                {
                    await this._dispatcher.RunAsync(0, new DispatchedHandler(this.Prev));
                };
            MediaControl.PreviousTrackPressed += async delegate(object sender, object o)
                {
                    await this._dispatcher.RunAsync(0, new DispatchedHandler(this.Prev));
                };
			this._positionTimer = new DispatcherTimer();
			this._positionTimer.Interval = (TimeSpan.FromMilliseconds(500.0));
			DispatcherTimer positionTimer = this._positionTimer;
		//	WindowsRuntimeMarshal.AddEventHandler<EventHandler<object>>(new Func<EventHandler<object>, EventRegistrationToken>(positionTimer.add_Tick), new Action<EventRegistrationToken>(positionTimer.remove_Tick), new EventHandler<object>(this.PositionTimerTick));
            positionTimer.Tick += PositionTimerTick;
			if (this.IsPlaying)
			{
				this._positionTimer.Start();
			}
			Messenger.Default.Register<LoginMessage>(this, new Action<LoginMessage>(this.OnLoginMessage));
		}
        public async void Play(Audio audio)
        {
            if (this.CurrentAudio != null)
            {
                this.CurrentAudio.IsPlaying = false;
            }
            audio.IsPlaying = true;
            if (audio.Uri == null)
            {
                Audio audio2 = await ViewModelLocator.DataService.GetAudioByArtistAndTitle(audio.Artist, audio.Title);
                if (audio2 != null)
                {
                    audio.Id = audio2.Id;
                    audio.Artist = audio2.Artist;
                    audio.Title = audio2.Title;
                    audio.Uri = audio2.Uri;
                    audio.OwnerId = audio2.OwnerId;
                    audio.PlaylistId = audio2.PlaylistId;
                    audio.LyricsId = audio2.LyricsId;
                }
            }
            this.MediaPlayer.Source = (audio.Uri);
            this.MediaPlayer.Play();
            MediaControl.IsPlaying = (true);
            try
            {
                MediaControl.ArtistName = (audio.Artist);
                MediaControl.TrackName = (audio.Title);
                this.CurrentAudio = audio;
            }
            catch (Exception ex)
            {
                var msgDlg = new MessageDialog(ex.Message);
            }
            this.CurrentAudio = audio;
        }
        public void Next()
        {
            if (this._playlist != null)
            {
                if (this.Repeat)
                {
                    this.Play(this._currentAudio);
                    return;
                }
                int num = -1;
                if (this._currentAudio != null)
                {
                    Audio audio = this._playlist.FirstOrDefault((Audio a) => a.Id == this._currentAudio.Id);
                    if (audio != null)
                    {
                        num = this._playlist.IndexOf(audio);
                    }
                }
                if (!this.Shuffle)
                {
                    num++;
                }
                else
                {
                    num = new Random(Environment.TickCount).Next(0, this._playlist.Count);
                }
                if (num < this._playlist.Count)
                {
                    this.Play(this._playlist[num]);
                }
            }
        }
        public void Prev()
        {
            if (this.CurrentAudioPosition.TotalSeconds >= 3.0)
            {
                this.Play(this.CurrentAudio);
                return;
            }
            if (this._playlist != null)
            {
                int num = -1;
                if (this._currentAudio != null)
                {
                    Audio audio = this._playlist.FirstOrDefault((Audio a) => a.Id == this._currentAudio.Id);
                    if (audio != null)
                    {
                        num = this._playlist.IndexOf(audio);
                    }
                }
               if (!this.Shuffle)
                {
                    num--;
                }
                else
                {
                    num = new Random(Environment.TickCount).Next(0, this._playlist.Count);
                }
                if (num >= 0)
                {
                    this.Play(this._playlist[num]);
                }
            }
        }
        public void Play()
        {
            if (this.MediaPlayer.Source == null && this.CurrentAudio != null)
            {
                this.MediaPlayer.Source = (this.CurrentAudio.Uri);
                this.CurrentAudio.IsPlaying = true;
            }
            this.MediaPlayer.Play();
            MediaControl.IsPlaying = (true);
        }
        public void Pause()
        {
            this.MediaPlayer.Pause();
            MediaControl.IsPlaying = (false);
        }
        public void Stop()
        {
            this.MediaPlayer.Stop();
            MediaControl.IsPlaying = (false);
        }
        private void OnLoginMessage(LoginMessage message)
        {
            if (message.Type == LoginType.LogOut)
            {
                this._positionTimer.Stop();
                return;
            }
            this._positionTimer.Start();
        }
        private void MediaPlayerCurrentStateChanged(object sender, RoutedEventArgs e)
        {
            PlayerPlayState newState = PlayerPlayState.Stopped;
            switch (this.MediaPlayer.CurrentState)
            {
                case (MediaElementState)1:
                    newState = PlayerPlayState.Opening;
                    break;
                case (MediaElementState)2:
                    newState = PlayerPlayState.Buffering;
                    break;
                case (MediaElementState)3:
                    newState = PlayerPlayState.Playing;
                    this._positionTimer.Start();
                    break;
                case (MediaElementState)4:
                    newState = PlayerPlayState.Paused;
                    break;
                case (MediaElementState)5:
                    newState = PlayerPlayState.Stopped;
                    break;
            }
            Messenger.Default.Send<PlayStateChangedMessage>(new PlayStateChangedMessage
            {
                NewState = newState
            });
        }
        private void MediaPlayerOnMediaOpened(object sender, RoutedEventArgs e)
        {
            MediaControl.IsPlaying = (true);
        }
        private async void MediaPlayerOnMediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            MessageDialog messageDialog = new MessageDialog(string.Concat(new string[]
			{
				AppResources.Strings["ErrorPlayback"],
				Environment.NewLine,
				AppResources.Strings["ErrorCode"],
				" ",
				e.ErrorMessage
			}), AppResources.Strings["ErrorPlaybackTitle"]);
           
            messageDialog.Commands.Add(new UICommand(AppResources.Strings["Cancel"]));
            messageDialog.CancelCommandIndex = (1u);
            messageDialog.DefaultCommandIndex = (0u);
            await messageDialog.ShowAsync();
        }
        private void MediaPlayerOnMediaEnded(object sender, RoutedEventArgs e)
        {
            this.Next();
        }
        private void NotifyAudioChanged(Audio oldAudio = null)
        {
            Messenger.Default.Send<AudioChangedMessage>(new AudioChangedMessage
            {
                OldAudio = oldAudio,
                NewAudio = this.CurrentAudio
            });
        }
        private void PositionTimerTick(object sender, object e)
        {
            try
            {
                Messenger.Default.Send<PlayerPositionChangedMessage>(new PlayerPositionChangedMessage
                {
                    NewPosition = this.MediaPlayer.Position
                });
            }
            catch (Exception)
            {
            }
        }
    }
}
