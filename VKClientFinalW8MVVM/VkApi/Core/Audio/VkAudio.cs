using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace VkApi.Core.Audio
{
    public class VkAudio
    {
        public long Id
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
        public TimeSpan Duration
        {
            get;
            set;
        }
        public string Title
        {
            get;
            set;
        }
        public string Artist
        {
            get;
            set;
        }
        public string Url
        {
            get;
            set;
        }
        public string LyricsId
        {
            get;
            set;
        }
        internal static VkAudio FromJson(JToken json)
        {
            if (json == null)
            {
                throw new ArgumentException("Json can not be null.");
            }
            VkAudio vkAudio = new VkAudio();
            vkAudio.Id = json["aid"].Value<long>();
            vkAudio.OwnerId = json["owner_id"].Value<long>();
            vkAudio.Duration = TimeSpan.FromSeconds(json["duration"].Value<double>());
            vkAudio.Url = json["url"].Value<string>();
            try
            {
                vkAudio.Title = WebUtility.HtmlDecode(json["title"].Value<string>()).Trim();
                vkAudio.Artist = WebUtility.HtmlDecode(json["artist"].Value<string>()).Trim();
            }
            catch (Exception)
            {
                vkAudio.Title = json["title"].Value<string>().Trim();
                vkAudio.Artist = json["artist"].Value<string>().Trim();
            }
            if (json["album_id"] != null)
            {
                vkAudio.AlbumId = json["album_id"].Value<long>();
            }
            if (json["lyrics_id"] != null)
            {
                vkAudio.LyricsId = json["lyrics_id"].Value<string>();
            }
            return vkAudio;
        }
    }
}
