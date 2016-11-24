using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	public float delay = 2.0f;

	// Use this for initialization
	void Start () {
		WaitAndDestroy ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void WaitAndDestroy() {
		Destroy (this.gameObject, delay);
		Destroy (this, delay);
	}
}
