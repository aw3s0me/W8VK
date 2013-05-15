using System;
using Windows.ApplicationModel;
using Windows.UI.Core;
using Windows.UI.Xaml.Media.Imaging;

namespace VKViewModels
{
    public class ImageViewModel : BaseViewModel
    {
        private static BitmapImage BitmapPreview { get; set; }
        
        
        public ImageViewModel(string url)
        {
            if (BitmapPreview == null)
            {
                BitmapPreview=new BitmapImage(new Uri("/Imagees/PreloadImage.png",UriKind.Relative));
            }
            Image = BitmapPreview;
            if (DesignMode.DesignModeEnabled)
            {
                return;
            }
            
            /*var client = new WebClient();
            client.OpenReadCompleted += ClientOpenReadCompleted;
            client.OpenReadAsync(new Uri(url)); */
        }


/*        void ClientOpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            //Dispatcher.BeginInvoke(() => {
            var img = new BitmapImage();
            img.SetSource(e.Result);
            Image = img;
            //});
        } */

        private BitmapImage bitmapImage;
        public BitmapImage Image
        {
            get { return bitmapImage; }
            set
            {
                Dispatcher.RunAsync(CoreDispatcherPriority.Normal,() =>
                {
                    bitmapImage = value;
                    OnPropertyChange("Image");
                });
            }
        }
    }
}
