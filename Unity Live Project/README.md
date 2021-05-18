# Unity Live Project

## Introduction
The Tech Academy runs a Live Project for two weeks where I worked with a team of my peers to develop a PuttPutt mini-golf game using Unity and C#. I worked on several stories to improve both the design and functionality of a new course, Hole03. This involved crafting a new course and modeling the environment with scenery, lighting, and particle effects. Systems were implemented to handle audio and scene management in a scalable way.

## Design Stories
The new level, Hole03, was provided with a very simple course consisting of only a start and end. I was then given a story to create my own level with provided models and materials and then decorate the background scene. Additionally, I was given stories to a create lighting effects (lanterns on the ground, fire light) as well as particle effects (fire and smoke, mist from the river rocks).

* [Level Design](https://github.com/brandinhisaw/Tech-Academy-Live-Projects/blob/main/Unity%20Live%20Project/Screenshots/Hole03Level.JPG)
* [Camera, Lighting, Particle Effects](https://github.com/brandinhisaw/Tech-Academy-Live-Projects/blob/main/Unity%20Live%20Project/Screenshots/Hole03Play.JPG)

## Systems Stories
As part of the overall game, many systems would need to be implemented that can also be extended and scaled easily. In order to provide sound to the game, I implemented the Sound class and AudioManager singleton pattern. Credit to Brackeys for the tutorial video on how to implement the Sound class and AudioManager: https://www.youtube.com/watch?v=6OT43pvUyfY

The sound class gives a basic structure to add clips of audio and adjust the volume and pitch. Could easily be extended to include other Unity and audio properties.
* [Sound Class](https://github.com/brandinhisaw/Tech-Academy-Live-Projects/blob/main/Unity%20Live%20Project/C%23%20Scripts/Sound.cs)

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

The AudioManager stores an array of Sounds and can be accessed through an instance of the class and can be attached to a designated AudioManager object in Unity. The Sound   array can be expanded and added to in the Unity Editor. This allows you to quickly add sound files, edit their volume and pitch, and create clips that can be called with the Play method. The awake method of the AudioManager script will assign each sound with their own AudioSource component.

![image](https://user-images.githubusercontent.com/26210440/118733697-9df4ce80-b7f1-11eb-9aaf-619cc3eb8af5.png)

* [AudioManager](https://github.com/brandinhisaw/Tech-Academy-Live-Projects/blob/main/Unity%20Live%20Project/C%23%20Scripts/AudioManager.cs)

      public Sound[] sounds;   

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
   
The AudioManager contains a Play method to play any Sound you have added to the Sounds array. Simply supply the Sound's name string and it was will check that it exists and will use the Play() method of the AudioSource to play the clip.

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

The SceneLoader script and Coroutine were added to a SceneLoader prefab UI Canvas which acts as a universal control for transitions between scenes. An entry animation is created that that SceneLoader will run as the entry/default animation. The LoadNextLevel() method would be called when the ball reaches the hole at the end of the course to transition to the next scene. You would simply edit the animations of the SceneLoader and adjust the Coroutine and the transition could be any animation you like. 

* [SceneLoader](https://github.com/brandinhisaw/Tech-Academy-Live-Projects/blob/main/Unity%20Live%20Project/C%23%20Scripts/SceneLoader.cs)

      public Animator anim;

      public float transitionTime = 1f;

      public void LoadNextLevel()
      {
          StartCoroutine(FadeToNextLevel());
      }

      IEnumerator FadeToNextLevel()
      {
          // Trigger fade out animation
          anim.SetTrigger("FadeOut");

          // Wait for transitionTime
          yield return new WaitForSeconds(transitionTime);

          // Load the next scene in the build index
          SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
      }

Alternatively, the animation can include an Animation Event to call the "OnFadeComplete" method to move to the next scene rather than use the Coroutine.

      public void OnFadeComplete()
      {
          SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
      }

