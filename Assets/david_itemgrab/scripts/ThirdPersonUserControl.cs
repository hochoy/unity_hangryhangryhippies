using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

  
public class ThirdPersonUserControl : MonoBehaviour
{
	


	// new stuff
	private float distToGround;
	private GameObject grabbed_Item;
	public float collisionCheckDistance = 1f;
	public bool aboutToCollide;
	public float distanceToCollision;
	private Rigidbody rb;
	private RaycastHit hit;
	private GameObject new_Item = null;
	// the new item that is being detected by rigidbody sweeptest
	private GameObject curr_Item = null;
	// the item that is currently within range
	public float jump_power = 200f;
	public float turnSpeed = 200f;
	public float moveSpeed = 100f;
	private bool m_Jump;
	private bool readyMount=false;


	private void Start ()
	{
		// no items grabbed at start
		grabbed_Item = null;
		// get the rigidbody component of this object
		rb = GetComponent<Rigidbody> ();
		//get distance of object from ground when grounded
		Invoke("GetGroundDist",0.5f);

	}


	private void Update () {
		if (readyMount) {
			Debug.Log ("mounted on robot");
//			RobotMove ();
		} else {
			ObjectScan ();
			Move ();
			ItemGrab ();

		}
	}


	// Fixed update is called in sync with physics
	private void FixedUpdate ()
	{
		

	}

	private void Move() {
		// for debugging
		Vector3 forward = transform.TransformDirection (Vector3.forward) * 10;
		Debug.DrawRay (transform.position, forward, Color.green);

		// read movement inputs
		float leftRight = CrossPlatformInputManager.GetAxisRaw("Horizontal");
		if (leftRight > 0) {
			transform.Rotate (0, turnSpeed * Time.deltaTime,0);
		} else if (leftRight < 0) {
			transform.Rotate (0, -turnSpeed * Time.deltaTime,0);
		}

		float frontBack = CrossPlatformInputManager.GetAxisRaw ("Vertical");
		if (frontBack > 0) {
			transform.Translate (Vector3.forward * moveSpeed * Time.deltaTime);
		} else if (frontBack < 0) {
			transform.Translate (Vector3.back * moveSpeed * Time.deltaTime);
		}

		if (CrossPlatformInputManager.GetButtonDown ("Jump") && IsGrounded()){
			rb.AddForce (Vector3.up * jump_power);
		}

	}

	private void GetGroundDist () {
		distToGround = this.gameObject.GetComponent<SphereCollider> ().bounds.extents.y;	
	}
		
	private bool IsGrounded() {
		return Physics.Raycast (transform.position, Vector3.down, distToGround + 0.1f);
	}

	private void ItemGrab() {
		// handle item grab and parenting
		// if button e is pressed and... 
		bool e_press = Input.GetKeyDown (KeyCode.E);
		if (e_press) {
			// if player currently has no item & there is NO curr_Item stored by sweeptest
			if (grabbed_Item == null & curr_Item == null) {

				//do nothing

				// if player currently has no item & there IS a curr_Item stored by sweeptest
			} else if (grabbed_Item == null & curr_Item != null) {
				grabbed_Item = curr_Item; // player has grabbed an item
				grabbed_Item.GetComponent<Item> ().GrabbedByPlayer (this.gameObject); //activate the item's "being grabbed by player" function

				// else if the player has previously grabbed an item
			} else if (grabbed_Item != null) {
				grabbed_Item.GetComponent<Item> ().DroppedByPlayer (); //activate the item's "being dropped by player" function
				grabbed_Item = null; // player no longer has an item
				curr_Item = null;
			}
			e_press = false;
		} 
	}

	private void ObjectScan() {
		if (grabbed_Item == null) {

			//check if there is an item is in front of player (and close enough) or not
			// if it is, assign it as new_Item
			if (rb.SweepTest (transform.forward, out hit, collisionCheckDistance)) {
				aboutToCollide = true;
				distanceToCollision = hit.distance;
				GameObject temp_Item = hit.transform.gameObject;

				if (temp_Item.gameObject.tag == "roboframe") {
					readyMount = true;
					Debug.Log ("ready to mount");
				} else if (temp_Item.gameObject.tag == "item") {
					new_Item = hit.transform.gameObject;
				} 
				Debug.Log (new_Item);
				// if the new_Item is not the same as the curr_Item. this has 2 use cases
				// 1. game just started and there is no stored current item, and 
				// 2. user changes his view from the curr_Item to the new_Item
				if (new_Item != curr_Item) {
					// in case 2., there is a current object, deactivate it when looking away
					if (curr_Item != null) {
						curr_Item.GetComponent<Item> ().Deactivate ();
					}
					// in both case 1 and 2, the new_Item needs to be activated. and the new_Item becomes the curr_Item
					if (new_Item.layer == 8) { // if the new_Item is on the item layer
						new_Item.GetComponent<Item> ().Activate (); // activate the new items proximity indicator and..
						curr_Item = new_Item; // assign the new_Item as the curr_Item
					}
				}    
				// if no new item is being discovered
			} else if (new_Item != null) {
				if (new_Item.layer == 8) {
					new_Item.GetComponent<Item> ().Deactivate (); 
					new_Item = null;  //remove the new_Item assignation
					curr_Item = null; // remove the curr_Item assignation
				} else {
					new_Item = null;
					curr_Item = null;
				}
			} else {
				Debug.Log ("nothin happenin");
			}
		}
	}
		
}

