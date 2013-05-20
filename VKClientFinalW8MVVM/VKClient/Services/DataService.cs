using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using VKClient.Models.Entities;
using VKClient.ViewModels;
using VkApi;
using VKClient.Models;
using VkApi.Core.Attachments;
using VkApi.Core.Audio;
using VkApi.Core.Groups;
using VkApi.Core.News;
using VkApi.Core.PhotoAlbums;
using VkApi.Core.Profile;
using VkApi.Core.Video;
using VkApi.Core.Wall;
using VkApi.Error;

namespace VKClient.Services
{
    class DataService
    {
        private readonly Vkontakte _vkontakte;

        public DataService(Vkontakte vkontakte)
        {
            this._vkontakte = vkontakte;
        }

        public async Task<Boolean> AddAudio(Audio audio)
        {
            Boolean flag;
            if (ViewModelLocator.AuthService.IsLoggedInVk(true))
            {
                if (audio.Uri == null)
                {
                    Audio audioByArtistAndTitle = await this.GetAudioByArtistAndTitle(audio.Artist, audio.Title);
                    Audio audio1 = audioByArtistAndTitle;
                    if (audio1 != null)
                    {
                        audio.Id = audio1.Id;
                        audio.Artist = audio1.Artist;
                        audio.Title = audio1.Title;
                        audio.Uri = audio1.Uri;
                        audio.OwnerId = audio1.OwnerId;
                        audio.PlaylistId = audio1.PlaylistId;
                        audio.LyricsId = audio1.LyricsId;
                    }
                }
                Double num = await this._vkontakte.Audio.Add(audio.Id, audio.OwnerId);
                Double num1 = num;
                if (num1 <= 0)
                {
                    flag = false;
                }
                else
                {
                    audio.Id = num1.ToString();
                    audio.OwnerId = this._vkontakte.AccessToken.UserId;
                    audio.IsAddedByCurrentUser = true;
                    flag = true;
                }
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        private Audio ConvertAudio(VkAudio a)
        {
            Audio audio = new Audio();
            Int64 id = a.Id;
            audio.Id = id.ToString(CultureInfo.InvariantCulture);
            Int64 albumId = a.AlbumId;
            audio.PlaylistId = albumId.ToString();
            Int64 ownerId = a.OwnerId;
            audio.OwnerId = ownerId.ToString();
            audio.Title = a.Title;
            audio.Uri = new Uri(a.Url);
            audio.Artist = a.Artist;
            audio.Duration = a.Duration;
            Int64 num = a.OwnerId;
            audio.IsAddedByCurrentUser = num.ToString() == this._vkontakte.AccessToken.UserId;
            audio.LyricsId = a.LyricsId;
            return audio;
        }

        private Video ConvertVideo(VkVideo v)
        {
            Video video = new Video();
            Int64 id = v.Id;
            video.Id = id.ToString(CultureInfo.InvariantCulture);
            Int64 ownerId = v.OwnerId;
            video.OwnerId = ownerId.ToString();
            video.Title = v.Title;
            video.Description = v.Description;
            video.Date = v.Date;
            video.Duration = v.Duration;
            video.Files = v.Files;
            video.ImageSmall = v.ImageSmall;
            video.ImageMedium = v.ImageMedium;
            video.Link = v.Link;
            video.Player = v.Player;
            return video;
        }

        public async Task<IList<AudioCollection>> GetAlbums(String userId = null, String groupId = null)
        {
            IList<AudioCollection> audioCollections;
            if (ViewModelLocator.AuthService.IsLoggedInVk(true))
            {
                VkAudioRequest audio = this._vkontakte.Audio;
                String str = userId;
                String str1 = str;
                if (str == null)
                {
                    str1 = this._vkontakte.AccessToken.UserId;
                }
                IEnumerable<VkAudioAlbum> albums = await audio.GetAlbums(str1, groupId, 0, 0);
                IEnumerable<VkAudioAlbum> vkAudioAlbums = albums;
                if (vkAudioAlbums == null)
                {
                    audioCollections = null;
                }
                else
                {
                    IEnumerable<VkAudioAlbum> vkAudioAlbums1 = vkAudioAlbums;
                    List<AudioCollection> list = vkAudioAlbums1.Select<VkAudioAlbum, AudioCollection>((VkAudioAlbum a) => {
                        AudioCollection audioCollection = new AudioCollection();
                        Double id = a.Id;
                        audioCollection.Id = id.ToString(CultureInfo.InvariantCulture);
                        audioCollection.Title = a.Title;
                        Double ownerId = a.OwnerId;
                        audioCollection.OwnerId = ownerId.ToString(CultureInfo.InvariantCulture);
                        return audioCollection;
                    }).ToList<AudioCollection>();
                    audioCollections = list;
                }
            }
            else
            {
                audioCollections = null;
            }
            return audioCollections;
        }

        public async Task<IList<Audio>> GetAudio(String albumId = null, Int32 count = 0, Int32 offset = 0, String userId = null, String groupId = null)
        {
            IList<Audio> audios;
            if (ViewModelLocator.AuthService.IsLoggedInVk(true))
            {
                VkAudioRequest audio = this._vkontakte.Audio;
                String str = userId;
                String str1 = str;
                if (str == null)
                {
                    str1 = this._vkontakte.AccessToken.UserId;
                }
                IEnumerable<VkAudio> vkAudios = await audio.Get(str1, groupId, albumId, false, count, offset);
                IEnumerable<VkAudio> vkAudios1 = vkAudios;
                if (vkAudios1 == null)
                {
                    audios = null;
                }
                else
                {
                    IEnumerable<VkAudio> vkAudios2 = vkAudios1;
                    List<Audio> list = vkAudios2.Select<VkAudio, Audio>((VkAudio a) => this.ConvertAudio(a)).ToList<Audio>();
                    audios = list;
                }
            }
            else
            {
                audios = null;
            }
            return audios;
        }

        public async Task<Audio> GetAudioByArtistAndTitle(string artist, string title)
        {
            Audio audioByArtistAndTitle;
            string str = artist;
            string str1 = title;
            IList<Audio> audios = await this.SearchAudio(String.Concat(str, " - ", str1), 10, 0);
            IList<Audio> audios1 = audios;
            if (audios1 == null || audios1.Count <= 0)
            {
                bool flag = false;
                if (str.Contains("(") && str.Contains(")"))
                {
                    str = String.Concat(str.Substring(0, str.IndexOf("(")), str.Substring(str.LastIndexOf(")") + 1));
                    flag = true;
                }
                if (str1.Contains("(") && str1.Contains(")"))
                {
                    str1 = String.Concat(str1.Substring(0, str1.IndexOf("(")), str1.Substring(str1.LastIndexOf(")") + 1));
                    flag = true;
                }
                if (!flag)
                {
                    audioByArtistAndTitle = null;
                }
                else
                {
                    audioByArtistAndTitle = await this.GetAudioByArtistAndTitle(str, str1);
                }
            }
            else
            {
                IList<Audio> audios2 = audios1;
                Audio audio = audios2.FirstOrDefault<Audio>((Audio x) => {
                    if (x.Title.ToLower() != str1.ToLower())
                    {
                        return false;
                    }
                    return x.Artist.ToLower() == str.ToLower();
                });
                if (audio == null)
                {
                    IList<Audio> audios3 = audios1;
                    audio = audios3.FirstOrDefault<Audio>((Audio x) => x.Title.ToLower() == str1.ToLower());
                }
                if (audio == null)
                {
                    audio = audios1.First<Audio>();
                }
                audioByArtistAndTitle = audio;
            }
            return audioByArtistAndTitle;
        }

        public async Task<List<UserProfile>> GetFriends(string uid, Int32 count = 0, Int32 offset = 0)
        {
            List<UserProfile> userProfiles;
            if (ViewModelLocator.AuthService.IsLoggedInVk(true))
            {
                IEnumerable<VkProfile> vkProfiles = await this._vkontakte.Friends.Get(uid, "photo,uid,photo_medium,photo_big", null, count, offset, true);

                IEnumerable<VkProfile> vkProfiles1 = vkProfiles;
                if (vkProfiles1 == null)
                {
                    userProfiles = null;
                }
                else
                {
                    IEnumerable<VkProfile> vkProfiles2 = vkProfiles1;
                    List<UserProfile> list = vkProfiles2.Select<VkProfile, UserProfile>((VkProfile f) => {
                        UserProfile userProfile = new UserProfile();
                        Int64 id = f.Id;
                        userProfile.Uid = id.ToString();
                        userProfile.FirstName = f.FirstName;
                        userProfile.LastName = f.LastName;
                        userProfile.Photo = f.Photo;
                        userProfile.PhotoMedium = f.PhotoMedium;
                        userProfile.PhotoBig = f.PhotoBig;
                        userProfile.Online = f.IsOnline;
                        return userProfile;
                    }).ToList<UserProfile>();
                    userProfiles = list;
                }
            }
            else
            {
                userProfiles = null;
            }
            return userProfiles;
        }

        public async Task<List<Group>> GetGroups(string uid, Int32 count = 0, Int32 offset = 0)
        {
            List<Group> groups;
            if (ViewModelLocator.AuthService.IsLoggedInVk(true))
            {
                IEnumerable<VkGroup> vkGroups =
                    await this._vkontakte.Groups.Get(uid, "photo", null, count, offset, true);
                IEnumerable<VkGroup> vkGroups1 = vkGroups;
                if (vkGroups1 == null)
                {
                    groups = null;
                }
                else
                {
                    IEnumerable<VkGroup> vkGroups2 = vkGroups1;
                    List<Group> list = vkGroups2.Select<VkGroup, Group>((VkGroup g) => {
                        Group group = new Group();
                        Int64 id = g.Id;
                        group.Id = id.ToString();
                        group.Name = g.Name;
                        group.PhotoUri = g.Photo;
                        return group;
                    }).ToList<Group>();
                    groups = list;
                }
            }
            else
            {
                groups = null;
            }
            return groups;
        }

     //   private Semaphore semaphore = new Semaphore(1, 1);

        public async Task<List<PhotoAlbum>> GetPhotoAlbums(string uid)
        {
            List<PhotoAlbum> photoAlbums;
            if (ViewModelLocator.AuthService.IsLoggedInVk(true))
            {
              //  semaphore.WaitOne();
                IEnumerable<VkPhotoAlbum> vkPhotoAlbums = await this._vkontakte.PhotoAlbums.Get(uid, null, 1, 1, 0);
                //IEnumerable<VkPhotoAlbum> vkPhotoAlbums = this._vkontakte.PhotoAlbums.Get(uid, null, 1, 1, 0).Result;
                Task.Delay(1000);
               // semaphore.Release();

                IEnumerable<VkPhotoAlbum> vkPhotoAlbums1 = vkPhotoAlbums;
                if (vkPhotoAlbums1 == null)
                {
                    photoAlbums = null;
                }
                else
                {
                    IEnumerable<VkPhotoAlbum> vkPhotoAlbums2 = vkPhotoAlbums1;
                    List<PhotoAlbum> list = vkPhotoAlbums2.Select<VkPhotoAlbum, PhotoAlbum>((VkPhotoAlbum g) =>
                    {
                        PhotoAlbum photoAlbum = new PhotoAlbum();
                        Int64 id = g.AId;
                        photoAlbum.AlbumId = g.AId;
                        photoAlbum.Description = g.Description;
                        photoAlbum.OwnerId = g.OwnerId;
                        photoAlbum.ThumbId = g.ThumbId;
                        photoAlbum.PhotoSrc = g.ThumbSrc;
                        photoAlbum.Title = g.Title;
                        return photoAlbum;
                    }).ToList<PhotoAlbum>();

                    photoAlbums = list;
                }
            }
            else
            {
                photoAlbums = null;
            }
            return photoAlbums;
        }

        public async Task<List<Photo>> GetPhotos(string oid, string aid)
        {
            List<Photo> photoAlbum;
            if (ViewModelLocator.AuthService.IsLoggedInVk(true))
            {
                IEnumerable<VkPhoto> vkPhotoAlbum = await _vkontakte.Photos.Get(oid, aid ,null, 1, 1, null, 0);
                IEnumerable<VkPhoto> vkPhotoAlbum1 = vkPhotoAlbum;
                if (vkPhotoAlbum1 == null)
                {
                    photoAlbum = null;
                }
                else
                {
                    IEnumerable<VkPhoto> vkPhotoAlbum2 = vkPhotoAlbum1;
                    List<Photo> list = vkPhotoAlbum2.Select<VkPhoto, Photo>((VkPhoto g) =>
                    {
                        Photo _photoAlbum = new Photo();
                        Int64 id = g.PhotoId;
                        _photoAlbum.PhotoId = g.PhotoId;
                        _photoAlbum.OwnerId = g.OwnerId;
                        _photoAlbum.AlbumId = g.AlbumId;
                        _photoAlbum.Source = g.ThumbSrcNormal;
                        _photoAlbum.SourceSmall = g.ThumbSrcSmall;
                        _photoAlbum.SourceBig = g.ThumbSrcBig;
                        _photoAlbum.SourceXBig = g.ThumbSrcXBig;
                        _photoAlbum.Width = g.Width;
                        _photoAlbum.Height = g.Height;
                        return _photoAlbum;
                    }).ToList<Photo>();

                    photoAlbum = list;
                }
            }
            else
            {
                photoAlbum = null;
            }
            return photoAlbum;
        }


        public async Task<String> GetLyrics(String lyricsId)
        {
            String str;
            if (ViewModelLocator.AuthService.IsLoggedInVk(true))
            {
                String lyrics = await this._vkontakte.Audio.GetLyrics(lyricsId);
                String str1 = lyrics;
                str = str1;
            }
            else
            {
                str = null;
            }
            return str;
        }

        public async Task<IList<Audio>> GetNewsAudio(Int32 count, Int32 offset)
        {
            IList<Audio> list;
            if (ViewModelLocator.AuthService.IsLoggedInVk(true))
            {
                try
                {
                    List<VkNewsEntry> vkNewsEntries = await this._vkontakte.News.Get(null, "post", count, offset);
                    List<VkNewsEntry> vkNewsEntries1 = vkNewsEntries;
                    if (vkNewsEntries1 != null)
                    {
                        List<String> strs = new List<String>();
                        foreach (VkNewsEntry vkNewsEntry in vkNewsEntries1)
                        {
                            if (vkNewsEntry.Attachments == null)
                            {
                                continue;
                            }
                            List<VkAttachment> attachments = vkNewsEntry.Attachments;
                            IEnumerable<VkAttachment> vkAttachments = attachments.Where<VkAttachment>((VkAttachment a) => a is VkAudioAttachment);
                            IEnumerable<String> strs1 = vkAttachments.Select<VkAttachment, String>((VkAttachment a) => {
                                Int64 ownerId = a.OwnerId;
                                Int64 id = a.Id;
                                return String.Concat(ownerId.ToString(), "_", id.ToString());
                            });
                            if (!strs1.Any<String>())
                            {
                                continue;
                            }
                            strs.AddRange(strs1.ToList<String>());
                        }
                        if (strs.Count != 0)
                        {
                            List<VkAudio> vkAudios = new List<VkAudio>();
                            if (strs.Count < 100)
                            {
                                IEnumerable<VkAudio> byId = await this._vkontakte.Audio.GetById(strs);
                                IEnumerable<VkAudio> vkAudios1 = byId;
                                if (vkAudios1 != null)
                                {
                                    vkAudios.AddRange(vkAudios1);
                                }
                            }
                            else
                            {
                                Int32 num = 0;
                                Int32 num1 = 99;
                                while (num + num1 < strs.Count)
                                {
                                    List<VkAudio> vkAudios2 = vkAudios;
                                    vkAudios2.AddRange(await this._vkontakte.Audio.GetById(strs.GetRange(num, num1)));
                                    num = num + 100;
                                    if (num1 < strs.Count)
                                    {
                                        continue;
                                    }
                                    num1 = strs.Count;
                                }
                            }
                            List<VkAudio> vkAudios3 = vkAudios;
                            IEnumerable<Audio> audios = vkAudios3.Select<VkAudio, Audio>((VkAudio a) => this.ConvertAudio(a));
                            list = audios.ToList<Audio>();
                            return list;
                        }
                        else
                        {
                            list = null;
                            return list;
                        }
                    }
                }
                catch (VkAccessDeniedException vkAccessDeniedException)
                {
                    LoginMessage loginMessage = new LoginMessage();
                    loginMessage.IsSuccess = true;
                    loginMessage.Type = LoginType.LogOut;
                    Messenger.Default.Send<LoginMessage>(loginMessage);
                }
                list = null;
            }
            else
            {
                list = null;
            }
            return list;
        }

        public async Task<IList<Audio>> GetPopular(Int32 count = 0, Int32 offset = 0)
        {
            IList<Audio> audios;
            if (ViewModelLocator.AuthService.IsLoggedInVk(true))
            {
                IEnumerable<VkAudio> popular = await this._vkontakte.Audio.GetPopular(count, offset);
                IEnumerable<VkAudio> vkAudios = popular;
                if (vkAudios == null)
                {
                    audios = null;
                }
                else
                {
                    IEnumerable<VkAudio> vkAudios1 = vkAudios;
                    List<Audio> list = vkAudios1.Select<VkAudio, Audio>((VkAudio a) => this.ConvertAudio(a)).ToList<Audio>();
                    audios = list;
                }
            }
            else
            {
                audios = null;
            }
            return audios;
        }

        public async Task<IList<Audio>> GetRecommendations(Int32 count = 0, Int32 offset = 0)
        {
            IList<Audio> audios;
            if (ViewModelLocator.AuthService.IsLoggedInVk(true))
            {
                IEnumerable<VkAudio> recommendations = await this._vkontakte.Audio.GetRecommendations(count, offset);
                IEnumerable<VkAudio> vkAudios = recommendations;
                if (vkAudios == null)
                {
                    audios = null;
                }
                else
                {
                    IEnumerable<VkAudio> vkAudios1 = vkAudios;
                    List<Audio> list = vkAudios1.Select<VkAudio, Audio>((VkAudio a) => this.ConvertAudio(a)).ToList<Audio>();
                    audios = list;
                }
            }
            else
            {
                audios = null;
            }
            return audios;
        }

        public async Task<UserProfile> GetUserProfile(string userid=null)
        {
            UserProfile userProfile;
            String[] userId;
            if (ViewModelLocator.AuthService.IsLoggedInVk(true))
            {
                if (userid == null)
                {
                    userId = new String[] {this._vkontakte.AccessToken.UserId};
                }
                else
                {
                    userId = new string[] {userid};
                }
                
                IEnumerable<VkProfile> vkProfiles = await this._vkontakte.Users.Get(userId,"photo,photo_medium,photo_big", null);
                IEnumerable<VkProfile> vkProfiles1 = vkProfiles;
                if (vkProfiles1 == null)
                {
                    userProfile = null;
                }
                else
                {
                    VkProfile vkProfile = vkProfiles1.First<VkProfile>();
                    UserProfile str = new UserProfile();
                    Int64 id = vkProfile.Id;
                    str.Uid = id.ToString();
                    str.FirstName = vkProfile.FirstName;
                    str.LastName = vkProfile.LastName;
                    str.Photo = vkProfile.Photo;
                    str.PhotoBig = vkProfile.PhotoBig;
                    str.PhotoMedium = vkProfile.PhotoMedium;
                    userProfile = str;
                }
            }
            else
            {
                userProfile = null;
            }
            return userProfile;
        }

        public async Task<UserProfile> GetUserProfileByUserId(string userId)
        {
            UserProfile userProfile;
            string[] uids=new string[1];
            uids[0] = userId;
            if (ViewModelLocator.AuthService.IsLoggedInVk(true))
            {
                IEnumerable<VkProfile> vkProfiles = await this._vkontakte.Users.Get(uids, "photo", null);
                IEnumerable<VkProfile> vkProfiles1 = vkProfiles;
                if (vkProfiles1 == null)
                {
                    userProfile = null;
                }
                else
                {
                    VkProfile vkProfile = vkProfiles1.First<VkProfile>();
                    UserProfile str = new UserProfile();
                    Int64 id = vkProfile.Id;
                    str.Uid = id.ToString();
                    str.FirstName = vkProfile.FirstName;
                    str.LastName = vkProfile.LastName;
                    str.Photo = vkProfile.Photo;
                    userProfile = str;
                }
            }
            else
            {
                userProfile = null;
            }
            return userProfile;
        }

        public async Task<IList<Video>> GetVideo(IList<String> videos=null, string oid=null)
        {
            IList<Video> videos1;
            if (ViewModelLocator.AuthService.IsLoggedInVk(true))
            {
                //IEnumerable<VkVideo> vkVideos = await this._vkontakte.Video.Get(videos, null, null, null, 0, 0, 0);
                IEnumerable<VkVideo> vkVideos = await _vkontakte.Video.Get(null, oid, null, null, 0, 0, 0);
                IEnumerable<VkVideo> vkVideos1 = vkVideos;
                if (vkVideos1 == null)
                {
                    videos1 = null;
                }
                else
                {
                    IEnumerable<VkVideo> vkVideos2 = vkVideos1;
                    List<Video> list = vkVideos2.Select<VkVideo, Video>((VkVideo v) => this.ConvertVideo(v)).ToList<Video>();
                    videos1 = list;
                }
            }
            else
            {
                videos1 = null;
            }
            return videos1;
        }

        public async Task<IList<Audio>> GetWallAudio(Int32 count, Int32 offset, String userId = null)
        {
            IList<Audio> list;
            if (ViewModelLocator.AuthService.IsLoggedInVk(true))
            {
                try
                {
                    VkWallResult vkWallResult = await this._vkontakte.Wall.Get(userId, "post", count, offset);
                    VkWallResult vkWallResult1 = vkWallResult;
                    if (vkWallResult1.Count >= offset)
                    {
                        IEnumerable<VkWallEntry> posts = vkWallResult1.Posts;
                        if (posts != null)
                        {
                            List<String> strs = new List<String>();
                            foreach (VkWallEntry post in posts)
                            {
                                if (post.Attachments == null)
                                {
                                    continue;
                                }
                                List<VkAttachment> attachments = post.Attachments;
                                IEnumerable<VkAttachment> vkAttachments = attachments.Where<VkAttachment>((VkAttachment a) => a is VkAudioAttachment);
                                IEnumerable<String> strs1 = vkAttachments.Select<VkAttachment, String>((VkAttachment a) => {
                                    Int64 ownerId = a.OwnerId;
                                    Int64 id = a.Id;
                                    return String.Concat(ownerId.ToString(), "_", id.ToString());
                                });
                                if (!strs1.Any<String>())
                                {
                                    continue;
                                }
                                strs.AddRange(strs1.ToList<String>());
                            }
                            List<VkAudio> vkAudios = new List<VkAudio>();
                            if (strs.Count < 100)
                            {
                                if (strs.Count > 0)
                                {
                                    IEnumerable<VkAudio> byId = await this._vkontakte.Audio.GetById(strs);
                                    IEnumerable<VkAudio> vkAudios1 = byId;
                                    if (vkAudios1 != null)
                                    {
                                        vkAudios.AddRange(vkAudios1);
                                    }
                                }
                            }
                            else
                            {
                                Int32 num = 0;
                                Int32 num1 = 99;
                                while (num + num1 < strs.Count)
                                {
                                    List<VkAudio> vkAudios2 = vkAudios;
                                    vkAudios2.AddRange(await this._vkontakte.Audio.GetById(strs.GetRange(num, num1)));
                                    num = num + 100;
                                    if (num1 < strs.Count)
                                    {
                                        continue;
                                    }
                                    num1 = strs.Count;
                                }
                            }
                            List<VkAudio> vkAudios3 = vkAudios;
                            IEnumerable<Audio> audios = vkAudios3.Select<VkAudio, Audio>((VkAudio a) => this.ConvertAudio(a));
                            list = audios.ToList<Audio>();
                            return list;
                        }
                    }
                    else
                    {
                        list = null;
                        return list;
                    }
                }
                catch (VkAccessDeniedException vkAccessDeniedException)
                {
                    LoginMessage loginMessage = new LoginMessage();
                    loginMessage.IsSuccess = true;
                    loginMessage.Type = LoginType.LogOut;
                    Messenger.Default.Send<LoginMessage>(loginMessage);
                }
                list = null;
            }
            else
            {
                list = null;
            }
            return list;
        }

        public async Task<Boolean> RemoveAudio(Audio audio)
        {
            Boolean flag;
            if (ViewModelLocator.AuthService.IsLoggedInVk(true))
            {
                Boolean flag1 = await this._vkontakte.Audio.Remove(audio.Id, audio.OwnerId);
                Boolean flag2 = flag1;
                if (flag2)
                {
                    audio.IsAddedByCurrentUser = false;
                }
                flag = flag2;
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        public async Task<IList<Audio>> SearchAudio(String query, Int32 count = 0, Int32 offset = 0)
        {
            IList<Audio> audios;
            if (ViewModelLocator.AuthService.IsLoggedInVk(true))
            {
                IEnumerable<VkAudio> vkAudios = await this._vkontakte.Audio.Search(query, count, offset, VkAudioSortType.DateAdded, false, true);
                IEnumerable<VkAudio> vkAudios1 = vkAudios;
                if (vkAudios1 == null)
                {
                    audios = null;
                }
                else
                {
                    IEnumerable<VkAudio> vkAudios2 = vkAudios1;
                    List<Audio> list = vkAudios2.Select<VkAudio, Audio>((VkAudio a) => this.ConvertAudio(a)).ToList<Audio>();
                    audios = list;
                }
            }
            else
            {
                audios = null;
            }
            return audios;
        }

        public async Task<IList<Video>> SearchVideo(String query, Int32 count = 0, Int32 offset = 0)
        {
            IList<Video> videos;
            if (ViewModelLocator.AuthService.IsLoggedInVk(true))
            {
                IEnumerable<VkVideo> vkVideos = await this._vkontakte.Video.Search(query, count, offset, false, VkAudioSortType.DateAdded, false);
                IEnumerable<VkVideo> vkVideos1 = vkVideos;
                if (vkVideos1 == null)
                {
                    videos = null;
                }
                else
                {
                    IEnumerable<VkVideo> vkVideos2 = vkVideos1;
                    List<Video> list = vkVideos2.Select<VkVideo, Video>((VkVideo v) => this.ConvertVideo(v)).ToList<Video>();
                    videos = list;
                }
            }
            else
            {
                videos = null;
            }
            return videos;
        }
    }

    
}
