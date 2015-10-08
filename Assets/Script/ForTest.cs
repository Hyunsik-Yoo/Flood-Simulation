using UnityEngine;
using System.Collections;
using System.IO;

public class ForTest : MonoBehaviour
{	private Shader shader;
	public Texture2D teximg;
	public int widthtest;
	public int heighttest;
	void Start ()
	{
		GameObject temp = Resources.Load ("ImjinRiver-Skin.png") as GameObject;
		shader = Shader.Find ("Unlit/Texture");
		MakeMap ("ImjinRiver_Bin.dem");
	}
	void Update ()
	{
		
	}
	
	public void MakeMap (string demname)
	{

		foreach (Transform child in transform)
			GameObject.Destroy (child.gameObject);
		GetComponent<MeshFilter> ().mesh.Clear ();
		transform.rotation = Quaternion.identity;
		transform.position = new Vector3 (0, 0, 0);
		//> file open
		FileStream fs = new FileStream (demname, FileMode.Open);
		BinaryReader reader = new BinaryReader (fs);


		float interval;
		int[] roi = new int[4];
		int[] regionMin = new int[2];
		int[] regionMax = new int[2]; 
		
		//> read the interval
		interval = reader.ReadSingle ();
		interval = 10;
		//> read the coordinate of regionMin and regionMax
		for (int i=0; i<4; i++)
			roi [i] = reader.ReadInt32 ();
		
		for (int i=0; i<2; i++) {
			regionMin [i] = roi [i];
			regionMax [i] = roi [i + 2];
		}
		
		//> Calculate the Resolution
		int reigonWidth = (regionMax [0] - regionMin [0]) / (int)interval + 1;
		int reigonHeight = (regionMax [1] - regionMin [1]) / (int)interval + 1;

		//Debug.Log ("regionWidth : " + reigonWidth);
		//Debug.Log ("reigonHeight : " + reigonHeight);

		widthtest = reigonWidth;
		heighttest = reigonHeight;
		int resolution = reigonWidth * reigonHeight;
		
		float[] height = new float[resolution];
		
		//>read the height fo each coordinate
		for (int i = 0; i < resolution; i++) {
			height [i] = reader.ReadSingle ();
		}
		//Debug.Log ("resolution: "+resolution);
		//Debug.Log ("Width : " + widthtest);
		//Debug.Log ("Height : " + heighttest);
		//>file close
		reader.Close ();
		fs.Close ();




		
		//>----------------------- make the polygon----------------------------
		
		
		//find map's center
		float heightMax = height [0];
		float heightMin = height [0];
		for (int i=0; i<resolution; i++) {
			if (heightMax < height [i])
				heightMax = height [i];
			if (heightMin > height [i])
				heightMin = height [i];
		}
		
		//>vertices
		Vector3[] verticies = new Vector3[resolution];
		float tmpWidth = (float)(regionMax [0] - regionMin [0]) / 2.0f;		
		float tmpHeight = (float)(regionMax [1] - regionMin [1]) / 2.0f;	
		float tmpDepth = (heightMax + heightMin) * 0.5f;					
		
		for (int i=0; i<reigonHeight; i++) {
			for (int j=0; j<reigonWidth; j++) {
				verticies [reigonWidth * i + j] = new Vector3 (j * interval - tmpWidth,  
				                                               (height [reigonWidth * i + j] - tmpDepth)*5f,
				                                               (i * interval - tmpHeight));

			}

		}

		
		
		//>triangles
		int[] tri = new int[(reigonHeight - 1) * (reigonWidth - 1) * 6]; //Number of triangles
		int tri_id = 0;
		
		for (int h=0; h<reigonHeight-1; h++) {
			for (int w=0; w<reigonWidth-1; w++) {
				//one vertex have 2 triangles
				//tri 1
				tri [tri_id] = reigonWidth * h + w;
				tri [tri_id + 1] = reigonWidth * (h + 1) + w;
				tri [tri_id + 2] = reigonWidth * h + w + 1;

				//tri 2
				tri [tri_id + 3] = reigonWidth * h + w + 1;
				tri [tri_id + 4] = reigonWidth * (h + 1) + w;
				tri [tri_id + 5] = reigonWidth * (h + 1) + w + 1;
				
				//two triangles has 6 vertexs, so next tri_id will be added to 6
				tri_id += 6;
			}		
		}
		
		
		//>normals (only if you want display object in the game)
		Vector3[] normals = new Vector3[resolution];
		for (int i=0; i< resolution; i++)
			normals [i] = Vector3.up;
		
		
		//>uvs (How textures are displayed)
		Vector2[] uv = new Vector2[resolution];
		for (int i=0; i<resolution; i++) {
			
			uv [i] = new Vector2 ((verticies [i].x / (reigonWidth - 1) / 10) + 0.5f, 
			                      (verticies [i].z / (reigonHeight - 1) / 10) + 0.5f);
			
		}
		
		int x = 0;
		int pre_x = 0;
		int pre_tri = 0;
		if (reigonWidth * reigonHeight > 6500) {
			while (reigonWidth * x<6500)
				x++;
			x--;
			int q = (int)(reigonHeight / x);
			int vertexSize = 0;
			int triSize = 0;
			for (int p=0; p<q+1; p++) {
				if (p < q) {
					vertexSize = reigonWidth * x;
					triSize = (reigonWidth - 1) * 6 * (x - 1);
				} else {
					vertexSize = reigonWidth * reigonHeight - reigonWidth * (x) * q;
					triSize = (reigonHeight - x * q - 1) * (reigonWidth - 1) * 6;
				}
				GameObject MapPiece;
				MapPiece = new GameObject ("MapPiece");
				MapPiece.transform.parent = transform;
				MapPiece.AddComponent<MeshFilter> ();
				MapPiece.AddComponent<MeshRenderer> ();
				MapPiece.AddComponent<MeshCollider>();
				//MapPiece.AddComponent<Rigidbody>();
				MeshFilter mf = MapPiece.GetComponent<MeshFilter> ();
				Mesh mesh = new Mesh ();
				mesh.Clear ();
				mf.mesh = mesh;
				MapPiece.GetComponent<MeshCollider>().sharedMesh = mesh;



				
				
				Vector3[] verticies2 = new Vector3[vertexSize];
				for (int i=0; i<verticies2.Length; i++)
					verticies2 [i] = verticies [i + reigonWidth * (x - 1) * p];
				
				Vector2[] uv2 = new Vector2[vertexSize];
				for (int i=0; i<uv2.Length; i++)
					uv2 [i] = uv [i + reigonWidth * (x - 1) * p];
				
				Vector3[] normals2 = new Vector3[vertexSize];
				for (int i=0; i<normals2.Length; i++)
					normals2 [i] = normals [i + reigonWidth * (x - 1) * p];
				
				int[] tri2 = new int[triSize];
				for (int i=0; i<tri2.Length; i++)
					tri2 [i] = tri [i];

				//>Assign Arrays
				mesh.name = "MeshTest";
				mesh.vertices = verticies2;
				mesh.triangles = tri2;
				mesh.normals = normals2;
				mesh.uv = uv2;
				
				//>texturing
				//teximg = new Texture2D (4, 4);	//because of constructing new Texture here, varable teximg is null
				//teximg.LoadImage (this.GetComponent<ClientSocket> ().GetTexData ());
				MapPiece.GetComponent<Renderer>().material.mainTexture = teximg;
				MapPiece.GetComponent<Renderer> ().material.shader = shader;


			}
			makeBuilding(interval,tmpWidth,reigonWidth,tmpDepth,tmpHeight,height);

		}
		transform.position = transform.position;
	}


