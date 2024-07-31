using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinTextInPlayerCanvas;
    //[SerializeField] private TextMeshProUGUI diamondTextInPlayer;

    [SerializeField] private TextMeshProUGUI coinScore;
    //[SerializeField] private TextMeshProUGUI score;

    private void Update()
    {
        coinScore.text = "Coin: " + coinTextInPlayerCanvas.text;
        //score.text = "Score: " + diamondTextInPlayer.text;

    }
}
