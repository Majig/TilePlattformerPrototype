using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    // Config Fields
    [SerializeField] float LevelLoadDelay;
    [SerializeField] float TimeScale;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // start coroutine
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel()
    {
        Time.timeScale = TimeScale;
        yield return new WaitForSecondsRealtime(LevelLoadDelay);
        Time.timeScale = 1;

        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
