using System.Collections.Generic;
using System.Linq;
using VKCore;
using VKModel.Entities;
using VKViewModels;
using VKViewModels.ItemsViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel;

namespace VkViewModels
{
    public class FriendsPageViewModel : BaseViewModel
    {
        private List<UserViewModel> _friendsCollection;
        public List<UserViewModel> ListFriends
        {
            get { return _friendsCollection??new List<UserViewModel>(); }
        }

        public FriendsPageViewModel()
        {
             //if(DesignerProperties.IsInDesignTool)
             if(DesignMode.DesignModeEnabled)  //Windows 8 version
             {
                 _friendsCollection=new List<UserViewModel>
                     {
                     new UserViewModel(new User { FirstName = "Имя", LastName = "Фамилия", NickName = "Ник", PhotoMedium = "http://cs615.vkontakte.ru/u457829/a_d81aa14a.jpg"}),
                     new UserViewModel(new User { FirstName = "Имя", LastName = "Фамилия", NickName = "Ник", PhotoMedium = "http://cs615.vkontakte.ru/u457829/a_d81aa14a.jpg" }),
                     new UserViewModel(new User { FirstName = "Имя", LastName = "Фамилия", NickName = "Ник", PhotoMedium = "http://cs615.vkontakte.ru/u457829/a_d81aa14a.jpg" }),
                     new UserViewModel(new User { FirstName = "Имя", LastName = "Фамилия", NickName = "Ник", PhotoMedium = "http://cs615.vkontakte.ru/u457829/a_d81aa14a.jpg" }),
                     new UserViewModel(new User { FirstName = "Имя", LastName = "Фамилия", NickName = "Ник", PhotoMedium = "http://cs615.vkontakte.ru/u457829/a_d81aa14a.jpg" }),
                     new UserViewModel(new User { FirstName = "Имя", LastName = "Фамилия", NickName = "Ник", PhotoMedium = "http://cs615.vkontakte.ru/u457829/a_d81aa14a.jpg" }),
                     new UserViewModel(new User { FirstName = "Имя", LastName = "Фамилия", NickName = "Ник", PhotoMedium = "http://cs615.vkontakte.ru/u457829/a_d81aa14a.jpg" }),
                     new UserViewModel(new User { FirstName = "Имя", LastName = "Фамилия", NickName = "Ник", PhotoMedium = "http://cs615.vkontakte.ru/u457829/a_d81aa14a.jpg" }),
                     new UserViewModel(new User { FirstName = "Имя", LastName = "Фамилия", NickName = "Ник", PhotoMedium = "http://cs615.vkontakte.ru/u457829/a_d81aa14a.jpg" }),
                     new UserViewModel(new User { FirstName = "Имя", LastName = "Фамилия", NickName = "Ник", PhotoMedium = "http://cs615.vkontakte.ru/u457829/a_d81aa14a.jpg" }),
                     new UserViewModel(new User { FirstName = "Имя", LastName = "Фамилия", NickName = "Ник", PhotoMedium = "http://cs615.vkontakte.ru/u457829/a_d81aa14a.jpg" }),
                     new UserViewModel(new User { FirstName = "Имя", LastName = "Фамилия", NickName = "Ник", PhotoMedium = "http://cs615.vkontakte.ru/u457829/a_d81aa14a.jpg" }),
                     new UserViewModel(new User { FirstName = "Имя", LastName = "Фамилия", NickName = "Ник", PhotoMedium = "http://cs615.vkontakte.ru/u457829/a_d81aa14a.jpg" }),
                 };
             }
        }

        public override void OnNavigatedTo(Page page, NavigationEventArgs e)
        {
            base.OnNavigatedTo(page, e);

            if (_friendsCollection == null)
            {
                GetService<IVkApi>().GetFriends(GetFriendsAction, error =>
                {
                    //GetService<ICommonErrorHandler>().HandleError(error);
                });
            }
        }
        

        private void GetFriendsAction(List<User> friends)
        {
            _friendsCollection = friends.Select(i => new UserViewModel(i)).ToList();
            OnPropertyChange("ListFriends");
        }

        public void SelectFriend(UserViewModel userViewModel)
        {
            //GetService<NavigationService>().NavigateToProfilePage(userViewModel.Uid);
        }
    }
}