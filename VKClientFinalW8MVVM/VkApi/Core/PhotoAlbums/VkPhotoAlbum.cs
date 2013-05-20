using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace VkApi.Core.PhotoAlbums
{
    public class VkPhotoAlbum
    {
        public long AId
        {
            get;
            set;
        }
        public long OwnerId
        {
            get;
            set;
        }
        public long ThumbId
        {
            get;
            set;
        }
        public string Title
        {
            get;
            set;
        }
        public string Description
        {
            get;
            set;
        }
        public Uri ThumbSrc
        {
            get;
            set;
        }
        
        public Dictionary<string, string> Files
        {
            get;
            set;
        }

        internal static VkPhotoAlbum FromJson(JToken json)
        {
            if (json == null)
            {
                throw new ArgumentException("Json can not be null.");
            }
            VkPhotoAlbum vkAlbum = new VkPhotoAlbum();
            if (json["aid"] != null)
            {
                vkAlbum.AId = json["aid"].Value<long>();
            }
            if (json["thumb_id"] != null)
            {
                vkAlbum.ThumbId = json["thumb_id"].Value<long>();
            }
            if (json["owner_id"] != null)
            {
                vkAlbum.OwnerId = json["owner_id"].Value<long>();
            }
            if (vkAlbum.AId > 0)
            {
                vkAlbum.Description = WebUtility.HtmlDecode(json["description"].Value<string>());
            }
            else
            {
                vkAlbum.Description = "Сохраненные фотографии";
            }
            vkAlbum.Title = WebUtility.HtmlDecode(json["title"].Value<string>());
            
            if (json["thumb_src"] != null)
            {
                vkAlbum.ThumbSrc = new Uri(json["thumb_src"].Value<string>());
            }

            return vkAlbum;

        }

    }
}
