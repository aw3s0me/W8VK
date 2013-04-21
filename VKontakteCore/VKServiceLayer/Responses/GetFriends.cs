using System.Runtime.Serialization;

namespace VKServiceLayer.Responses
{
    [DataContract]
    public class GetFriends : IServiceResult
    {
        [DataMember(Name = "response")]
        public string[] Result { get; set; }

        public bool ResponseIsSuccessful()
        {
            return Result != null;
        }

        public string Error { get; set; }
    }
}
