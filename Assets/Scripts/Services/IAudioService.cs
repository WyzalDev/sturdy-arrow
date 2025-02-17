using UnityEngine;

namespace SturdyArrow.Services
{
    public interface IAudioService
    {
        void PlayMusic(string name, bool fade);
        void PlaySFXGlobal(string name);
        void PlaySFXLocal(string name, AudioSource audioSource);
    }
}