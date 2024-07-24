using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Exit_Tutorial : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var Player = collision.GetComponent<PlatMov>();
        //On player collision return to the menu
        if (Player)
        {
            LivesSystem livesSystem = FindObjectOfType<LivesSystem>();

            if (livesSystem != null && livesSystem.life < 3)
            {
                livesSystem.life += 1;
                livesSystem.ReplenishLife();
            }
            // For this script only the code below is used
            Scene currentScene = SceneManager.GetActiveScene();
            if (currentScene.name == "Tutorial")
            {   
                //Delete certain game objects to prevent them from going to the next scene
                SceneManager.LoadSceneAsync("Menu");
                Destroy(Player.gameObject);
                Destroy(GameObject.FindWithTag("MainCamera"));
                Destroy(GameObject.FindWithTag("GameController"));
                Destroy(GameObject.FindWithTag("Canvas"));

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
