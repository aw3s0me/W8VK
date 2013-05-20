using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;
using VKClient.Converters;

namespace VKClient.Models
{
    public class Audio : INotifyPropertyChanged
    {
        private MetaData _info;
        private bool _isPlaying;
        private bool _isAddedByCurrentUser;
        private string _lyrics;
        private TimeSpan _duration;
        private string _artist;
        private string _title;
        private string _lyricsId;
        public event PropertyChangedEventHandler PropertyChanged;
        public string Id
        {
            get;
            set;
        }
        public int Order
        {
            get;
            set;
        }
        public string OwnerId
        {
            get;
            set;
        }
        public string Title
        {
            get
            {
                return this._title;
            }
            set
            {
                if (this._title == value)
                {
                    return;
                }
                this._title = value;
                this.OnPropertyChanged("Title");
            }
        }
        public string Artist
        {
            get
            {
                return this._artist;
            }
            set
            {
                if (this._artist == value)
                {
                    return;
                }
                this._artist = value;
                this.OnPropertyChanged("Artist");
            }
        }
        [JsonConverter(typeof(JsonTimeSpanConverter))]
        public TimeSpan Duration
        {
            get
            {
                return this._duration;
            }
            set
            {
                if (this._duration == value)
                {
                    return;
                }
                this._duration = value;
                this.OnPropertyChanged("Duration");
            }
        }
        public string PlaylistId
        {
            get;
            set;
        }
        public string LyricsId
        {
            get
            {
                return this._lyricsId;
            }
            set
            {
                if (this._lyricsId == value)
                {
                    return;
                }
                this._lyricsId = value;
                this.OnPropertyChanged("LyricsId");
            }
        }
        [XmlIgnore]
        public Uri Image
        {
            get;
            set;
        }
        [XmlIgnore]
        public Uri Uri
        {
            get;
            set;
        }
        [XmlElement("Uri")]
        public string UriString
        {
            get
            {
                if (this.Uri != null)
                {
                    return this.Uri.OriginalString;
                }
                return null;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.Uri = new Uri(value);
                }
            }
        }
        [XmlIgnore]
        public MetaData Info
        {
            get
            {
                return this._info;
            }
            set
            {
                if (this._info == value)
                {
                    return;
                }
                this._info = value;
                this.OnPropertyChanged("Info");
            }
        }
        [XmlIgnore]
        public bool IsPlaying
        {
            get
            {
                return this._isPlaying;
            }
            set
            {
                if (this._isPlaying == value)
                {
                    return;
                }
                this._isPlaying = value;
                this.OnPropertyChanged("IsPlaying");
            }
        }
        [XmlIgnore]
        public bool IsAddedByCurrentUser
        {
            get
            {
                return this._isAddedByCurrentUser;
            }
            set
            {
                if (this._isAddedByCurrentUser == value)
                {
                    return;
                }
                this._isAddedByCurrentUser = value;
                this.OnPropertyChanged("IsAddedByCurrentUser");
            }
        }
        [XmlIgnore]
        public bool HasLyrics
        {
            get
            {
                return !string.IsNullOrEmpty(this.LyricsId);
            }
        }
        [XmlIgnore]
        public string Lyrics
        {
            get
            {
                return this._lyrics;
            }
            set
            {
                if (this._lyrics == value)
                {
                    return;
                }
                this._lyrics = value;
                this.OnPropertyChanged("Lyrics");
            }
        }
        public Audio()
        {
            this._info = new MetaData();
        }
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
