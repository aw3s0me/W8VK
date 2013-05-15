using System;
using System.Collections.Generic;
using System.Linq;
using VKModel.Entities;
using VKModel.Interfaces;
using VKServiceLayer;

namespace VKCore
{
    public interface IVkApi
    {
        void Initialize(string appId, string currentUserId, string accessToken);
        void Initialize(AuthorizationContext context);

        void GetFriends(Action<List<User>> getFriendsAction, Action<Error> errorAction);
        void GetCurrentUserProfile(Action<User> getUser, Action<Error> errorAction);
        void GetUserProfile(string uid, Action<User> getUserProfileComplete, Action<Error> getUserProfileError);
        void GetUserProfiles(List<String> uids, Action<List<User>> getUserProfileComplete, Action<Error> getUserProfileError);
        void GetPhotoAlbums(string uid, Action<List<PhotoAlbum>> getPhotoAlbums, Action<Error> getErrorAction);
        void GetPhotos(string uid, List<string> photoIds, Action<List<Photo>> getPhotosAction, Action<Error> errorAction);
        void GetPhotosByAlbum(string aid, string uid, Action<List<Photo>> getPhotosAction, Action<Error> getErrorAction);
        void GetMessages(Action<List<Message>> getListMessagesAction, Action<Error> errorAction);
        void GetMessageConversation(string uid, Action<List<Message>> messages, Action<Error> errorAction);
        void SendMessage(string uid, string textmessage, Action sendMessageCompleteAction,
                         Action<Error> sendMessageErrorAction);

        bool IsAuthorized();
        void RestoreContext();

        void DeleteContext();
        string GetCurrentUid();

    }


    public class VkApi : IVkApi
    {
        private readonly IEntityStorage _entityDataStorage;

        public VkApi(IEntityStorage entityDataStorage)
        {
            _entityDataStorage = entityDataStorage;
        }

        private AuthorizationContext _context;
        private AuthorizationContext Context
        {
            get
            {
                if (_context == null)
                {
                    var authorizationContext = _entityDataStorage.LoadEntity<AuthorizationContext>();
                    _context = authorizationContext;
                }
                return _context;
            }
            set 
            { 
                _context = value;
                _entityDataStorage.SaveEntity(_context);
            }
        }

        public void Initialize(string appId, string currentUserId, string accessToken)
        {
            Context = new AuthorizationContext
                {
                AppId = appId,
                CurrentUserId = currentUserId,
                AccessToken = accessToken
            };
        }

        public void Initialize(AuthorizationContext authorizationContext)
        {
            if (authorizationContext == null) throw new ArgumentNullException("authorizationContext");
            Context = authorizationContext;
        }

        public void GetFriends(Action<List<User>> getFriendsAction, Action<Error> errorAction)
        {
            var serviceLayer=new ServiceLayer();
            serviceLayer.GetFriends(Context,getFriendsAction,errorAction);
        }

        public void GetCurrentUserProfile(Action<User> getUser, Action<Error> errorAction)
        {
            if (Context == null)
            {
                errorAction.Invoke(new Error {ErrorCode = "5"});
                return;
            }

            var serviceLayer = new ServiceLayer();
            serviceLayer.GetProfiles(Context,new[] {Context.CurrentUserId},listResult=>
                { if (listResult != null) getUser.Invoke(listResult.First()); },errorAction);
        }


        public void GetMessages(Action<List<Message>> getListMessagesAction, Action<Error> errorAction)
        {
            var serviceLayer = new ServiceLayer();
            serviceLayer.GetMessages(Context,getListMessagesAction,errorAction);
        }

        public void GetMessageConversation(string uid, Action<List<Message>> getListMessagesAction, Action<Error> errorAction)
        {
            var serviceLayer = new ServiceLayer();
            serviceLayer.GetMessageConversation(Context,uid, getListMessagesAction, errorAction);
        }

        public void GetUserProfile(string uid, Action<User> getUserProfileComplete, Action<Error> getUserProfileError)
        {
            var serviceLayer = new ServiceLayer();
            serviceLayer.GetProfiles(Context,new []{uid},
                                     delegate(List<User> resultList)
                                         {
                                             if (resultList == null) throw new ArgumentNullException("resultList");
                                             getUserProfileComplete.Invoke(resultList.First());
                                         }, getUserProfileError);
        }

        public void GetUserProfiles(List<string> uids, Action<List<User>> getUserProfileComplete, Action<Error> getUserProfileError)
        {
            var serviceLayer = new ServiceLayer();
            serviceLayer.GetProfiles(Context, uids.ToArray(), getUserProfileComplete, getUserProfileError);
        }

        public void GetPhotoAlbums(string uid, Action<List<PhotoAlbum>> getPhotoAlbums, Action<Error> getErrorAction)
        {
            var serviceLayer = new ServiceLayer();
            serviceLayer.GetUserPhotoAlbums(Context, uid, getPhotoAlbums, getErrorAction);
        }

        public void GetPhotos(string uid, List<string> photoIds, Action<List<Photo>> getPhotosAction, Action<Error> errorAction)
        {
            var serviceLayer = new ServiceLayer();
            serviceLayer.GetPhotos(Context, uid,photoIds, getPhotosAction, errorAction);
        }

        public void GetPhotosByAlbum(string aid, string uid, Action<List<Photo>> getPhotosAction, Action<Error> getErrorAction)
        {
            var serviceLayer = new ServiceLayer();
            serviceLayer.GetPhotosByAlbum(Context,aid, uid, getPhotosAction, getErrorAction);
        }

        public void SendMessage(string uid, string textmessage, Action sendMessageCompleteAction , Action<Error> sendMessageErrorAction)
        {
            var serviceLayer = new ServiceLayer();
            serviceLayer.SendMessage(Context, uid, textmessage, sendMessageCompleteAction, sendMessageErrorAction);
        }

        public bool IsAuthorized()
        {
            return _entityDataStorage.LoadEntity<AuthorizationContext>()!=null;
        }

        public void RestoreContext()
        {
            Context = _entityDataStorage.LoadEntity<AuthorizationContext>();
        }

        public void DeleteContext()
        {
            _entityDataStorage.DeleteEntity<AuthorizationContext>();
        }

        public string GetCurrentUid()
        {
            return Context.CurrentUserId;
        }
    } 
}
