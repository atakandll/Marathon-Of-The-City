using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_EndGame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI distance;
    [SerializeField] private TextMeshProUGUI coins;
    [SerializeField] private TextMeshProUGUI score;


    void Start()
    {
        GameManager manager = GameManager.instance;
        // Time.timeScale = 0;

        if (manager.distance <= 0)
        {
            return;
        }
        if (manager.coins <= 0)
        {
            return;
        }




        distance.text = "Distance: " + manager.distance.ToString("#,#") + " m";
        coins.text = "Coins: " + manager.coins.ToString("#,#");
        score.text = "Score: " + manager.score.ToString("#,#");

    }

    // Update is called once per frame
    void Update()
    {

    }
}
