using System;
using System.Collections.Generic;
using System.Linq;
using VKCore;
using VKModel.Entities;
using VKViewModels.ItemsViewModels;
using VKViewModels.Resources;
using Windows.ApplicationModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace VKViewModels
{
    public class PhotoAlbumPageViewModel : BaseViewModel
    {
        public PhotoAlbumPageViewModel()
        {
            if (DesignMode.DesignModeEnabled)
            {
                var user = new User()
                {
                    Uid = "1",
                    FirstName = "Имя",
                    LastName = "Фамилия",
                    NickName = "Ник",
                    PhotoMedium = "http://cs615.vkontakte.ru/u457829/a_d81aa14a.jpg"
                };
                UserViewModel = new UserViewModel(user);

                var album = new PhotoAlbum()
                {
                    AlbumId = "1",
                    Created = new DateTime(2000, 10, 10),
                    Description = "Description",
                    Title = "Title Title Title Title Title Title Title",
                    OwnerId = user.Uid,
                    ThumbId = "3",
                    Size = "10"
                };

                var list = new List<Photo>()
                {
                    new Photo()
                    {
                        AlbumId = album.AlbumId,
                        Created = new DateTime(2000, 10, 10),
                        OwnerId = user.Uid,
                        PhotoId = "3",
                        Source = "http://cs615.vkontakte.ru/u457829/a_d81aa14a.jpg",
                        SourceBig = "http://cs615.vkontakte.ru/u457829/a_d81aa14a.jpg",
                        SourceSmall = "http://cs615.vkontakte.ru/u457829/a_d81aa14a.jpg",
                        SourceXBig = "http://cs615.vkontakte.ru/u457829/a_d81aa14a.jpg",
                        SourceX2Big = "http://cs615.vkontakte.ru/u457829/a_d81aa14a.jpg",
                    }
                };

                PhotoAlbums = new List<PhotoAlbumViewModel>()
                {
                    new PhotoAlbumViewModel(album, list),
                    new PhotoAlbumViewModel(album, list),
                    new PhotoAlbumViewModel(album, list),
                    new PhotoAlbumViewModel(album, list),
                    new PhotoAlbumViewModel(album, list),
                    new PhotoAlbumViewModel(album, list),
                    new PhotoAlbumViewModel(album, list),
                    new PhotoAlbumViewModel(album, list),
                };
            }
        }

        private UserViewModel userViewModel;
        public UserViewModel UserViewModel
        {
            get { return userViewModel; }
            set { userViewModel = value; OnPropertyChange("UserViewModel"); }
        }

        private List<PhotoAlbumViewModel> photoAlbums;
        public List<PhotoAlbumViewModel> PhotoAlbums
        {
            get { return photoAlbums; }
            set { photoAlbums = value; OnPropertyChange("PhotoAlbums"); }
        }

        private PhotoAlbumViewModel selectedPhotoAlbum;
        public PhotoAlbumViewModel SelectedPhotoAlbum
        {
            get { return selectedPhotoAlbum; }
            set
            {
                selectedPhotoAlbum = value;
                OnPropertyChange("SelectedPhotoAlbum");
                if (value != null)
                {
                    GetService<ISimpleNavigationService>().NavigateToPhotosViewPage(UserViewModel.Uid, value.AlbumId);
                }
            }
        }

        public override void OnNavigatedTo(Page page, NavigationEventArgs e)
        {
            base.OnNavigatedTo(page, e);

            var uid = GetStateOrUrlParam("uid");

            GetService<IVkApi>().GetUserProfile(uid, GetUserProfileComplete, GetUserProfileError);
        }

        public void GetUserProfileComplete(User user)
        {
            userViewModel = new UserViewModel(user);
            OnPropertyChange("UserViewModel");

            GetService<IVkApi>().GetPhotoAlbums(user.Uid, (albums) =>
            {
                GetService<IVkApi>().GetPhotos(user.Uid,albums.Select(i => i.ThumbId).ToList(), 
                (photos)=>
                {
                    photoAlbums = albums.Select(i => new PhotoAlbumViewModel(i, photos)).ToList();
                    OnPropertyChange("PhotoAlbums");
                },
                (error)=> {GetService<ICommonErrorHandler>().HandleError(error); });
            }, 
            (error) => { GetService<ICommonErrorHandler>().HandleError(error); });
        }

        public void GetUserProfileError(Error error)
        {
            GetService<ICommonErrorHandler>().HandleError(error);
        }

        public void PinToStart()
        {
            var url = "/PhotoAlbumPage.xaml?pinToStart=1&uid=" + userViewModel.Uid;
            var shellTile = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains(url));

            if (shellTile != null)
            {
                shellTile.Delete();
            }

            var standardTileData = new StandardTileData()
            {

                BackgroundImage = new Uri(userViewModel.PhotoMedium, UriKind.Absolute),
                BackBackgroundImage = new Uri(userViewModel.PhotoMedium, UriKind.Absolute),
                Title = userViewModel.FirstName,
                BackContent = userViewModel.LastName,
                BackTitle = AppResource.PhotoAlbums,
                Count = PhotoAlbums.Count
            };
            ShellTile.Create(new Uri(url, UriKind.Relative), standardTileData);
        }

        public void GoToHomePage()
        {
            GetService<ISimpleNavigationService>().NavigateToHomePage();
        }
    }
}