using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgain : MonoBehaviour {
    public void LoadFirstLevel ()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(1);
    }
}
