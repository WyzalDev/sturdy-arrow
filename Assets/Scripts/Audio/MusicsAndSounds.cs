using System;
using UnityEngine;

namespace SturdyArrow.Audio
{
    [CreateAssetMenu(menuName = "ScriptableObjects/MusicAndSounds", order = 1)]
    public class MusicsAndSounds : ScriptableObject
    {
        public Sound[] musicSounds;

        public Sound[] sfxSounds;

        public Sound GetMusic(string name)
        {

            if(Array.Exists(musicSounds, x => x.name == name))
            {
                return Array.Find<Sound>(musicSounds, x => x.name == name);
            }
            else
            {
                throw new ArgumentException($"Music with {name} name did'nt exists.");
            }
        }

        public Sound GetSfx(string name)
        {

            if(Array.Exists(sfxSounds, x => x.name == name))
            {
                return Array.Find<Sound>(sfxSounds, x => x.name == name);
            }
            else
            {
                throw new ArgumentException($"SFX with {name} name did'nt exists.");
            }
        }

    }
}