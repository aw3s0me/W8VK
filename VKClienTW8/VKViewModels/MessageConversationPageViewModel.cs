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
    public class MessageConversationPageViewModel : BaseViewModel
    {
        public MessageConversationPageViewModel()
        {
            if (DesignMode.DesignModeEnabled)
            {
                FillDesignTimeData();
            }
        }

        private void FillDesignTimeData()
        {
            var user = new User() { FirstName = "Имя", LastName = "Фамилия", NickName = "Ник", PhotoMedium = "http://cs615.vkontakte.ru/u457829/a_d81aa14a.jpg", Uid = "12345" };
            var users = new[] { user };
            var message = new MessageViewModel(new Message()
            {
                Body = "Message body. Message body. Message body. Message body. Message body. Message body. Message body. Message body. Message body. Message body. Message body. Message body. ",
                Uid = user.Uid,
                Title = "title of message",
                Date = new DateTime(2010, 05, 05, 04, 04, 04),
                IsNewMsg = false,
                MsgId = "0123"
            }, users, "0123");

            var messageOut = new MessageViewModel(new Message()
            {
                Body = "Message",
                Uid = user.Uid,
                Title = "title",
                Date = new DateTime(2010, 05, 05, 04, 04, 04),
                IsNewMsg = false,
                MsgId = "0123",
                IsOutMsg = true,
            }, users, "12345");
            messages = new List<MessageViewModel>()
                 {
                     messageOut,
                     message,
                     messageOut,
                     message,
                     messageOut,
                     message,
                     messageOut,
                     message,
                     messageOut,
                     message,
                     messageOut,
                     message,
                     messageOut,
                     message,
                 };
             
        }

        private string sendMessageEnabled;
        public string SendMessageEnabled
        {
            get { return sendMessageEnabled; }
            set
            {
                sendMessageEnabled = value;
                OnPropertyChange("SendMessageEnabled");
            }
        }

        private UserViewModel userViewModel;

        public UserViewModel UserViewModel
        {
            get { return userViewModel; }
            set { userViewModel = value; OnPropertyChange("UserViewModel"); }
        }

        private List<MessageViewModel> messages;

        private string newMessageText;

        public string NewMessageText
        {
            get { return newMessageText; }
            set
            {
                newMessageText = value; 
                OnPropertyChange("NewMessageText");
                NewMessageTextUpdateAction.Invoke(value);
            }
        }

        public List<MessageViewModel> Messages
        {
            get { return messages; }
            set { messages = value; OnPropertyChange("Messages"); }
        }

        /*public int ScreenWidth
        {
            get
            {
                return base.IsPortraitOrientation ? 440 : 600;
            }
        } */

        /*public void OrientationChanged(OrientationChangedEventArgs e)
        {
            OnPropertyChange("ScreenWidth");
        } */

        public void SendMessage()
        {
            var uid = GetStateOrUrlParam("uid");
            var textmessage = NewMessageText;
            this.GetService<IVkApi>().SendMessage(uid, textmessage, ()=>
            {
                NewMessageText = String.Empty;
                UpdateMessages();
            }, errorResponse => this.GetService<ICommonErrorHandler>().HandleError(errorResponse));
        }

        public Action UpdateCompleteAction { get; set; }
        public Action<String> NewMessageTextUpdateAction { get; set; }

        public bool SendMessageButtonIsEnabled
        {
            get { return newMessageText.Length > 0; }
        }


        public override void OnNavigatedTo(Page page, NavigationEventArgs e)
        {
            base.OnNavigatedTo(page, e);

            UpdateMessages();
        }

        
        

        private void UpdateMessages()
        {
            var uid = GetStateOrUrlParam("uid");
            var currentUid = GetService<IVkApi>().GetCurrentUid();


            this.GetService<IVkApi>().GetUserProfile(uid, (user) =>
            {
                UserViewModel = new UserViewModel(user);
                GetService<IVkApi>().GetMessageConversation(uid, resultMessages =>
                {
                    var uids = resultMessages.Select(i => i.Uid).ToList();
                    uids.Add(currentUid);

                    GetService<IVkApi>().GetUserProfiles(uids, profiles =>
                    {
                        var messageViewModels = resultMessages.OrderBy(i => i.Date).Select(
                            i => new MessageViewModel(i, profiles, currentUid)).
                            ToList();

                        Messages = messageViewModels;
                        if(UpdateCompleteAction!=null)
                            UpdateCompleteAction.Invoke();
                    },
                        error =>
                        {

                        });
                },
                error =>
                {
                    GetService<ICommonErrorHandler>().HandleError(error);
                });
            },
            error =>
            {
                GetService<ICommonErrorHandler>().HandleError(error);
            });
        }


        public void Refresh()
        {
            UpdateMessages();
        }

        public void GoToHome()
        {
            GetService<ISimpleNavigationService>().NavigateToHomePage();
        }

        public void PinToStart()
        {
            var uid = GetStateOrUrlParam("uid");
            GetService<IVkApi>().GetUserProfile(uid,user =>
            {
                var userViewModel = new UserViewModel(user);

                var shellTile = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains("/MessageConversationPage.xaml?pinToStart=1&uid=" + userViewModel.Uid));

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
                    BackTitle = AppResource.Conversation,
                    Count = 10
                };
                ShellTile.Create(new Uri("/MessageConversationPage.xaml?pinToStart=1&uid=" + userViewModel.Uid, UriKind.Relative), standardTileData);
            },
            error =>
            {
                GetService<ICommonErrorHandler>().HandleError(error);
            });
        }
    }
}
