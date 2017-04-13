using UnityEngine;
using System.Collections;


public class PhotoViewer : MonoBehaviour {

	public GameObject card;
	GameObject[] gameObj;
	Texture2D[] textList;

	string[] files;
	string pathPreFix; 
	public string Path = "path";

	// Use this for initialization
	void Start () {
		//Change this to change pictures folder
		string path = Path;

		pathPreFix = @"file://";

		files = System.IO.Directory.GetFiles(path, "*.jpg");

//		gameObj= GameObject.FindGameObjectsWithTag("Pics");

		StartCoroutine(LoadImages());
	}


	void Update () {

	}

	private IEnumerator LoadImages(){
		//load all images in default folder as textures and apply dynamically to plane game objects.
		//6 pictures per page
		textList = new Texture2D[files.Length];

		int dummy = 0;
		foreach(string tstring in files){

			string pathTemp = pathPreFix + tstring;
			WWW www = new WWW(pathTemp);
			yield return www;
			Texture2D texTmp = new Texture2D(1024, 1024, TextureFormat.DXT1, false);  
			www.LoadImageIntoTexture(texTmp);

			textList[dummy] = texTmp;

			GameObject c = Instantiate (card);
			c.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", texTmp);
			c.GetComponent<ON_ScaleToAspectRatio> ().Rescale ();

//			gameObj[dummy].GetComponent<Renderer>().material.SetTexture("_MainTex", texTmp);
			dummy++;
		}

	}
}