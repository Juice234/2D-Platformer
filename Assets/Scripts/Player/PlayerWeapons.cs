using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;





public class PlayerWeapons : MonoBehaviour
{
    //Everything to do with the ammo in the script was completed in the video provided
    [SerializeField] private TextMeshProUGUI AmmoCount;

    [SerializeField]
    private InputActionReference RateOfFire;

    private Coroutine firingCoroutine;

    [SerializeField]
    private float bulletSpeed = 20;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private float firingRate = 10;

    private SpriteRenderer mySprite;

    public static int numBulletsFired = 0;
    private int maxBullets = 10;



    // Start is called before the first frame update
    void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
        if (mySprite == null)
        {
            mySprite = gameObject.AddComponent<SpriteRenderer>();
        }
        //Initate ammo text to nothing
        AmmoCount.text = "Ammo: ";

    }

    // Update is called once per frame
    void Update()
    {
        //If e is pressed fire the courtine
        if (Input.GetKeyDown(KeyCode.E))
        {
            // implement a coroutine to fire
            firingCoroutine = StartCoroutine(FireCoroutine());
        }

        {
            //Stop firing bullets and assuming the player fired less than 10 still allow the player to fire bullets
            if (Input.GetKeyUp(KeyCode.E) && numBulletsFired < 10)
            {
                StopCoroutine(firingCoroutine);
            }
        }
    }

    // coroutine returns an IEnumerator type
    private IEnumerator FireCoroutine()
    {
        while (numBulletsFired < maxBullets)
        {
            // instantiate bullet here
            GameObject bullet =
                Instantiate(bulletPrefab,
                transform.position,
                transform.rotation);


            // give it velocity and move right or left depending on the player's sprite flipX value
            Rigidbody2D rbb = bullet.GetComponent<Rigidbody2D>();
            if (mySprite.flipX)
            {
                bullet.transform.position = transform.position + new Vector3(-1, 0);
                rbb.velocity = Vector2.left * bulletSpeed;
            }
            else
            {
                bullet.transform.position = transform.position + new Vector3(1, 0);
                rbb.velocity = Vector2.right * bulletSpeed;
            }
            //Update the ammo text with the current ammo
            AmmoCount.text = "Ammo: " + (numBulletsFired+1) + "/10";

            // increment the number of bullets fired
            numBulletsFired++;

            // sleep for short time
            yield return new WaitForSeconds(firingRate);
        }
    }

    //Method to reset amount of bullets fired used by other scripts
    public void ResetBulletCount()
    {
        numBulletsFired = 0;
    }
}
