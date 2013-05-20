using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using VkApi.Core.Attachments;

namespace VkApi.Core.Wall
{
    public class VkWallEntry
    {
        public double Id
        {
            get;
            set;
        }
        public List<VkAttachment> Attachments
        {
            get;
            set;
        }
        public static VkWallEntry FromJson(JToken json)
        {
            if (json == null)
            {
                throw new ArgumentException("Json can not be null.");
            }
            VkWallEntry vkWallEntry = new VkWallEntry();
            vkWallEntry.Id = json["id"].Value<double>();
            if (json["attachments"] != null)
            {
                vkWallEntry.Attachments = new List<VkAttachment>();
                using (IEnumerator<JToken> enumerator = json["attachments"].AsEnumerable().GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        JToken current = enumerator.Current;
                        string text;
                        if ((text = current["type"].Value<string>()) != null && text == "audio")
                        {
                            vkWallEntry.Attachments.Add(VkAudioAttachment.FromJson(current["audio"]));
                        }
                    }
                }
            }
            return vkWallEntry;
        }
    }
}
