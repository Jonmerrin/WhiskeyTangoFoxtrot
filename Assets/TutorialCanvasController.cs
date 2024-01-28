using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCanvasController : MonoBehaviour
{

    [SerializeField]
    private GameObject OpenDialog;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseAndOpenNewDialog(GameObject nextDialog)
    {
        OpenDialog.SetActive(false);
        nextDialog.SetActive(true);
        OpenDialog = nextDialog;
    }

    public void CloseDialogAndStartLevel()
    {
        LevelLoader.Instance.LoadNextLevel();
    }
}
