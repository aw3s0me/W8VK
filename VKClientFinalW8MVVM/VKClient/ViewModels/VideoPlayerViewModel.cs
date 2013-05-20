using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using VKClient.Models;
using Windows.System;

namespace VKClient.ViewModels
{
    public class VideoPlayerViewModel : BaseViewModel
    {
        private Video _video;
        private Uri _videoSource;
        private Uri _playerSource;
        private bool _isPlaying = true;
        private TimeSpan _duration;
        private TimeSpan _position;
        public RelayCommand PlayPauseCommand
        {
            get;
            private set;
        }
        public Uri VideoSource
        {
            get
            {
                return this._videoSource;
            }
            set
            {
                if (this._videoSource == value)
                {
                    return;
                }
                this._videoSource = value;
                this.RaisePropertyChanged("VideoSource");
            }
        }
        public Uri PlayerSource
        {
            get
            {
                return this._playerSource;
            }
            set
            {
                if (this._playerSource == value)
                {
                    return;
                }
                this._playerSource = value;
                this.RaisePropertyChanged("PlayerSource");
            }
        }
        public bool IsPlaying
        {
            get
            {
                return this._isPlaying;
            }
            set
            {
                if (this._isPlaying == value)
                {
                    return;
                }
                this._isPlaying = value;
                this.RaisePropertyChanged("IsPlaying");
            }
        }
        public TimeSpan Duration
        {
            get
            {
                return this._duration;
            }
            set
            {
                if (this._duration == value)
                {
                    return;
                }
                this._duration = value;
                this.RaisePropertyChanged("Duration");
            }
        }
        public TimeSpan Position
        {
            get
            {
                return this._position;
            }
            set
            {
                if (this._position == value)
                {
                    return;
                }
                this._position = value;
                this.RaisePropertyChanged("Position");
                this.RaisePropertyChanged("CurrentPositionSeconds");
            }
        }
        public double CurrentPositionSeconds
        {
            get
            {
                if (base.IsInDesignMode)
                {
                    return 0.0;
                }
                return this.Position.TotalSeconds;
            }
            set
            {
                this.Position = TimeSpan.FromSeconds(value);
                this.RaisePropertyChanged("SetCurrentPositionSeconds");
            }
        }
        public Video Video
        {
            get
            {
                return this._video;
            }
            set
            {
                if (this._video == value)
                {
                    return;
                }
                this._video = value;
                this.RaisePropertyChanged("Video");
            }
        }
        public VideoPlayerViewModel(Video video)
        {
            this.InitializeCommands();
            this._video = video;
            this.GetVideo();
        }
        private void InitializeCommands()
        {
            this.PlayPauseCommand = new RelayCommand(delegate
            {
                this.IsPlaying = !this.IsPlaying;
            });
        }
        private async void GetVideo()
        {
            if (this._video != null)
            {
                base.IsWorking = true;
                try
                {
                    IList<Video> list = await ViewModelLocator.DataService.GetVideo(new List<string>
					{
						this._video.OwnerId + "_" + this._video.Id
					});
                    if (list != null && list.Count > 0)
                    {
                        this.Video = list.First<Video>();
                        this.Duration = this._video.Duration;
                        if (this._video.Files.Any((KeyValuePair<string, string> x) => x.Key == "external"))
                        {
                            if (!string.IsNullOrEmpty(this._video.Player))
                            {
                                this.OpenExternalVideo(new Uri(this._video.Player));
                            }
                            else
                            {
                                this.OpenExternalVideo(new Uri(this._video.Files.First<KeyValuePair<string, string>>().Value));
                            }
                            base.MessengerInstance.Send<GoBackMessage>(new GoBackMessage());
                        }
                        else
                        {
                            string text = string.Empty;
                            if (this._video.Files.Any((KeyValuePair<string, string> x) => x.Key.Contains("mp4_720")))
                            {
                                text = this._video.Files.First((KeyValuePair<string, string> x) => x.Key.Contains("mp4_720")).Value;
                            }
                            else
                            {
                                if (this._video.Files.Any((KeyValuePair<string, string> x) => x.Key.Contains("mp4_480")))
                                {
                                    text = this._video.Files.First((KeyValuePair<string, string> x) => x.Key.Contains("mp4_480")).Value;
                                }
                                else
                                {
                                    if (this._video.Files.Any((KeyValuePair<string, string> x) => x.Key.Contains("mp4_360")))
                                    {
                                        text = this._video.Files.First((KeyValuePair<string, string> x) => x.Key.Contains("mp4_360")).Value;
                                    }
                                    else
                                    {
                                        if (this._video.Files.Any((KeyValuePair<string, string> x) => x.Key.Contains("mp4_240")))
                                        {
                                            text = this._video.Files.First((KeyValuePair<string, string> x) => x.Key.Contains("mp4_240")).Value;
                                        }
                                    }
                                }
                            }
                            if (string.IsNullOrEmpty(text))
                            {
                                this.OpenExternalVideo(new Uri("http://vk.com/video" + this._video.OwnerId + "_" + this._video.Id));
                                base.MessengerInstance.Send<GoBackMessage>(new GoBackMessage());
                            }
                            else
                            {
                                this.VideoSource = new Uri(text);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                }
                base.IsWorking = false;
            }
        }
        private async void OpenExternalVideo(Uri uri)
        {
            await Launcher.LaunchUriAsync(uri);
        }
    }
}
