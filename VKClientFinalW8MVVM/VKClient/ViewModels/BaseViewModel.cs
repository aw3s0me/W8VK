using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace VKClient.ViewModels
{
    public class BaseViewModel : ViewModelBase
    {
        private bool _isWorking;
        public bool IsWorking
        {
            get
            {
                return this._isWorking;
            }
            set
            {
                if (this._isWorking == value)
                {
                    return;
                }
                this._isWorking = value;
                this.RaisePropertyChanged("IsWorking");
            }
        }
    }
}
