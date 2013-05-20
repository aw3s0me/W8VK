using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKClient.Services
{
    public interface IAuthService
    {
        bool IsLoggedInVk(bool goToLoginIfFalse = true);

        Task LoginVk(string login, string password);

        void LoginVk();

        void LogOutVk();
    }
}
