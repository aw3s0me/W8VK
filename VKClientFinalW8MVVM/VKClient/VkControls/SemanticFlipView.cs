using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace VKClient.VkControls
{
    public class SemanticFlipView : FlipView, ISemanticZoomInformation
    {
        public bool IsActiveView { get; set; }

        public bool IsZoomedInView { get; set; }

        public SemanticZoom SemanticZoomOwner { get; set; }

        public void CompleteViewChangeTo(SemanticZoomLocation source, SemanticZoomLocation destination)
        {
            Focus((FocusState)3);
        }
        public void StartViewChangeFrom(SemanticZoomLocation source, SemanticZoomLocation destination)
        {
            destination.Item = (SelectedItem);
        }
        public void CompleteViewChange()
        {
        }
        public void CompleteViewChangeFrom(SemanticZoomLocation source, SemanticZoomLocation destination)
        {
        }
        public void InitializeViewChange()
        {
        }
        public void MakeVisible(SemanticZoomLocation item)
        {
            if (item != null && item.Item != null)
            {
                SelectedItem = (item.Item);
            }
        }
        public void StartViewChangeTo(SemanticZoomLocation source, SemanticZoomLocation destination)
        {
            destination.Item = (source.Item);
        }
        

        
    }
}
