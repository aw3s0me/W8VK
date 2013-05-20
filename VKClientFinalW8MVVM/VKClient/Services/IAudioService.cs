using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VKClient.Models;

namespace VKClient.Services
{
    public interface IAudioService
    {
        Audio CurrentAudio
        {
            get;
            set;
        }
        bool IsPlaying
        {
            get;
        }
        bool Shuffle
        {
            get;
            set;
        }
        bool Repeat
        {
            get;
            set;
        }
        double Volume
        {
            get;
            set;
        }
        bool IsMuted
        {
            get;
            set;
        }
        TimeSpan CurrentAudioPosition
        {
            get;
            set;
        }
        TimeSpan CurrentAudioDuration
        {
            get;
        }
        IList<Audio> Playlist
        {
            get;
            set;
        }
        void Play(Audio audio);
        void Next();
        void Prev();
        void Play();
        void Pause();
        void Stop();
    }
}
