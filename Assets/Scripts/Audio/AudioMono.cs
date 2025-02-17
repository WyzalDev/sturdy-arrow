using DG.Tweening;
using System;
using UnityEngine;

namespace SturdyArrow.Audio
{
    public class AudioMono : MonoBehaviour
    {
        #region MUSIC_PARAMS
        [Header("Clips")]
        [SerializeField]
        private MusicsAndSounds musicAndSound;

        [Header("Sources")]
        [SerializeField]
        private AudioSource musicSource;

        [SerializeField]
        private AudioSource sfxSource;

        [Header("Transitions (in seconds)")]
        [SerializeField]
        private float transitionsDuration;

        [Header("Volume")]
        [SerializeField]
        [Range(0f, 1f)]
        private float sourceVolume;

        private Sound currentMusic;

        #endregion

        #region MUSIC_METHODS
        public void PlayMusic(string name, bool fade)
        {
            var nextMusic = musicAndSound.GetMusic(name);
            if(!nextMusic.name.Equals(currentMusic?.name))
                SetNextMusic(nextMusic, fade);
        }

        private void SetNextMusic(Sound music, bool fade)
        {
            if(fade)
            {
                musicSource.DOFade(0, transitionsDuration * 0.5f)
                    .OnKill(() =>
                        PlayNextMusic(music))
                    .OnComplete(() =>
                        musicSource.DOFade(sourceVolume, transitionsDuration * 0.5f));
            }
            else
            {
                PlayNextMusic(music);
            }
        }

        private void PlayNextMusic(Sound nextMusic)
        {
            musicSource.clip = nextMusic.clip;
            musicSource.Play();
            currentMusic = nextMusic;
        }
        #endregion

        #region SFX_METHODS
        public void PlaySfxGlobal(string name)
        {
            Sound sound = musicAndSound.GetSfx(name);
            sfxSource.PlayOneShot(sound.clip, sound.volume);
        }
        public void PlaySfxLocal(string name, AudioSource source)
        {
            Sound sound = musicAndSound.GetSfx(name);
            if(source != null)
            {
                source.PlayOneShot(sound.clip, sound.volume);
            }
            else
            {
                throw new NullReferenceException("Audio source is null");
            }
        }
        #endregion
    }
}