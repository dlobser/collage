using UnityEngine;

public class PolygonTester2: MonoBehaviour {


	public bool makeGeo;
	public GameObject theThing;

	void Update () {

		if (makeGeo) {
			if(theThing==null)
				theThing = this.gameObject;
			// Create Vector2 vertices
//			Vector2[] vertices2D = new Vector2[] {
//				new Vector2 (0, 0),
//				new Vector2 (0, 1),
//				new Vector2 (25, 5),
//				new Vector2 (13, 100),
//				new Vector2 (0, 100),
//				new Vector2 (0, 150),
//				new Vector2 (150, 150),
//				new Vector2 (150, 100),
//				new Vector2 (100, 100),
//				new Vector2 (100, 50),
//				new Vector2 (150, 50),
//				new Vector2 (150, 0),
//			};

			Vector2[] vertices2D = new Vector2[theThing.transform.childCount];
			Vector2[] uv = new Vector2[theThing.transform.childCount];

			Vector2 min = Vector2.one * 1e6f;
			Vector2 max = Vector2.one * -1e6f;

			for (int i = 0; i < theThing.transform.childCount; i++) {
				Transform g = theThing.transform.GetChild(i);
				vertices2D[i] = new Vector2(g.localPosition.x,g.localPosition.y);

				if (vertices2D [i].x < min.x)
					min.x = vertices2D [i].x;
				if (vertices2D [i].y < min.y)
					min.y = vertices2D [i].y;

				if (vertices2D [i].x > max.x)
					max.x = vertices2D [i].x;
				if (vertices2D [i].y > max.y)
					max.y = vertices2D [i].y;

				uv[i] = new Vector2(g.localPosition.x+.5f,g.localPosition.y+.5f);
			}

//			Debug.Log (min + " , " + max);
			Vector2 mid = Vector2.Lerp (min, max, .5f);
//
//			Instantiate (thing, new Vector3 (min.x, min.y, 0), Quaternion.identity);
//			Instantiate (thing, new Vector3 (max.x, max.y, 0), Quaternion.identity);

			for (int i = 0; i < theThing.transform.childCount; i++) {
				vertices2D [i] -=  mid;

			}

			// Use the triangulator to get indices for creating triangles
			Triangulator tr = new Triangulator (vertices2D);
			int[] indices = tr.Triangulate ();

			// Create the Vector3 vertices
			Vector3[] vertices = new Vector3[vertices2D.Length];
			for (int i = 0; i < vertices.Length; i++) {
				vertices [i] = new Vector3 (vertices2D [i].x, vertices2D [i].y, 0);
			}

			// Create the mesh
			Mesh msh = new Mesh ();
			msh.vertices = vertices;
			msh.uv = uv;
			msh.triangles = indices;
			msh.RecalculateNormals ();
			msh.RecalculateBounds ();

			// Set up game object with mesh;

//			gameObject.AddComponent (typeof(MeshRenderer));
//			MeshFilter filter = gameObject.AddComponent (typeof(MeshFilter)) as MeshFilter;
			MeshFilter filter = gameObject.GetComponent<MeshFilter>();
			filter.mesh = msh;
			makeGeo = false;

			GameObject empty = new GameObject ();
			empty.transform.parent = this.transform;

			empty.transform.localPosition = new Vector3(mid.x,mid.y,0);

			this.transform.position = theThing.transform.position + empty.transform.position;
			this.transform.localScale = theThing.transform.localScale;
			this.transform.localEulerAngles = theThing.transform.localEulerAngles;

			this.GetComponent<MeshRenderer> ().material = theThing.GetComponent<MeshRenderer> ().material;

			for (int i = 0; i < theThing.transform.childCount; i++) {
				Destroy(theThing.transform.GetChild (i).gameObject);
			}
		}
	}
}