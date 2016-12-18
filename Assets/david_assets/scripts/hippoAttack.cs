using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class hippoAttack : MonoBehaviour
{
	// Hippo movement and AI
	private Vector3 hippoHome;
	public float attackRadius = 3;
	public bool hippoReady;
	public bool hippoAttacking;
	public bool hippoReturning;
	public float speed = 5f;
	public float rotateSpeed = 0.5f;

	private List<GameObject> detectedObjects = new List<GameObject> ();
	public GameObject targetObject;
	public Vector3 attackSpot;

	// Hippo health point and death
	public int health = 100;
	public GameObject explosionPrefab;
	public GameObject scorePrefab;
	public AudioClip explosionSound;
	public float volume = 1.0f;


	private void Start ()
	{
		hippoHome = this.gameObject.transform.position;
		hippoReady = true;

	}


	void Update ()
	{
		if (health <= 0) {
			AnimateExplosion ();
			Destroy (this.gameObject);
		}
	}

	void FixedUpdate(){
		
		HippoAction ();

		//Debug.Log (detectedObjects);
		//Debug.Log (targetObject);
	}


	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "item") {
			other.gameObject.GetComponent<FallingBalls> ().ExplodeBall ();
			health -= 10;
			ShowDamage ();
			hippoAttacking = false;
			hippoReturning = true;
		}

	}

	private void HippoAction ()
	{
			
		if (hippoReady) {
			FindObjects ();
			if (detectedObjects.Count > 0) {
				FindClosestObject ();
				hippoAttacking = true;
				hippoReady = false;
			}
		}

		if (hippoAttacking) {
			AttackObject ();
		}

		if (hippoReturning) {
			ReturnHome ();
		}
			
	}

	private void FindObjects ()
	{
		List<GameObject> objects = new List<GameObject> ();

		foreach (Collider col in Physics.OverlapSphere(hippoHome, attackRadius)) {
			if (col.gameObject.tag == "item" | col.gameObject.tag == "Player") {
				objects.Add (col.gameObject);
			}
		}
		detectedObjects = objects;
	}

	private void FindClosestObject ()
	{
		List<GameObject> objects = detectedObjects;	
		GameObject closestObj = null;		// holds the closest target gameobject
		float minDist = Mathf.Infinity;		
		Vector3 hippoPos = hippoHome;	// the hippos location
		foreach (GameObject go in objects) {
			float dist = Vector3.Distance (go.transform.position, hippoPos);
			if (dist < minDist) {
				closestObj = go;
				minDist = dist;
			}
		}
		targetObject = closestObj;
		attackSpot = new Vector3(targetObject.transform.position.x, hippoHome.y,targetObject.transform.position.z);
	}

	private void AttackObject ()
	{
		if (transform.position != attackSpot) {
			Debug.Log ("Attacking");
			hippoReady = false;
			float step = speed * Time.deltaTime;
			RotateHead (transform.position,attackSpot);
			transform.position = Vector3.MoveTowards (transform.position, attackSpot, step);
		} else {
			Debug.Log ("Attacked");
			hippoAttacking = false;
			hippoReturning = true;
		}

	}

	private void ReturnHome ()
	{
		if (transform.position != hippoHome) {
			Debug.Log ("Returning");
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, hippoHome, step);
		} else {
			Debug.Log ("Returned");
			hippoReturning = false;
			hippoReady = true;
		}

	}

	private void RotateHead(Vector3 origin, Vector3 target) {
		float str = Mathf.Min (rotateSpeed * Time.deltaTime, 1);
		Quaternion targetRotation = Quaternion.LookRotation (target - origin);
		transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation, str);
	}

	private void AnimateExplosion(){
		Vector3 explosionPos = this.gameObject.transform.position;
		Instantiate (explosionPrefab, explosionPos, Quaternion.identity);
		AudioSource.PlayClipAtPoint (explosionSound, Camera.main.transform.position,volume);
	}

	private void ShowDamage() {
		Vector3 scorePos = new Vector3 (transform.position.x, transform.position.y + 2, transform.position.z);
		Instantiate (scorePrefab, scorePos, Quaternion.Euler(0,45,0), this.transform);
	}


}
