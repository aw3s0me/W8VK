using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace VkApi.Core.PhotoAlbums
{
    public class VkPhoto
    {
        public long PhotoId
        {
            get;
            set;
        }
        public long OwnerId
        {
            get;
            set;
        }
        public long AlbumId
        {
            get;
            set;
        }
        public string Text
        {
            get;
            set;
        }
        public long Width
        {
            get;
            set;
        }
        public long Height
        {
            get;
            set;
        }
        public Uri ThumbSrcSmall
        {
            get;
            set;
        }
        public Uri ThumbSrcNormal
        {
            get;
            set;
        }
        public Uri ThumbSrcBig
        {
            get;
            set;
        }
        public Uri ThumbSrcXBig
        {
            get;
            set;
        }
        public static VkPhoto FromJson(JToken json)
        {
            if (json == null)
            {
                throw new ArgumentException("Json can not be null.");
            }
            VkPhoto vkPhoto = new VkPhoto();
            if (json["pid"] != null)
            {
                vkPhoto.PhotoId = Math.Abs(json["pid"].Value<long>());
            }
            if (json["aid"] != null)
            {
                vkPhoto.AlbumId = Math.Abs(json["aid"].Value<long>());
            }
            if (json["owner_id"] != null)
            {
                vkPhoto.OwnerId = Math.Abs(json["owner_id"].Value<long>());
            }
            if (json["width"] != null)
            {
                vkPhoto.Width = Math.Abs(json["width"].Value<long>());
            }
            if (json["height"] != null)
            {
                vkPhoto.Height = Math.Abs(json["height"].Value<long>());
            }
            vkPhoto.Text = json["text"].Value<string>();
            if (json["src"] != null)
            {
                vkPhoto.ThumbSrcNormal = new Uri(json["src"].Value<string>());
            }
            if (json["src_big"] != null)
            {
                vkPhoto.ThumbSrcBig = new Uri(json["src_big"].Value<string>());
            }
            if (json["src_small"] != null)
            {
                vkPhoto.ThumbSrcSmall = new Uri(json["src_small"].Value<string>());
            }
            if (json["src_xbig"] != null)
            {
                vkPhoto.ThumbSrcXBig = new Uri(json["src_xbig"].Value<string>());
            }

            return vkPhoto;
        }
    }
}
