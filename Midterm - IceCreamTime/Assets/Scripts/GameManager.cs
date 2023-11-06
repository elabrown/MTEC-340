using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject projectilePrefab;
    public Scener _scener;
    private bool isGamePaused = false;
    public GameObject pausetext;
    private int _score;
    public int Score
    {
        get => _score;
        set
        {
            _score = value;
            _scoreGui.text = _score.ToString();
        }
    }
    [SerializeField] private TextMeshProUGUI _scoreGui;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        _scener = FindObjectOfType<Scener>();
    }

    private void Start()
    {
        StartCoroutine(SpawnProjectileRoutine());
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        isGamePaused = !isGamePaused;

        if (isGamePaused)
        {
            Time.timeScale = 0f;
            pausetext.SetActive(true);        
        }
        else
        {
            Time.timeScale = 1f;
            pausetext.SetActive(false);
        }
    }
    public void LevelFailed()
    {
        _scener.Lives--;

        if (_scener.Lives <= 0)
        {
            StopCoroutine(SpawnProjectileRoutine());
            _scener.GameOver();
        }
        else
        {
            SceneManager.LoadScene("Level1");
        }
    }

    public void IncreaseScore()
    {
        Score += 100;
        if (Score >= 1000)
        {
            StopCoroutine(SpawnProjectileRoutine());
            _scener.GameWon();
        }
    }

    private IEnumerator SpawnProjectileRoutine()
    {
        while (true)
        {
            Debug.Log("SpawnProjectile called");
            SpawnRandomProjectile();
            yield return new WaitForSeconds(3f);
        }
    }

    public void SpawnRandomProjectile()
    {
        float randomY = Random.Range(-3f, 4f);
        float spawnX = Random.value < 0.5f ? -7f : 7f;
        Vector2 spawnPosition = new Vector2(spawnX, randomY);

        Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
    }
}