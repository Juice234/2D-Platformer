using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tut_Manager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int counter = 0;

    //On player entering the tutorial zones show a different message
    private void OnTriggerEnter2D(Collider2D collison)
    {
        if (collison.CompareTag("Tut"))
        {
            switch (counter)
            {
                case 0:
                    scoreText.text = "Press W,A,S,D to move";
                    scoreText.enabled = true;
                    break;
                case 1:
                    scoreText.text = "Press Space to jump";
                    scoreText.enabled = true;
                    break;
                case 2:
                    scoreText.text = "Press Space twice to double jump \n Tip: For a longer jump quickly press Space";
                    scoreText.enabled = true;
                    break;
                case 3:
                    scoreText.text = "Press E to shoot enemies";
                    scoreText.enabled = true;
                    break;
                case 4:
                    scoreText.text = "Press Q for a boost";
                    scoreText.enabled = true;
                    break;
            }
            
            counter++; // increase counter based on how many zones were entered
        }
    }


    private void OnTriggerExit2D(Collider2D collison)
    {
        if (collison.CompareTag("Tut"))
        {
            //Disable the text if the zone was left and destroy the zone so it cannot be re-entered
            scoreText.enabled = false;
            Destroy(collison.gameObject);
        }

    }

}
