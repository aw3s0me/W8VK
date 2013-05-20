
namespace VKClient.Models
{
    namespace Entities
    {
        public class AuthorizationContext : IEntity
        {
            public string CurrentUserId { get; set; }

            public string AccessToken { get; set; }

            public string AppId { get; set; }

        }

        public enum AuthorizationStatus
        {
            Unknown,
            Success,
            Error
        };

        public class AuthorizationResult
        {
            public AuthorizationStatus Status { get; set; }
            public AuthorizationContext Context { get; set; }
            public string Description { get; set; }
        }
    }
}
