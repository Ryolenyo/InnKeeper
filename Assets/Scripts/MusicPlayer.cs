using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MusicEnum
{
    None,
    Menu,
    GameplayNormal,
    Result
}

public class MusicPlayer : MonoBehaviour
{
    [Header("Music Audio Clip")]
    public AudioClip menuClip;
    public AudioClip gameplayNormalClip;
    public AudioClip resultClip;

    private static MusicPlayer instance;
    private AudioSource audioSource;
    private MusicEnum currentMusic;

    private Dictionary<MusicEnum, AudioClip> musicDictionary;

    // Start is called before the first frame update
    void Start()
    {
        UpdateSingleton();
        CheckComponent();
        PopulateSfxDictionary();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    public static void PlayMusic(MusicEnum musicEnum)
    {
        if(instance)
        {
            instance.PlayMusicInstance(musicEnum);
        }
        else
        {
            Debug.LogError("MusicPlayer: No instance refernce");
        }
        
    }

    public static void Play()
    {
        if (instance)
        {
            instance.PlayInstance();
        }
        else
        {
            Debug.LogError("MusicPlayer: No instance refernce");
        }
    }

    public static void Stop()
    {
        if (instance)
        {
            instance.StopInstance();
        }
        else
        {
            Debug.LogError("MusicPlayer: No instance refernce");
        }
    }

    public static void Pause()
    {
        if (instance)
        {
            instance.PauseInstance();
        }
        else
        {
            Debug.LogError("MusicPlayer: No instance refernce");
        }
    }

    private void PlayMusicInstance(MusicEnum musicEnum)
    {
        if (musicEnum == MusicEnum.None)
        {
            StopInstance();
        }
        else
        {
            AudioClip musicClip = null;
            musicDictionary.TryGetValue(musicEnum, out musicClip);

            if (musicClip)
            {
                StopInstance();
                audioSource.clip = musicClip;
                PlayInstance();
            }
        }
    }

    private void PlayInstance()
    {
        if (audioSource.clip)
        {
            audioSource.Play();
        }
    }

    private void StopInstance()
    {
        audioSource.Stop();
    }

    private void PauseInstance()
    {
        audioSource.Pause();
    }

    private void UpdateSingleton()
    {
        Debug.Assert(!instance, "MusicPlayer: Already has instance refernce");
        instance = this;
        Debug.Assert(instance, "MusicPlayer: No instance refernce");
    }

    private void CheckComponent()
    {
        audioSource = GetComponent<AudioSource>();
        Debug.Assert(audioSource, "MusicPlayer: audioSource not found");

        Debug.Assert(menuClip, "MusicPlayer: menuClip not found");
        Debug.Assert(gameplayNormalClip, "MusicPlayer: gameplayNormalClip not found");
        Debug.Assert(resultClip, "MusicPlayer: resultClip not found");
    }

    private void PopulateSfxDictionary()
    {
        musicDictionary = new Dictionary<MusicEnum, AudioClip>();
        musicDictionary.Add(MusicEnum.Menu, menuClip);
        musicDictionary.Add(MusicEnum.GameplayNormal, gameplayNormalClip);
        musicDictionary.Add(MusicEnum.Result, resultClip);
    }
}
