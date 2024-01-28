using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader Instance;
    [SerializeField]
    private Animator transition;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void LoadNextLevel()
    {
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextLevel);
        //StartCoroutine(WaitForTransition(0.1f, nextLevel));
    }

    public void LoadLevelWithIndex(int nextLevel)
    {
        SceneManager.LoadScene(nextLevel);
        //StartCoroutine(WaitForTransition(0.1f, nextLevel));
    }


    public void TransitionLoadNextLevel()
    {
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        //SceneManager.LoadScene(nextLevel);
        StartCoroutine(WaitForTransition(0.1f, nextLevel));
    }

    public void TransitionLoadLevelWithIndex(int nextLevel)
    {
        //SceneManager.LoadScene(nextLevel);
        StartCoroutine(WaitForTransition(0.1f, nextLevel));
    }


    IEnumerator WaitForTransition(float waitTime, int nextLevel)
    {
        // Wait time until fadeout
        yield return new WaitForSeconds(waitTime);
        // Fade out starts, yield time to allow the fadeout to happen
        transition.SetTrigger("StartCrossfade");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(nextLevel);
    }

}
