using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using VkApi.Auth;
using VkApi.Extensions;
using Windows.Security.Authentication.Web;

namespace VkApi.Core.Auth
{
    public class VkDirectAuthRequest
    {
        public async Task<AccessToken> Login(string login, string password, string appId, string clientSecret, VkScopeSettings scopeSettings = VkScopeSettings.CanAccessFriends, string captchaSid = null, string captchaKey = null)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("username", login);
            dictionary.Add("password", password);
            dictionary.Add("grant_type", "password");
            Dictionary<string, string> arg_7B_0 = dictionary;
            string arg_7B_1 = "scope";
            int num = (int)scopeSettings;
            arg_7B_0.Add(arg_7B_1, num.ToString(CultureInfo.InvariantCulture));
            if (!string.IsNullOrEmpty(captchaSid) && !string.IsNullOrEmpty(captchaKey))
            {
                dictionary.Add("captcha_sid", captchaSid);
                dictionary.Add("captcha_key", captchaKey);
            }
            dictionary.Add("client_id", appId);
            dictionary.Add("client_secret", clientSecret);
            VkRequest vkRequest = new VkRequest(new Uri("https://oauth.vk.com/token"), dictionary, "GET");
            //JObject jObject = await vkRequest.Execute();
            var VkUrl = 
                String.Format("https://oauth.vk.com/authorize?client_id={0}&scope=" +
                              "notify,friends,photos,audio,video,docs,notes,pages,status,wall,groups,messages,notifications,stats,ads,offline" +
                              "&redirect_uri=http://oauth.vk.com/blank.html&display=touch&response_type=token",appId);

            var requestUri = new Uri(VkUrl);
            var callbackUri = new Uri("http://oauth.vk.com/blank.html");

            WebAuthenticationResult webAuthResult = await WebAuthenticationBroker.AuthenticateAsync(
                                                    WebAuthenticationOptions.None,
                                                    requestUri,
                                                    callbackUri);

            if (webAuthResult.ResponseStatus == WebAuthenticationStatus.Success)
            {
                var responseString = webAuthResult.ResponseData.ToString();
                char[] separators = {'=', '&'};
                string[] responseContent = responseString.Split(separators);
                string accessToken = responseContent[1];
                int userId = Int32.Parse(responseContent[5]);
                int expiresInSeconds = Int32.Parse(responseContent[3]);
                
                return new AccessToken
                    {
                        Token = accessToken,
                        UserId = userId.ToString(),
                        //ExpiresIn = DateTime.Now.AddSeconds(expiresInSeconds)
                        ExpiresIn = expiresInSeconds == 0 ? DateTime.MaxValue : DateTime.Now.AddSeconds(expiresInSeconds)
                    };
            }
            else
            {
                return null;
            }
            

         /*   return new AccessToken
            {
                Token = jObject["access_token"].Value<string>(),
                UserId = jObject["user_id"].Value<string>(),
                ExpiresIn = (jObject["expires_in"].Value<double>() == 0.0) ? DateTime.MaxValue : DateTimeExtensions.UnixTimeStampToDateTime(jObject["expires_in"].Value<double>())
            }; */
        }
    }
}
