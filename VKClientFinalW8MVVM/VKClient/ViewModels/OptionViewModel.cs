using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using VKClient.Services;

namespace VKClient.ViewModels
{
    class OptionViewModel : BaseViewModel
    {
        private string _colorName;
        private string _fontColorName;

        public string ColorName { get; set; }
        public string FontColorName { get; set; }

        public RelayCommand ChangeAppProperties { get; private set; }

        private void InitializeCommands()
        {
            ChangeAppProperties = new RelayCommand(new Action(ChangeAppPropertiesMethod));
        }

        public OptionViewModel()
        {
            InitializeCommands();
        }

        public async void ChangeAppPropertiesMethod()
        {
            ApplicationService.Instance.Settings.FontColor = FontColorName;
            ApplicationService.Instance.Settings.BackGroundColor = ColorName;
            ApplicationService.Instance.Settings.Save();
        }
    }

}
