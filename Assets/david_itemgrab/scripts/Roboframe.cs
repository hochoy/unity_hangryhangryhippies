using UnityEngine;
using System.Collections;

public class Roboframe : MonoBehaviour {

	public float partLocationX = 2f;
	public float partLocationZ = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void CreatePartClone(string cloneName){
		Vector3 partPosition = this.gameObject.transform.position;
		partPosition.x += partLocationX;
		partPosition.z += partLocationZ;

		GameObject clone = Instantiate (Resources.Load (cloneName), partPosition, Quaternion.identity) as GameObject;
		Debug.Log (cloneName);
		clone.gameObject.GetComponent<Item>().SetName (cloneName);
	}
}
