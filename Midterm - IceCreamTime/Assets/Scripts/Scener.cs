using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scener : MonoBehaviour
{
    public static Scener Instance;
    [SerializeField] private TextMeshProUGUI _livesGui;
    private int _lives = 3;
    public int Lives
    {
        get => _lives;
        set => _lives = value;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void LoadLevel1()
    {
        Lives = 3;
        Debug.Log("Loading First Level");
        LoadScene("Level1");
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        LoadScene("GameOver");
    }
    public void GameWon()
    {
        Debug.Log("Game Won!");
        LoadScene("GameWon");
    }
    public void AssignLivesText()
    {
        _livesGui = GameObject.FindWithTag("LivesText")?.GetComponent<TextMeshProUGUI>();

        // Update the text immediately with the current lives if the component is found
        if (_livesGui != null)
        {
            _livesGui.text = Lives.ToString();
        }
        else
        {
            Debug.LogError("LivesText object with TextMeshProUGUI component not found after scene load.");
        }
    }   
    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));

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
