using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using VKClient.Helpers;
using VKClient.Models.Entities;
using VKClient.Resources;
using VKClient.VkControls;
using Windows.UI.Popups;

namespace VKClient.ViewModels
{
    public class PhotoAlbumsViewModel : BaseViewModel
    {
        private UserProfile _user;
        public UserProfile User { get; set; }
        public RelayCommand AddPhotoAlbumsCommand { get; private set; }
        public RelayCommand LoadPhotosFromAlbumCommand { get; private set; }
        private ObservableCollection<PhotoItemToListView> _photoAlbums;
        private ObservableCollection<PhotoItemGroupToGridView> _items; 
        private ObservableCollection<PhotoItemToPhotoGridView> _photos;
        private PhotoItemToListView _selectedPhoto;
        public PhotoItemToListView SelectedPhoto
        {
            get { return _selectedPhoto; }
            set
            {
                if (_selectedPhoto == value)
                {
                    return;
                }
                else if (value == null)
                {
                    return;
                }
                _selectedPhoto = value;
            }

        }

        public ObservableCollection<PhotoItemGroupToGridView> Items
        {
            get { return _items; }
            set
            {
                if (_items == value)
                {
                    return;
                }
                else if (value == null)
                {
                    return;
                }

                _items = value;
                this.RaisePropertyChanged("Items");
            }
        }

        public ObservableCollection<PhotoItemToListView> PhotoAlbums
        {
            get { return _photoAlbums; }
            set
            {
                if (_photoAlbums == value)
                {
                    return;
                }
                else if (value == null)
                {
                    return;
                }

                _photoAlbums = value;
                this.RaisePropertyChanged("PhotoAlbums");
            }
        }

        public ObservableCollection<PhotoItemToPhotoGridView> Photos
        {
            get { return _photos; }
            set
            {
                if (_photos == value)
                {
                    return;
                }
                else if (value == null)
                {
                    return;
                }

                _photos = value;
                this.RaisePropertyChanged("Photos");
            }
        } 


        /*private PhotoItemToGridView _photoItemToGridView;
        private ObservableCollection<PhotoItemGroupToGridView> _items;
        



        public PhotoItemToGridView PhotoItemGroupToGridView
        {
            get { return _photoItemToGridView; }
            set
            {
                if (_photoItemToGridView == value)
                {
                    return;
                }
                else if (value == null)
                {
                    return;
                }

                _photoItemToGridView = value;
                this.RaisePropertyChanged("Items");
            }
        }

        public ObservableCollection<PhotoItemGroupToGridView> Items
        {
            get { return _items; }
            set
            {
                if (_items == value)
                {
                    return;
                }
                else if (value == null)
                {
                    return;
                }

                _items = value;
                this.RaisePropertyChanged("Items");
            }
        } */


        private void InitializeCommands()
        {
            AddPhotoAlbumsCommand = new RelayCommand(new Action(this.LoadAllPhotoAlbums));
            LoadPhotosFromAlbumCommand = new RelayCommand(new Action(this.LoadPhotosFromAlbum));
        }

        public PhotoAlbumsViewModel()
        {
            InitializeCommands();
        }

    /*    private async void LoadAllPhotoAlbums()
        {
            base.IsWorking = true;
            try
            {
                if (User != null)
                {
                    Items = new ObservableCollection<PhotoItemGroupToGridView>();

                    var photoAlbums = new PhotoItemGroupToGridView();
                    photoAlbums.Title = "Фотоальбомы";
                    

                    List<PhotoAlbum> albumsGot = await ViewModelLocator.DataService.GetPhotoAlbums(User.Uid);
                    
                    for (int i = 0; i < albumsGot.Count; i++)
                    {
                        var PhotoItem = new PhotoItemToGridView
                        {
                            Title = albumsGot[i].Title,
                            Id = albumsGot[i].AlbumId,
                            PhotoSrc = albumsGot[i].PhotoSrc
                        };
                        RaisePropertyChanged("Items");
                        photoAlbums.Items.Add(PhotoItem);
                    }
                   
                    Items.Add(photoAlbums);
                    RaisePropertyChanged("Items");
                }
                else throw new Exception("Ошибка");
            }
            catch (Exception ex)
            {
                string messageBoxText = AppResources.Strings["MainErrorLoadProfile"];
                MessageBoxHelper.Show(messageBoxText + " " + ex.ToString());

                base.IsWorking = false;
            }
        } */


        private async void LoadPhotosFromAlbum()
        {
            base.IsWorking = true;
            try
            {
                if (User != null | SelectedPhoto!=null) 
                {
                    //Items = new ObservableCollection<PhotoItemGroupToGridView>();
                    _photos = new ObservableCollection<PhotoItemToPhotoGridView>();

                    List<Photo> photosGot = await ViewModelLocator.DataService.GetPhotos(User.Uid, SelectedPhoto.Aid.ToString());

                    for (int i = 0; i < photosGot.Count; i++)
                    {
                        var PhotoItem = new PhotoItemToPhotoGridView
                            {
                                Aid = photosGot[i].PhotoId.ToString(),
                                Height = photosGot[i].Height,
                                Width = photosGot[i].Width,
                                PhotoMiniUri = photosGot[i].SourceSmall,
                                PhotoUri = photosGot[i].SourceBig
                            };
                        _photos.Add(PhotoItem);
                    }
                    RaisePropertyChanged("Photos");
                }
            }
            catch (Exception ex)
            {
                string messageBoxText = AppResources.Strings["MainErrorLoadPhotos"];
                MessageBoxHelper.Show(messageBoxText + " " + ex.ToString());

                base.IsWorking = false;
            }
        }


        private async void LoadAllPhotoAlbums()
        {
            base.IsWorking = true;
            try
            {
                if (User != null)
                {
                    _photoAlbums = new ObservableCollection<PhotoItemToListView>();
                    List<PhotoAlbum> albumsGot = await ViewModelLocator.DataService.GetPhotoAlbums(User.Uid);

                    for (int i = 0; i < albumsGot.Count; i++)
                    {
                        var PhotoItem = new PhotoItemToListView
                            {
                                PhotoUri = albumsGot[i].PhotoSrc,
                                Title = albumsGot[i].Title,
                                Aid = albumsGot[i].AlbumId.ToString()
                            };
                        _photoAlbums.Add(PhotoItem);
                    }
                    RaisePropertyChanged("PhotoAlbums");
                }
                else throw new Exception("Ошибка");
            }
            catch (Exception ex)
            {
                string messageBoxText = AppResources.Strings["MainErrorLoadProfile"];
                MessageBoxHelper.Show(messageBoxText + " " + ex.ToString());

                base.IsWorking = false;
            }
        }
    }
}
