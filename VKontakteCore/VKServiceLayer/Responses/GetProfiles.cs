using System.Runtime.Serialization;

namespace VKServiceLayer.Responses
{
    [DataContract]
    public class GetProfiles : IServiceResult
    {
        [DataMember(Name = "response")]
        public UserServiceItem[] Result { get; set; }

        public bool ResponseIsSuccessful()
        {
            return Result != null;
        }

        public string Error { get; set; }
    }
}