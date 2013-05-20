using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKClient.VkControls
{
    public class PhotoItemGroupToGridView
    {
        public string Title { get; set; }

        public ObservableCollection<PhotoItemToGridView> Items { get; set; }

        public PhotoItemGroupToGridView()
        {
            Items = new ObservableCollection<PhotoItemToGridView>();
        }

    }
}
