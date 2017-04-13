using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ON_MouseInteraction : MonoBehaviour {

    public bool UseMouse;
    public bool useObject;
    public string objectName = "Controller (left)";
    public static GameObject rayObject { get; set; }
    public Vector3 hitPosition;
    public static Vector3 theHitPosition;
    public Vector3 hitNormal;
	public GameObject hitObject;
    public static GameObject theHitObject;
    public static bool beenHit;

    public delegate void MouseHasHit();
    public static event MouseHasHit mouseHasHit;

    private void Start() {
        rayObject = GameObject.Find(objectName);
        if (rayObject == null) {
            //rayObject = GameObject.Find("Controller (left)");
            //if(rayObject==null)
            //    rayObject = GameObject.Find("Controller (right)");
            //if (rayObject == null)
            //    rayObject = Camera.main.gameObject;
            Debug.LogWarning("ray object not found");
        }
    }
    void Update() {

        if (UseMouse) { 
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            beenHit = hit;
            if (hit) {
//                if (hitInfo.transform.gameObject.GetComponent<ON_Display>() != null) {
                    Trigger pinger = hitInfo.transform.gameObject.GetComponent<Trigger>();
                    hitPosition = hitInfo.point;
                    hitNormal = hitInfo.normal;
					hitObject = hitInfo.collider.gameObject;
//				Debug.Log (hitObject);
                if (pinger != null)
                        pinger.Ping();
//                }

            }
            else {
                hitPosition = Vector3.zero;
                hitNormal = Vector3.zero;
				hitObject = null;
            }
        }
        
//        else {
//            GameObject objPos;
//            if (useObject) {
//                if(rayObject == null || rayObject.name!=objectName) { 
//                    rayObject = GameObject.Find(objectName);
//                    if(rayObject == null)
//                        rayObject = GameObject.Find("Controller (left)");
//                    if (rayObject == null)
//                        rayObject = GameObject.Find("Controller (right)");
//                    if (rayObject == null)
//                        rayObject = Camera.main.gameObject;
//                    //Debug.Log(objPos);
//                }
//                objPos = rayObject;
//               
//            }
//            else {
//                objPos = Camera.main.gameObject;
//            }
//
//            RaycastHit hitInfo = new RaycastHit();
//            //Camera cam = Camera.main;
//            Debug.DrawRay (objPos.transform.position, objPos.transform.forward*10000f, Color.green);
//            bool hit = Physics.Raycast(new Ray(objPos.transform.position, objPos.transform.forward), out hitInfo, 1e6f);// ( Camera.main.ViewportPointToRay(new Vector3(.5f,.5f,0)), out hitInfo);
//            //Debug.Log(hit);
//            beenHit = hit;
//            if (hit) {
//           
//                //if (hitInfo.transform.gameObject.GetComponent<Trigger>() != null) {
//                    Trigger pinger = hitInfo.transform.gameObject.GetComponent<Trigger>();
//                    hitPosition = hitInfo.point;
//                    hitNormal = hitInfo.normal;
//                    hitObject = hitInfo.collider.gameObject;
//                if (pinger != null)
//                        pinger.Ping();
//                //}
//
//            }
//            else {
//                hitPosition = Vector3.zero;
//                hitNormal = Vector3.zero;
//                hitObject = null;
//            }
//        }
        theHitPosition = hitPosition;
        theHitObject = hitObject;
//        if (beenHit) {
//            mouseHasHit();
//        }
    }
}

