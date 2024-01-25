using DG.Tweening;
using UnityEngine;
using Utility.Singletons;

namespace Utility.Audio
{
    public class AudioPlayer : SingletonMonoBehaviour<AudioPlayer>
    {
        private const float FadeOutVolume = 0.3f;

        [SerializeField] private float _maxMusicVolume;
        [SerializeField] private AudioSource _musicSource;

        public bool MusicIsPlaying => _musicSource.isPlaying;
        public float MusicPlaybackTime => _musicSource.time;

        public Tween PlayMusic(float time)
        {
            var sequence = DOTween.Sequence();

            if (_musicSource.isPlaying)
                sequence.Append(FadeMusic(0f));
            else
                sequence.AppendCallback(() => _musicSource.volume = 0);

            sequence.AppendCallback(() =>
            {
                _musicSource.time = time;
                _musicSource.Play();
            });
            sequence.Append(_musicSource.DOFade(_maxMusicVolume, 0.5f));

            return sequence;
        }

        public Tween StopMusic()
        {
            var sequence = DOTween.Sequence();

            sequence.Append(FadeMusic(0f));
            sequence.AppendCallback(() => _musicSource.Stop());

            return sequence;
        }

        public Tween FadeInMusic()
        {
            var sequence = DOTween.Sequence();

            sequence.AppendCallback(() => _musicSource.volume = FadeOutVolume);
            sequence.Append(FadeMusic(_maxMusicVolume));

            return sequence;
        }

        public Tween FadeOutMusic()
        {
            var sequence = DOTween.Sequence();

            sequence.AppendCallback(() => _musicSource.volume = _maxMusicVolume);
            sequence.Append(FadeMusic(FadeOutVolume));

            return sequence;
        }

        private Tween FadeMusic(float volume)
        {
            return _musicSource.DOFade(volume, 0.5f);
        }
    }
}