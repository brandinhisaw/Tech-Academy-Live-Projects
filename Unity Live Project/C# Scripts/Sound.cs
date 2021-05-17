using UnityEngine.Audio;
using UnityEngine;

// Sound object tutorial/solution by Brackeys
// https://www.youtube.com/watch?v=6OT43pvUyfY

[System.Serializable]
public class Sound
{
    // Name string to identify sound file by name
    public string name;

    // The playable audio portion of the Sound object
    public AudioClip clip;

    // Control the volume and pitch of each individual Sound
    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;

    // Allow sound to be looped
    public bool loop;

    // Instantiate AudioSource object to be used by the AudioManager
    [HideInInspector]
    public AudioSource source;
}
