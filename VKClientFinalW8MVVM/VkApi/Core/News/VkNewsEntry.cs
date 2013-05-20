using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using VkApi.Core.Attachments;
using VkApi.Core.Profile;
using VkApi.Extensions;

namespace VkApi.Core.News
{
    public class VkNewsEntry
    {
        public long Id
        {
            get;
            set;
        }
        public string Type
        {
            get;
            set;
        }
        public long SourceId
        {
            get;
            set;
        }
        public DateTime Date
        {
            get;
            set;
        }
        public string Text
        {
            get;
            set;
        }
        public List<VkAttachment> Attachments
        {
            get;
            set;
        }
        public VkProfileBase Author
        {
            get;
            set;
        }
        public long CommentsCount
        {
            get;
            set;
        }
        public bool CanWriteComment
        {
            get;
            set;
        }
        public static VkNewsEntry FromJson(JToken json)
        {
            if (json == null)
            {
                throw new ArgumentException("Json can not be null.");
            }
            VkNewsEntry vkNewsEntry = new VkNewsEntry();
            if (json["post_id"] != null)
            {
                vkNewsEntry.Id = json["post_id"].Value<long>();
            }
            if (json["type"] != null)
            {
                vkNewsEntry.Type = json["type"].Value<string>();
            }
            if (json["source_id"] != null)
            {
                vkNewsEntry.SourceId = json["source_id"].Value<long>();
            }
            if (json["date"] != null)
            {
                vkNewsEntry.Date = DateTimeExtensions.UnixTimeStampToDateTime((double)json["date"].Value<long>());
            }
            if (json["text"] != null)
            {
                vkNewsEntry.Text = json["text"].Value<string>();
                vkNewsEntry.Text = vkNewsEntry.Text.Replace("<br>", Environment.NewLine);
            }
            if (json["attachments"] != null)
            {
                vkNewsEntry.Attachments = new List<VkAttachment>();
                using (IEnumerator<JToken> enumerator = json["attachments"].AsEnumerable().GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        JToken current = enumerator.Current;
                        string text;
                        if ((text = current["type"].Value<string>()) != null)
                        {
                            if (!(text == "audio"))
                            {
                                if (text == "photo")
                                {
                                    vkNewsEntry.Attachments.Add(VkPhotoAttachment.FromJson(current["photo"]));
                                }
                            }
                            else
                            {
                                vkNewsEntry.Attachments.Add(VkAudioAttachment.FromJson(current["audio"]));
                            }
                        }
                    }
                }
            }
            if (json["comments"] != null)
            {
                vkNewsEntry.CommentsCount = json["comments"]["count"].Value<long>();
                vkNewsEntry.CanWriteComment = (json["comments"]["can_post"].Value<int>() == 1);
            }
            return vkNewsEntry;
        }
    }
}
