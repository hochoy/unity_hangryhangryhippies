using UnityEngine;
using System.Collections;

public class FallingBalls : MonoBehaviour {

	// Destroy ball after 6 seconds
	//public float delay = 6.0f; //This implies a delay of 2 seconds.
	public float timeToExplode = 8.0f;
	private float timeElapsed;

	// Grabbed by player
	public GameObject whichPlayer;
	private Color tempColor;

	// resist forces when grabbed
	private RigidbodyConstraints originalConstraints;
	private RigidbodyConstraints grabbedConstraints;

	// explosion
	public GameObject explosionPrefab;
	public AudioClip explosionSound;
	private bool soundPlayed = false;
	public float volume = 1.0f;


	// Use this for initialization
	void Start () {
		originalConstraints = this.gameObject.GetComponent<Rigidbody>().constraints;
		grabbedConstraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;	
		timeElapsed = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		// constrain item when near ground
		if (whichPlayer == null) {
			this.gameObject.GetComponent<Rigidbody> ().constraints = originalConstraints;

		} else if (whichPlayer != null) {
			this.gameObject.GetComponent<Rigidbody> ().constraints = grabbedConstraints;
		} 

		timeElapsed += Time.deltaTime;
		//destroy object after a while
		if (timeElapsed > timeToExplode) {
			ExplodeBall ();
		}
	}
		
		
	private void ExplodeBall(){
		AnimateExplosion () ;
		// NullReferencesToThisObject ();
		Destroy (this.gameObject);
		Destroy (this);
	}

	public void Activate() {
		//Debug.Log ("Activated!");
		if (whichPlayer == null) {
			tempColor = Color.blue;
			tempColor.a = 0.3f;
			this.gameObject.GetComponent<Renderer> ().material.color = tempColor;
		}
	}

	public void Deactivate() {
		//Debug.Log ("Deactivated!");
		if (whichPlayer == null) {
			tempColor = Color.red;
			tempColor.a = 0.3f;
			this.gameObject.GetComponent<Renderer> ().material.color = tempColor;
		}
	}

	public void GrabbedByPlayer(GameObject grabPlayer) {
		if (whichPlayer == null) {
			whichPlayer = grabPlayer;
			Vector3 itemPos = grabPlayer.transform.position + grabPlayer.transform.forward * 1.5f;
			Quaternion itemRot = grabPlayer.transform.rotation;

			this.gameObject.transform.position = itemPos;
			this.gameObject.transform.rotation = itemRot;
			this.gameObject.transform.parent = grabPlayer.transform;
			this.gameObject.GetComponent<Renderer> ().material.color = Color.green;
		}
	}

	public void DroppedByPlayer() {
		if (whichPlayer != null) {

			whichPlayer = null;
			this.gameObject.transform.parent = null;
			this.gameObject.GetComponent<Renderer> ().material.color = Color.red;
			this.gameObject.GetComponent<Rigidbody> ().useGravity = true;
		}
	}

	private void AnimateExplosion(){
		Vector3 explosionPos = this.gameObject.transform.position;
		Instantiate (explosionPrefab, explosionPos, Quaternion.identity);
		AudioSource.PlayClipAtPoint (explosionSound, Camera.main.transform.position,volume);
	}

}
