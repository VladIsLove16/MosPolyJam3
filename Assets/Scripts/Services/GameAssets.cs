using UnityEngine;
using UnityEngine.Audio;
public class GameAssets : MonoBehaviour, IService
{
    public SoundAudioClip[] soundAudioClipArray;

    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }
    public AudioMixer audioMixer;
    public AudioMixerGroup SoundsAudioMixerGroup;
    public AudioMixerGroup MusicAudioMixer;
    public AudioMixerGroup GlobalAudioMixer;

}