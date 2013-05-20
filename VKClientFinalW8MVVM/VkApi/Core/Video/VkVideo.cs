using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace VkApi.Core.Video
{
    public class VkVideo
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
        public TimeSpan Duration
        {
            get;
            set;
        }
        public string Link
        {
            get;
            set;
        }
        public string ImageSmall
        {
            get;
            set;
        }
        public string ImageMedium
        {
            get;
            set;
        }
        public DateTime Date
        {
            get;
            set;
        }
        public string Player
        {
            get;
            set;
        }
        public Dictionary<string, string> Files
        {
            get;
            set;
        }
        internal static VkVideo FromJson(JToken json)
        {
            if (json == null)
            {
                throw new ArgumentException("Json can not be null.");
            }
            VkVideo vkVideo = new VkVideo();
            if (json["id"] != null)
            {
                vkVideo.Id = json["id"].Value<long>();
            }
            if (json["vid"] != null)
            {
                vkVideo.Id = json["vid"].Value<long>();
            }
            vkVideo.OwnerId = json["owner_id"].Value<long>();
            vkVideo.Duration = TimeSpan.FromSeconds(json["duration"].Value<double>());
            if (json["link"] != null)
            {
                vkVideo.Link = json["link"].Value<string>();
            }
            vkVideo.Title = WebUtility.HtmlDecode(json["title"].Value<string>());
            vkVideo.Description = WebUtility.HtmlDecode(json["description"].Value<string>());
            if (json["thumb"] != null)
            {
                vkVideo.ImageSmall = json["thumb"].Value<string>();
            }
            if (json["image_medium"] != null)
            {
                vkVideo.ImageMedium = json["image_medium"].Value<string>();
            }
            if (json["player"] != null)
            {
                vkVideo.Player = json["player"].Value<string>();
            }
            if (json["files"] != null)
            {
                vkVideo.Files = new Dictionary<string, string>();
                using (IEnumerator<JToken> enumerator = json["files"].Children().GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        JProperty jProperty = (JProperty)enumerator.Current;
                        if (jProperty.HasValues)
                        {
                            vkVideo.Files.Add(jProperty.Name, jProperty.Value.Value<string>());
                        }
                    }
                }
            }
            return vkVideo;
        }
    }
}
