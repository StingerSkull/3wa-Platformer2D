using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject winPanel;
    public GameObject pauseBtn;
    public GameObject player;
    public TextMeshProUGUI textScore;
    public TextMeshProUGUI textTime; 
    public TextMeshProUGUI finalTextScore;
    public TextMeshProUGUI finalTextTime;
    
    public TextMeshProUGUI playerScore;

    private bool isPaused = false;
    private bool canPause = true;
    private float chrono;


    private int _score = 0;
    public int Score { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        pausePanel.SetActive(isPaused);
        winPanel.SetActive(false);
        Score = 0;
        chrono = 0;
        textTime.text = "0";
        textScore.text = "0";
    }

    private void Update()
    {
        if (canPause)
        {
            chrono += Time.deltaTime;
        }
        float min = Mathf.FloorToInt(chrono / 60);
        float sec = Mathf.FloorToInt(chrono % 60);
        float milli = (chrono - (min * 60 + sec)) * 1000;
        textTime.text = string.Format("{0:00}:{1:00}:{2:000}", min, sec,milli); ;
    }

    public void Pause()
    {
        if (player.activeSelf && canPause)
        {
            isPaused = !isPaused;
            pausePanel.SetActive(isPaused);

            if (isPaused)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
    }

    public void AddScore(int score)
    {
        Score += score;
        textScore.text = Score.ToString();
    }

    public void Win()
    {
        canPause = false;
        winPanel.SetActive(true);
        pauseBtn.SetActive(false);
        finalTextScore.text = Score.ToString();

        int timeScore;

        if (chrono <= 120)
        {
            timeScore = 24000 - Mathf.FloorToInt(chrono * 100);
        }
        else
        {
            timeScore = Mathf.Min(12000 - Mathf.FloorToInt(chrono * 50), 0);
        }
        finalTextTime.text = timeScore.ToString();

        int finalscore = timeScore + Score;
        playerScore.text = finalscore.ToString();
    }

    public void ReloadScene()
    {
        string activeScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(activeScene);
        Time.timeScale = 1f;
    }
}
