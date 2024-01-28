using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCanvasController : MonoBehaviour
{


    private void Start()
    {
        GameManager.Instance.ResetGameData();
    }

    public void OnPlayGame()
    {
        LevelLoader.Instance.TransitionLoadLevelWithIndex(2);
    }

}
