using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using VKClient.Helpers;
using VKClient.Models;
using VKClient.Models.Entities;
using VKClient.Resources;
using VKClient.VkControls;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace VKClient.ViewModels
{
    class ProfileViewPageViewModel : BaseViewModel
    {
        private Uri _profileImage;
        private UserProfile _userProfile;
        private string _name;
        private string _info;
        private PhotoItemToGridView _photoItemToGridView;
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
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value)
                {
                    return;
                }

                this._name = value;
                this.RaisePropertyChanged("Name");
            }
        }

        public string Info
        {
            get { return _info; }
            set
            {
                if (_info == value)
                {
                    return;
                }

                this._info = value;
                this.RaisePropertyChanged("Info");
            }
        }

        public Uri ProfileImage
        {
            get { return _profileImage; }
            set
            {
                if (this._profileImage == value)
                {
                    return;
                }

                this._profileImage = value;
                this.RaisePropertyChanged("ProfileImage");
            }
        }

        public UserProfile UserProfile
        {
            get { return _userProfile; }
            set
            {
                if (this._userProfile == value)
                {
                    return;
                }
                this._userProfile = value;
                this.RaisePropertyChanged("UserProfile");
            }
        }

        
       
        public RelayCommand LoadProfileInfoCommand { get; private set; }
        



        private void InitializeCommands()
        {
            LoadProfileInfoCommand = new RelayCommand(new Action(LoadInfo));
        }

        public ProfileViewPageViewModel(UserProfile user=null)
        {
            InitializeCommands();
            
        }

        public string MakeUpAnInfo(UserProfile user)
        {
            string TotalInfo = String.Empty;
            if (!String.IsNullOrEmpty(user.Country))
            {
                TotalInfo += "Страна:" + "\n" + user.Country + "\n";
            }
            if (!String.IsNullOrEmpty(user.City))
            {
                TotalInfo += "Город:" + "\n" + user.City + "\n";
            }
            if (!String.IsNullOrEmpty(user.BirthDate))
            {
                TotalInfo += "Дата рождения:" + "\n" + user.BirthDate + "\n";
            }
            if (!String.IsNullOrEmpty(user.UniversityName))
            {
                TotalInfo += "ВУЗ:" + "\n" + user.UniversityName;
            }
            return TotalInfo;
        }

        public async void LoadInfo()
        {
            base.IsWorking = true;
            try
            {
                if (UserProfile == null)
                {
                    UserProfile = await ViewModelLocator.DataService.GetUserProfile();
                }
                else
                {
                    UserProfile = await ViewModelLocator.DataService.GetUserProfile(UserProfile.Uid);
                }
                ProfileImage = UserProfile.PhotoBig;
                Name = UserProfile.Name;
                Info = MakeUpAnInfo(UserProfile);

                if (UserProfile != null)
                {
                    Items = new ObservableCollection<PhotoItemGroupToGridView>();
                    
                    var photoAlbums = new PhotoItemGroupToGridView();
                    photoAlbums.Title = "Фотоальбомы";
                    var videos = new PhotoItemGroupToGridView();
                    videos.Title = "Видеозаписи";
                    var friends = new PhotoItemGroupToGridView();
                    friends.Title = "Друзья";

                    List<PhotoAlbum> albumsGot = await ViewModelLocator.DataService.GetPhotoAlbums(UserProfile.Uid);
                    IList<Video> videosGot = await ViewModelLocator.DataService.GetVideo(null, UserProfile.Uid);
                    List<UserProfile> friendsGot = await ViewModelLocator.DataService.GetFriends(UserProfile.Uid, 4);
                    if (albumsGot != null)
                    {
                        for (int i = 0; i < 4 && albumsGot.Count > i; i++)
                        {
                            var PhotoItem = new PhotoItemToGridView
                            {
                                Title = albumsGot[i].Title,
                                Type = "Photo",
                                PhotoSrc = albumsGot[i].PhotoSrc,
                                Id = albumsGot[i].AlbumId.ToString()
                            };
                            RaisePropertyChanged("Items");
                            photoAlbums.Items.Add(PhotoItem);
                        }
                        Items.Add(photoAlbums);
                    }
                    if (videosGot != null)
                    {
                        for (int i = 0; i < 4 && videosGot.Count > i; i++)
                        {
                            var VideoItem = new PhotoItemToGridView
                            {
                                Title = videosGot[i].Title,
                                Type = "Video",
                                Player = videosGot[i].Player,
                                PhotoSrc = new Uri(videosGot[i].ImageMedium)
                            };
                            RaisePropertyChanged("Items");
                            videos.Items.Add(VideoItem);
                        }
                        Items.Add(videos);
                    }
                    if (friendsGot != null)
                    {
                        for (int i = 0; i < 4 && friendsGot.Count > i; i++)
                        {
                            var FriendItem = new PhotoItemToGridView
                            {
                                Title = friendsGot[i].Name,
                                Id = friendsGot[i].Uid,
                                Type = "Friend",
                                PhotoSrc = friendsGot[i].PhotoBig
                            };
                            RaisePropertyChanged("Items");
                            friends.Items.Add(FriendItem);
                        }
                        Items.Add(friends);
                    }
                    
                    
                    
                    
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

        }
        

    }
}
