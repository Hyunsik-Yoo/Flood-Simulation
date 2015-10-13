using UnityEngine;
using System.Collections;

public class Event : MonoBehaviour {


	public void cancel(){
		Transform camera_trans = GameObject.Find("Main Camera").GetComponent<Transform>();
		//Debug.Log (camera_trans);
		camera_trans.localPosition = new Vector3 (-369, 146, -637);
		camera_trans.localRotation = Quaternion.Euler (40,40,0);
		//Debug.Log (camera_trans);
		move_camera.camera_movable = true;
	}
}
