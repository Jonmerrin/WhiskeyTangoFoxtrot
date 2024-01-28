using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class NoteReceiverController : MonoBehaviour
{
    public LaneManager lane;
    public TextMeshPro text;
    private KeyCode key;
    public bool isKeyDown = false;
    public NoteController noteHeld;
    public float maxDistance;
    public NotePressedEvent noteHit;
    public SpriteRenderer sprite;
    public Animator anim;

    private bool paused = false;
    [SerializeField]
    private BoolEvent OnPausedEvent;

    private void OnEnable()
    {
        OnPausedEvent.Event += OnPaused;
    }

    private void OnDisable()
    {
        OnPausedEvent.Event -= OnPaused;
    }

    private void Start()
    {
        key = lane.getKey();
        text.text = key.ToString();
        sprite.color = lane.getLaneColor();
        text.color = lane.getLaneColor();
    }

    public void Refresh()
    {
        key = lane.getKey();
        text.text = key.ToString();
        sprite.color = lane.getLaneColor();
        text.color = lane.getLaneColor();
    }

    private void Update()
    {
        if(paused)
        {
            return;
        }
        else if (lane.notes.Count > 0 && lane.notes[0].transform.position.y - transform.position.y > Constants.GOOD_THRESHOLD)
        {
            //Destroy(lane.notes[0].gameObject);
            lane.notes.RemoveAt(0);
            noteHit.RaiseEvent(NotePressLevels.MISS);
        }
        if (Input.GetKeyDown(key))
        {
            OnKeyPressed();
        } else if(noteHeld !=null && isKeyDown && (Input.GetKeyUp(key) || Input.GetKey(key) == false))
        {
            OnKeyUp();
        }
    }

    private void OnKeyPressed()
    {
        isKeyDown = true;
        if(lane.notes.Count == 0)
        {
            anim.SetTrigger("Miss");
            noteHit.RaiseEvent(NotePressLevels.MISS);
            return;
        }
        NoteController nextNote = lane.notes[0];
        float dist = Mathf.Abs(nextNote.transform.position.y - transform.position.y);
        if (dist <= maxDistance)
        {
            //TODO: add whatever we need for levels other than perfect.
            if(dist < Constants.PERFECT_THRESHOLD)
            {
                noteHit.RaiseEvent(NotePressLevels.PERFECT);
            } else if (dist < Constants.GREAT_THRESHOLD)
            {
                noteHit.RaiseEvent(NotePressLevels.GREAT);
            } else if (dist < Constants.GOOD_THRESHOLD)
            {
                noteHit.RaiseEvent(NotePressLevels.GOOD);
            }
            else
            {
                anim.SetTrigger("Miss");
                noteHit.RaiseEvent(NotePressLevels.MISS);
                return;
            }
            if (nextNote.isDrag)
            {
                noteHeld = nextNote;
            } else
            {
                lane.notes.RemoveAt(0);
                nextNote.anim.SetTrigger("Pop");
                Destroy(nextNote.gameObject,0.5f);
            }
        } else
        {
            anim.SetTrigger("Miss");
            noteHit.RaiseEvent(NotePressLevels.MISS);
        }
    }

    private void OnKeyUp()
    {
        isKeyDown = false;
        if (Mathf.Abs(noteHeld.tailPos.y - transform.position.y) <= maxDistance)
        {
            //TODO: add whatever we need for levels other than perfect.
            noteHit.RaiseEvent(NotePressLevels.PERFECT);
        }
    }

    private void OnPaused(bool newVal)
    {
        paused = newVal;
    }
}
