using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace VkApi.Core.Audio
{
    public class VkAudioAlbum
    {
        public double OwnerId
        {
            get;
            set;
        }
        public double Id
        {
            get;
            set;
        }
        public string Title
        {
            get;
            set;
        }
        public static VkAudioAlbum FromJson(JToken json)
        {
            if (json == null)
            {
                throw new ArgumentException("Json can not be null.");
            }
            return new VkAudioAlbum
            {
                Id = json["album_id"].Value<double>(),
                OwnerId = Math.Abs(json["owner_id"].Value<double>()),
                Title = WebUtility.HtmlDecode(json["title"].Value<string>())
            };
        }
    }
}
