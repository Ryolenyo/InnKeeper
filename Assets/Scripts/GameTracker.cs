using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTracker : MonoBehaviour
{
    public delegate void OnVoidEvent();
    public static event OnVoidEvent OnTimerDone;

    private static GameTracker instance;
    private int currentScore = 0;
    private float timerLength = 0;
    private float timeLeft = 0;
    private bool isTimerUse = false;

    // Start is called before the first frame update
    void Start()
    {
        UpdateSingleton();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimerUse)
        {
            timeLeft -= Time.deltaTime;
            if(timeLeft <= 0)
            {
                timeLeft = 0;
                isTimerUse = false;
                OnTimerDone();
            }
        }
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    public static void AddScore(int score)
    {
        if (instance)
        {
            instance.currentScore += score;
        }
        else
        {
            Debug.LogError("GameTracker: No instance refernce");
        }
    }

    public static int GetCurrentScore()
    {
        if (instance)
        {
            return instance.currentScore;
        }
        else
        {
            Debug.LogError("GameTracker: No instance refernce");
            return -1;
        }
    }

    public static void StartTimer(float time)
    {
        if (instance)
        {
            if (time > 0)
            {
                instance.timerLength = time;
                instance.timeLeft = time;
                instance.isTimerUse = true;
            }
            else
            {
                Debug.LogError("GameTracker: Timer Length must be more than 0 second.");
            }
        }
        else
        {
            Debug.LogError("GameTracker: No instance refernce");
        }
    }

    public static float GetTimeLeft()
    {
        if (instance)
        {
            return instance.timeLeft;
        }
        else
        {
            Debug.LogError("GameTracker: No instance refernce");
            return 0;
        }
    }

    public static float GetTimeLeftNormalized()
    {
        if (instance)
        {
            if (instance.timerLength <= 0)
            {
                return 0;
            }
            else
            {
                return instance.timeLeft / instance.timerLength;
            }
        }
        else
        {
            Debug.LogError("GameTracker: No instance refernce");
            return 0;
        }
    }

    private void UpdateSingleton()
    {
        Debug.Assert(!instance, "GameTracker: Already has instance refernce");
        instance = this;
        Debug.Assert(instance, "GameTracker: No instance refernce");
    }
}
