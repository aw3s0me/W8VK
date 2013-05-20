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

        public Boolean AutoNowPlaying
        {
            get;
            set;
        }

        public String ColorScheme
        {
            get;
            set;
        }

        public Boolean EnableScrobbling
        {
            get;
            set;
        }

        public Boolean EnableStatusBroadcasting
        {
            get;
            set;
        }

        public Boolean IsMuted
        {
            get;
            set;
        }

        public String LastFmSession
        {
            get;
            set;
        }

        public String LastFmUsername
        {
            get;
            set;
        }

        public Boolean Repeat
        {
            get;
            set;
        }

        public Boolean ShowToasts
        {
            get;
            set;
        }

        public Boolean Shuffle
        {
            get;
            set;
        }

        public String UserId
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
                this.LastFmUsername = this.Get<String>("LastFmUsername", String.Empty);
                this.LastFmSession = this.Get<String>("LastFmSession", String.Empty);
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
                this.Set<String>("LastFmUsername", this.LastFmUsername);
                this.Set<String>("LastFmSession", this.LastFmSession);
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
