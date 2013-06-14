using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VKClient.Models;
using VKClient.Models.Entities;
using VKClient.ViewModels;
using VKClient.VkControls;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Automation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Grouped Items Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234231

namespace VKClient.Views
{
    /// <summary>
    /// A page that displays a grouped collection of items.
    /// </summary>
    public sealed partial class ProfileViewPage : VKClient.Common.LayoutAwarePage
    {
        private string _currentViewModelType;

        public ProfileViewPage()
        {
            this.InitializeComponent();
            base.DataContext = new ProfileViewPageViewModel();   
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            // TODO: Assign a collection of bindable groups to this.DefaultViewModel["Groups"]
        }

        

        private void ProfileAudios(object sender, RoutedEventArgs e)
        {
            //ViewModelLocator.TempUserId = ((ProfileViewPageViewModel) base.DataContext).UserProfile.Uid;
            _currentViewModelType = "Audio";
            this.Frame.Navigate(typeof(AudioViewPage), ((ProfileViewPageViewModel)base.DataContext).UserProfile.Uid);
            //this.Frame.Navigate(typeof (FriendsViewPage));
            //this.Frame.Navigate(new AudioViewPage("fe"));

            //ViewModelLocator.StaticMain.GoToPageCommand.Execute("/AudioViewPage" + "|" + ((ProfileViewPageViewModel)base.DataContext).UserProfile.Uid);
            /*    ((ProfileViewPageViewModel)DataContext).MessengerInstance.Send<NavigateToPageMessage>(new NavigateToPageMessage
            {
                Page = "/MyMusicView"
            }); */
        }

        private void GroupClicked(object sender, RoutedEventArgs e)
        {
            _currentViewModelType = "Group";
            this.Frame.Navigate(typeof(GroupsViewPage), ((ProfileViewPageViewModel)base.DataContext).UserProfile.Uid);
        }

        private void NewsClick(object sender, RoutedEventArgs e)
        {
            _currentViewModelType = "News";
            this.Frame.Navigate(typeof(GroupsViewPage), ((ProfileViewPageViewModel)base.DataContext).UserProfile.Uid);
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                var parameter = e.Parameter as string;
                if (parameter == null || parameter == String.Empty)
                {
                    ((ProfileViewPageViewModel) base.DataContext).LoadProfileInfoCommand.Execute(null);
                }
                else
                {
                    ((ProfileViewPageViewModel) base.DataContext).UserProfile = new UserProfile {Uid = parameter};
                    ((ProfileViewPageViewModel)base.DataContext).LoadProfileInfoCommand.Execute(null);
                }
            }
            catch (Exception ex)
            {
                var msgDlg = new MessageDialog(ex.Message);
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            // NavigationEventArgs returns destination page
            Page destinationPage = e.Content as Page;
            if (destinationPage != null)
            {
                switch (_currentViewModelType)
                {
                    case "Audio":
                        {
                            //((AudioViewPage)destinationPage).UserId = ((ProfileViewPageViewModel)base.DataContext).UserProfile.Uid;
                        }
                        break;
                    default:
                        break;
                }
                // Change property of destination page
                
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                var group = (sender as FrameworkElement).DataContext;

                // Navigate to the appropriate destination page, configuring the new page
                // by passing required information as a navigation parameter

                //this.Frame.Navigate(typeof(GroupDetailPage), ((SampleDataGroup)group).UniqueId);

                switch (((PhotoItemGroupToGridView) group).Title)
                {
                    case ("Фотоальбомы"):
                        {
                            Frame.Navigate(typeof(NewPhotoAlbumsViewPage), ((ProfileViewPageViewModel)base.DataContext).UserProfile.Uid);
                            break;
                        }
                    case ("Видеозаписи"):
                        {
                            Frame.Navigate(typeof(VideoViewPage), ((ProfileViewPageViewModel)base.DataContext).UserProfile.Uid);
                            break;
                        }
                    case ("Друзья"):
                        {
                            Frame.Navigate(typeof(FriendsViewPage), ((ProfileViewPageViewModel)base.DataContext).UserProfile.Uid);
                            break;
                        }
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                var msgDlg = new MessageDialog(ex.Message);
            }
        }

        private void GridViewToFrinedsPhoto_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                var item = ((PhotoItemToGridView)e.ClickedItem);
                string type = item.Type;
                string info = item.Id;
                
                
                switch (type)
                {
                    case "Photo":
                        {
                            string strToSend = ((ProfileViewPageViewModel)base.DataContext).UserProfile.Uid + "|" + info.ToString();
                            Frame.Navigate(typeof(NewPhotoAlbumsViewPage), strToSend);
                            break;
                        }
                    case "Video":
                        {
                            string playerUri = item.Player;
                            Frame.Navigate(typeof(VideoPlayerPage), playerUri);
                            break;
                        }
                    case "Friend":
                        {
                            Frame.Navigate(typeof(ProfileViewPage), info);
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
                
            }
            catch (Exception ex)
            {
                var msgDlg = new MessageDialog(ex.Message);
            }
        }

        private void MyProfileClicked(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ProfileViewPage), ViewModelLocator.Vkontakte.AccessToken.UserId);
        }

        private void MyFriendsClicked(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(FriendsViewPage), ViewModelLocator.Vkontakte.AccessToken.UserId);
        }

        private void MyPhotoClicked(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NewPhotoAlbumsViewPage), ViewModelLocator.Vkontakte.AccessToken.UserId);
        }

        private void MyVideoesClicked(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(VideoViewPage), ViewModelLocator.Vkontakte.AccessToken.UserId);
        }

        private void MyAudioClicked(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AudioViewPage), ViewModelLocator.Vkontakte.AccessToken.UserId);
        }

        private void MyGroupsClicked(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(GroupsViewPage), ViewModelLocator.Vkontakte.AccessToken.UserId);
        }

        private void MyMessagesClicked(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MessagesViewPage));
        }

        private void MyNewsClicked(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NewsViewPage));
        }

        private void MyOptionButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(OptionViewPage));
        }

        private void MySwitchButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModelLocator.AuthService.LogOutVk();
            Frame.Navigate(typeof(LoginView));
        }

        private void MyExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }
            
        
    }
}
