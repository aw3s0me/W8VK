using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VkApi.Error
{
    public class VkCaptchaNeededException : VkException
    {
        public string CaptchaSid
        {
            get;
            set;
        }
        public string CaptchaImg
        {
            get;
            set;
        }
        public VkCaptchaNeededException(string captchaSid, string captchaImg)
        {
            this.CaptchaSid = captchaSid;
            this.CaptchaImg = captchaImg;
        }
    }
}
