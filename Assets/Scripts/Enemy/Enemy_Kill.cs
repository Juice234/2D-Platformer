using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Enemy_Kill : MonoBehaviour
{
    //This method might not be used or it might be, but i wont be the one to delete it incase its attached to an object
    private void OnTriggerEnter2D(Collider2D collision)
    {

        var Player = collision.GetComponent<PlatMov>();
        if (Player)
        {
            LivesSystem livesSystem = FindObjectOfType<LivesSystem>();
            if (livesSystem != null)
            {
                livesSystem.TakeDamage(1);
                if (livesSystem.life <= 0)
                {
                    SceneManager.LoadSceneAsync(2);
                    Destroy(GameController.instance.gameObject);
                    Destroy(livesSystem.gameObject);
                    PlayerWeapons.numBulletsFired = 0;

                }
            }

        }
    }
}
