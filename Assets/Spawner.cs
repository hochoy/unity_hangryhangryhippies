using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    public GameObject ball;
	public int spawnInterval = 2;
	public float spawnWidth = 5f;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("SpawnBall", 1, spawnInterval);
    }

    // Update is called once per frame
    void SpawnBall()
    {
		Instantiate(ball, new Vector3(Random.Range(-spawnWidth, spawnWidth), 15, Random.Range(-spawnWidth, spawnWidth)), Quaternion.identity);
        
    }
}
