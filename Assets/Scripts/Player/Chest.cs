using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlatMov>();
        //On chest collection destroy chest and increase player score
        if (player != null)
        {
            GameController.instance.playerScore += 100;
            ScoreScript.scoreValue = GameController.instance.playerScore;
            Destroy(gameObject);
        }
    }
}
