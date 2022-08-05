using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Via the Unity editor, instances of the Sound class can be added to this array
    public Sound[] sounds;

    [Range(0f, 1f)]
    public float globalVolume = 1;

    void Awake()
    {
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.name = s.clip.name;
        }
    }

    public static void PlaySound(string name, float volume = 1f, float pitch = 1f)
    {
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        Sound sound = audioManager.FindSound(name, audioManager.sounds);
        if (sound == null) return;

        audioManager.Play(sound, volume * audioManager.globalVolume, pitch);
    }

    private Sound FindSound(string soundName, Sound[] soundArray)
    {
        Sound sound = Array.Find(soundArray, sound => sound.name == soundName);
        if (sound == null)
        {
            Debug.LogError($"AudioClip {name} not found. Is the specified name in the AudioManager's sound array?");
        }

        return sound;
    }

    private void Play(Sound sound, float volume, float pitch)
    {
        sound.source.volume = volume;
        sound.source.pitch = pitch;
        sound.source.Play();
    }
}
