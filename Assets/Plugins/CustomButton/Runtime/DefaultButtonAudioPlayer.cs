using UnityEngine;
using Utility.Audio;

namespace CustomButton
{
    public class DefaultButtonAudioPlayer : ButtonDecoratorComponent
    {
        [SerializeField] private SoundId _soundId;

        public override void OnClick(bool interactable)
        {
            //SoundService.PlaySoundEffect(_soundId);
        }
    }
}