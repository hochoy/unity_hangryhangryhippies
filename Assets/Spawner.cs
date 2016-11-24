using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    public GameObject ball;
	public int spawnRate = 2;
    // Use this for initialization
    void Start()
    {
        InvokeRepeating("SpawnBall", 1, spawnRate);
    }

    // Update is called once per frame
    void SpawnBall()
    {
        Instantiate(ball, new Vector3(Random.Range(-1, 1), 15, Random.Range(-1, 1)), Quaternion.identity);
        
    }
}
