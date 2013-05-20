using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkApi.Auth;
using VkApi.Core.Audio;
using VkApi.Core.Auth;
using VkApi.Core.Friends;
using VkApi.Core.Groups;
using VkApi.Core.News;
using VkApi.Core.PhotoAlbums;
using VkApi.Core.Profile;
using VkApi.Core.Status;
using VkApi.Core.Video;
using VkApi.Core.Wall;

namespace VkApi
{
    public sealed class Vkontakte
    {
        private static Vkontakte _instance;
        private readonly string _clientSecret;
        private readonly string _appId;
        private VkAudioRequest _audio;
        private VkUsersRequest _users;
        private VkFriendsRequest _friends;
        private VkGroupsRequest _groups;
        private VkNewsRequest _news;
        private VkWallRequest _wall;
        private VkStatusRequest _status;
        private VkVideoRequest _video;
        private VkPhotoAlbumsRequest _photoAlbums;
        private VkPhotoRequest _photos;

        internal static Vkontakte Instance
        {
            get { return Vkontakte._instance; }
        }

        public AccessToken AccessToken { get; private set; }

        public VkAudioRequest Audio
        {
            get
            {
                if (this._audio == null)
                {
                    this._audio = new VkAudioRequest();
                }
                return this._audio;
            }
        }

        public VkUsersRequest Users
        {
            get
            {
                if (this._users == null)
                {
                    this._users = new VkUsersRequest();
                }
                return this._users;
            }
        }

        public VkFriendsRequest Friends
        {
            get
            {
                if (this._friends == null)
                {
                    this._friends = new VkFriendsRequest();
                }
                return this._friends;
            }
        }

        public VkGroupsRequest Groups
        {
            get
            {
                if (this._groups == null)
                {
                    this._groups = new VkGroupsRequest();
                }
                return this._groups;
            }
        }

        public VkNewsRequest News
        {
            get
            {
                if (this._news == null)
                {
                    this._news = new VkNewsRequest();
                }
                return this._news;
            }
        }

        public VkPhotoAlbumsRequest PhotoAlbums
        {
            get
            {
                if (_photoAlbums == null)
                {
                    _photoAlbums = new VkPhotoAlbumsRequest();
                }
                return _photoAlbums;
            }
        }

        public VkPhotoRequest Photos
        {
            get
            {
                if (_photos == null)
                {
                    _photos = new VkPhotoRequest();
                }
                return _photos;
            }
        }

        public VkWallRequest Wall
        {
            get
            {
                if (this._wall == null)
                {
                    this._wall = new VkWallRequest();
                }
                return this._wall;
            }
        }

        public VkStatusRequest Status
        {
            get
            {
                if (this._status == null)
                {
                    this._status = new VkStatusRequest();
                }
                return this._status;
            }
        }

        public VkVideoRequest Video
        {
            get
            {
                if (this._video == null)
                {
                    this._video = new VkVideoRequest();
                }
                return this._video;
            }
        }

        public Vkontakte(string appId, string clientSecret = null)
        {
            this.AccessToken = new AccessToken();
            this._appId = appId;
            this._clientSecret = clientSecret;
            Vkontakte._instance = this;
        }

        public async Task<AccessToken> Login(string login, string password,
                                             VkScopeSettings scopeSettings = VkScopeSettings.CanAccessFriends,
                                             string captchaSid = null, string captchaKey = null)
        {
            VkDirectAuthRequest vkDirectAuthRequest = new VkDirectAuthRequest();
            AccessToken accessToken =
                await
                vkDirectAuthRequest.Login(login, password, this._appId, this._clientSecret, scopeSettings, captchaSid,
                                          captchaKey);
            this.AccessToken = accessToken;
            return accessToken;
        }
    }
}
