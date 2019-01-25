using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Sfx
{
    ReceptionBell,
    UiClick,
    HeroCheckoutHappy,
    HeroCheckoutNeutral,
    HeroCheckoutAngry
}

public class SfxPlayer : MonoBehaviour
{
    private static SfxPlayer instance;

    [Header("SFX Audio Clip")]
    public AudioClip ReceptionBellClip;
    public AudioClip UiClickClip;
    public AudioClip HeroCheckoutHappyClip;
    public AudioClip HeroCheckoutNeutralClip;
    public AudioClip HeroCheckoutAngryClip;

    [Header("Volume")]
    [Range(0.0f, 1.0f)] public float volume = 1.0f;

    private Dictionary<Sfx, AudioClip> sfxDictionary;

    // Start is called before the first frame update
    void Start()
    {
        UpdateSingleton();
        CheckAudioClip();
        PopulateSfxDictionary();
    }


    // Update is called once per frame
    void Update()
    {

    }

    public static void PlaySfx(Sfx sfxEnum)
    {
        if(instance)
        {
            instance.PlaySfxInstance(sfxEnum, Vector3.zero);
        }
    }

    public static void PlaySfxAtLocation(Sfx sfxEnum, Vector3 location)
    {
        if (instance)
        {
            instance.PlaySfxInstance(sfxEnum, location);
        }
    }

    private void PlaySfxInstance(Sfx sfxEnum, Vector3 location)
    {
        AudioClip targetClip = null;
        sfxDictionary.TryGetValue(sfxEnum, out targetClip);

        if(targetClip)
        {
            AudioSource.PlayClipAtPoint(targetClip, location, volume);
        }
        else
        {
            Debug.LogError("SfxPlayer: Clip not found");
        }
    }

    private void PopulateSfxDictionary()
    {
        sfxDictionary = new Dictionary<Sfx, AudioClip>();

        sfxDictionary.Add(Sfx.ReceptionBell, ReceptionBellClip);
        sfxDictionary.Add(Sfx.UiClick, UiClickClip);
        sfxDictionary.Add(Sfx.HeroCheckoutHappy, HeroCheckoutHappyClip);
        sfxDictionary.Add(Sfx.HeroCheckoutNeutral, HeroCheckoutNeutralClip);
        sfxDictionary.Add(Sfx.HeroCheckoutAngry, HeroCheckoutAngryClip);
    }

    private void OnDestroy()
    {
        if(instance == this)
        {
            instance = null;
        }
    }

    private void UpdateSingleton()
    {
        Debug.Assert(!instance, "SfxPlayer: Already has instance refernce");
        instance = this;
        Debug.Assert(instance, "SfxPlayer: No instance refernce");
    }

    private void CheckAudioClip()
    {
        Debug.Assert(ReceptionBellClip, "SfxPlayer: ReceptionBellClip");
        Debug.Assert(UiClickClip, "SfxPlayer: UiClickClip");
        Debug.Assert(HeroCheckoutHappyClip, "SfxPlayer: HeroCheckoutHappyClip");
        Debug.Assert(HeroCheckoutNeutralClip, "SfxPlayer: HeroCheckoutNeutralClip");
        Debug.Assert(HeroCheckoutAngryClip, "SfxPlayer: HeroCheckoutAngryClip");
    }
}
