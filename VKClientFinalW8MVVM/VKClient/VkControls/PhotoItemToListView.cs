using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;

namespace VKClient.VkControls
{
    public class PhotoItemToListView : DependencyObject
    {
        private const int MaxSize = 3600;
        public string Aid { get; set; }
        public string Title { get; set; }
        public long Width { get; set; }
        public long Height { get; set; }
        public Uri PhotoMiniUri { get; set; }
        public Uri PhotoUri { get; set; }
    }
}
