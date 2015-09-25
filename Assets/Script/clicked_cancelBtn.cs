using UnityEngine;
using System.Collections;

public class clicked_cancelBtn : MonoBehaviour {


	public void cancel(){
		Transform camera_trans = GameObject.Find("Main Camera").GetComponent<Transform>();
		Debug.Log (camera_trans);
		camera_trans.localPosition = new Vector3 (camera_trans.localPosition.x, 858, camera_trans.localPosition.z);
		camera_trans.localRotation = Quaternion.Euler (90,270,0);
		Debug.Log (camera_trans);
		move_camera.camera_movable = true;
	}
}
