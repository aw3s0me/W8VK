using System;
using System.Net;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Threading;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

namespace VKClient.Services
{
    public class IApplicationService<TSettings>
    where TSettings : class, new()
    {
        private Boolean _appClosingFinished;

        public TSettings AppSettings
        {
            get;
            private set;
        }

        public IApplicationService()
        {
            this.AppSettings = Activator.CreateInstance<TSettings>();
        }

        public void ApplicationActivated()
        {
            this.OnApplicationActivated();
        }

        public void ApplicationClosing()
        {
            this.OnApplicationClosing();
        }

        public void ApplicationDeactivated()
        {
            this.OnApplicationDeactivated();
        }

        public void ApplicationLaunching()
        {
            this.OnApplicationLoading();
        }

        public async void HandleException(UnhandledExceptionEventArgs e)
        {
            UnhandledExceptionEventArgs unhandledExceptionEventArg = e;
            unhandledExceptionEventArg.Handled=await this.OnHandleException(e.Exception);
        }

        public async void HandleException(NavigationFailedEventArgs e)
        {
            NavigationFailedEventArgs navigationFailedEventArg = e;
            navigationFailedEventArg.Handled=await this.OnHandleException(e.Exception);
        }

        public void HandleException(Exception ex)
        {
            this.OnHandleException(ex);
        }

        protected virtual void LoadSettings()
        {
        }

        protected virtual void OnApplicationActivated()
        {
            this.LoadSettings();
        }

        protected virtual void OnApplicationClosing()
        {
            if (this._appClosingFinished)
            {
                return;
            }
            this.SaveSettings();
            this._appClosingFinished = true;
        }

        protected virtual void OnApplicationDeactivated()
        {
            this.SaveSettings();
        }

        protected virtual void OnApplicationLoading()
        {
            this.LoadSettings();
        }

        protected virtual async Task<Boolean> OnHandleException(Exception exception)
        {
            Boolean flag = false;
            Boolean hasThreadAccess = DispatcherHelper.UIDispatcher.HasThreadAccess;
            if (DispatcherHelper.UIDispatcher == null || hasThreadAccess)
            {
                Boolean flag1 = await this.ProcessException(exception);
                flag = flag1;
            }
            else
            {
                CoreDispatcher uIDispatcher = DispatcherHelper.UIDispatcher;
                Int32 num = 0;
                await uIDispatcher.RunAsync((CoreDispatcherPriority)num, () => this.ProcessException(exception));
            }
            Boolean flag2 = flag;
            return flag2;
        }

        private async Task<Boolean> ProcessException(Exception exception)
        {
            Boolean flag;
            if (!(exception is WebException))
            {
                var messageDialog = new MessageDialog("Unexpected critical error occured. We are sorry for any inconveniencies caused. Please, try again later.", "Application Failed");
                messageDialog.Commands.Add(new UICommand("Ok"));
                await messageDialog.ShowAsync();
                flag = false;
            }
            else
            {
                flag = true;
            }
            return flag;
        }

        protected virtual void SaveSettings()
        {
        }
    }
}
