using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKClient.Models
{
    public class MetaData
    {
        public string Album
        {
            get;
            set;
        }

        public object AlbumArt
        {
            get;
            set;
        }

        public Uri AlbumArtUri
        {
            get;
            set;
        }

        public string Artist
        {
            get;
            set;
        }

        public MetaData()
        {
        }
    }
}
