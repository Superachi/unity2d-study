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
            s.source.volume = 1f;
            s.source.pitch = 1f;
        }
    }

    public static void PlaySound(string name, float volume = 1f, float pitch = 1f)
    {
        AudioManager a = FindObjectOfType<AudioManager>();
        Sound s = Array.Find(a.sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogError($"AudioClip {name} not found (Array.Find returned {s}). Was the filename spelled correctly?");
            return;
        }

        s.source.volume = volume * a.globalVolume;
        s.source.pitch = pitch;
        s.source.Play();
    }
}
