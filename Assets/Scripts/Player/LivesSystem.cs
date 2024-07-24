using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesSystem : MonoBehaviour
{
    public GameObject[] hearts;
    public int life;

    public static LivesSystem instance;

    //Dont destroy object on scene change
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        life = hearts.Length; // Initialize life based on the number of hearts
    }

    // Update is called once per frame
    void Update()
    {
        //Hearts are either active or not
        //This was done to prevent issues when destroying the heart
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < life)
            {
                hearts[i].SetActive(true);
            }
            else
            {
                hearts[i].SetActive(false);
            }
        }
    }

    //If damage is taken take a life from the player
    public void TakeDamage(int d)
    {
        life -= d;
        life = Mathf.Clamp(life, 0, hearts.Length); // Ensure life stays within the range of 0 to the number of hearts
    }

    //Re-activate the hearts if one is missing 
    public void ReplenishLife()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (!hearts[i].activeSelf)
            {
                hearts[i].SetActive(true);//Acitate missing heart
            }
        }

        life = hearts.Length; // Reset life to the number of hearts
    }
}
