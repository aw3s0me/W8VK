using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using VkApi.Core.Profile;

namespace VkApi.Core.Groups
{
    public class VkGroup : VkProfileBase
    {
        public static VkGroup FromJson(JToken json)
        {
            if (json == null)
            {
                throw new ArgumentException("Json can not be null.");
            }
            VkGroup vkGroup = new VkGroup();
            vkGroup.Id = Math.Abs(json["gid"].Value<long>());
            vkGroup.Name = json["name"].Value<string>();
            if (json["photo"] != null)
            {
                vkGroup.Photo = new Uri(json["photo"].Value<string>());
            }
            return vkGroup;
        }
    }
}
