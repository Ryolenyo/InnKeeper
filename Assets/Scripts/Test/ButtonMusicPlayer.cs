using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMusicPlayer : MonoBehaviour
{
    public enum Command
    {
        PlayMusic,
        Play,
        Stop,
        Pause
    }

    public Command command;
    public MusicEnum musicEnum;

    public void OnClickCommand()
    {
        switch (command)
        {
            case Command.PlayMusic:
                MusicPlayer.PlayMusic(musicEnum);
                break;
            case Command.Play:
                MusicPlayer.Play();
                break;
            case Command.Stop:
                MusicPlayer.Stop();
                break;
            case Command.Pause:
                MusicPlayer.Pause();
                break;
            default:
                break;
        }
    }
}
