using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scener : MonoBehaviour
{
    public static Scener Instance;

    public string level = "Level1";
    public int lives = 3;

    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return; // Ensure that the rest of the Awake method doesn't run for this duplicate
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadScene("MenuScreen");
    }

    public void LoadLevel1()
    {
        Debug.Log("Loading First Level");
        LoadScene("Level1");
    }

    public void NewGame()
    {
        lives = 3;
        LoadScene("MenuScreen");
    }

    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        Scene currentScene = SceneManager.GetActiveScene();

        // Load the new scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        // Wait for the scene to finish loading
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Set the new scene as active
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));

        // Unload the previous scene only if more than one scene is loaded
        if (SceneManager.sceneCount > 1)
        {
            yield return SceneManager.UnloadSceneAsync(currentScene);
        }
    }

    private void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }
}
