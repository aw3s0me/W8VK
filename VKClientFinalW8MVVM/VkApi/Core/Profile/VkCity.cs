using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace VkApi.Core.Profile
{
    public class VkCity
    {
        public string Name { get; set; }
        public long Cid { get; set; }

        public static VkCity FromJson(JToken json)
        {
            var vkCity = new VkCity();

            vkCity.Cid = json["cid"].Value<long>();

            if (json["name"] != null)
            {
                vkCity.Name = json["name"].Value<string>();
            }

            return vkCity;
        }
    }
}
