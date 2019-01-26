using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreSystem : MonoBehaviour
{
    private static string playerPrefKey = "HighScoreTest";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static int LoadHighScore()
    {
        if (!PlayerPrefs.HasKey(playerPrefKey))
        {
            PlayerPrefs.SetInt(playerPrefKey, 0);
        }

        return PlayerPrefs.GetInt(playerPrefKey);
    }

    public static bool SaveHighScore(int score)
    {
        bool result = score > LoadHighScore();

        if (result)
        {
            PlayerPrefs.SetInt(playerPrefKey, score);
        }

        return result;
    }
}
