using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LaneManager : MonoBehaviour
{
    public GameObject notePrefab;

    public Color originalColor;
    private Color laneColor;
    public KeyCode originalKey;
    private KeyCode key;

    public GameObject NotesParent;
    public NoteReceiverController Receiver;
    public List<NoteController> notes;

    public BoolEvent OnPauseEvent;
    private float currentMusicTime = 0;

    private bool paused = false;


    private int laneNumber;

    private void OnEnable()
    {
        OnPauseEvent.Event += OnPause;
    }

    private void OnDisable()
    {
        OnPauseEvent.Event -= OnPause;
    }

    private void Awake()
    {
        key = originalKey;
        laneColor = originalColor;
    }

    private void Start()
    {
        float startTime = GameManager.Instance.startAtTime;
        NotesParent.transform.localPosition += new Vector3(0, 1, 0) * GameManager.Instance.tempo * startTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(paused)
        {
            return;
        }
        //NotesParent.transform.localPosition += new Vector3(0, 1, 0) * GameManager.Instance.tempo * Time.deltaTime;
        float deltaTime = AudioManager.Instance.MusicPlayer.time - currentMusicTime;
        currentMusicTime = AudioManager.Instance.MusicPlayer.time;
        NotesParent.transform.localPosition += new Vector3(0, 1, 0) * GameManager.Instance.tempo * deltaTime;
    }

    [ContextMenu("Add note")]
    public void AddNote()
    {
        GameObject newNote = PrefabUtility.InstantiatePrefab(notePrefab) as GameObject;
        newNote.transform.parent = NotesParent.transform;
        newNote.transform.position = notes[notes.Count - 1].transform.position - new Vector3(0,1,0);
        NoteController noteController = newNote.GetComponent<NoteController>();
        noteController.lane = this;
        notes.Add(noteController);
    }

    private void OnPause(bool newValue)
    {
        paused = newValue;
    }

    public void ChangeColor(Color newColor)
    {
        ChangeKeyAndColor(newColor, key);
    }

    public void ChangeKey(KeyCode newKey)
    {
        ChangeKeyAndColor(laneColor, newKey);
    }

    public void ChangeKeyAndColor(Color newColor, KeyCode newKey)
    {
        laneColor = newColor;
        key = newKey;
        RefreshNotes();
    }

    public KeyCode getKey()
    {
        return key;
    }

    public Color getLaneColor()
    {
        return laneColor;
    }

    public void RefreshNotes()
    {
        Receiver.Refresh();
        foreach(NoteController note in notes)
        {
            note.Refresh();
        }
    }

}
