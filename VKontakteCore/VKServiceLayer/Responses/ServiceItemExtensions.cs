using System.Collections.Generic;
using System.Linq;
using User = VKModel.Entities.User;

namespace VKServiceLayer.Responses
{
    public static class ServiceItemExtensions
    {
        public static User GetUserItem(this UserServiceItem userServiceItem)
        {
            var result = new User
            {
                BirthDate = userServiceItem.BrithDate,
                City = userServiceItem.City,
                Country = userServiceItem.Country,
                Domain = userServiceItem.Domain,
                Faculty = userServiceItem.Faculty,
                FacultyName = userServiceItem.FacultyName,
                FirstName = userServiceItem.FirstName,
                Graduation = userServiceItem.Graduation,
                HasMobile = userServiceItem.HasMobile,
                HomePhone = userServiceItem.HomePhone,
                LastName = userServiceItem.LastName,
                MobilePhone = userServiceItem.MobilePhone,
                NickName = userServiceItem.Nickname,
                Online = userServiceItem.Online == "1" ? "1":"0",
                Photo = userServiceItem.Photo,
                PhotoBig = userServiceItem.PhotoBig,
                PhotoMedium = userServiceItem.PhotoMedium,
                Rate = userServiceItem.Rate,
                Sex = userServiceItem.Sex,
                TimeZone = userServiceItem.Timezone,
                Uid = userServiceItem.Uid,
                University = userServiceItem.University,
                UniversityName = userServiceItem.UniversityName
            };
            return result;
        }

        public static List<User> GetUserItems(this UserServiceItem[] userServiceItems)
        {
            if (userServiceItems == null) return null;
            return userServiceItems.Select(serviceItem => serviceItem.GetUserItem()).ToList();
        }

        public static VKModel.Entities.Message GetMessageItem(this Message message)
        {
            var result = new VKModel.Entities.Message
            {
                Title = message.Title,
                Uid = message.Uid,
                Body = message.Body,
                Date = UnixTimeConvertor.ConvertFromUnixTimestampString(message.Date),
                //Date = (message.Date),
                MsgId = message.MsgId,
                IsNewMsg = message.ReadState == "0",
                IsOutMsg = message.IsOutMsg == "1"
            };
            return result;
        }

        public static VKModel.Entities.Message GetMessageItem(this MessageHistory message,string currentUid)
        {
            var result = new VKModel.Entities.Message
            {
                Title=message.Body,
                Uid = message.Uid,
                Body = message.Body,
                Date = UnixTimeConvertor.ConvertFromUnixTimestampString(message.Date),
                //Date = (message.Date),
                MsgId = message.Mid,
                IsNewMsg = message.ReadState == "0",
                IsOutMsg = message.Uid==currentUid ,
            
            };
            return result;
        }

        public static List<VKModel.Entities.Message> GetMessageItems(this List<Message> listMessages)
        {
            if (listMessages == null) return null;
            return listMessages.Select(i => i.GetMessageItem()).ToList();
        }

        public static List<VKModel.Entities.Message> GetMessageItems(this List<MessageHistory> listMessages, string uid)
        {
            if (listMessages == null) return null;
            return listMessages.Select(i => i.GetMessageItem(uid)).ToList();
        }


        public static VKModel.Entities.PhotoAlbum GetPhotoAlbumItem(this PhotoAlbum photoAlbum)
        {
            var result = new VKModel.Entities.PhotoAlbum
            {
                AlbumId = photoAlbum.AlbumId,
                Created = UnixTimeConvertor.ConvertFromUnixTimestampString(photoAlbum.Created),
                Updated = UnixTimeConvertor.ConvertFromUnixTimestampString(photoAlbum.Updated),
                //Created = (photoAlbum.Created),
                //Updated = (photoAlbum.Updated),
                Description = photoAlbum.Description,
                OwnerId = photoAlbum.OwnerId,
                ThumbId = photoAlbum.ThumbId,
                Title = photoAlbum.Title,
                Privacy = photoAlbum.Privacy,
                Size = photoAlbum.Size,
            };
            return result;
        }



        public static List<VKModel.Entities.PhotoAlbum> GetPhotoAlbumItem(this List<PhotoAlbum> listAlbums)
        {
            if (listAlbums == null) return null;
            return listAlbums.Select(i => i.GetPhotoAlbumItem()).ToList();
        }

        public static VKModel.Entities.Photo GetPhotoItem(this Photo photo)
        {
            var result = new VKModel.Entities.Photo
            {
                AlbumId = photo.AlbumId,
                Created = UnixTimeConvertor.ConvertFromUnixTimestampString(photo.Created),
                //Created = (photo.Created),
                OwnerId = photo.OwnerId,
                PhotoId = photo.PhotoId,
                Source = photo.Source,
                SourceBig = photo.SourceBig,
                SourceSmall = photo.SourceSmall,
                SourceXBig = photo.SourceXBig,
                SourceX2Big = photo.SourceX2Big,
            };

            return result;
        }

        public static List<VKModel.Entities.Photo> GetPhotoItems(this List<Photo> photos)
        {
            if (photos == null) return null;
            return photos.Select(i => i.GetPhotoItem()).ToList();
        }
    }

    
}