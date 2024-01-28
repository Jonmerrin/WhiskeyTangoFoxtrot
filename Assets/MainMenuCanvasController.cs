using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuCanvasController : MonoBehaviour
{

    [SerializeField]
    TMP_Dropdown dropdown;

    private void Start()
    {
        GameManager.Instance.ResetGameData();
    }

    public void OnPlayGame()
    {
        LevelLoader.Instance.TransitionLoadLevelWithIndex((int)Constants.SceneList.TANGO);
    }

    public void OnPracticeLevel()
    {
        GameManager.Instance.returnToMainAfterLevel = true;
        switch (dropdown.value)
        {
            case 0:
                LevelLoader.Instance.TransitionLoadLevelWithIndex((int)Constants.SceneList.TANGO);
                break;
            case 1:
                LevelLoader.Instance.TransitionLoadLevelWithIndex((int)Constants.SceneList.FOXTROT);
                break;
            case 2:
                LevelLoader.Instance.TransitionLoadLevelWithIndex((int)Constants.SceneList.STEAMBOAT);
                break;
        }
    }

}
