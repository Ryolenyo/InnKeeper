using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusicPlayer : MonoBehaviour
{
    bool flag = false;

    void Start()
    {
        
    }

    private void Update()
    {
        if(!flag)
        {
            MusicPlayer.PlayMusic(MusicEnum.Menu);
            flag = true;
        }
    }
}
