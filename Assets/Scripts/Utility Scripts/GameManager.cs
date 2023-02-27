using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Public Variables
    public int coins = 0;

    [Header("Times of Waiting")]
    public float timeStartRound = 2.0f;
    public float timeBetweenSpawn = 2.0f;

    [Header("Spawners")]
    public Transform[] spawners;

    [Header("Enemies")]
    public GameObject enemy;
    public float enemyHealth = 1.0f;
    public float enemySpeed = 2.5f;

    [Header("UI Elements")]
    public GameObject mainCanvas;
    public Text roundText;
    public Text enemiesText;
    public Text coinsText;
    public Text storeCoinsText;
    public Text gameOverRoundsText;

    [Header("Menus Elements")]
    public GameObject pauseMenu;
    public GameObject panel;
    public GameObject storeScreen;
    public GameObject gameOverMenu;

    //Private Variables
    private int enemiesToSpawn;
    private int enemiesAlive;
    private int round = 1;
    private bool isPaused = false;

    private void Start()
    {
        enemiesToSpawn = 3;
        enemiesAlive = enemiesToSpawn;

        roundText.text = round.ToString();
        enemiesText.text = enemiesAlive.ToString();
        coinsText.text = coins.ToString();

        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(timeStartRound);

        for(int i = 0; i < enemiesToSpawn; i++)
        {
            GameObject newEnemy = Instantiate(enemy, spawners[Random.Range(0, spawners.Length)].position, Quaternion.identity);
            newEnemy.GetComponent<Enemy_Movement>().health = enemyHealth;
            newEnemy.GetComponent<Enemy_Movement>().speed = enemySpeed;
            yield return new WaitForSeconds(timeBetweenSpawn);
        }
    }

    public void NextRound()
    {
        round++;
        roundText.text = round.ToString();

        enemiesToSpawn += 2;
        enemiesAlive = enemiesToSpawn;
        enemiesText.text = enemiesAlive.ToString();
        coinsText.text = coins.ToString();

        storeScreen.gameObject.SetActive(false);
        mainCanvas.gameObject.SetActive(true);

        Time.timeScale = 1;

        if(round % 2 == 0)
        {
            enemySpeed += 0.25f;
        }
        else
        {
            enemyHealth += 0.5f;
        }

        StartCoroutine(SpawnEnemies());
    }

    public void PauseGame()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0;
            panel.gameObject.SetActive(true);
            pauseMenu.gameObject.SetActive(true);
            gameObject.GetComponent<AudioSource>().Pause();
        }
        else
        {
            Time.timeScale = 1;
            panel.gameObject.SetActive(false);
            pauseMenu.gameObject.SetActive(false);
            gameObject.GetComponent<AudioSource>().Play();
        }
    }

    public void KillEnemy(int money)
    {
        enemiesAlive--;
        enemiesText.text = enemiesAlive.ToString();

        coins += money;
        coinsText.text = coins.ToString();

        if(enemiesAlive == 0)
        {
            Time.timeScale = 0;
            storeCoinsText.text = coins.ToString();
            mainCanvas.gameObject.SetActive(false);
            storeScreen.gameObject.SetActive(true);
            storeScreen.GetComponent<StoreManager>().OpenStore();
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverRoundsText.text = round.ToString();
        gameOverMenu.SetActive(true);
    }
}
