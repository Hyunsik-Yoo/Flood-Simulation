using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;


public class init_water : MonoBehaviour {

	public Transform brick;
	public Transform ammo2;
	public GameObject ammo;
	public Scrollbar bar;

	static int minute = 60;
	string text;
	string[] strArr;
	int tempcount = 0;
	Animation anim;
	AnimationCurve curve_x,curve;
	AnimationClip clip;

	class time_slice{
		float[] height = new float[144];
	}

	void Start () {
		for (float i=-15; i<-5; i++) {
			for (float j=-19; j<-10; j++) {
				ammo2 = Instantiate(brick, new Vector3 (i*100+50, -70, j*100+100), Quaternion.identity) as Transform;
				//ammo2.gameObject.AddComponent<Collider>();
				ammo2.name = i.ToString()+","+j.ToString()+"terrain";
			}
		}
		fileReader ();

	}

	void init(){
		//for (float k=0; k<144; k++) {
			for (float i=-15; i<-5; i++) {
				for (float j=-19; j<-10; j++) {
					ammo = GameObject.Find (i.ToString () + "," + j.ToString () + "terrain");
					//anim = ammo.GetComponent<Animation>();
					//curve = AnimationCurve.Linear(0,k*minute,strArr[tempcount-1],(k+1)*minute);

					ammo.transform.position = new Vector3(float.Parse(strArr[tempcount-2]),float.Parse (strArr[tempcount-1]),float.Parse(strArr[tempcount-0]));
					tempcount = tempcount+4;
				}
			}
		//}
	}
	void fileReader(){
		FileInfo theSourceFile = new FileInfo("compiler.txt");
		StreamReader reader = theSourceFile.OpenText ();
		text = reader.ReadLine();
		strArr = text.Split(',');
		Debug.Log (strArr.Length);
		/*tempcount = 3;
		time_slice[] height_data = new time_slice[40 * 33];
		for (int i=0; i<40*33; i++) {

		}*/
		
	}

	void Update () {
		
		bar.value = bar.value+0.001f;
		
		if (bar.value > 0 && bar.value < 0.1) {
			tempcount=3;
			for (float i=-15; i<15; i++) {
				for (float j=-19; j<14; j++) {
					ammo = GameObject.Find (i.ToString () + "," + j.ToString () + "terrain");
					ammo.transform.position = new Vector3(float.Parse(strArr[tempcount-2]),float.Parse (strArr[tempcount-1]),float.Parse(strArr[tempcount-0]));
					tempcount = tempcount+4;
				}
			}
		}else if (bar.value > 0.1 && bar.value < 0.2) {
			for (float i=-15; i<15; i++) {
				for (float j=-19; j<14; j++) {
					ammo = GameObject.Find (i.ToString () + "," + j.ToString () + "terrain");
					ammo.transform.position = new Vector3(float.Parse(strArr[tempcount-2]),float.Parse (strArr[tempcount-1]),float.Parse(strArr[tempcount-0]));
					tempcount = tempcount+4;
				}
			}
		}

	}

}
