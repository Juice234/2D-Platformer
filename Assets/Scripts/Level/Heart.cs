using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    //This was achieved with a unity forum post i cant find
    public float floatAmplitude = 0.5f; 
    public float floatSpeed = 1f; 

    private float startY;

    
    void Start()
    {
       
        startY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        // Move the heart up and down in a sine wave pattern
        Vector3 pos = transform.position;
        pos.y = startY + floatAmplitude * Mathf.Sin(Time.time * floatSpeed);
        transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Replenish the player life if they are missing one
        var player = collision.GetComponent<PlatMov>();

        if (player)
        {
            LivesSystem livesSystem = FindObjectOfType<LivesSystem>();

            if (livesSystem != null && livesSystem.life < 3)
            {
                livesSystem.life += 1;
                livesSystem.ReplenishLife();
                Destroy(gameObject); 
            }
        }
    }
}
