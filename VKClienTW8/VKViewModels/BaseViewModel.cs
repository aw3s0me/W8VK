using System;
using System.Collections.Generic;
using System.ComponentModel;
using VKCore;
using VKDataLayer;
using VKViewModels.Resources;
using VKModel.Entities;
using VKModel.Interfaces;
using VKViewModels.Services;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace VKViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        static AppResource appResource = new AppResource();

        public AppResource Resource
        {
            get { return appResource; }
        }

    /*    public bool IsOpenedFromPinned
        {
            get
            {
                return GetStateOrUrlParamNullable("pinToStart")=="1";
            }
        } */

        public void OnPropertyChange(string propertyName)
        {
            if(PropertyChanged!=null)
            {
                PropertyChanged(this,new PropertyChangedEventArgs(propertyName));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected Page ApplicationPage;

        private static IVkApi _vkontakteApi = null;

        public virtual void OnNavigatedTo(Page page, NavigationEventArgs e)
        {
            ApplicationPage = page;
        }

        public virtual void OnNavigatedFrom(Page page, NavigationEventArgs e)
        {

        }

        protected T GetService<T>() where T : class
        {
            if (typeof (T) == typeof (IEntityStorage))
            {
                return new EntityStorage() as T;
            }
            if (typeof (T) == typeof (IVkApi))
            {
                if (_vkontakteApi == null) _vkontakteApi = new VkApi(GetService<IEntityStorage>());
                return _vkontakteApi as T;
            }
            if(typeof(T)==typeof(ISimpleNavigationService))
            {
                return new SimpleNavigationService() as T;
            }
            if (typeof(T) == typeof(ICommonErrorHandler))
            {
                return new CommonErrorHandler(GetService<IVkApi>(), GetService<ISimpleNavigationService>()) as T;
            }
            throw new Exception("service not found"); 
        }



        /*public string GetStateOrUrlParam(string key)
        {
            var result = GetStateOrUrlParamNullable(key);
            if (result == null) throw new Exception("Key " + key + " not found");
            return result;
        }  */

            /*    public string GetStateOrUrlParamNullable(string key)
                {
                    if (PageState.ContainsKey(key))
                    {
                        return (string)PageState[key];
                    }
                    if (ApplicationPage.NavigationContext.QueryString.ContainsKey(key))
                    {
                        return ApplicationPage.NavigationContext.QueryString[key];
                    }
                    return null;
                } */

            /*   public T GetState<T>(string key)
               {
                   if(PageState.ContainsKey(key))
                   {
                       return (T)PageState[key];
                   }
                   return default(T);
               } */


            //protected IDictionary<string, object> PageState
            //{
            //  get { return ApplicationPage.State; }
            //}
            /*
            public bool IsPortraitOrientation
            {
                get
                {
                    return Orientation == PageOrientation.Portrait || 
                        Orientation == PageOrientation.PortraitUp ||
                        Orientation == PageOrientation.PortraitDown;
                }
            }

            public bool IsLandscapeOrientation
            {
                get
                {
                    return Orientation == PageOrientation.Landscape ||
                        Orientation == PageOrientation.LandscapeLeft ||
                        Orientation == PageOrientation.LandscapeRight;
                }
            }

            protected PageOrientation Orientation
            {
                get { return ((PhoneApplicationFrame) Application.Current.RootVisual).Orientation; }
            }
            */
            
        protected CoreDispatcher Dispatcher
        {
            get { return CoreWindow.GetForCurrentThread().Dispatcher; }
        } 

       
    }

}
