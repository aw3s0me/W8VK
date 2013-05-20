using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Ioc;
using VKClient.Views;
using Windows.UI.Xaml.Controls;

namespace VKClient.Services
{
    public class NavigationService
    {
        private Frame frame;

        public Frame Frame
        {
            get
            {
                return frame;
            }
            set
            {
                frame = value;
                frame.Navigated += OnFrameNavigated;
            }
        }

        private void OnFrameNavigated(object sender, Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
          //  var view = e.Content as Content;
          //  var viewModel = view.ViewModel;
          //  viewModel.Initialize();

        }

        public void Navigate(string pageName)
        {
            switch (pageName)
            {

                case "MainView":
                    var mainPageType = SimpleIoc.Default.GetInstance<MainView>();
                    Frame.Navigate(mainPageType.GetType());
                    break;
                case "AudioView": var editPageType = SimpleIoc.Default.GetInstance<AudioViewPage>();
                    Frame.Navigate(editPageType.GetType());
                    break;
                default:
                    var defaultPageType = SimpleIoc.Default.GetInstance<ProfileViewPage>();
                    Frame.Navigate(defaultPageType.GetType());
                    break;
            }
        }
    }
}
