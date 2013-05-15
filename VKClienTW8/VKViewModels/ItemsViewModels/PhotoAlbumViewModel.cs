using System;
using System.Collections.Generic;
using System.Linq;
using VKModel.Entities;

namespace VKViewModels.ItemsViewModels
{
    public class PhotoAlbumViewModel : BaseViewModel
    {
        private readonly PhotoAlbum _photoAlbum;

        private readonly PhotoViewModel _photoViewModel;
        public PhotoAlbumViewModel(PhotoAlbum photoAlbum, IEnumerable<Photo> photos)
        {
            _photoAlbum = photoAlbum;


            var photo = photos.FirstOrDefault(i => i.PhotoId == photoAlbum.ThumbId);
            _photoViewModel=new PhotoViewModel(photo);

            //ImageThumb=new ImageViewModel(photoViewModel.SourceSmall);
        }

        //private ImageViewModel imageViewModel;
        //public ImageViewModel ImageThumb
        //{
        //    get { return imageViewModel; }
        //    set { imageViewModel = value; OnPropertyChange("ImageViewModel"); }
        //}


        public string ThumbSource
        {
            get { return _photoViewModel.Source; }
        }

        public string AlbumId { get { return _photoAlbum.AlbumId;  } }

        public string ThumbId { get { return _photoAlbum.ThumbId; } }

        public string OwnerId { get { return _photoAlbum.OwnerId; } }

        public string Title { get { return _photoAlbum.Title; } }

        public string Description { get { return _photoAlbum.Description; } }

        public DateTime Created { get { return _photoAlbum.Created; } }

        public DateTime Updated { get { return _photoAlbum.Updated; } }

        public string Size { get { return _photoAlbum.Size; } }

        public string Privacy { get { return _photoAlbum.Privacy; } }
    }
}