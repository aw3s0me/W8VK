using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using VkApi.Error;

namespace VkApi.Core
{
    internal static class VkErrorProcessor
    {
        public static bool ProcessError(JObject response)
        {
            if (response["error"] == null)
            {
                return false;
            }
            if (response["error"]["error_code"] != null)
            {
                if (response["error"]["error_code"].Value<string>() == "201")
                {
                    throw new VkAccessDeniedException();
                }
                if (response["error"]["error_code"].Value<string>() == "221")
                {
                    throw new VkStatusBroadcastDisabledException();
                }
            }
            if (response["error"].HasValues)
            {
                int num = response["error"]["error_code"].Value<int>();
                if (num == 7)
                {
                    throw new VkAccessDeniedException();
                }
                return true;
            }
            else
            {
                string text;
                if ((text = response["error"].Value<string>()) != null && text == "need_captcha")
                {
                    throw new VkCaptchaNeededException(response["captcha_sid"].Value<string>(), response["captcha_img"].Value<string>());
                }
                throw new VkException(response["error"].Value<string>(), response["error"].Value<string>());
            }
        }
    }
}
