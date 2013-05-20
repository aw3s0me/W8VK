using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace VKClient.VkControls
{
    public partial class PhotoGridView :GridView
    {
        protected override void PrepareContainerForItemOverride(Windows.UI.Xaml.DependencyObject element, object item)
        {
            var photoItem = item as PhotoItemToGridView;

            if (photoItem != null)
            {
                element.SetValue(VariableSizedWrapGrid.ColumnSpanProperty, photoItem.HorizontalSize);
                element.SetValue(VariableSizedWrapGrid.RowSpanProperty, photoItem.VerticalSize);
            }
            base.PrepareContainerForItemOverride(element, item);
        }
    }
}
