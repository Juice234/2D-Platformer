using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Level_Complete : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var Player = collision.GetComponent<PlatMov>();

        if (Player)
        {
            LivesSystem livesSystem = FindObjectOfType<LivesSystem>();

            if (livesSystem != null && livesSystem.life < 3)
            {
                livesSystem.life += 1;
                livesSystem.ReplenishLife();
            }
            //This part of the script is used 
            Scene currentScene = SceneManager.GetActiveScene();
            if (currentScene.name == "Level 2")
            {
                //Scene is loaded back to the menu using single to prevent assets from previous scene loading
                SceneManager.LoadScene("Menu", LoadSceneMode.Single);

            }
            else
            {
                int nextSceneIndex = currentScene.buildIndex + 1;
                SceneManager.LoadSceneAsync(nextSceneIndex);
            }
            PlayerWeapons.numBulletsFired = 0;
        }
    }
}
