using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Patrol : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;
    public float chaseSpeed = 4f;
    private Vector3 target;
    public Transform player;
    public float minPlayerDistance = 0.5f;
    private bool isChasingPlayer = false;
    public float maxVerticalDistance = 1f;
    private Animator myAnimator;

    void Start()
    {
        //Start at poing A
        target = pointA.position;
        myAnimator = GetComponent<Animator>();
        UpdatePlayerReference();

    }

    void Update()
    {
        if (isChasingPlayer)
        {
            ChasePlayer(); //Enable if player is seen
        }
        else
        {
            PatrolBetweenPoints(); //Otherwise return to patrol
        }

        CheckForPlayer();

        if (player == null)
        {
            UpdatePlayerReference();
        }
    }

    private void UpdatePlayerReference()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; //Refrence the player (objects which will be chased)
    }


    void PatrolBetweenPoints()
    {
        //Patrol between two points flipping the sprite to face correct direction
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        FlipSprite();

        
        // Go back to the other point if target point is reached
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            target = target == pointA.position ? pointB.position : pointA.position;
        }
    }

    void CheckForPlayer()
    {
        float playerPositionX = player.position.x;
        float playerPositionY = player.position.y;
        float enemyPositionY = transform.position.y;

        //If the player is in a veritcal distance in the patrol zone then chase the player
        if (playerPositionX > pointA.position.x && playerPositionX < pointB.position.x && Mathf.Abs(playerPositionY - enemyPositionY) < maxVerticalDistance)
        {
            isChasingPlayer = true; // Run towards the player
            myAnimator.SetBool("IsDetected", true); // Play follow animation
        }
        else
        {
            ReturnToPatrol();
        }
    }


    void ChasePlayer()
    {
        //Run towards the player at a increased speed
        transform.position = Vector3.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
        FlipSprite();
        myAnimator.SetBool("IsDetected", true);

        //If playes is outside of vertical zone then return to normal patrol
        if (Vector3.Distance(transform.position, player.position) < minPlayerDistance)
        {
            isChasingPlayer = false;
        }
    }

    //If player is no longer chased return to patroling between the points
    void ReturnToPatrol()
    {
        if (isChasingPlayer)
        {
            //If player is outside of vertical distance they are no longer chased 
            target = Vector3.Distance(transform.position, pointA.position) < Vector3.Distance(transform.position, pointB.position) ? pointA.position : pointB.position;
            isChasingPlayer = false;
            myAnimator.SetBool("IsDetected", false);

        }
    }


    //Flip the sprite based on the direction they are going
    void FlipSprite()
    {
        Vector3 spriteScale = transform.localScale;

        if (target.x < transform.position.x)
        {
            spriteScale.x = Mathf.Abs(spriteScale.x);
        }
        else
        {
            spriteScale.x = -Mathf.Abs(spriteScale.x);
        }

        transform.localScale = spriteScale;
    }

//Draw gizmos to visulize the vertical distance
    void OnDrawGizmos()
    {
        if (pointA == null || pointB == null)
        {
            return;
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(pointA.position, pointB.position);

        Vector3 verticalRangeCenter = (pointA.position + pointB.position) / 2;
        Vector3 verticalRangeTop = verticalRangeCenter + new Vector3(0, maxVerticalDistance / 2, 0);
        Vector3 verticalRangeBottom = verticalRangeCenter - new Vector3(0, maxVerticalDistance / 2, 0);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(verticalRangeTop, verticalRangeBottom);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        var Player = collision.GetComponent<PlatMov>();
        if (Player)
        {
            myAnimator.SetBool("IsDetected", true);
            Invoke("DestroyEnemy", 0.15f);

        }
    }

    void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}

       
