using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EnemyMov : MonoBehaviour
{
    [SerializeField]
    private float speed = 8.0f;
    private Rigidbody2D rb;
    private int MaxEnemyPoints = 0;
    private Animator myAnimator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    //Used for enemies flying right
    void Update()
    {
        rb.velocity = Vector2.right * speed;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Enemy collides with player
        var Player = collision.GetComponent<PlatMov>();
        if (Player)
        {
            //If player has lives take off a life
            LivesSystem livesSystem = FindObjectOfType<LivesSystem>();
            if (livesSystem != null)
            {
                livesSystem.TakeDamage(1);
                //If player has no lives load back level 1 and reset the bullets fired and reset the lives
                if (livesSystem.life <= 0)
                {
                    SceneManager.LoadSceneAsync(2);
                    Destroy(GameController.instance.gameObject);
                    Destroy(livesSystem.gameObject);
                    PlayerWeapons.numBulletsFired = 0;

                }
            }
        }

        //If enemy collides with player bullet destroy enemy 
        var bullet = collision.GetComponent<Bullet>();
        if (bullet)
        {
            //Play enemy death animation
            myAnimator.SetBool("IsHit", true);
            if (MaxEnemyPoints < 10)
            {
                //Increase player score
                GameController.instance.playerScore += 1;
                ScoreScript.scoreValue = GameController.instance.playerScore;

                //The enemies can only gived a max of ten points to prevent point farming
                MaxEnemyPoints += 1;
            }
            Destroy(bullet.gameObject);
            //This is to allow the animation to play
            Invoke("DestroyEnemy", 0.15f); // Destroy enemy after 0.15 seconds
        }
       
        if (collision.gameObject.CompareTag("Void"))
        {
            Destroy(gameObject);

        }
    }

    //Roundabout way of adding a delay and then destroying the enemy
    void DestroyEnemy()
    {
        Destroy(gameObject);
    }

}
