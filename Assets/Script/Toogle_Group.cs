using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Toogle_Group : MonoBehaviour {
	public Toggle X1,X2,X4,X8,X16,X32,X64;
	int speed;
	GameObject ammo;
	Animation anim;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void check_speed(){
		if (X1.isOn)
			speed = 1;
		else if (X2.isOn)
			speed = 2;
		else if (X4.isOn)
			speed = 4;
		else if (X8.isOn)
			speed = 8;
		else if (X16.isOn)
			speed = 16;
		else if (X32.isOn)
			speed = 32;
		else if (X64.isOn)
			speed = 600;

		for (float i=0; i<20; i++) {
			for (float j=-0; j<20; j++) {
				ammo = GameObject.Find (i.ToString () + "," + j.ToString () + "terrain");
				anim = ammo.gameObject.GetComponent<Animation>();
				//anim.Play("anim");
				anim["anim"].speed =speed;
			}
		}
		Debug.Log ("speed : " + anim["anim"].speed);



	}
}
