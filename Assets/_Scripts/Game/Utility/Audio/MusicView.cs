using DG.Tweening;
using UnityEngine;
using Utility.Audio;
using Utility.Singletons;

namespace Game.Utility.Audio
{
    public class MusicView : SingletonMonoBehaviour<MusicView>
    {
        private float _playbackTime;
        private Tween _currentAnimation;

        protected override void OnInitialize()
        {
            AudioSettings.OnAudioConfigurationChanged += PlayMusic;
        }

        private void UpdateMuteState(bool muted)
        {
            if (muted)
                _playbackTime = AudioPlayer.Instance.MusicPlaybackTime;

            PlayMusic(false);
        }

        private void PlayMusic(bool outputChanged)
        {
            _currentAnimation?.Kill(true);

            if (SettingsModel.Instance.IsMusicMuted.Value)
            {
                _currentAnimation = AudioPlayer.Instance.StopMusic();
                return;
            }

            if (AudioPlayer.Instance.MusicIsPlaying)
                _playbackTime = AudioPlayer.Instance.MusicPlaybackTime;

            _currentAnimation = AudioPlayer.Instance.PlayMusic(_playbackTime);
        }
    }
}