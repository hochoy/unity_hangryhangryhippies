using UnityEngine;
using System.Collections;

public class ScoreFloat : MonoBehaviour {

	public float floatSpeed = 0.1f;
	private float timer = 0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		Vector3 newPos = transform.position + new Vector3 (0, 10, 0);
		transform.position = Vector3.MoveTowards (transform.position,newPos, floatSpeed);
		if (timer > 3) {
			Debug.Log ("destroy damage text");
			Destroy (this.gameObject);
		}
	}
}
