using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTrigger : MonoBehaviour {

    public bool triggered { get; set; }
    public virtual void Ping() { }
}
