using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public CameraController cam;
    public Animator spaceBarAnim;
    public LaneManager[] lanes;
    private bool paused;
    private bool canDrink = false;

    [SerializeField]
    private BoolEvent Pause;
    [SerializeField]
    private VoidEvent UIRefresh;


    private void OnEnable()
    {
        Pause.Event += OnPause;
    }

    private void OnDisable()
    {
        Pause.Event -= OnPause;
    }

    private void Start()
    {
        RefreshUI();
    }

    void Update()
    {
        if(GameManager.Instance.GetCrowdScore() < 0)
        {
            // Game over, man. Game over.
            LevelLoader.Instance.LoadNextLevel();
        }
        // TODO: Also add a condition for when the song ends. Actually, maybe that's an event. IDK.


        //TODO: Remove these! Dev only!

        if (Input.GetKeyDown(KeyCode.Return))
        {
            ChangeLane();
        }
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            SwapLanes();
        }
        // TODO: DON'T FORGET!!


        if (paused)
        {
            return;
        }

        canDrink = GameManager.Instance.GetScore() >= Constants.NEXT_DRINK_THRESHOLD * (GameManager.Instance.GetDrinkCount() + 1) * (GameManager.Instance.GetDrinkCount() + 1);
        spaceBarAnim.SetBool("CanDrink", canDrink);
        if (Input.GetKeyDown(KeyCode.Space) && canDrink)
        {
            Drink();
        }
    }

    private void Drink()
    {
        StartCoroutine(cam.BloomFlash(50.0f, 100));
        StartCoroutine(cam.BlurFlash(150, 300));
        StartCoroutine(cam.LensFlash(0.25f, 200));
        StartCoroutine(cam.ChromAbFlash(1, 500));
        AudioManager.Instance.SloMo();
        GameManager.Instance.Drink(); // Replace with event eventually
    }

    private void ChangeLane()
    {
        int laneNum = Random.Range(0, lanes.Length);
        if (AccessibilityManager.Instance.OneHandedMode)
        {
            List<KeyCode> possibleKeys = AccessibilityManager.Instance.GetNearbyKeys(lanes[laneNum].getKey());
            lanes[laneNum].ChangeKey(Random.value < 0.5 ? possibleKeys[0] : possibleKeys[1]);
        }
        else
        {
            lanes[laneNum].ChangeKey((KeyCode)Random.Range(97, 122));
        }
    }

    private void SwapLanes()
    {
        int lane1 = Random.Range(0, lanes.Length);
        int lane2 = Random.Range(0, lanes.Length);
        while (lane2 == lane1)
        {
            lane2 = Random.Range(0, lanes.Length);
        }

        StartCoroutine(MoveSwappedLanes(lanes[lane1], lanes[lane2]));
    }


    IEnumerator MoveSwappedLanes(LaneManager lane1, LaneManager lane2)
    {
        Vector3 lane1Pos = lane1.transform.position;
        Vector3 lane2Pos = lane2.transform.position;
        float time = 0f;
        float duration = 0.5f;
        while (time <= duration)
        {
            lane1.transform.position = Vector3.Lerp(lane1Pos, lane2Pos, time / duration);
            lane2.transform.position = Vector3.Lerp(lane2Pos, lane1Pos, time / duration);
            yield return null;
            time += Time.deltaTime;
        }
        lane1.transform.position = lane2Pos;
        lane2.transform.position = lane1Pos;
        yield return 0;
    }

    private void OnPause(bool isPaused)
    {
        paused = isPaused;
    }


    public void RefreshUI()
    {
        UIRefresh.RaiseEvent();
    }


    // Not necessary for jam, especially since it has no outward references.
    //private void OnNotePressed(NotePressLevels level)
    //{
    //    print(level.ToString());
    //    if (level != NotePressLevels.MISS)
    //    {
    //        GameManager.Instance.GetCombo() += 1;
    //        crowdScore += Constants.HIT_MISS_COUNT_VALUE;
    //        if (combo > longestCombo)
    //        {
    //            longestCombo = combo;
    //        }
    //    }
    //    else
    //    {
    //        combo = 0;
    //        crowdScore -= Constants.MISS_MISS_COUNT_VALUE;
    //    }
    //    switch (level)
    //    {
    //        case NotePressLevels.PERFECT:
    //            score += Constants.PERFECT_NOTE_SCORE;
    //            break;
    //        case NotePressLevels.GREAT:
    //            score += Constants.GREAT_NOTE_SCORE;
    //            break;
    //        case NotePressLevels.GOOD:
    //            score += Constants.GOOD_NOTE_SCORE;
    //            break;
    //        case NotePressLevels.MISS:
    //            break;
    //        default:
    //            break;
    //    }
    //    if (combo > 1)
    //    {
    //        score += Constants.COMBO_BONUS_SCORE * combo * (drinkCount > 0 ? drinkCount * Constants.PER_DRINK_SCORE_MULTIPLIER : 1);
    //    }

    //    RefreshUI();
    //}
}
