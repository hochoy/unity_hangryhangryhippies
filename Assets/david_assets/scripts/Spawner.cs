using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{

	public GameObject ball;
	public int spawnInterval = 2;
	public float spawnWidth = 5f;
	public float setTimeToExplode = 8f;
	public bool spawnerOn = true;

	// Use this for initialization
	IEnumerator Start ()
	{
		while (spawnerOn) {
			yield return new WaitForSeconds (spawnInterval);
			SpawnBall ();
		}
	}

	// Update is called once per frame
	void SpawnBall ()
	{
		
		GameObject newBall = Instantiate (ball, new Vector3 (Random.Range (-spawnWidth, spawnWidth), 15, Random.Range (-spawnWidth, spawnWidth)), Quaternion.identity) as GameObject;
		FallingBalls newBallscript = newBall.GetComponent<FallingBalls> ();
		newBallscript.timeToExplode = setTimeToExplode;

	}
}
