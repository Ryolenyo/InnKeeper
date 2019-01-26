using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Spawn Parameter")]
    public AnimationCurve spawnDelay = AnimationCurve.Linear(0, 30, 1, 0);
    public float gameTime = 180f;
    private float currentDelay;
    private float currentTime;

    [Header("Hero Prefabs")]
    public GameObject[] heroPrefabs;

    private bool flag = false;
    // Start is called before the first frame update
    void Start()
    {
        GameTracker.OnTimerDone += OnTimeUp;
    }

    // Update is called once per frame
    void Update()
    {
        if(!flag)
        {
            MusicPlayer.PlayMusic(MusicEnum.GameplayNormal);
            spawnNewHero();
            GameTracker.StartTimer(gameTime);
            ResetDalay();
            flag = true;
        }

        currentTime += Time.deltaTime * Time.timeScale;
        if(currentTime >= currentDelay)
        {
            spawnNewHero();
            ResetDalay();
        }
    }

    private void OnDestroy()
    {
        GameTracker.OnTimerDone -= OnTimeUp;
    }

    void spawnNewHero()
    {
        GameObject newHero = Instantiate(heroPrefabs[Random.Range(0, heroPrefabs.Length)]) as GameObject;
        Debug.Log(newHero);
        ReceptionRoom.instance.AddHeroCheckin(newHero.GetComponent<Hero>());
    }

    private void ResetDalay()
    {
        currentDelay = spawnDelay.Evaluate(1f - GameTracker.GetTimeLeftNormalized());
        currentTime = 0;
    }

    private void OnTimeUp()
    {
        Time.timeScale = 0f;
        int currentScore = GameTracker.GetCurrentScore();
        HighScoreSystem.SaveHighScore(currentScore);
    }
}
