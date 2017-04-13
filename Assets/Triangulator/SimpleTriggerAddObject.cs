using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTriggerAddObject : MonoBehaviour {

	public GameObject pointObject;
//	bool readyToAdd = false;
//	public override void Ping(){
//		Debug.Log ("pinging");
//		if (Input.GetMouseButtonUp(0)) {
//			GameObject g = Instantiate (pointObject);
//			Vector3 pos = ON_MouseInteraction.theHitPosition;
//			g.transform.position = pos;
//			g.transform.parent = ON_MouseInteraction.theHitObject.transform;
//			readyToAdd = true;
//		}
//	}

	void OnMouseDrag(){
		if (ON_MouseInteraction.theHitObject == this.gameObject) {
			GameObject g = Instantiate (pointObject);
			Vector3 pos = ON_MouseInteraction.theHitPosition;
			g.transform.position = pos;
			g.transform.parent = ON_MouseInteraction.theHitObject.transform;
		}
	}


}
