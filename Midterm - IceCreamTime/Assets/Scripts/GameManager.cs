using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject projectilePrefab;
    public Scener _scener;
    private int _score;

    public int Score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
            _scoreGui.text = _score.ToString();
        }
    }

    [SerializeField] TextMeshProUGUI _scoreGui;

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

        _scener = FindObjectOfType<Scener>();
    }

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        StartCoroutine(SpawnProjectileRoutine());
    }


    public void LevelFailed()
    {
        _scener.lives--;

        if (_scener.lives <= 0)
        {
            _scener.NewGame();
        }
        else
        {
            SceneManager.LoadScene("Level1");
        }
    }

    private void Update()
    {
        if (Score > 800)
        {
            SceneManager.LoadScene("You Won!");
        }
    }

    public void IncreaseScore()
    {
        Score += 100;
    }

    private IEnumerator SpawnProjectileRoutine()
    {
        while (true) // This will keep the routine running indefinitely
        {
            Debug.Log("called");
            SpawnRandomProjectile();
            yield return new WaitForSeconds(3f); // Wait for 3 seconds before the next call
        }
    }

    public void SpawnRandomProjectile()
    {
        float randomY = Random.Range(-3f, 4f);

        float spawnX = (Random.value < 0.5f) ? -7f : 7f;

        Vector2 spawnPosition = new Vector2(spawnX, randomY);

        GameObject spawnedProjectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
    }
}
