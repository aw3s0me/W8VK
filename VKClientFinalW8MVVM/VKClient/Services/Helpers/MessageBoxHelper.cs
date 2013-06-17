using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace VKClient.Helpers
{
    public static class MessageBoxHelper
    {
        public static async void Show(string messageBoxText)
        {
            MessageDialog messageDialog = new MessageDialog(messageBoxText);
            await messageDialog.ShowAsync();
        }
    }
}
