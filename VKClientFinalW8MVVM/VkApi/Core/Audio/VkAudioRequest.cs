using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using VkApi.Auth;

namespace VkApi.Core.Audio
{
    public class VkAudioRequest
    {
        private const int MAX_AUDIO_COUNT = 200;
        public async Task<IEnumerable<VkAudio>> Get()
        {
            return await this.Get(Vkontakte.Instance.AccessToken.UserId, null, null, false, 0, 0);
        }
        public async Task<IEnumerable<VkAudio>> Get(string userId, string groupId, string albumId, bool needUser, int count = 0, int offset = 0)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(groupId))
            {
                dictionary.Add("gid", groupId);
            }
            else
            {
                dictionary.Add("uid", userId);
            }
            if (!string.IsNullOrEmpty(albumId))
            {
                dictionary.Add("album_id", albumId);
            }
            if (needUser)
            {
                dictionary.Add("need_user", "1");
            }
            if (count > 0)
            {
                dictionary.Add("count", count.ToString(CultureInfo.InvariantCulture));
            }
            if (offset > 0)
            {
                dictionary.Add("offset", offset.ToString(CultureInfo.InvariantCulture));
            }
            dictionary.Add("access_token", Vkontakte.Instance.AccessToken.Token);
            JObject jObject = await new VkRequest(new Uri("https://api.vk.com/method/audio.get"), dictionary, "GET").Execute();
            IEnumerable<VkAudio> result;
            if (VkErrorProcessor.ProcessError(jObject))
            {
                result = null;
            }
            else
            {
                if (jObject["response"].HasValues)
                {
                    result = Enumerable.Select<JToken, VkAudio>(jObject["response"], (JToken a) => VkAudio.FromJson(a));
                }
                else
                {
                    result = null;
                }
            }
            return result;
        }
        public async Task<IEnumerable<VkAudioAlbum>> GetAlbums(string userId, string groupId, int count, int offset)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(groupId))
            {
                dictionary.Add("gid", groupId);
            }
            else
            {
                dictionary.Add("uid", userId);
            }
            if (count > 0)
            {
                dictionary.Add("count", count.ToString(CultureInfo.InvariantCulture));
            }
            if (offset > 0)
            {
                dictionary.Add("offset", offset.ToString(CultureInfo.InvariantCulture));
            }
            dictionary.Add("access_token", Vkontakte.Instance.AccessToken.Token);
            JObject jObject = await new VkRequest(new Uri("https://api.vk.com/method/audio.getAlbums"), dictionary, "GET").Execute();
            IEnumerable<VkAudioAlbum> result;
            if (VkErrorProcessor.ProcessError(jObject))
            {
                result = null;
            }
            else
            {
                if (jObject["response"].HasValues)
                {
                    result = Enumerable.Select<JToken, VkAudioAlbum>(Enumerable.Skip<JToken>(jObject["response"], 1), (JToken a) => VkAudioAlbum.FromJson(a));
                }
                else
                {
                    result = null;
                }
            }
            return result;
        }
        public async Task<IEnumerable<VkAudio>> Search(string query, int count = 0, int offset = 0, VkAudioSortType sort = VkAudioSortType.DateAdded, bool withLyricsOnly = false, bool autoFix = true)
        {
            if (count > 200)
            {
                throw new ArgumentException("Maximum count is 200.");
            }
            if (query == null)
            {
                throw new ArgumentException("Query must not be null.");
            }
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("q", query);
            if (autoFix)
            {
                dictionary.Add("auto_complete", "1");
            }
            Dictionary<string, string> arg_98_0 = dictionary;
            string arg_98_1 = "sort";
            int num = (int)sort;
            arg_98_0.Add(arg_98_1, num.ToString(CultureInfo.InvariantCulture));
            if (withLyricsOnly)
            {
                dictionary.Add("lyrics", "1");
            }
            if (count > 0)
            {
                dictionary.Add("count", count.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                dictionary.Add("count", 200.ToString(CultureInfo.InvariantCulture));
            }
            if (offset > 0)
            {
                dictionary.Add("offset", offset.ToString(CultureInfo.InvariantCulture));
            }
            dictionary.Add("access_token", Vkontakte.Instance.AccessToken.Token);
            JObject jObject = await new VkRequest(new Uri("https://api.vk.com/method/audio.search"), dictionary, "GET").Execute();
            IEnumerable<VkAudio> result;
            if (VkErrorProcessor.ProcessError(jObject))
            {
                result = null;
            }
            else
            {
                if (jObject["response"].HasValues)
                {
                    result = Enumerable.Select<JToken, VkAudio>(Enumerable.Where<JToken>(jObject["response"], (JToken a) => a.HasValues && !string.IsNullOrEmpty(a["url"].Value<string>())), (JToken a) => VkAudio.FromJson(a));
                }
                else
                {
                    result = null;
                }
            }
            return result;
        }
        public async Task<double> Add(string aid, string oid)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("aid", aid);
            dictionary.Add("oid", oid);
            dictionary.Add("access_token", Vkontakte.Instance.AccessToken.Token);
            JObject jObject = await new VkRequest(new Uri("https://api.vk.com/method/audio.add"), dictionary, "GET").Execute();
            double result;
            if (VkErrorProcessor.ProcessError(jObject))
            {
                result = 0.0;
            }
            else
            {
                if (jObject["response"] != null)
                {
                    result = jObject["response"].Value<double>();
                }
                else
                {
                    result = 0.0;
                }
            }
            return result;
        }
        public async Task<bool> Remove(string aid, string oid)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("aid", aid);
            dictionary.Add("oid", oid);
            dictionary.Add("access_token", Vkontakte.Instance.AccessToken.Token);
            JObject jObject = await new VkRequest(new Uri("https://api.vk.com/method/audio.delete"), dictionary, "GET").Execute();
            bool result;
            if (VkErrorProcessor.ProcessError(jObject))
            {
                result = false;
            }
            else
            {
                if (jObject["response"] != null && jObject["response"].Value<double>() > 0.0)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            return result;
        }
        public async Task<IEnumerable<VkAudio>> GetById(IList<string> ids)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("audios", string.Join(",", ids));
            dictionary.Add("access_token", Vkontakte.Instance.AccessToken.Token);
            JObject jObject = await new VkRequest(new Uri("https://api.vk.com/method/audio.getById"), dictionary, "GET").Execute();
            IEnumerable<VkAudio> result;
            if (VkErrorProcessor.ProcessError(jObject))
            {
                result = null;
            }
            else
            {
                if (jObject["response"].HasValues)
                {
                    result = Enumerable.Select<JToken, VkAudio>(jObject["response"], (JToken a) => VkAudio.FromJson(a));
                }
                else
                {
                    result = null;
                }
            }
            return result;
        }
        public async Task<string> GetLyrics(string lyricsId)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("lyrics_id", lyricsId);
            dictionary.Add("access_token", Vkontakte.Instance.AccessToken.Token);
            JObject jObject = await new VkRequest(new Uri("https://api.vk.com/method/audio.getLyrics"), dictionary, "GET").Execute();
            string result;
            if (VkErrorProcessor.ProcessError(jObject))
            {
                result = null;
            }
            else
            {
                if (jObject.SelectToken("response.text") != null)
                {
                    result = jObject.SelectToken("response.text").Value<string>();
                }
                else
                {
                    result = null;
                }
            }
            return result;
        }
        public async Task<IEnumerable<VkAudio>> GetRecommendations(int count, int offset)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            if (count > 0)
            {
                dictionary.Add("count", count.ToString(CultureInfo.InvariantCulture));
            }
            if (offset > 0)
            {
                dictionary.Add("offset", offset.ToString(CultureInfo.InvariantCulture));
            }
            dictionary.Add("access_token", Vkontakte.Instance.AccessToken.Token);
            JObject jObject = await new VkRequest(new Uri("https://api.vk.com/method/audio.getRecommendations"), dictionary, "GET").Execute();
            IEnumerable<VkAudio> result;
            if (VkErrorProcessor.ProcessError(jObject))
            {
                result = null;
            }
            else
            {
                if (jObject["response"].HasValues)
                {
                    result = Enumerable.Select<JToken, VkAudio>(jObject["response"], (JToken a) => VkAudio.FromJson(a));
                }
                else
                {
                    result = null;
                }
            }
            return result;
        }
        public async Task<IEnumerable<VkAudio>> GetPopular(int count, int offset)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            if (count > 0)
            {
                dictionary.Add("count", count.ToString(CultureInfo.InvariantCulture));
            }
            if (offset > 0)
            {
                dictionary.Add("offset", offset.ToString(CultureInfo.InvariantCulture));
            }
            dictionary.Add("access_token", Vkontakte.Instance.AccessToken.Token);
            JObject jObject = await new VkRequest(new Uri("https://api.vk.com/method/audio.getPopular"), dictionary, "GET").Execute();
            IEnumerable<VkAudio> result;
            if (VkErrorProcessor.ProcessError(jObject))
            {
                result = null;
            }
            else
            {
                if (jObject["response"].HasValues)
                {
                    result = Enumerable.Select<JToken, VkAudio>(jObject["response"], (JToken a) => VkAudio.FromJson(a));
                }
                else
                {
                    result = null;
                }
            }
            return result;
        }
    }
}
