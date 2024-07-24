using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text MyScoreText;
    private int ScoreNum;
   
    void Start()
    {
        //Initiate score to 0 and increase from methods which increase the player score
        ScoreNum = 0;
        MyScoreText.text = " Score : " + ScoreNum;
    }

}