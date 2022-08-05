using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public AudioClip clip;

    // The name of the sound will automatically be the name of the AudioClip
    [HideInInspector]
    public string name;

    [HideInInspector]
    public AudioSource source;
}
