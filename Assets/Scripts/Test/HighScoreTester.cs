using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreTester : MonoBehaviour
{
    public int currentScore;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("HighScoreTester: Current Highscore = " + HighScoreSystem.LoadHighScore());
        Debug.Log("HighScoreTester: Current Score = " + currentScore);
        if(HighScoreSystem.SaveHighScore(currentScore))
        {
            Debug.Log("HighScoreTester: New Highscore");
        }
        Debug.Log("HighScoreTester: Current Highscore = " + HighScoreSystem.LoadHighScore());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
