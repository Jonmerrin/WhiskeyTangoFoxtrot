using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public float tempo;
    public DrunkModifierFlags flags;

    public float horizontalDriftAmount;
    public float verticalDriftAmount;
    public float horizontalDriftLimit;
    public float verticalDriftLimit;
    public float laneDriftAmount;
    private int score = 0;
    private int crowdScore;
    private int combo;
    private int longestCombo;
    public bool returnToMainAfterLevel;

    // For dev purposes only
    public float startAtTime = 0;

    private int drinkCount;
    private bool paused;

    [SerializeField]
    private NotePressedEvent notePressed;
    [SerializeField]
    private BoolEvent OnPausedEvent;
    [SerializeField]
    private VoidEvent UIRefresh;

    // TODO: Would be better as a scriptable object that also can execute the modifiers
    public DrunkModifiers[] modOrder;

    private void OnEnable()
    {
        notePressed.Event += OnNotePressed;
    }

    private void OnDisable()
    {
        notePressed.Event -= OnNotePressed;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
            OnPausedEvent.RaiseEvent(paused);
            print(paused);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        combo = 0;
        score = 0;
        crowdScore = Constants.MAX_MISS_COUNT_LIMIT / 2;
        horizontalDriftLimit = horizontalDriftAmount;
        verticalDriftLimit = verticalDriftAmount;
        //RefreshUI();
    }

    private void OnNotePressed(NotePressLevels level)
    {
        print(level.ToString());
        if (level != NotePressLevels.MISS)
        {
            combo += 1;
            crowdScore += Constants.HIT_MISS_COUNT_VALUE;
            if(combo > longestCombo)
            {
                longestCombo = combo;
            }
        }
        else
        {
            if (combo > 20) AudioManager.Instance.ComboBreak();
            combo = 0;
            crowdScore -= Constants.MISS_MISS_COUNT_VALUE;
        }
        switch(level)
        {
            case NotePressLevels.PERFECT:
                score += Constants.PERFECT_NOTE_SCORE * (drinkCount > 0 ? drinkCount * Constants.PER_DRINK_SCORE_MULTIPLIER : 1);
                break;
            case NotePressLevels.GREAT:
                score += Constants.GREAT_NOTE_SCORE * (drinkCount > 0 ? drinkCount * Constants.PER_DRINK_SCORE_MULTIPLIER : 1);
                break;
            case NotePressLevels.GOOD:
                score += Constants.GOOD_NOTE_SCORE * (drinkCount > 0 ? drinkCount * Constants.PER_DRINK_SCORE_MULTIPLIER : 1);
                break;
            case NotePressLevels.MISS:
                break;
            default:
                break;
        }

        if (combo > 1)
        {
            score += Constants.COMBO_BONUS_SCORE * combo * (drinkCount > 0 ? drinkCount * Constants.PER_DRINK_SCORE_MULTIPLIER : 1);
        }

        RefreshUI();
    }

    public void Drink()
    {
        drinkCount += 1;
        RefreshUI();
    }

    public void RefreshUI()
    {
        UIRefresh.RaiseEvent();
    }

    public int GetScore()
    {
        return score;
    }

    public void SetScore(int newScore)
    {
        score = newScore;
    }

    public int GetCombo()
    {
        return combo;
    }

    public int GetLongestCombo()
    {
        return longestCombo;
    }

    public void SetCombo(int newCombo)
    {
        combo = newCombo;
    }

    public int GetDrinkCount()
    {
        return drinkCount;
    }

    public int GetCrowdScore()
    {
        return crowdScore;
    }

    public void ResetGameData()
    {
        score = 0;
        combo = 0;
        drinkCount = 0;
        longestCombo = 0;
        crowdScore = Constants.MAX_MISS_COUNT_LIMIT / 2;
        flags = new DrunkModifierFlags();
    }

}

[System.Serializable]
public struct DrunkModifierFlags
{
    public bool driftingHorizontal;
    public bool driftingVertical;
    public bool driftingLane;
    public bool wispyNotes;
    public bool everyoneInTheMiddle;
    public bool blurryVision;

    public DrunkModifierFlags(DrunkModifierFlags flags)
    {
        driftingHorizontal = flags.driftingHorizontal;
        driftingVertical = flags.driftingVertical;
        driftingLane = flags.driftingLane;
        wispyNotes = flags.wispyNotes;
        everyoneInTheMiddle = flags.everyoneInTheMiddle;
        blurryVision = flags.blurryVision;
    }
}

public enum DrunkModifiers
{
    START_DRIFT_HORIZONTAL,
    START_DRIFT_VERTICAL,
    INTENSIFY_DRIFT_HORIZONTAL,
    INTENSIFY_DRIFT_VERTICAL,
    SWAP_LANES,
    CHANGE_KEY
}