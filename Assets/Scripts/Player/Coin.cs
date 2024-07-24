using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlatMov>();
        //On coint collection increase player score and destroy the coin
        if (player != null)
        {
            GameController.instance.playerScore += 10;
            ScoreScript.scoreValue = GameController.instance.playerScore;
            Destroy(gameObject);
        }
    }
}
