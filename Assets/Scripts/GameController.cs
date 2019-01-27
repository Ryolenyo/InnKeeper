using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Spawn Parameter")]
    public AnimationCurve spawnDelay = AnimationCurve.Linear(0, 30, 1, 0);
    public float gameTime = 180f;
    private float currentDelay = 0.01f;
    private float currentTime = 0f;

    [Header("Hero Prefabs")]
    public GameObject[] heroPrefabs;

    private bool flag = false;
    private bool flag2 = false;
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
            GameTracker.StartTimer(gameTime);
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
