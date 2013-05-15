using System;
using VKModel.Entities;

namespace VKViewModels.ItemsViewModels
{
    public class PhotoViewModel : BaseViewModel
    {
        private readonly Photo _photo;

        public PhotoViewModel(Photo photo)
        {
            _photo = photo ?? new Photo() ;
        }

        public string SourceMaxSize
        {
            get
            {
                if (SourceXxbig != null) return SourceXxbig;
                if (SourceXbig != null) return SourceXbig;
                if (SourceBig != null) return SourceBig;
                if (SourceSmall != null) return SourceSmall;
                return Source;
            }
        }

        public string Pid { get { return _photo.PhotoId; } }
        public string Aid { get { return _photo.AlbumId; } }

        public string Source { get { return _photo.Source; } }
        public string SourceBig { get { return _photo.SourceBig; } }
        public string SourceSmall { get { return _photo.SourceSmall; } }
        public string SourceXbig { get { return _photo.SourceXBig; } }
        public string SourceXxbig { get { return _photo.SourceX2Big; } }
        public DateTime Created { get { return _photo.Created; } }
    }
}