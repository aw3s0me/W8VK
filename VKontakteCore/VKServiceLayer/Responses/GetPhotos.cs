using System.Runtime.Serialization;

namespace VKServiceLayer.Responses
{
    [DataContract]
    public class Photo
    {
        [DataMember(Name = "pid")]
        public string PhotoId { get; set; }
        [DataMember(Name = "aid")]
        public string AlbumId { get; set; }
        [DataMember(Name = "owner_id")]
        public string OwnerId { get; set; }
        [DataMember(Name = "src")]
        public string Source { get; set; }
        [DataMember(Name = "src_big")]
        public string SourceBig { get; set; }
        [DataMember(Name = "src_small")]
        public string SourceSmall { get; set; }
        [DataMember(Name = "src_xbig")]
        public string SourceXBig { get; set; }
        [DataMember(Name = "src_xxbig")]
        public string SourceX2Big { get; set; }
        [DataMember(Name = "created")]
        public string Created { get; set; }
    }

    [DataContract]
    public class GetPhotos : IServiceResult
    {
        [DataMember(Name = "response")]
        public Photo[] Photos { get; set; }

        public bool ResponseIsSuccessful()
        {
            return Photos != null;
        }

        public string Error { get; set; }
    }
}