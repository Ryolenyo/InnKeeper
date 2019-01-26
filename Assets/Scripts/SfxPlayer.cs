using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SfxEnum
{
    KitchenFoodDone,
    BedroomCleanDone,
    Click1,
    Click2,
    Click3,
    HeroCheckIn,
    HeroHungry,
    HeroGetFood,
    HeroRoomGood,
    HeroRoomNormal,
    HeroRoomBad,
    HeroCheckout,
    ResultGood,
    ResultNormal,
    ResultBad,
}

public class SfxPlayer : MonoBehaviour
{
    [Header("SFX Audio Clip")]
    public AudioClip KitchenFoodDone;
    public AudioClip BedroomCleanDone;
    public AudioClip Click1;
    public AudioClip Click2;
    public AudioClip Click3;
    public AudioClip HeroCheckIn;
    public AudioClip HeroHungry;
    public AudioClip HeroGetFood;
    public AudioClip HeroRoomGood;
    public AudioClip HeroRoomNormal;
    public AudioClip HeroRoomBad;
    public AudioClip HeroCheckout;
    public AudioClip ResultGood;
    public AudioClip ResultNormal;
    public AudioClip ResultBad;

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

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    public static void PlaySfx(SfxEnum sfxEnum)
    {
        if (instance)
        {
            instance.PlaySfxInstance(sfxEnum, Vector3.zero);
        }
        else
        {
            Debug.LogError("SfxPlayer: No instance refernce");
        }
    }

    public static void PlaySfxAtLocation(SfxEnum sfxEnum, Vector3 location)
    {
        if (instance)
        {
            instance.PlaySfxInstance(sfxEnum, location);
        }
        else
        {
            Debug.LogError("SfxPlayer: No instance refernce");
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
        sfxDictionary.Add(SfxEnum.KitchenFoodDone, KitchenFoodDone);
        sfxDictionary.Add(SfxEnum.BedroomCleanDone, BedroomCleanDone);
        sfxDictionary.Add(SfxEnum.Click1, Click1);
        sfxDictionary.Add(SfxEnum.Click2, Click2);
        sfxDictionary.Add(SfxEnum.Click3, Click3);
        sfxDictionary.Add(SfxEnum.HeroCheckIn, HeroCheckIn);
        sfxDictionary.Add(SfxEnum.HeroHungry, HeroHungry);
        sfxDictionary.Add(SfxEnum.HeroGetFood, HeroGetFood);
        sfxDictionary.Add(SfxEnum.HeroRoomGood, HeroRoomGood);
        sfxDictionary.Add(SfxEnum.HeroRoomNormal, HeroRoomNormal);
        sfxDictionary.Add(SfxEnum.HeroRoomBad, HeroRoomBad);
        sfxDictionary.Add(SfxEnum.HeroCheckout, HeroCheckout);
        sfxDictionary.Add(SfxEnum.ResultGood, ResultGood);
        sfxDictionary.Add(SfxEnum.ResultNormal, ResultNormal);
        sfxDictionary.Add(SfxEnum.ResultBad, ResultBad);

    }

    private void UpdateSingleton()
    {
        Debug.Assert(!instance, "SfxPlayer: Already has instance refernce");
        instance = this;
        Debug.Assert(instance, "SfxPlayer: No instance refernce");
    }

    private void CheckComponent()
    {
        Debug.Assert(KitchenFoodDone, "SfxPlayer: KitchenFoodDone not found");
        Debug.Assert(BedroomCleanDone, "SfxPlayer: BedroomCleanDone not found");
        Debug.Assert(Click1, "SfxPlayer: Click1 not found");
        Debug.Assert(Click2, "SfxPlayer: Click2 not found");
        Debug.Assert(Click3, "SfxPlayer: Click3 not found");
        Debug.Assert(HeroCheckIn, "SfxPlayer: HeroCheckIn not found");
        Debug.Assert(HeroHungry, "SfxPlayer: HeroHungry not found");
        Debug.Assert(HeroGetFood, "SfxPlayer: HeroGetFood not found");
        Debug.Assert(HeroRoomGood, "SfxPlayer: HeroRoomGood not found");
        Debug.Assert(HeroRoomNormal, "SfxPlayer: HeroRoomNormal not found");
        Debug.Assert(HeroRoomBad, "SfxPlayer: HeroRoomBad not found");
        Debug.Assert(HeroCheckout, "SfxPlayer: HeroCheckout not found");
        Debug.Assert(ResultGood, "SfxPlayer: ResultGood not found");
        Debug.Assert(ResultNormal, "SfxPlayer: ResultNormal not found");
        Debug.Assert(ResultBad, "SfxPlayer: ResultBad not found");
    }
}
