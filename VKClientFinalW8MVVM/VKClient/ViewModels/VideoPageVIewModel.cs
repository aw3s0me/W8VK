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

namespace VKClient.ViewModels
{
    class VideoPageVIewModel : BaseViewModel
    {
        private UserProfile _user;
        private ObservableCollection<VideoItemToGridView> _videos;
        private VideoItemToGridView _selectedVideo;

        public VideoItemToGridView SelectedVideo
        {
            get { return _selectedVideo; }
            set
            {
                if (_selectedVideo == value)
                {
                    return;
                }
                else if (value == null)
                {
                    return;
                }
                _selectedVideo = value;
            }

        }
        public ObservableCollection<VideoItemToGridView> Videos
        {
            get { return _videos; }
            set
            {
                if (_videos == value)
                {
                    return;
                }
                else if (value == null)
                {
                    return;
                }

                _videos = value;
                this.RaisePropertyChanged("Videos");
            }
        }
        public UserProfile User { get; set; }
        public RelayCommand AddVideosCommand { get; private set; }
        //public RelayCommand PlayVideoCommand { get; private set; }

        private void InitializeCommands()
        {
            AddVideosCommand = new RelayCommand(new Action(this.LoadVideos));
            //PlayVideoCommand = new RelayCommand(new Action(this.PlayVideo));
        }

        public VideoPageVIewModel()
        {
            InitializeCommands();
        }

        private async void LoadVideos()
        {
            base.IsWorking = true;
            try
            {
                if (User != null)
                {
                    _videos = new ObservableCollection<VideoItemToGridView>();
                    IList<Video> videosGot = await ViewModelLocator.DataService.GetVideo(null, User.Uid);

                    for (int i = 0; i < videosGot.Count; i++)
                    {
                        var VideoItem = new VideoItemToGridView
                            {
                                PhotoUri = new Uri(videosGot[i].ImageMedium),
                                PlayerUri = new Uri(videosGot[i].Player),
                                Title = videosGot[i].Title,
                                Vid = videosGot[i].Id
                            };
                        _videos.Add(VideoItem);
                    }
                    RaisePropertyChanged("Videos");
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
