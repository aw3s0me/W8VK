using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKClient.Models
{
    public class Album
    {
        public string Id
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public string Artist
        {
            get;
            set;
        }
        public Uri ImageSmallUri
        {
            get;
            set;
        }
        public Uri ImageUri
        {
            get;
            set;
        }
        public List<Audio> Tracks
        {
            get;
            set;
        }
        public List<string> Tags
        {
            get;
            set;
        }
    }
}
