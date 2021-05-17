using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public Animator anim;

    public float transitionTime = 1f;

    private void Update()
    {
        // Input check used to test functionality
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    LoadNextLevel();
        //}
    }

    public void LoadNextLevel()
    {
        StartCoroutine(FadeToNextLevel());
    }

    // The function below can be called using an animation event at the end 
    // of the Fade_Out animation instead of using the coroutine

    //// Called by the animation event at the end Fade_Out animation
    //public void OnFadeComplete()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //}

    IEnumerator FadeToNextLevel()
    {
        // Trigger fade out animation
        anim.SetTrigger("FadeOut");

        // Wait for transitionTime
        yield return new WaitForSeconds(transitionTime);

        // Load the next scene in the build index
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
