using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SfxEnum
{
    ReceptionBell,
    UiClick,
    HeroCheckoutHappy,
    HeroCheckoutNeutral,
    HeroCheckoutAngry
}

public class SfxPlayer : MonoBehaviour
{
    [Header("SFX Audio Clip")]
    public AudioClip receptionBellClip;
    public AudioClip uiClickClip;
    public AudioClip heroCheckoutHappyClip;
    public AudioClip heroCheckoutNeutralClip;
    public AudioClip heroCheckoutAngryClip;

    [Header("Volume")]
    [Range(0.0f, 1.0f)] public float volume = 1.0f;

    private static SfxPlayer instance;
    private Dictionary<SfxEnum, AudioClip> sfxDictionary;

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

    public static void PlaySfx(SfxEnum sfxEnum)
    {
        if (instance)
        {
            instance.PlaySfxInstance(sfxEnum, Vector3.zero);
        }
    }

    public static void PlaySfxAtLocation(SfxEnum sfxEnum, Vector3 location)
    {
        if (instance)
        {
            instance.PlaySfxInstance(sfxEnum, location);
        }
    }

    private void PlaySfxInstance(SfxEnum sfxEnum, Vector3 location)
    {
        AudioClip targetClip = null;
        sfxDictionary.TryGetValue(sfxEnum, out targetClip);

        if (targetClip)
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
        sfxDictionary = new Dictionary<SfxEnum, AudioClip>();
        sfxDictionary.Add(SfxEnum.ReceptionBell, receptionBellClip);
        sfxDictionary.Add(SfxEnum.UiClick, uiClickClip);
        sfxDictionary.Add(SfxEnum.HeroCheckoutHappy, heroCheckoutHappyClip);
        sfxDictionary.Add(SfxEnum.HeroCheckoutNeutral, heroCheckoutNeutralClip);
        sfxDictionary.Add(SfxEnum.HeroCheckoutAngry, heroCheckoutAngryClip);
    }

    private void OnDestroy()
    {
        if (instance == this)
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

    private void CheckComponent()
    {
        Debug.Assert(receptionBellClip, "SfxPlayer: ReceptionBellClip not found");
        Debug.Assert(uiClickClip, "SfxPlayer: UiClickClip not found");
        Debug.Assert(heroCheckoutHappyClip, "SfxPlayer: HeroCheckoutHappyClip not found");
        Debug.Assert(heroCheckoutNeutralClip, "SfxPlayer: HeroCheckoutNeutralClip not found");
        Debug.Assert(heroCheckoutAngryClip, "SfxPlayer: HeroCheckoutAngryClip not found");
    }
}
