using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSoundPlayer : MonoBehaviour
{
    public Sfx soundEnum;
    public bool useLocation = false;
    public Vector3 location;

    public void OnClickCommand()
    {
        if(useLocation)
        {
            PlaySfx();
        }
        else
        {
            PlaySfxAtLocation();
        }
    }

    private void PlaySfx()
    {
        SfxPlayer.PlaySfx(soundEnum);
    }

    private void PlaySfxAtLocation()
    {
        SfxPlayer.PlaySfxAtLocation(soundEnum, location);
    }
}
