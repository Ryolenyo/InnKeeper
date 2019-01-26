using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTrackerTester : MonoBehaviour
{
    bool isTesting = true;
    // Start is called before the first frame update
    void Start()
    {
        GameTracker.StartTimer(0.5f);
        GameTracker.OnTimerDone += OnTimerDone;

        Debug.Log("GameTrackerTester: Current score = " + GameTracker.GetCurrentScore());
        GameTracker.AddScore(5);
        Debug.Log("GameTrackerTester: Add 5 score");
        Debug.Log("GameTrackerTester: Current score = " + GameTracker.GetCurrentScore());
    }

    // Update is called once per frame
    void Update()
    {
        if(isTesting)
        {
            Debug.Log("GameTrackerTester: Time Left = " + GameTracker.GetTimeLeft() 
                + " / Normalized = " + GameTracker.GetTimeLeftNormalized());
        }
    }

    private void OnDestroy()
    {
        GameTracker.OnTimerDone -= OnTimerDone;
    }

    private void OnTimerDone()
    {
        isTesting = false;
        Debug.Log("GameTrackerTester: Timer is done.");
    }
}
