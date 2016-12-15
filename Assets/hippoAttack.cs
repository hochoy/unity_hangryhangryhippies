using UnityEngine;
using System.Collections;

public class hippoAttack : MonoBehaviour {

    // Applies an upwards force to all rigidbodies that enter the trigger.
    void OnTriggerEnxster(Collider other)
    {
        //Debug.Log("name " + other.name);
        GameObject obj = other.gameObject;
        Rigidbody rb = obj.GetComponent<Rigidbody>();
		if (rb != null) {
			Vector3 vel = rb.velocity;
			vel = Vector3.back*10;
			rb.velocity = vel;
			
			//rb.AddForce (Vector3.back * 5);
		}
		//Debug.Log (rb);
    }
}
