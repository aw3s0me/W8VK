using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using VKClient.Models.Entities;
using Windows.UI.Popups;

namespace VKClient.ViewModels
{
    class MessagePageViewModel : BaseViewModel
    {
        private ObservableCollection<UserProfile> _friends;
        private ObservableCollection<UserProfile> _onlineFriends; 
        private UserProfile _selectedFriend;
        public RelayCommand LoadFriendsCommand { get; private set; }
        public RelayCommand LoadUidsUsersCommand { get; private set; }
        public List<long> OnlineUidsUserList { get; set; } 
        public UserProfile User { get; set; }
        public ObservableCollection<UserProfile> Friends
        {
            get
            {
                return this._friends;
            }
            set
            {
                if (this._friends == value)
                {
                    return;
                }
                this._friends = value;
                this.RaisePropertyChanged("Friends");
            }
        }
        public ObservableCollection<UserProfile> OnlineFriends
        {
            get
            {
                return this._onlineFriends;
            }
            set
            {
                if (this._onlineFriends == value)
                {
                    return;
                }
                this._onlineFriends = value;
                this.RaisePropertyChanged("OnlineFriends");
            }
        }
        public UserProfile SelectedFriend
        {
            get
            {
                return this._selectedFriend;
            }
            set
            {
                if (this._selectedFriend == value)
                {
                    return;
                }
                this._selectedFriend = value;
                this.RaisePropertyChanged("SelectedFriend");
            }
        }

        private async void LoadOnlineUidsUsers()
        {
            
        }

        private async void LoadFriends()
        {
            base.IsWorking = true;
            try
            {
                List<UserProfile> list = await ViewModelLocator.DataService.GetFriends(User.Uid, 0, 0);
                if (list != null)
                {
                    this.Friends = new ObservableCollection<UserProfile>(list);
                    this.RaisePropertyChanged("Friends");
                    this.SelectedFriend = list.FirstOrDefault<UserProfile>();
                }
                if (list != null)
                {
                    int arg_E3_0 = list.Count;
                }
            }
            catch (Exception ex)
            {
                var msgDlg = new MessageDialog(ex.Message);
                this.RaisePropertyChanged("FriendsCountString");
                base.IsWorking = false;
            }
        }

        private void InitializeCommands()
        {
            LoadFriendsCommand = new RelayCommand(new Action(LoadFriends));
        }

        public MessagePageViewModel()
        {
            InitializeCommands();
        }

    }
}
