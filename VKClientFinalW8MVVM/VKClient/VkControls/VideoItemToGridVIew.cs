using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace VKClient.VkControls
{
    class VideoItemToGridView : DependencyObject
    {
        private const int MaxSize = 3600;
        public string Vid { get; set; }
        public string Title { get; set; }
        public Uri PhotoUri { get; set; }
        public Uri PlayerUri { get; set; }
    }
}
