using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace VkApi.Core.Attachments
{
    public class VkPhotoAttachment : VkAttachment
    {
        public string Source
        {
            get;
            set;
        }
        public string SourceBig
        {
            get;
            set;
        }
        public static VkPhotoAttachment FromJson(JToken json)
        {
            if (json == null)
            {
                throw new ArgumentException("Json can not be null.");
            }
            VkPhotoAttachment vkPhotoAttachment = new VkPhotoAttachment();
            vkPhotoAttachment.Id = json["pid"].Value<long>();
            vkPhotoAttachment.OwnerId = json["owner_id"].Value<long>();
            if (json["src"] != null)
            {
                vkPhotoAttachment.Source = json["src"].Value<string>();
            }
            if (json["src_big"] != null)
            {
                vkPhotoAttachment.SourceBig = json["src_big"].Value<string>();
            }
            return vkPhotoAttachment;
        }
    }
}
