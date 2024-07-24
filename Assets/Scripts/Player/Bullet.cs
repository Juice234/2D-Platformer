using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var bullet = collision.GetComponent<Bullet>();

        if (bullet)
        {
            // destroy bullet
            Destroy(bullet.gameObject);
            // destroy this game object
            Destroy(gameObject);
        }
}
}
