using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKClient.Services
{
    public class ApplicationService : IApplicationService<Settings>
    {
        private readonly static Lazy<ApplicationService> ServiceInstance;

        public static ApplicationService Instance
        {
            get
            {
                return ApplicationService.ServiceInstance.Value;
            }
        }

        public Settings Settings
        {
            get
            {
                return base.AppSettings;
            }
            set { 
                this.Settings.AccessToken = value.AccessToken;
                this.Settings.AutoNowPlaying = value.AutoNowPlaying;
                this.Settings.BackGroundColor = value.BackGroundColor;
                this.Settings.ColorScheme = value.ColorScheme;
                this.Settings.EnableScrobbling = value.EnableScrobbling;
                this.Settings.EnableStatusBroadcasting = value.EnableStatusBroadcasting;
                this.Settings.FontColor = value.FontColor;
                this.Settings.IsMuted = value.IsMuted;
                this.Settings.Repeat = value.Repeat;
                this.Settings.ShowToasts = value.ShowToasts;
                this.Settings.Shuffle = value.Shuffle;
                this.Settings.UserId = value.UserId;
                this.Settings.Volume = value.Volume;
            }
        }

        static ApplicationService()
        {
            ApplicationService.ServiceInstance = new Lazy<ApplicationService>(() => new ApplicationService());
        }

        public ApplicationService()
        {
        }

        protected override void LoadSettings()
        {
            base.AppSettings.Load();
            base.LoadSettings();
        }

        protected override void SaveSettings()
        {
            base.AppSettings.Save();
            base.SaveSettings();
        }
    }
}
