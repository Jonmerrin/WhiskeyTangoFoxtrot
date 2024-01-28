using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScreenCanvasPopulator : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI ComboText;
    public TextMeshProUGUI BACText;

    private void Start()
    {
        ScoreText.text = GameManager.Instance.GetScore().ToString();
        ComboText.text = GameManager.Instance.GetLongestCombo().ToString();
        BACText.text = GameManager.Instance.GetDrinkCount().ToString() + " drinks";
    }
}
