using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace VkApi.Core.Profile
{
    public class VkProfile : VkProfileBase
    {
        public string FirstName
        {
            get;
            set;
        }
        public string LastName
        {
            get;
            set;
        }
        public new string Name
        {
            get
            {
                return this.FirstName + " " + this.LastName;
            }
        }
        public bool IsOnline { get; set; }
        public static VkProfile FromJson(JToken json)
        {
            VkProfile vkProfile = new VkProfile();
            vkProfile.Id = json["uid"].Value<long>();
            vkProfile.FirstName = json["first_name"].Value<string>();
            vkProfile.LastName = json["last_name"].Value<string>();
            if (json["photo"] != null)
            {
                vkProfile.Photo = new Uri(json["photo"].Value<string>());
            }
            if (json["photo_medium"] != null)
            {
                vkProfile.PhotoMedium = new Uri(json["photo_medium"].Value<string>());
            }
            if (json["photo_big"] != null)
            {
                vkProfile.PhotoBig = new Uri(json["photo_big"].Value<string>());
            }
            if (json["online"] != null)
            {
                vkProfile.IsOnline = json["online"].Value<bool>();
            }
            return vkProfile;
        }
    }
}
