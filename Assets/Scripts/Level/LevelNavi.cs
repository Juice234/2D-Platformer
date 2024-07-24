using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelNavi1 : MonoBehaviour
{

    [SerializeField] private string sceneName;
    public AudioSource soundPlayer;
    public void changeScene()
    {
        //Change selected scene 
        //SceneManager.LoadScene(sceneName);
        SceneManager.LoadSceneAsync(sceneName);
    }

    public void playThisSound()
    {
        //Play sound
        soundPlayer.Play();
    }
}
