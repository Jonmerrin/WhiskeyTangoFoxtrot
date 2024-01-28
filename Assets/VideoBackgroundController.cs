using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoBackgroundController : MonoBehaviour
{
    [SerializeField]
    private BoolEvent Pause;
    [SerializeField]
    private VideoPlayer video;

    private void OnEnable()
    {
        Pause.Event += OnPause;
    }
    private void OnDisable()
    {
        Pause.Event -= OnPause;
    }
    private void OnPause(bool pause)
    {
        if(pause)
        {
            video.Pause();
        } else
        {
            video.Play();
        }
    }
}
