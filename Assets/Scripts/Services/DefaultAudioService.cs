using SturdyArrow.Audio;
using System;
using UnityEngine;

namespace SturdyArrow.Services
{
    public class DefaultAudioService : IAudioService
    {
        private AudioMono audioMono;

        public DefaultAudioService(AudioMono mono) => audioMono = mono;

        //TODO : refactor PlayMusic to add two methods - PLayMusicGlobal, PlayMusicLocal
        public void PlayMusic(string name, bool fade)
        {
            try
            {
                audioMono.PlayMusic(name, fade);
            }
            catch(ArgumentException exception)
            {
                Debug.LogException(exception);
            }
        }

        public void PlaySFXGlobal(string name)
        {
            try
            {
                audioMono.PlaySfxGlobal(name);
            }
            catch(ArgumentException exception)
            {
                Debug.LogException(exception);
            }
        }

        public void PlaySFXLocal(string name, AudioSource audioSource)
        {
            try
            {
                audioMono.PlaySfxLocal(name, audioSource);
            }
            catch(NullReferenceException exception)
            {
                Debug.LogException(exception);
            }
            catch(ArgumentException exception)
            {
                Debug.LogException(exception);
            }
        }
    }
}