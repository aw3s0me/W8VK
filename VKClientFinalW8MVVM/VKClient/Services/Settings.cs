using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace VKClient.Services
{
    public class Settings
    {
        public string AccessToken
        {
            get;
            set;
        }

        public bool AutoNowPlaying
        {
            get;
            set;
        }

        public string ColorScheme
        {
            get;
            set;
        }

        public bool EnableScrobbling
        {
            get;
            set;
        }

        public bool EnableStatusBroadcasting
        {
            get;
            set;
        }

        public bool IsMuted
        {
            get;
            set;
        }

        public bool Repeat
        {
            get;
            set;
        }

        public bool ShowToasts
        {
            get;
            set;
        }

        public bool Shuffle
        {
            get;
            set;
        }

        public string UserId
        {
            get;
            set;
        }

        public string BackGroundColor
        {
            get;
            set;
        }

        public string FontColor { get; set; }

        public double Volume
        {
            get;
            set;
        }

        public Settings()
        {
        }

        private T Get<T>(string key, T defaultValue)
        {
            return AppSettingsProvider.Get<T>(key, defaultValue);
        }

        internal void Load()
        {
            try
            {
                this.AccessToken = this.Get<string>("AccessToken", String.Empty);
                this.UserId = this.Get<string>("UserId", String.Empty);
                this.Shuffle = this.Get<bool>("Shuffle", false);
                this.Repeat = this.Get<bool>("Repeat", false);
                this.Volume = this.Get<double>("Volume", 50);
                this.IsMuted = this.Get<bool>("IsMuted", false);
                this.ColorScheme = this.Get<string>("ColorScheme", "Deep Blue");
                this.BackGroundColor = this.Get<string>("BackGroundColor", "Black");
                this.FontColor = this.Get<string>("FontColor", "Dark");
                this.ShowToasts = this.Get<bool>("ShowToasts", true);
                this.AutoNowPlaying = this.Get<bool>("AutoNowPlaying", true);
                this.EnableStatusBroadcasting = this.Get<bool>("EnableStatusBroadcasting", false);
            }
            catch (Exception ex)
            {
                var msgDlg = new MessageDialog(ex.Message);

            }
            
        }

        internal void Save()
        {
            try
            {
                this.Set<string>("AccessToken", this.AccessToken);
                this.Set<string>("UserId", this.UserId);
                this.Set<bool>("Shuffle", this.Shuffle);
                this.Set<bool>("Repeat", this.Repeat);
                this.Set<double>("Volume", this.Volume);
                this.Set<bool>("IsMuted", this.IsMuted);
                this.Set<string>("ColorScheme", this.ColorScheme);
                this.Set<string>("BackGroundColor", this.BackGroundColor);
                this.Set<string>("FontColor", this.FontColor);
                this.Set<bool>("ShowToasts", this.ShowToasts);
                this.Set<bool>("AutoNowPlaying", this.AutoNowPlaying);
                this.Set<bool>("EnableStatusBroadcasting", this.EnableStatusBroadcasting);
            }
            catch (Exception ex)
            {
                var msgDlg = new MessageDialog(ex.Message);

            }
        }

        private void Set<T>(String key, T value)
        {
            try
            {
                AppSettingsProvider.Set<T>(key, value);
            }
            catch (Exception ex)
            {
                var msgDlg = new MessageDialog(ex.Message);

            }
        }
    }
}
