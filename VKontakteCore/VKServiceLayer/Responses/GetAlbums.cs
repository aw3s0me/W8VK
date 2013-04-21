using System.Runtime.Serialization;

namespace VKServiceLayer.Responses
{
    [DataContract]
    public class PhotoAlbum
    {
        [DataMember(Name = "aid")]
        public string AlbumId { get; set; }
        [DataMember(Name = "thumb_id")]
        public string ThumbId { get; set; }
        [DataMember(Name = "owner_id")]
        public string OwnerId { get; set; }
        [DataMember(Name = "title")]
        public string Title { get; set; }
        [DataMember(Name = "description")]
        public string Description { get; set; }
        [DataMember(Name = "created")]
        public string Created { get; set; }
        [DataMember(Name = "updated")]
        public string Updated { get; set; }
        [DataMember(Name = "size")]
        public string Size { get; set; }
        [DataMember(Name = "privacy")]
        public string Privacy { get; set; }
    }

    [DataContract]
    public class GetAlbums : IServiceResult
    {
        [DataMember(Name = "response")]
        public PhotoAlbum[] Albums { get; set; }

        public bool ResponseIsSuccessful()
        {
            return Albums != null;
        }
        public string Error { get; set; }
    }
}