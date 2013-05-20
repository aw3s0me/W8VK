using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace VKClient.VkControls
{
    public class PhotoItemToGridView
    {
        public Uri PhotoSrc { get; set; }
        public string Title { get; set; }
        public string Id { get; set; }
        public string Type { get; set; }
        public string Player { get; set; }

        private int _horizontalSize = 1;
        public int HorizontalSize
        {
            get { return _horizontalSize; }
            set { _horizontalSize = value; }
        }

        private int _verticalSize = 1;
        public int VerticalSize
        {
            get { return _verticalSize; }
            set { _verticalSize = value; }
        }


    }
}
