using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VkApi.Auth
{
    public class AccessToken
    {
        public string Token
        {
            get;
            set;
        }
        public DateTime ExpiresIn
        {
            get;
            set;
        }
        public bool HasExpired
        {
            get
            {
                return DateTime.Now > this.ExpiresIn;
            }
        }
        public string UserId
        {
            get;
            set;
        }
    }
}
