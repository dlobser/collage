using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {

    //public bool;
    public float timeToTrigger = 0;
	public float returnSpeedMultiply = 1;
	public float triggerCounter { get; set; }
    public bool pinged;
    public SimpleTrigger[] triggers;
    public TriggerPrewarm[] prewarms;
//    public ON_Node node;
	public bool triggerable { get; set; }

	public bool triggerOnlyOnce = false;
	public int triggerCount { get; set; }

	public bool outsideTriggerCtrl { get; set; }

    public bool neverTrigger = false;
    public bool debug;

    // Use this for initialization
    void Start () {
		outsideTriggerCtrl = true;
		triggerable = true;
        triggerCount = 0;
        triggers = GetComponents<SimpleTrigger>();
        prewarms = GetComponents<TriggerPrewarm>();
//        if(GetComponent<ON_Display>()!=null)
//            node = GetComponent<ON_Display>().connectedNode;
	}
	
	// Update is called once per frame
	public void Ping () {
//		if (node != null) {
//			if (triggerable && !node.NodePingsAreActive ()&& !triggerOnlyOnce)
//				pinged = true;
//			else if (triggerable && !node.NodePingsAreActive ()&& triggerOnlyOnce && triggerCount<1)
//				pinged = true;
//		} else 
		if (triggerable && !triggerOnlyOnce)
			pinged = true;
		else if (triggerable && triggerOnlyOnce && triggerCount < 1)
			pinged = true;
   	}

    private void Update()
    {
		if (outsideTriggerCtrl) {
			if (pinged) {
				if (debug) {
					Debug.Log (this.gameObject.name);
				}
				if (triggerCounter < timeToTrigger) {
					triggerCounter += Time.deltaTime;
					for (int i = 0; i < prewarms.Length; i++) {
						prewarms [i].Animate (triggerCounter / timeToTrigger);
					}
				} else if (triggerable && !neverTrigger) { 
					{
						for (int i = 0; i < triggers.Length; i++) {
							triggers [i].Ping ();
						}
//						if (node != null)
//							node.Ping ();
						triggerCount++;
						triggerable = false;
					}
				}
			} else if (!pinged && triggerCounter > 0) {
				triggerCounter -= Time.deltaTime * returnSpeedMultiply;
				for (int i = 0; i < prewarms.Length; i++) {
					prewarms [i].Animate (triggerCounter / timeToTrigger);
				}
			} else if (triggerCounter <= 0 && !triggerable) {
				triggerable = true;
				for (int i = 0; i < prewarms.Length; i++) {
					prewarms [i].Reset ();
				}
			}
			pinged = false;
		}
    }
}
