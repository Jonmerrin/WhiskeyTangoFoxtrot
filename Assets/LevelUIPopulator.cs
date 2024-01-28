using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUIPopulator : MonoBehaviour
{

    public VoidEvent UIRefresh;

    [SerializeField]
    TextMeshProUGUI comboText;
    [SerializeField]
    TextMeshProUGUI scoreText;
    [SerializeField]
    TextMeshPro drinkCountText;
    [SerializeField]
    Slider nextDrinkSlider;
    [SerializeField]
    Slider crowdMeter;

    private void OnEnable()
    {
        UIRefresh.Event += OnUIRefresh;
    }
    private void OnDisable()
    {
        UIRefresh.Event -= OnUIRefresh;
    }

    private void Start()
    {
        crowdMeter.maxValue = Constants.MAX_MISS_COUNT_LIMIT * 3;
    }

    private void OnUIRefresh()
    {
        comboText.text = GameManager.Instance.GetCombo().ToString();
        scoreText.text = GameManager.Instance.GetScore().ToString();
        drinkCountText.text = GameManager.Instance.GetDrinkCount().ToString();
        nextDrinkSlider.maxValue = Constants.NEXT_DRINK_THRESHOLD * (GameManager.Instance.GetDrinkCount() + 1) * (GameManager.Instance.GetDrinkCount() + 1);
        nextDrinkSlider.value = Constants.NEXT_DRINK_THRESHOLD * (GameManager.Instance.GetDrinkCount() + 1) * (GameManager.Instance.GetDrinkCount() + 1) - GameManager.Instance.GetScore();
        crowdMeter.value = GameManager.Instance.GetCrowdScore();
    }
}
