using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace VkApi.Core
{
    internal class VkRequest
    {
        private readonly Uri _uri;
        private readonly string _method;
        private readonly Dictionary<string, string> _parameters;
        public VkRequest(Uri uri)
        {
            this._uri = uri;
            this._method = "GET";
        }
        public VkRequest(Uri uri, Dictionary<string, string> parameters, string method = "GET")
        {
            this._uri = uri;
            this._method = method;
            this._parameters = parameters;
        }
        public async Task<JObject> Execute()
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
            {
                throw new Exception("Network is not available.");
            }
            Uri fullUri = this.GetFullUri();
            //string new_uri =
              //  "https://oauth.vk.com/token?grant_type=password&client_id=3501149&client_secret=6Cu0XE498xkM68wljycg&username=89539292964&password=Calendar1";
            HttpWebRequest httpWebRequest = WebRequest.CreateHttp(fullUri);
            //HttpWebRequest httpWebRequest = WebRequest.CreateHttp(new Uri(new_uri));
            httpWebRequest.Method = (this._method);
            WebResponse webResponse = await httpWebRequest.GetResponseAsync(); 
            JObject jObject;



            using (Stream responseStream = webResponse.GetResponseStream())
            {
                using (StreamReader streamReader = new StreamReader(responseStream))
                {
                    string json = await streamReader.ReadToEndAsync();
                    jObject = JObject.Parse(json);
                    VkErrorProcessor.ProcessError(jObject);
                }
            }
            return jObject;
        } 
        private Uri GetFullUri()
        {
            if (this._method == "GET" && this._parameters != null && this._parameters.Count > 0)
            {
                string text = string.Join("&", Enumerable.Select<KeyValuePair<string, string>, string>(this._parameters, (KeyValuePair<string, string> kp) => string.Format("{0}={1}", new object[]
				{
					Uri.EscapeDataString(kp.Key),
					Uri.EscapeDataString(kp.Value)
				})));
                return new Uri(this._uri + "?" + text);
            }
            return this._uri;
        }
    }
}
