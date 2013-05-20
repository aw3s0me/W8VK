using System;

namespace VKClient.Models
{
    namespace Entities
    {
        public class PhotoAlbum : IEntity
        {
            public long AlbumId { get; set; }
            public long ThumbId { get; set; }
            public long OwnerId { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public long Size { get; set; }
            public string Privacy { get; set; }
            public DateTime Created { get; set; }
            public DateTime Updated { get; set; }
            public Uri PhotoSrc { get; set; }

        }
    }
}
