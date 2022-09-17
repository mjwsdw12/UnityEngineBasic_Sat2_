using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    public void OnClick()
    {
        if (SongSelector.Instance.TryLoadSelectedSongData())
        {
            SceneMover.MoveTo("Play");
        }
    }
}
