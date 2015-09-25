using UnityEngine;
using System.Collections;

public class move_camera : MonoBehaviour {

	public float sensitivityX = 8F; 
	public float sensitivityY = 8F; 
	public float speed = 30.0f;
	public static bool camera_movable = true;
	float mHdg = 0F; 
	float mPitch = 0F; 

	void Start() 
	{ 
		// owt? 
	} 

	void Update() 
	{ 

		if (camera_movable) {
			if (Input.GetAxis ("Mouse ScrollWheel") < 0) { 
				transform.Translate (0, 0, -speed); 
			} 
			if (Input.GetAxis ("Mouse ScrollWheel") > 0) { 
				transform.Translate (0, 0, speed); 
			} 
			if (Input.GetMouseButton (0)) { 
				if (Input.GetAxis ("Mouse X") < 0) { 
					transform.Translate (speed, 0, 0); 
				} 
			} 
			if (Input.GetMouseButton (0)) { 
				if (Input.GetAxis ("Mouse X") > 0) { 
					transform.Translate (-speed, 0, 0); 
				} 
			} 
			if (Input.GetMouseButton (0)) { 
				if (Input.GetAxis ("Mouse Y") < 0) { 
					transform.Translate (0, speed, 0); 
				} 
			} 
			if (Input.GetMouseButton (0)) { 
				if (Input.GetAxis ("Mouse Y") > 0) { 
					transform.Translate (0, -speed, 0); 
				} 
			} 
			if (Input.GetMouseButton (1)) { 
				if (Input.GetAxis ("Mouse X") > 0) { 
					transform.Rotate (0, 0.5f, 0, Space.World); 
					transform.Translate (-0.17f, 0, 0); 
				} 
			} 
			if (Input.GetMouseButton (1)) { 
				if (Input.GetAxis ("Mouse X") < 0) { 
					transform.Rotate (0, -0.5f, 0, Space.World); 
					transform.Translate (0.17f, 0, 0); 
				} 
			} 
		}

	} 
}
