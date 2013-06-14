using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace VkApi.Core.Profile
{
    public class VkCountry
    {
        public string Name { get; set; }

        public long Cid { get; set; }

        public static VkCountry FromJson(JToken json)
        {
            var vkCountry = new VkCountry();

            vkCountry.Cid = json["cid"].Value<long>();

            if (json["name"] != null)
            {
                vkCountry.Name = json["name"].Value<string>();
            }

            return vkCountry;
        }
    }
}
