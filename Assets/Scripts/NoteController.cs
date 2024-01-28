using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NoteController : MonoBehaviour
{
    public bool isDrag;
    public Vector3 tailPos;
    public LaneManager lane;
    public TextMeshPro text;
    public SpriteRenderer sprite;
    public Animator anim;

    private Vector2 originalPosition;
    private bool invertHorizontalDrift;
    private bool pastThreshold = false;
    private DrunkModifierFlags flags;

    [SerializeField]
    int spriteTop = 0;

    private void Start()
    {
        Refresh();
        float newYPos = (transform.localPosition.y - lane.Receiver.transform.position.y) * GameManager.Instance.tempo + lane.Receiver.transform.position.y;
        originalPosition = new Vector3(transform.localPosition.x, newYPos, transform.localPosition.z);
        invertHorizontalDrift = Random.value > 0.5f;
        flags = new DrunkModifierFlags(GameManager.Instance.flags);
    }

    private void Update()
    {
        Vector2 pos = lane.NotesParent.transform.position;
        pos += originalPosition;

        // If the distance from the receiver is less than the threshold.
        // Going this route in case we decide to do the up/down thing.
        if(!pastThreshold && Mathf.Abs(lane.Receiver.transform.position.y - transform.position.y) < Constants.DRUNK_EFFECT_THRESHOLD)
        {
            Refresh();
            pastThreshold = true;
        }

        float xOffset = 0;
        float yOffset = 0;

        if (flags.driftingHorizontal)
        {
            xOffset = Mathf.Sin(lane.Receiver.transform.position.y - pos.y) * GameManager.Instance.horizontalDriftAmount * (invertHorizontalDrift ? -1 : 1);
        }
        if (flags.driftingVertical)
        {
            yOffset = -Mathf.Sin(lane.Receiver.transform.position.y - (pos.y)) * GameManager.Instance.verticalDriftAmount;
        }

        transform.localPosition = originalPosition + new Vector2(xOffset, yOffset);
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -originalPosition.y);
    }


    public void Refresh()
    {
        text.text = lane.getKey().ToString();
        sprite.color = lane.getLaneColor();

        flags = new DrunkModifierFlags(GameManager.Instance.flags);
    }

    private void OnDestroy()
    {
    }
}
