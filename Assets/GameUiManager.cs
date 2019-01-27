using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUiManager : MonoBehaviour
{
    public GameObject[] gameOverlay;
    public GameObject[] resultOverlay;

    [Header("Text")]
    public Text scoreText;
    public Text timerText;
    public Text resultHighscoreText;
    public Text resultScoreText;

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject gameObject in resultOverlay)
        {
            gameObject.SetActive(false);
        }

        GameTracker.OnTimerDone += OnTimeOut;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = GameTracker.GetCurrentScore().ToString();
        timerText.text = (Mathf.CeilToInt(GameTracker.GetTimeLeft())).ToString();
    }

    private void OnDestroy()
    {
        GameTracker.OnTimerDone -= OnTimeOut;
    }

    private void OnTimeOut()
    {
        foreach (GameObject gameObject in resultOverlay)
        {
            gameObject.SetActive(true);
        }

        foreach (GameObject gameObject in gameOverlay)
        {
            gameObject.SetActive(false);
        }

        resultScoreText.text = GameTracker.GetCurrentScore().ToString();
        resultHighscoreText.text = HighScoreSystem.LoadHighScore().ToString();
    }
}
