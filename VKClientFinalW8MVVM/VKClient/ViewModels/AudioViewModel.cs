using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using VKClient.Models;
using VKClient.Models.Entities;
using VKClient.Resources;
using VKClient.Services;
using Windows.UI.Popups;

namespace VKClient.ViewModels
{
    class AudioViewModel : BaseViewModel
    {
        private UserProfile _user = new UserProfile();
        private ObservableCollection<Audio> _audios;
        private ObservableCollection<AudioCollection> _albums;
        public int _currentIndex;
        private string _audiosListError;
        private string _albumsListError;
        private AudioCollection _selectedAlbum = new AudioCollection();
        public UserProfile User { get; set; }
        public bool IsPlaying
        {
            get
            {
                return ViewModelLocator.AudioService.IsPlaying;
            }
            set
            {
            }
        }
        public string AudiosListError
        {
            get
            {
                return this._audiosListError;
            }
            set
            {
                if (this._audiosListError == value)
                {
                    return;
                }
                this._audiosListError = value;
                this.RaisePropertyChanged("AudiosListError");
            }
        }
        public string AlbumsListError
        {
            get
            {
                return this._albumsListError;
            }
            set
            {
                if (this._albumsListError == value)
                {
                    return;
                }
                this._albumsListError = value;
                this.RaisePropertyChanged("AlbumsListError");
            }
        }
        public RelayCommand<Audio> PlayAudioCommand
        {
            get;
            private set;
        }
        public RelayCommand AddAudiosCommand
        {
            get;
            private set;
        }
        public RelayCommand<double> OnChangedVolume { get; set; }

        public ObservableCollection<Audio> Audios
        {
            get
            {
                return this._audios;
            }
            set
            {
                if (this._audios == value)
                {
                    return;
                }
                this._audios = value;
                this.RaisePropertyChanged("Audios");
            }
        }
        public ObservableCollection<AudioCollection> Albums
        {
            get
            {
                return this._albums;
            }
            set
            {
                if (this._albums == value)
                {
                    return;
                }
                this._albums = value;
                this.RaisePropertyChanged("Albums");
            }
        }
        
        public AudioViewModel()
        {
            
            this.InitializeCommands();
            this.InitializeMessageInterception();
            //this.LoadAlbums();
        }
        private void InitializeCommands()
        {
            this.PlayAudioCommand = new RelayCommand<Audio>(delegate(Audio audio)
            {
                ViewModelLocator.AudioService.Play(audio);
                //NULL exception WTF, maybe audioservice == 0
                if (Audios == null)
                {
                    return;
                }
                else
                {
                    ViewModelLocator.AudioService.Playlist = this.Audios.ToList<Audio>();
                }
                
            });
        
            this.AddAudiosCommand = new RelayCommand(new Action(this.LoadAllAudios)); 
            this.OnChangedVolume = new RelayCommand<double>(delegate(double val)
                {
                    ViewModelLocator.AudioService.Volume = val;
                });
        }
        private void InitializeMessageInterception()
        {
            base.MessengerInstance.Register<LoginMessage>(this, new Action<LoginMessage>(this.OnLogin));
        }
        private void OnLogin(LoginMessage message)
        {
            if (message.Type == LoginType.LogOut && message.IsSuccess)
            {
                this.Audios = null;
                this.Albums = null;
                
                return;
            }
        /*    if (message.Type == LoginType.LogIn && message.IsSuccess)
            {
                this.LoadAlbums();
            } */
        }

        public double Volume
        {
            get
            {
                try
                {
                    return ViewModelLocator.AudioService.Volume;
                }
                catch (Exception ex)
                {
                    var msgDlg = new MessageDialog(ex.Message);
                    return ViewModelLocator.AudioService.Volume;
                }

            }
            set
            {
                try
                {
                    ViewModelLocator.AudioService.Volume = value;
                }
                catch (Exception ex)
                {
                    var msgDlg = new MessageDialog(ex.Message);
                }

            }
        }

        private async void LoadAlbums(string userId=null, string groupId = null)
        {
            base.IsWorking = true;
            this.AudiosListError = null;
            this.AlbumsListError = null;
            try
            {
                IList<AudioCollection> list = await ViewModelLocator.DataService.GetAlbums(userId, groupId);
                if (list != null && list.Count > 0)
                {
                    this.Albums = new ObservableCollection<AudioCollection>(list);
                }
                else
                {
                    this.Albums = new ObservableCollection<AudioCollection>();
                }
                this.Albums.Insert(0, new AudioCollection
                {
                    Id = "0",
                    Title = AppResources.Strings["MusicAllAudios"]
                });
            }
            catch (Exception)
            {
            }
            base.IsWorking = false;
        }
        
        private async void LoadAllAudios()
        {
            base.IsWorking = true;
            this.AudiosListError = null;
            if (this.Audios != null)
            {
                this.Audios.Clear();
            }
            try
            {
                IList<Audio> list = await ViewModelLocator.DataService.GetAudio(null,0, 0, User.Uid, null);
                if (list != null && list.Count > 0)
                {
                    this.Audios = new ObservableCollection<Audio>(list);
                    if (ViewModelLocator.AudioService.CurrentAudio == null)
                    {
                        ViewModelLocator.AudioService.CurrentAudio = list.First<Audio>();
                        ViewModelLocator.AudioService.Playlist = list;
                    }
                }
                else
                {
                    this.AudiosListError = AppResources.Strings["MyAudioListError"];
                    this.Audios = null;
                }
            }
            catch (Exception)
            {
                this.AudiosListError = AppResources.Strings["MyAudioListError"];
            }
            this.RaisePropertyChanged("TracksCountString");
            base.IsWorking = false;
        }

    }

}
