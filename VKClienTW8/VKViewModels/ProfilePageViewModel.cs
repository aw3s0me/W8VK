using System;
using System.Windows.Input;
using VKCore;
using VKModel.Entities;
using VKViewModels.ItemsViewModels;
using VKViewModels.Resources;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace VKViewModels
{
    public class ProfilePageViewModel : BaseViewModel
    {
        public ProfilePageViewModel()
        {
            OpenMessageConversationCommand=new DelegateCommand(MessageConverClick);
            OpenPhotosCommand=new DelegateCommand(obj=>GoToPhotoPage());
            OpenFriendsCommand=new DelegateCommand(obj=> { });
        }

        public void MessageConverClick(object obj)
        {
            GetService<ISimpleNavigationService>().NavigateToMessageConversationPage(UserViewModel.Uid);
        }

        private UserViewModel userViewModel;
        public UserViewModel UserViewModel
        {
            get { return userViewModel; }
            set { userViewModel = value; OnPropertyChange("UserViewModel"); }
        }

        public override void OnNavigatedTo(Page page, NavigationEventArgs e)
        {
            base.OnNavigatedTo(page,e);

            var uid = GetStateOrUrlParam("uid");
            userViewModel=new UserViewModel(new User() { Uid = uid});
            GetService<IVkApi>().GetUserProfile(uid, GetUserProfileComplete, GetUserProfileError);
        }

        public ICommand OpenMessageConversationCommand { get; set; }

        public ICommand OpenPhotosCommand { get; set; }

        public ICommand OpenFriendsCommand { get; set; }

        public void GetUserProfileComplete(User user)
        {
            userViewModel=new UserViewModel(user);
            OnPropertyChange("UserViewModel");
        }

        public void GetUserProfileError(Error error)
        {
            GetService<ICommonErrorHandler>().HandleError(error);
        }

        public void PinToStart()
        {
            var shellTile = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains("/ProfilePage.xaml?pinToStart=1&uid=" + userViewModel.Uid));

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
                BackTitle = AppResource.Profile,
                
            };
            ShellTile.Create(new Uri("/ProfilePage.xaml?pinToStart=1&uid=" + userViewModel.Uid, UriKind.Relative), standardTileData);
        }

        public void GoToHome()
        {
            GetService<ISimpleNavigationService>().NavigateToHomePage();
        }

        public void GoToPhotoPage()
        {
            GetService<ISimpleNavigationService>().NavigateToPhotoAlbumPage(UserViewModel.Uid);
        }
    }
}