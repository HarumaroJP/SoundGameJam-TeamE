using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TextMeshProUGUI ScoreText;
    [SerializeField] private GameObject ResultPanel;
    public void result(int Score)
    {
        ScoreText.text = "Score"+ Score.ToString("d5");
        ResultPanel.SetActive(true);
    }
}
