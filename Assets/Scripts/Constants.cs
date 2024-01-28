using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants
{
    // Space thresholds
    public const float PERFECT_THRESHOLD = 0.3f;
    public const float GREAT_THRESHOLD = 0.5f;
    public const float GOOD_THRESHOLD = 0.75f;
    public const float DRUNK_EFFECT_THRESHOLD = 15f;
    // Scores
    public const int PERFECT_NOTE_SCORE = 50;
    public const int GREAT_NOTE_SCORE = 40;
    public const int GOOD_NOTE_SCORE = 20;
    public const int COMBO_BONUS_SCORE = 2;
    public const int NEXT_DRINK_THRESHOLD = 100;
    public const int PER_DRINK_SCORE_MULTIPLIER = 2;

    public const int MAX_MISS_COUNT_LIMIT = 100;
    public const int MISS_MISS_COUNT_VALUE = 2;
    public const int HIT_MISS_COUNT_VALUE = 1;
}

public enum NotePressLevels
{
    MISS,
    GOOD,
    GREAT,
    PERFECT
}