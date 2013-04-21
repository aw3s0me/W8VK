using VKontakteModelViewVM.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Windows.Input;
using VKontakteModelViewVM;
using VkontakteModelViewVM.Resources;

namespace VKontakteModelViewVM.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public static AppResource appResource = new AppResource();

        public  AppResource Resource
        {
            get { return appResource; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }

        }
    }
}