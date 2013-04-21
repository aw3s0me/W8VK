using System;

namespace VKModel
{
    namespace Entities
    {
        public class PhotoAlbum : IEntity
        {
            public string AlbumId { get; set; }
            public string ThumbId { get; set; }
            public string OwnerId { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Size { get; set; }
            public string Privacy { get; set; }
            public DateTime Created { get; set; }
            public DateTime Updated { get; set; }

        }
    }
}
