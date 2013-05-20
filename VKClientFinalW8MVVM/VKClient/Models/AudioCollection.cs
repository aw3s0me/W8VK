using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKClient.Models
{
    public class AudioCollection
    {
        public string GroupId
        {
            get;
            set;
        }

        public string Id
        {
            get;
            set;
        }

        public List<object> Items
        {
            get;
            set;
        }

        public string OwnerId
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public AudioCollection()
        {
        }
    }
}
