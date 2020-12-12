using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public GameObject hazard;
    public Vector3 spawnValues;
    public int asteroidCount;
    public float startWait;
    public float spawnWait;
    public float waveWait;

    public Text scoreText;
    public int score;

    public Text restartText;
    public Text gameOverText;
    private bool restart;
    public bool gameOver;
    public CardsController cardContainer;


    private void Update()
    {
        if (restart && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }
        if (gameOver)
        {
            restartText.gameObject.SetActive(true);
            restart = true;
        }
    }

    void Start()
    {
        restart = false;
        restartText.gameObject.SetActive(false);
        gameOver = false;
        gameOverText.gameObject.SetActive(false);
        score = 0;
        updateScore();
        StartCoroutine(SpawnWaves());
    }

    // Update is called once per frame
    IEnumerator SpawnWaves(){
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < asteroidCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Instantiate(hazard, spawnPosition, Quaternion.identity);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
            if (gameOver)
            {
                break;
            }
        }
        
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        if (score % 10 == 0)
        {
            spawnWait /= 2;
            CardsController.instance.cardsNumber++;
        }
        updateScore();
        if (score == 40) GameOver("Felicidades, Salvaste a los Dinosaurios");
    }

    void updateScore()
    {
        scoreText.text = "Score:" + score;
    }

    public void GameOver(string text="Game Over")
    {
        gameOverText.gameObject.SetActive(true);
        gameOver = true;
        gameOverText.text = text;
        cardContainer.gameObject.SetActive(true);
        for (int i = 0; i < CardsController.instance.cardsNumber; i++)
        {
            CardsController.instance.cards[i].SetActive(true);
        }
    }
}
