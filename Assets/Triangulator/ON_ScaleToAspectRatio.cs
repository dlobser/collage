using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ON_ScaleToAspectRatio : MonoBehaviour {

	public Texture tex;
	// Use this for initialization
	void Start () {
		Rescale ();
	}

	public void Rescale(){
		if(tex==null || GetComponent<MeshRenderer> ().material.GetTexture ("_MainTex") != tex)
			tex = GetComponent<MeshRenderer> ().material.GetTexture ("_MainTex");
		float aspect = (float)tex.height / (float)tex.width;
		Debug.Log (tex.width+"  , " +tex.height);
		this.transform.localScale = new Vector3 (this.transform.localScale.x, this.transform.localScale.y * aspect, this.transform.localScale.z);

	}

}
