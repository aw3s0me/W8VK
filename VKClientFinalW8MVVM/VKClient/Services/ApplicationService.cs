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
