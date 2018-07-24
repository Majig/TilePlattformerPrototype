using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersist : MonoBehaviour {
    // config fields

    // stats
    private int initialSceneBuildIndex;

    private void Awake()
    {
        int scenePersistCount = FindObjectsOfType<ScenePersist>().Length;

        if (scenePersistCount > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Use this for initialization
    void Start ()
    {
        initialSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
	}

    private void Update()
    {
        int currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneBuildIndex != initialSceneBuildIndex)
        {
            Destroy(gameObject);
        }
    }
}
