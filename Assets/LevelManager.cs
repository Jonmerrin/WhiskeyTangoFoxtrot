using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public CameraController cam;
    public Animator spaceBarAnim;
    public LaneManager[] lanes;
    public AudioClip levelMusic;
    private bool paused;
    private bool levelOngoing = false;
    private bool canDrink = false;
    [SerializeField]
    private Constants.SceneList nextLevel;

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
        // TODO: Honestly should do something with the game manager. Like
        // resetting values or giving it info on this level, such as "do you
        // continue to the next level after this or go home" kind of thing.
        // Maybe the other way around. Who knows. Everything's spaghetti.
        AudioManager.Instance.SetLevelMusic(levelMusic);
        AudioManager.Instance.StartLevelMusicWithDelay(1);
        RefreshUI();
    }

    void Update()
    {
        if(GameManager.Instance.GetCrowdScore() < 0)
        {
            // Game over, man. Game over.
            LevelLoader.Instance.TransitionLoadLevelWithIndex((int)Constants.SceneList.GAME_OVER);
        }
        // TODO: Also add a condition for when the song ends. Actually, maybe that's an event. IDK.

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

        if(AudioManager.Instance.MusicPlayer.isPlaying)
        {
            print("something must be happening");
            levelOngoing = true;
        } else if(levelOngoing)
        {
            //End level here
            print("Ending the level");
            StartCoroutine(EndLevelAfterSeconds(2));
            levelOngoing = false;
        }
    }

    private void Drink()
    {
        StartCoroutine(cam.BloomFlash(50.0f, 100));
        StartCoroutine(cam.BlurFlash(150, 300));
        StartCoroutine(cam.LensFlash(0.25f, 200));
        StartCoroutine(cam.ChromAbFlash(1, 500));
        AudioManager.Instance.SloMo();
        if (GameManager.Instance.GetDrinkCount() >= GameManager.Instance.modOrder.Length)
        {
            ImplementDrunkModifier(Random.value < 0.5 ? DrunkModifiers.CHANGE_KEY : DrunkModifiers.SWAP_LANES);
        } else
        {
            ImplementDrunkModifier(GameManager.Instance.modOrder[GameManager.Instance.GetDrinkCount()]);
        }
        GameManager.Instance.Drink(); // Replace with event eventually

    }

    private void ImplementDrunkModifier(DrunkModifiers mod)
    {
        switch(mod)
        {
            case DrunkModifiers.START_DRIFT_HORIZONTAL:
                GameManager.Instance.flags.driftingHorizontal = true;
                break;
            case DrunkModifiers.START_DRIFT_VERTICAL:
                GameManager.Instance.flags.driftingVertical = true;
                break;
            case DrunkModifiers.INTENSIFY_DRIFT_HORIZONTAL:
                GameManager.Instance.horizontalDriftAmount += 1.0f; // MAGIC NUMBERS!!! TODO: Remove them
                break;
            case DrunkModifiers.INTENSIFY_DRIFT_VERTICAL:
                GameManager.Instance.verticalDriftAmount += 1.0f; // MAGIC NUMBERS!!! TODO: Remove them
                break;
            case DrunkModifiers.CHANGE_KEY:
                ChangeLane();
                break;
            case DrunkModifiers.SWAP_LANES:
                SwapLanes();
                break;
        }
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

    private void OnLevelEnd()
    {
        print("Transitioning to scene " + nextLevel.ToString() + " at index " + ((int)nextLevel).ToString());
        if(nextLevel == Constants.SceneList.GAME_OVER)
        {
            LevelLoader.Instance.TransitionLoadLevelWithIndex(((int)nextLevel));
        } else
        {
            LevelLoader.Instance.LoadLevelWithIndex(((int)nextLevel));
        }
    }

    private IEnumerator EndLevelAfterSeconds(float time)
    {
        yield return new WaitForSeconds(time);
        OnLevelEnd();
    }




    // Not necessary for jam, especially since it has no outward references.
    //private void OnNotePressed(NotePressLevels level)
}
