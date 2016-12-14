using UnityEngine;
using System.Collections;

public class PlayerAnimate : MonoBehaviour {

	Animator anim;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {



	}

	void FixedUpdate() {

		bool move = (Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.S));
		anim.SetBool ("Move", move);
	}
}
