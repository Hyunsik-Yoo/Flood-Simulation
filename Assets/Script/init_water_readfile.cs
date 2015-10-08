using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using UnityEngine.EventSystems;

//IBeginDragHandler is required if you use function "OnDragBegin()"
public class init_water_readfile : MonoBehaviour, IBeginDragHandler, IEndDragHandler {

	int tempcount;
	int tempcount2;
	
	bool swit = true;
	public Transform brick;
	public Scrollbar bar;
	public GameObject ammo;
	public Transform ammo2;
	
	string text;
	string[] strArr;
	
	
	
	
	void Start() {
		bar.value=0;
		
		fileReader ();
		
		
		for (float i=-15; i<15; i++) {
			for (float j=-19; j<14; j++) {
				ammo2 = Instantiate(brick, new Vector3(float.Parse(strArr[1]),float.Parse (strArr[2]),float.Parse(strArr[3])), Quaternion.identity) as Transform;
				ammo2.name = i.ToString()+","+j.ToString()+"terrain";
				//ammo2.gameObject.transform.localScale = new Vector3(0.2F,0.2F,0.2F);
				ammo2.gameObject.AddComponent<BoxCollider>();
			}
		}
	}
	
	void fileReader(){
		
		FileInfo theSourceFile = new FileInfo("compiler.txt");
		StreamReader reader = theSourceFile.OpenText ();
		
		text = reader.ReadLine();
		strArr = text.Split(',');
	}

	
	// Update is called once per frame
	void Update () {

		if (bar.value != 1) {
			bar.value = bar.value + 0.006944444f; // 0.006944444 = 100/144
			tempcount = (int)(bar.value * 144);
		
			tempcount2 = (tempcount * 3096) + 4;
		
		
			for (float i=-15; i<15; i++) {
				for (float j=-19; j<14; j++) {
					ammo = GameObject.Find (i.ToString () + "," + j.ToString () + "terrain");
					ammo.transform.position = new Vector3 (float.Parse (strArr [tempcount2 - 3]), float.Parse (strArr [tempcount2 - 2]), float.Parse (strArr [tempcount2 - 1]));
					tempcount2 = tempcount2 + 4;
				
				}
			}
		}

	}


	public void OnBeginDrag(PointerEventData eventData)
	{
		Debug.Log ("OnBeginDrag");
		move_camera.camera_movable = false;
	}
	
	public void OnEndDrag(PointerEventData eventData)
	{
		Debug.Log ("OnEndDrag");
		move_camera.camera_movable = true;
	}
}
