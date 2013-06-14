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
        public String AccessToken
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

        public Double Volume
        {
            get;
            set;
        }

        public Settings()
        {
        }

        private T Get<T>(String key, T defaultValue)
        {
            return AppSettingsProvider.Get<T>(key, defaultValue);
        }

        internal void Load()
        {
            try
            {
                this.AccessToken = this.Get<String>("AccessToken", String.Empty);
                this.UserId = this.Get<String>("UserId", String.Empty);
                this.Shuffle = this.Get<Boolean>("Shuffle", false);
                this.Repeat = this.Get<Boolean>("Repeat", false);
                this.Volume = this.Get<Double>("Volume", 50);
                this.IsMuted = this.Get<Boolean>("IsMuted", false);
                this.ColorScheme = this.Get<String>("ColorScheme", "Deep Blue");
                this.ShowToasts = this.Get<Boolean>("ShowToasts", true);
                this.AutoNowPlaying = this.Get<Boolean>("AutoNowPlaying", true);
                this.EnableStatusBroadcasting = this.Get<Boolean>("EnableStatusBroadcasting", false);
                this.EnableScrobbling = this.Get<Boolean>("EnableScrobbling", false);
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
                this.Set<String>("AccessToken", this.AccessToken);
                this.Set<String>("UserId", this.UserId);
                this.Set<Boolean>("Shuffle", this.Shuffle);
                this.Set<Boolean>("Repeat", this.Repeat);
                this.Set<Double>("Volume", this.Volume);
                this.Set<Boolean>("IsMuted", this.IsMuted);
                this.Set<String>("ColorScheme", this.ColorScheme);
                this.Set<Boolean>("ShowToasts", this.ShowToasts);
                this.Set<Boolean>("AutoNowPlaying", this.AutoNowPlaying);
                this.Set<Boolean>("EnableStatusBroadcasting", this.EnableStatusBroadcasting);
                this.Set<Boolean>("EnableScrobbling", this.EnableScrobbling);
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
