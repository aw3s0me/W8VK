using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKontakteModel.Entities
{
    public class AuthorizationContext : IEntity
    {
        public string CurrentUserId { get; set; }

        public string AccessToken { get; set; }

        public string AppId { get; set; }

    }

    public  enum  AuthorizationStatus
    {
        Unknown,
        Success,
        Error,
    }

    public  class AuthorizationResult
    {
        public AuthorizationStatus Status { get; set; }
        public AuthorizationContext Context { get; set; }
        public string Description { get; set; }
    }
}
