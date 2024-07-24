using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ExitLevel : MonoBehaviour
{

       //Used in level one to continue to the next level
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var Player = collision.GetComponent<PlatMov>();

        if (Player)
        {
            LivesSystem livesSystem = FindObjectOfType<LivesSystem>();
            //Replenish the players lives
            if (livesSystem != null && livesSystem.life < 3)
            {
                livesSystem.life += 1;
                livesSystem.ReplenishLife();
            }

            Scene currentScene = SceneManager.GetActiveScene();
            if (currentScene.name == "Tutorial")
            {
                SceneManager.LoadSceneAsync("Menu");
                Destroy(Player.gameObject);
            }
            else
            {
                //If the scene is not tutrial move on the next level
                int nextSceneIndex = currentScene.buildIndex + 1;
                SceneManager.LoadSceneAsync(nextSceneIndex);
            }
            PlayerWeapons.numBulletsFired = 0; //Reset the amount of bullets back to 0
        }
    }



}
