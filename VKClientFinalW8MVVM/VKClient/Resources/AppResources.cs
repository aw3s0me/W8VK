using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;

namespace VKClient.Resources
{
    public class AppResources
    {
        private readonly static ResourceLoader _loader = new ResourceLoader("Resources");

        private static AppResources _instance;

        public string this[string key]
        {
            get
            {
                if (String.IsNullOrEmpty(key))
                {
                    throw new ArgumentNullException("key");
                }
                return AppResources._loader.GetString(key);
            }
        }

        static AppResources()
        {
            //AppResources._loader = new ResourceLoader();
            _instance = new AppResources();
        }

        public static AppResources Strings
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AppResources();
                }
                return _instance;
               // return this;
            }
        }

    }
}
