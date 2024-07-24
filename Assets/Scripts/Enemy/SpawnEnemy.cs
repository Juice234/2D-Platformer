using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{   
   [SerializeField] private GameObject[] SpawnPoint;
    [SerializeField] private GameObject trianglePrefab;

    private float enemySpeed = 6.0f;
    private float spawnDelay = 1.0f;
    private float spawnInterval = 1.55f;

    const string oneEnemy = "SpawnOneEnemy";

    // Start is called before the first frame update
    void Start()
    {
       SpawnEnemyWaves(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnEnemyWaves(){
        //Spawn waves based on the delay and spawn interval
        InvokeRepeating(oneEnemy, spawnDelay, spawnInterval);
    
    }

    private void SpawnOneEnemy(){

        //Spawn enemy at random location
        int Spawn = UnityEngine.Random.Range(0, SpawnPoint.Length);

        //Enemy prefact
        GameObject triangle =
        Instantiate(trianglePrefab);

        //Enemy position speed
        triangle.transform.position = SpawnPoint[Spawn].transform.position;
        Rigidbody2D rbb = triangle.GetComponent<Rigidbody2D>();
        rbb.velocity = Vector2.right * enemySpeed;
 

    }
}
