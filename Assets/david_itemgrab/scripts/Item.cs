using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {


	public GameObject whichPlayer;
	private bool itemStartFalling;
	private bool itemWasFalling;
	public float distanceToPlayer = 0.5f;
	private float distToGround;
	private RigidbodyConstraints originalConstraints;
	private RigidbodyConstraints airborneConstraints;
	private bool itemAlreadyAttached;
	private bool itemScriptEnabled;
	public string objectName;

	private Color tempColor;

	// Use this for initialization
	void Start () {
		whichPlayer = null;
		// save original constraints
		originalConstraints = this.gameObject.GetComponent<Rigidbody>().constraints;
		airborneConstraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;	
		// item not attached to roboframe at start
		itemAlreadyAttached = false;
		// item script is enabled at start
		EnableItemScript();
		// set item color and transparency
		tempColor = Color.red;
		tempColor.a = 0.1f;
		this.gameObject.GetComponent<Renderer> ().material.color = tempColor;

		objectName = this.gameObject.name;

	}

	// Update is called once per frame
	void Update () {

		// constrain item when near ground
		if (itemStartFalling) {
			this.gameObject.GetComponent<Rigidbody> ().constraints = airborneConstraints;
			itemStartFalling = false;
			itemWasFalling = true;
			Debug.Log ("object fall");
		} else if (itemWasFalling & IsGrounded ()) {
			this.gameObject.GetComponent<Rigidbody> ().constraints = originalConstraints;
			itemWasFalling = false;
			Debug.Log ("object grounded");
		} 

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
			itemStartFalling = true;
		}
	}

	void OnCollisionEnter (Collision hit)
	{
		if (hit.gameObject.tag == "floor") {
			this.gameObject.GetComponent<Rigidbody> ().useGravity = false;
		}
	}

	void OnTriggerStay (Collider other)
	{
		if (whichPlayer != null & other.gameObject.tag == "roboframe" & !itemAlreadyAttached) {
			Debug.Log (other.gameObject);
			other.gameObject.GetComponent<Roboframe>().CreatePartClone (objectName);
			itemAlreadyAttached = true;

		}
	}

	private void GetGroundDist () {
		distToGround = this.gameObject.GetComponent<BoxCollider> ().bounds.extents.y;	
	}

	private bool IsGrounded() {
		return Physics.Raycast (transform.position, Vector3.down, distToGround + 0.5f);
	}
		
	public void EnableItemScript() {
		this.gameObject.GetComponent<Item> ().enabled = true;
		itemScriptEnabled = true;
	}

	public void DisableItemScript() {
		itemScriptEnabled = false;
		this.gameObject.GetComponent<Item> ().enabled = false;
	}

	public void SetName(string newObjectName) {
		this.gameObject.name = newObjectName;
	}
}
