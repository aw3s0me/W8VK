using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using VKClient.Models;
using VKClient.Models.Entities;
using Windows.UI.Popups;

namespace VKClient.ViewModels
{
    public class GroupViewModel : BaseViewModel
    {
        private ObservableCollection<Group> _groups;
        private Group _selectedGroup;
        public RelayCommand LoadGroupsCommand { get; private set; }
        public UserProfile User { get; set; }
        public ObservableCollection<Group> Groups
        {
            get
            {
                return this._groups;
            }
            set
            {
                if (this._groups == value)
                {
                    return;
                }
                this._groups = value;
                this.RaisePropertyChanged("Groups");
            }
        }
        public Group SelectedGroup
        {
            get
            {
                return this._selectedGroup;
            }
            set
            {
                if (this._selectedGroup == value)
                {
                    return;
                }
                this._selectedGroup = value;
                this.RaisePropertyChanged("SelectedGroup");
            }
        }


        private async void LoadGroups()
        {
            base.IsWorking = true;
            try
            {
                List<Group> list = await ViewModelLocator.DataService.GetGroups(User.Uid);
                if (list != null)
                {
                    this.Groups = new ObservableCollection<Group>(list);
                    this.RaisePropertyChanged("Groups");
                    this.SelectedGroup = list.FirstOrDefault<Group>();
                }
                if (list != null)
                {
                    int arg_E3_0 = list.Count;
                }
            }
            catch (Exception ex)
            {
                var msgDlg = new MessageDialog(ex.Message);
                this.RaisePropertyChanged("GroupsCountString");
                base.IsWorking = false;
            }
        }

        private void InitializeCommands()
        {
            LoadGroupsCommand = new RelayCommand(new Action(LoadGroups));
        }

        public GroupViewModel()
        {
            InitializeCommands();
        }

    }
}
