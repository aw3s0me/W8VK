using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using VKModel.Entities;
using VKModel.Exceptions;
using VKModel.Interfaces;
using Newtonsoft.Json;

namespace VKServiceLayer
{
    public class RequestBuilder
    {
        private readonly AuthorizationContext context;
        private string apiMethodName;
        private Dictionary<string, string> keyValuePair;
        public RequestBuilder(AuthorizationContext context)
        {
            this.context = context;
            keyValuePair = new Dictionary<string, string>();
            apiMethodName = String.Empty;
        }

        public void SetMethod(string methodName)
        {
            apiMethodName = methodName;
        }

        public void AddParam(string key, string value)
        {
            keyValuePair.Add(key,value);
        }

        public string GetRequestUrl()
        {
            keyValuePair.Add("access_token", context.AccessToken);

            var stringBuilder = new StringBuilder();
            var firstKey = true;
            foreach (var key in keyValuePair.Keys)
            {
                if (firstKey)
                {
                    stringBuilder.Append("?");
                    firstKey = false;
                }
                else
                {
                    stringBuilder.Append("&");
                }
                stringBuilder.Append(key);
                stringBuilder.Append("=");
                stringBuilder.Append(keyValuePair[key]);
            }

            const string template = "https://api.vkontakte.ru/method/{0}{1}";
            var url = String.Format(template, apiMethodName, stringBuilder);
            return url;
        }

        public async void SendRequest<T>(Action<T> actionResult, Action<Error> errorAction) where T : new()
        {
            /*var client = new WebClient();
            client.OpenReadCompleted += (sender, e) =>
            {
                var response = new JsonSerializer<T>().Deserialize(e.Result);
                if (!response.ResponseIsSuccess())
                {
                    e.Result.Position = 0;
                    var errorResponse = new JsonSerializer<ErrorResponse>().Deserialize(e.Result);
                    if (errorResponse.ResponseIsSuccess())
                    {
                        errorAction.Invoke(errorResponse.ErrorResult);
                    }
                    errorAction.Invoke(new Error() { ErrorMsg = "Unknown error" });
                }
                else
                {
                    actionResult.Invoke(response);
                }
            };

            client.OpenReadAsync(new Uri(GetRequestUrl()));    */

            ////WINDOWS 8 VARIANT
            using (var client = new HttpClient())
            {
                client.MaxResponseContentBufferSize = Int32.MaxValue;
                //var response = client.GetAsync()
                Stream responseStream = await client.GetStreamAsync(new Uri(GetRequestUrl()));
                using (StreamReader sr = new StreamReader(responseStream))
                {
                    //string result = "";
                    string result = sr.ReadToEnd();
                    //while (!sr.EndOfStream)
                }
            }
            
        }

    }
}
