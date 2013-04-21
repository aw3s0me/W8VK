using System;
using System.Collections.Generic;
using System.Linq;
using VKModel.Entities;
using VKServiceLayer.Responses;


namespace VKServiceLayer
{
    public class ServiceLayer
    {
        public void GetFriends(AuthorizationContext context, Action<List<User>> getFriendsAction, Action<Error> errorAction)
        {
            var requestBuilder = new RequestBuilder(context);
            requestBuilder.SetMethod("friends.get");
            requestBuilder.AddParam("uid", context.CurrentUserId);

            requestBuilder.SendRequest<GetFriends>(getFriendsResponse => GetProfiles(context, getFriendsResponse.Result, getFriendsAction, errorAction), errorAction);
        }

        public void GetProfiles(AuthorizationContext context, string[] profileUids, Action<List<User>> getProfilesAction, Action<Error> errorAction)
        {
            var requestBuilder = new RequestBuilder(context);
            requestBuilder.SetMethod("getProfiles");
            requestBuilder.AddParam("uids", String.Join(",", profileUids));
            requestBuilder.AddParam("fields", "uid, first_name, last_name, nickname, domain, sex, bdate, city, country, timezone, photo, photo_medium, photo_big, has_mobile, rate, contacts, education, online");

            requestBuilder.SendRequest<GetProfiles>(getProfilesResponse => getProfilesAction.Invoke(getProfilesResponse.Result.GetUserItems()), errorAction);
        }

        public void GetMessages(AuthorizationContext context, Action<List<VKModel.Entities.Message>> getMessagesAction, Action<Error> errorAction)
        {
            var requestBuilder = new RequestBuilder(context);
            requestBuilder.SetMethod("messages.getDialogs");
            requestBuilder.AddParam("count", "100");

            requestBuilder.SendRequest<GetMessages>(getMessagesResult =>
            {
                var count = getMessagesResult.Count;
                
                getMessagesAction.Invoke(getMessagesResult.Messages.ToList().GetMessageItems());
            }, errorAction);
        }

        public void GetMessageConversation(AuthorizationContext context, string uid, Action<List<VKModel.Entities.Message>> getMessagesAction, Action<Error> errorAction)
        {
            var requestBuilder = new RequestBuilder(context);
            requestBuilder.SetMethod("messages.getHistory");
            requestBuilder.AddParam("uid", uid);

            requestBuilder.SendRequest<GetMessagesHistory>(getMessagesResult =>
            {
                var count = getMessagesResult.Count;
                getMessagesAction.Invoke(getMessagesResult.Messages.ToList().GetMessageItems(context.CurrentUserId));
            }, errorAction);
        }

        public void GetUserPhotoAlbums(AuthorizationContext context, string uid, Action<List<VKModel.Entities.PhotoAlbum>> getPhotoAlbumsAction, Action<Error> errorAction)
        {
            var requestBuilder = new RequestBuilder(context);
            requestBuilder.SetMethod("photos.getAlbums");
            requestBuilder.AddParam("uid", uid);
            requestBuilder.SendRequest<GetAlbums>(getAlbumsResponse => getPhotoAlbumsAction.Invoke(getAlbumsResponse.Albums.ToList().GetPhotoAlbumItem()), errorAction);
        }

        public void GetPhotos(AuthorizationContext context, string uid, List<string> photoIds, Action<List<VKModel.Entities.Photo>> getPhotosAction, Action<Error> errorAction)
        {
            var requestBuilder = new RequestBuilder(context);
            requestBuilder.SetMethod("photos.getById");

            var userPhotos = photoIds.Select(i => String.Format("{0}_{1}", uid, i)).ToArray();
            var photosParam = String.Join(",", userPhotos);
            requestBuilder.AddParam("photos", photosParam);
            requestBuilder.AddParam("extended", "1");

            requestBuilder.SendRequest<GetPhotos>(getPhotosResult => getPhotosAction.Invoke(getPhotosResult.Photos.Select(i => i.GetPhotoItem()).ToList()), errorAction);
        }

        public void GetPhotosByAlbum(AuthorizationContext context, string aid, string uid, Action<List<VKModel.Entities.Photo>> getPhotosAction, Action<Error> errorAction)
        {
            var requestBuilder = new RequestBuilder(context);
            requestBuilder.SetMethod("photos.get");
            requestBuilder.AddParam("uid", uid);
            requestBuilder.AddParam("aid", aid);
            requestBuilder.AddParam("extended", "1");

            requestBuilder.SendRequest<GetPhotos>(getPhotosResult => getPhotosAction.Invoke(getPhotosResult.Photos.Select(i => i.GetPhotoItem()).ToList()), errorAction);
        }

        public void SendMessage(AuthorizationContext context, string uid, string textmessage, Action sendMessageCompleteAction, Action<Error> sendMessageErrorAction)
        {
            var requestBuilder = new RequestBuilder(context);
            requestBuilder.SetMethod("messages.send");
            requestBuilder.AddParam("uid", uid);
            requestBuilder.AddParam("message", textmessage);

            requestBuilder.SendRequest<SendMessageResult>(result => sendMessageCompleteAction.Invoke(), sendMessageErrorAction);
        }
    }
}
