using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace VkApi.Core.Attachments
{
    public class VkAudioAttachment : VkAttachment
    {
        public string Artist
        {
            get;
            set;
        }
        public string Title
        {
            get;
            set;
        }
        public TimeSpan Duration
        {
            get;
            set;
        }
        public string Url
        {
            get;
            set;
        }
        public static VkAudioAttachment FromJson(JToken json)
        {
            if (json == null)
            {
                throw new ArgumentException("Json can not be null.");
            }
            VkAudioAttachment vkAudioAttachment = new VkAudioAttachment();
            vkAudioAttachment.Id = json["aid"].Value<long>();
            vkAudioAttachment.OwnerId = json["owner_id"].Value<long>();
            vkAudioAttachment.Artist = json["performer"].Value<string>();
            vkAudioAttachment.Title = json["title"].Value<string>();
            vkAudioAttachment.Duration = TimeSpan.FromSeconds((double)json["duration"].Value<int>());
            if (json["url"] != null)
            {
                vkAudioAttachment.Url = json["url"].Value<string>();
            }
            return vkAudioAttachment;
        }
    }
}
