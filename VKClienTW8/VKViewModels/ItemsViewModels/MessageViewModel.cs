using System;
using System.Collections.Generic;
using VKModel.Entities;
using Windows.UI.Text;
using Windows.UI.Xaml;

namespace VKViewModels.ItemsViewModels
{
    public class MessageViewModel : BaseViewModel
    {
        private readonly Message _message;
        private readonly string _currentUid;
        private readonly Dictionary<string,User> _usersIndex;

        public Visibility IncomeVisibility
        {
            get { return _message.IsOutMsg ? Visibility.Collapsed : Visibility.Visible; }
        }

        public Visibility OutVisibility
        {
            get { return _message.IsOutMsg ? Visibility.Visible : Visibility.Collapsed; }
        }

        public bool IsOut
        {
            get { return _message.IsOutMsg; }
        }

        private string _newTextMessage;
        public string NewTextMessage
        {
            get { return _newTextMessage; }
            set
            {
                _newTextMessage = value; 
                OnPropertyChange("NewTextMessage");
                SendMessageEnabled = value.Length > 0;
            }
        }

        private bool _sendMessageEnabled;
        public bool SendMessageEnabled
        {
            get { return _sendMessageEnabled; }
            set { _sendMessageEnabled = value; OnPropertyChange("SendMessageEnabled"); }
        }

        public FontWeight ReadStateFontWeight
        {
            get
            {
                return  _message.IsNewMsg? FontWeights.Bold : FontWeights.Normal;
            }
        }


        public MessageViewModel(Message message, IEnumerable<User> usersInProfile, string currentUid)
        {
            _message = message;
            _currentUid = currentUid;
            _usersIndex=new Dictionary<string, User>();

            foreach (var user in usersInProfile)
            {
                _usersIndex[user.Uid] = user;
            }
        }

        public User UserFrom
        {
            get
            {
                return _message.IsOutMsg ? _usersIndex[_currentUid] : _usersIndex[Uid];
            }
        }

        public User User
        {
            get
            {
                return _usersIndex[Uid];
            }
        }

        public User UserTo
        {
            get
            {
                return _message.IsOutMsg ? _usersIndex[Uid] : _usersIndex[_currentUid];
            }
        }

        public string Body { get { return _message.Body; } }

        public string Title { get { return String.IsNullOrWhiteSpace(_message.Title) ? "..":_message.Title; } }

        public DateTime Date { get { return _message.Date; } }

        public string Uid { get { return _message.Uid; } }

        public string Mid { get { return _message.MsgId; } }

        public bool IsNewMessage { get { return _message.IsNewMsg; } }
    }
}
