using System;
using UnityEngine.Audio;
using UnityEngine;

// Audio Manager tutorial/solution by Brackeys
// https://www.youtube.com/watch?v=6OT43pvUyfY

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    
    public static AudioManager instance;

    void Awake()
    {
        // Ensure only one instance of AudioManager that persists
        #region SingletonPattern 

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        #endregion

        foreach (Sound s in sounds)
        {
            // Instantiate AudioSource on the Sound object and load the clip
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            // Prepare sounds by writing inspector values to the Sound object source
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play (string name)
    {
        // Search for sound by name parameter
        Sound s = Array.Find(sounds, sound => sound.name == name);

        // Handle sound that is not found in array
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found.");
            return;
        }

        // Play the sound
        s.source.Play();
    }
}
