using UnityEngine;
using System.Collections;

public class playerMove : MonoBehaviour
{
	// movement 
    public float moveSpeed = 6f;
    public float turnSpeed = 100f;

	// item scan and grab 
	private GameObject curr_Item;
	private GameObject new_Item;
	private GameObject grabbed_Item;

	private Rigidbody rb;
	public float collisionCheckDistance = 1f;
	public bool aboutToCollide;
	public float distanceToCollision;
	private RaycastHit hit;


	private void Start() {
		grabbed_Item = null;
		new_Item = null;
		rb = this.gameObject.GetComponent<Rigidbody> ();
	}

	private void Update() {
		ObjectScan ();
		ItemGrab ();
	}

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.S))
            transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.A))
            transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.D))
            transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);

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
				grabbed_Item.GetComponent<FallingBalls> ().GrabbedByPlayer (this.gameObject); //activate the item's "being grabbed by player" function

				// else if the player has previously grabbed an item
			} else if (grabbed_Item != null) {
				grabbed_Item.GetComponent<FallingBalls> ().DroppedByPlayer (); //activate the item's "being dropped by player" function
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

				if (temp_Item.gameObject.tag == "item") {
					new_Item = hit.transform.gameObject;
				} 
				//Debug.Log (new_Item);

				// if the new_Item is not the same as the curr_Item. this has 2 use cases
				// 1. game just started and there is no stored current item, and 
				// 2. user changes his view from the curr_Item to the new_Item
				if (new_Item != curr_Item) {
					// in case 2., there is a current object, deactivate it when looking away
					if (curr_Item != null) {
						curr_Item.GetComponent<FallingBalls> ().Deactivate ();
					}
					// in both case 1 and 2, the new_Item needs to be activated. and the new_Item becomes the curr_Item
					if (new_Item.layer == 8) { // if the new_Item is on the item layer
						new_Item.GetComponent<FallingBalls> ().Activate (); // activate the new items proximity indicator and..
						curr_Item = new_Item; // assign the new_Item as the curr_Item
					}
				}    
				// if no new item is being discovered
			} else if (new_Item != null) {
				if (new_Item.layer == 8) {
					new_Item.GetComponent<FallingBalls> ().Deactivate (); 
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