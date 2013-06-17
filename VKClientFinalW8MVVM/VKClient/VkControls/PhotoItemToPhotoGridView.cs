using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VKClient.Services.Helpers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;

namespace VKClient.VkControls
{
    public class PhotoItemToPhotoGridView : DependencyObject
    {
        private const int MaxSize = 3600;
        private const long MAX_WIDTH = 800;
        private const long MAX_HEIGHT = 400;
        private long _width;
        private long _height;

        public string Aid { get; set; }
        public string Title { get; set; }
        public long Width { 
            get { return ImageProcessingHelper.ScaleImageWidth(_width, _height, MAX_WIDTH, MAX_HEIGHT); }
            set { _width = value; } 
        }
        public long Height
        {
            get { return ImageProcessingHelper.ScaleImageHeight(_width, _height, MAX_WIDTH, MAX_HEIGHT); }
            set { _height = value; }
        }
        public Uri PhotoMiniUri { get; set; }
        public Uri PhotoUri { get; set; }

    }
}
