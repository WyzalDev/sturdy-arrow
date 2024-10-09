using System;
using UnityEngine;

namespace SturdyArrow.Audio
{
    [Serializable]
    public class Sound
    {
        public string name;

        public float volume = 1f;

        public AudioClip clip;
    }
}