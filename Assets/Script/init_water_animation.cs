using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;


public class init_water_animation : MonoBehaviour {

	public Transform brick;
	public Transform ammo2;
	public GameObject ammo;
	public Scrollbar bar;

	GameObject Time;
	static int minute = 60;
	string text;
	string[] strArr;
	int tempcount = 0;


	class time_slice{
		public float[] height;
		public float[] x;
		public float[] z;
		public time_slice(){
			height = new float[30*33];
			x = new float[30*33];
			z = new float[30*33];
		}
	}

	// when Start function was called water will created at same position?
	void Start () {
		bar.value=0;

		fileReader ();
		int tempcount2 = 4;
		for (float i=0; i<20; i++) {
			for (float j=0; j<20; j++) {
				ammo2 = Instantiate(brick, new Vector3(float.Parse(strArr[tempcount2-3]),float.Parse (strArr[tempcount2-2]),float.Parse(strArr[tempcount2-1])), Quaternion.identity) as Transform;
				ammo2.name = i.ToString()+","+j.ToString()+"terrain";
				ammo2.gameObject.AddComponent<Animation>();
				ammo2.gameObject.AddComponent<BoxCollider>();
				tempcount2 = tempcount2 + 4;

			}
		}

		create_animation ();



	}

	void fileReader(){
		FileInfo theSourceFile = new FileInfo("compiler.txt");
		StreamReader reader = theSourceFile.OpenText ();
		text = reader.ReadLine();
		strArr = text.Split(',');
		Debug.Log (strArr.Length);
	}

	void changespeed(){
		for (float i=0; i<20; i++) {
			for (float j=0; j<20; j++) {
				ammo2.name = i.ToString()+","+j.ToString()+"terrain";
				
			}
		}
	}

	void create_animation(){
		AnimationCurve curve_height,curve_x,curve_z;
		AnimationClip clip;
		Animation anim;

		tempcount = 4;
		
		time_slice[] height_data = new time_slice[144];
		for (int time=0; time<143; time++) {
			height_data[time] = new time_slice();
			for(int i=0;i<20*20;i++){
				height_data[time].height[i] = float.Parse(strArr[tempcount-2]);
				height_data[time].x[i] = float.Parse(strArr[tempcount-3]);
				height_data[time].z[i] = float.Parse(strArr[tempcount-1]);
				tempcount = tempcount+4;
			}
		}
		
		
		tempcount = 0;
		for (float i=0; i<20; i++) {
			for (float j=-0; j<20; j++) {
				ammo = GameObject.Find (i.ToString () + "," + j.ToString () + "terrain");
				anim = ammo.gameObject.GetComponent<Animation>();
				curve_height = new AnimationCurve();
				curve_x = new AnimationCurve();
				curve_z = new AnimationCurve();

				clip = new AnimationClip();
				for(int k=0;k<143;k++){
					curve_height.AddKey(k*minute,height_data[k].height[tempcount]);
					curve_x.AddKey(k*minute,height_data[k].x[tempcount]);
					curve_z.AddKey(k*minute,height_data[k].z[tempcount]);
				}
				tempcount++;
				clip.legacy = true;
				clip.SetCurve("",typeof(Transform), "localPosition.y", curve_height);
				clip.SetCurve("",typeof(Transform),"localPosition.x",curve_x);
				clip.SetCurve("",typeof(Transform), "localPosition.z", curve_z);
				anim.AddClip(clip,"anim");
				anim.Play("anim");
				anim["anim"].speed = 30;
			}
		}

	}



	void Update () {
		Animation time = GameObject.Find ("0,0terrain").GetComponent<Animation> ();
		Time = GameObject.Find ("Time");
		Time.GetComponent<Text> ().text = time["anim"].time.ToString();
	}

}
