using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance = null;
    public int playerScore = 0;

    //Dont destroy the gamecontroller on scene change
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    //Initate the player score to 0
    void Start()
    {
        //initate with player score
        ScoreScript.scoreValue = playerScore;
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var bullet = collision.GetComponent<Bullet>();
        var coin = collision.GetComponent<Coin>();
        var enemy = collision.GetComponent<EnemyMov>();

        
        if (bullet)
        {
            Destroy(bullet.gameObject);
            Destroy(gameObject);
        }
        //If a coint is collected increase player score
        if (coin)
        {
            playerScore += 1;
            ScoreScript.scoreValue = playerScore;
            Destroy(coin.gameObject);
        }

        if (enemy)
        {   
            //If player collides with enemy  and dies then reset the player back to level 1 with no score and max lives again
            LivesSystem livesSystem = FindObjectOfType<LivesSystem>();
            if (livesSystem != null)
            {
                livesSystem.TakeDamage(1);
                playerScore = 0;
                ScoreScript.scoreValue = playerScore; //Reset Score
                Destroy(livesSystem.hearts[livesSystem.life]);
                SceneManager.LoadSceneAsync(2);//Load first level
                PlayerWeapons.numBulletsFired = 0;

            }
        }

    }

}