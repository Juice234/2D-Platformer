using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class PlatMov : MonoBehaviour

{
   
    private Animator myAnimator;
    [SerializeField] private TextMeshProUGUI boostCountdownText;


    [SerializeField]
    private int runSpeed = 5;
    [SerializeField]
    private int boostForce = 5;
    [SerializeField] private float jumpSpeed = 6.5f;
    int xBound = 10000;
    private int jumpCount = 0;

    private Rigidbody2D myRb;
    private SpriteRenderer mySprite;

    [SerializeField]
    private Sprite defaultSprite;

    [SerializeField]
    private Sprite jumpingSprite;

    [SerializeField]
    private Sprite fallingSprite;

    [SerializeField]
    private Sprite horizontalMovingSprite;

    [SerializeField]
    private Sprite doubleJumpingSprite;

    private float boostDuration = 2f; // The duration of the boost in seconds
    private float boostEndTime = -Mathf.Infinity; // The time when the boost will end

    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        mySprite = GetComponent<SpriteRenderer>();
        if (mySprite == null)
        {
            mySprite = gameObject.AddComponent<SpriteRenderer>();
            mySprite.sprite = defaultSprite;
        }
        myAnimator = GetComponent<Animator>();
        boostCountdownText.text = "";//Boost text instantiated to nothing


    }

    void Update()
    {
        MovePlayer();
        Jump();
    }
    private IEnumerator UpdateBoostCountdown()
    {
        //If
        while (Time.time < boostEndTime)
        {
            float remainingTime = boostEndTime - Time.time;
            boostCountdownText.text = $"Boost: {remainingTime:0.0}";
            yield return new WaitForSeconds(1f);
        }
        boostCountdownText.text = "Boost";
    }

    private void MovePlayer()
    {
        //Input to move player on the horizontal axis
        float hInput = Input.GetAxis("Horizontal");

        float xOffset = hInput * runSpeed * Time.deltaTime;

        //Bind the player on map to prevent from going out of bounds(this is not really necessary anymore)
        float xPosition =
            Mathf.Clamp(transform.position.x + xOffset, -xBound, xBound);

        transform.position = new Vector3(xPosition, transform.position.y, transform.position.z);

        //If q i pressed boost the player
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (Time.time >= boostEndTime)
            {
                //Boost the player in the direction they are facing
                Vector2 boostDirection = mySprite.flipX ? Vector2.left : Vector2.right;
                myRb.velocity += boostDirection * boostForce; //Boost by current velocity and boost velocity, moving = bigger boost
                boostEndTime = Time.time + boostDuration; // End boost and start boost cooldown
                StartCoroutine(UpdateBoostCountdown());
            }
        }


        //Change direction in which the sprite is facing bosed on the input
        if (hInput < 0)
        {
            mySprite.flipX = true;
            if (myRb.velocity.x == 0)
            {
                mySprite.sprite = horizontalMovingSprite;
            }
        }
        else if (hInput > 0)
        {        
            mySprite.flipX = false;
            if (myRb.velocity.x == 0)
            {
                mySprite.sprite = horizontalMovingSprite;             
            }
        }


        //This deprecated and no longer used
        //Succeded by my discovery of the animator
        if (myRb.velocity.y < 0)
        {
            mySprite.sprite = fallingSprite;
        }
     
        else if (myRb.velocity.y > 0)
        {
            mySprite.sprite = jumpingSprite;
        }
        
        else
        {
            mySprite.sprite = defaultSprite;
        }
        
        //Change player animation based on input from running to idle
        if (hInput == 0 && myRb.velocity.y == 0)
        {
            myAnimator.SetBool("IsIdle", true);
            myAnimator.SetBool("IsRunning", false);

        }
        else
        {
            myAnimator.SetBool("IsRunning", true);
            myAnimator.SetBool("IsIdle", false);
        }


    }

    private void Jump()
    {
        //If the jump key (space) is pressed jump
        if (Input.GetButtonDown("Jump"))
        {
            //If jump count is less than two allow to double jump
            if (jumpCount < 2)
            {
                Vector2 jumpVelToAdd = new Vector2(0f, jumpSpeed);
                myRb.velocity += jumpVelToAdd;
                jumpCount++;
                myAnimator.SetBool("IsJump", true); // Change to jumping animation
            }
            if (jumpCount == 1)
            {
                jumpSpeed = jumpSpeed - 3; //Jump speed reduced to prevent a very high double jump
            }
            if (jumpCount == 2)
            {

                //Double jump sprite animation
                mySprite.sprite = doubleJumpingSprite;
                myAnimator.SetBool("Djump", true);

            }

        }
        //If velocity is 0 allow to jump or double jump again-
        if (myRb.velocity.y == 0f)
        {
            //If the player is not moving then reset the double jump mechanic
            jumpCount = 0;
            jumpSpeed = 6.5f;
            myAnimator.SetBool("IsJump", false);
            myAnimator.SetBool("Djump", false);


        }
    }
}
