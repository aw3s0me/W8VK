using System.Windows.Input;
using VKCore;
using VKModel.Entities;
using VKViewModels;
using VKViewModels.ItemsViewModels;
using Windows.ApplicationModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;


namespace VkViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        private UserViewModel userViewModel;
        public UserViewModel UserViewModel
        {
            get { return userViewModel; }
        }

        public HomePageViewModel()
        {
            if (DesignMode.DesignModeEnabled)
            {
                userViewModel =
                    new UserViewModel(new User()
                    {
                        FirstName = "Имя",
                        LastName = "Фамилия",
                        NickName = "Ник",
                        PhotoMedium = "http://cs615.vkontakte.ru/u457829/a_d81aa14a.jpg"
                    });
            }

            PhotosViewCommand=new DelegateCommand(PhotoViewCommand);
            MessagesViewCommand = new DelegateCommand((obj) => GetService<ISimpleNavigationService>().NavigateToMessagesPage());
            FriendsViewCommand = new DelegateCommand(obj => GetService<ISimpleNavigationService>().NavigateToFriendsPage());

        }

        public ICommand PhotosViewCommand { get; set; }

        private void PhotoViewCommand(object parameter)
        {
            GetService<ISimpleNavigationService>().NavigateToPhotoAlbumPage(GetService<IVkApi>().GetCurrentUid());
        }

        public ICommand MessagesViewCommand { get; set; }

        public ICommand FriendsViewCommand { get; set; }


        public override void OnNavigatedTo(Page page, NavigationEventArgs e)
        {
            base.OnNavigatedTo(page,e);
            if(userViewModel==null)
            {
                GetService<IVkApi>().GetCurrentUserProfile(GetUser, (error)=>
                {
                    //GetService<ICommonErrorHandler>().HandleError(error);
                });
            }
        }

        private void GetUser(User user)
        {
            userViewModel=new UserViewModel(user);
            OnPropertyChange("UserViewModel");
        }
    }
}
