using SFML.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPJ_SpaceCrusader_OgrizovicFlavio
{
    static class Music
    {
        static Sound _musicSound;
        static SoundBuffer _musicSoundBuffer;

        static Music ()
        {
            _musicSoundBuffer = new SoundBuffer("Audio/Dark Matter.ogg");
            _musicSound = new Sound(_musicSoundBuffer);
        }

        public static void PlayMusic()
        {
            if (_musicSound.Status != SoundStatus.Playing)
            {
                _musicSound.Play();
            }
        }

        public static void StopMusic()
        {
            if (_musicSound.Status == SoundStatus.Playing)
            {
                _musicSound.Stop();
            }
        }
    }
}