	public void makeBuilding(float interval,float tmpWidth,int regionWidth,float tmpDepth,float tmpHeight,float[] height){

		int afterX,afterY,galo,selo,building_height;
		FileStream building_fs = new FileStream ("building_data_bin", FileMode.Open);
		BinaryReader reader = new BinaryReader (building_fs);

		int ncols_building = reader.ReadInt32();
		int nrows_building = reader.ReadInt32();
		
		//Debug.Log ("ncols_building :" + ncols_building);
		//Debug.Log ("nrows_building : " + nrows_building);

		while (reader.PeekChar () != -1) {
			
			afterX = reader.ReadInt32 ()/(int)interval;		//To adjust the resolution, divid with interval 
			afterY = reader.ReadInt32 ()/(int)interval;
			galo = reader.ReadInt32 ();
			selo = reader.ReadInt32 ();
			
			building_height = reader.ReadInt32();
			
			/*Debug.Log ("afterX : " + afterX);
			Debug.Log ("afterY : " + afterY);
			Debug.Log ("galo : " + galo);
			Debug.Log ("selo : " + selo);
			Debug.Log ("height : " + building_height);*/
			
			GameObject building = GameObject.CreatePrimitive(PrimitiveType.Cube);
			building.transform.localScale = new Vector3(galo,selo,building_height);
			building.transform.position = new Vector3(afterX*interval - tmpWidth,((height [regionWidth * afterY + afterX] - tmpDepth)*5f)+(building.transform.localScale.y/2),(afterY* interval - tmpHeight));
			//building.AddComponent<BoxCollider>();
			//building.AddComponent<Rigidbody>().useGravity=false;
			building.AddComponent<clicked_building>();
		}

	}
}


