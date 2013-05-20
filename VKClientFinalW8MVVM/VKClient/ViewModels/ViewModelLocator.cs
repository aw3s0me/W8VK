using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VKClient.Services;
using VkApi;



namespace VKClient.ViewModels
{
    class ViewModelLocator
    {
        public const string AppId = "3501149";
        public const string Protected_Key = "6Cu0XE498xkM68wljycg";
        public static Vkontakte Vkontakte = new Vkontakte(AppId, Protected_Key);
        private static readonly Lazy<IStorageService> _isolatedStorageService;
        private static readonly Lazy<DataService> _dataService = new Lazy<DataService>(() => new DataService(ViewModelLocator.Vkontakte));
        private static readonly Lazy<AuthService> _authService = new Lazy<AuthService>(() => new AuthService(ViewModelLocator.Vkontakte));
        private static readonly Lazy<IAudioService> _audioService = new Lazy<IAudioService>(() => new AudioService());
        private static MainViewModel _main;
        private static AudioViewModel _music = new AudioViewModel();
        public static string TempUserId = String.Empty;

        public AudioViewModel Music
        {
            get
            {
                if (ViewModelLocator._music == null)
                {
                    ViewModelLocator._music = new AudioViewModel();
                }
                return ViewModelLocator._music;
            }
        }

        public static AudioViewModel StaticMusic
        {
            get
            {
                return ViewModelLocator._music;
            }
        }

        public MainViewModel Main
        {
            get
            {
                return ViewModelLocator._main;
            }
        }

        public static MainViewModel StaticMain
        {
            get
            {
                return ViewModelLocator._main;
            }
        }

        public static AuthService AuthService
        {
            get
            {
                return ViewModelLocator._authService.Value;
            }
        }
        public static IStorageService IsolatedStorageService
        {
            get
            {
                return ViewModelLocator._isolatedStorageService.Value;
            }
        }
        public static DataService DataService
        {
            get
            {
                return ViewModelLocator._dataService.Value;
            }
        }

        public static IAudioService AudioService
        {
            get
            {
                return ViewModelLocator._audioService.Value;
            }
        }

        public ViewModelLocator()
        {
            ViewModelLocator._main = new MainViewModel();
        }
    }
}
