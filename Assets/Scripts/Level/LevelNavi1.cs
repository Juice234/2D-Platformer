using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelNavi : MonoBehaviour
{

    [SerializeField] private string sceneName;
    public AudioSource soundPlayer;
    public void changeScene()
    {
        //Select scene to load 

        //SceneManager.LoadScene(sceneName);
        SceneManager.LoadSceneAsync(sceneName);
    }

    public void playThisSound()
    {
        //Play sound
        soundPlayer.Play();
    }
}
