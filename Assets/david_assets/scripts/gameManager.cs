using UnityEngine;
using System.Collections;

public class gameManager : MonoBehaviour
{

	public GameObject hippoPrefab;
	public GameObject playerPrefab;
	public GameObject spawnerPrefab;

	// Use this for initialization
	void Start ()
	{
		SpawnHippo ();
		SpawnPlayer ();
		SpawnSpawner ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	private void SpawnHippo ()
	{
		Instantiate (hippoPrefab, new Vector3 (5, 0, 5), Quaternion.Euler (0, 225, 0), this.transform);
		Instantiate (hippoPrefab, new Vector3 (-5, 0, 5), Quaternion.Euler (0, 145, 0), this.transform);
		Instantiate (hippoPrefab, new Vector3 (5, 0, -5), Quaternion.Euler (0, -35, 0), this.transform);
		Instantiate (hippoPrefab, new Vector3 (-5, 0, -5), Quaternion.Euler (0, 45, 0), this.transform);
	}

	private void SpawnPlayer ()
	{
		Instantiate (playerPrefab, new Vector3 (0, 3, 0), Quaternion.Euler (0, 0, 0), this.transform);
	}

	private void SpawnSpawner ()
	{
		Instantiate (spawnerPrefab, new Vector3 (0, 3, 0), Quaternion.Euler (0, 0, 0), this.transform);

	}
}
