using UnityEngine;
using System.Collections;

public class clicked_cancelBtn : MonoBehaviour {

	public void onClick(){
		Transform back_trans = clicked_building.camera_back;
		Transform camera_trans = GameObject.Find("Main Camera").GetComponent<Transform>();
		Debug.Log (camera_trans);
		camera_trans.localPosition = new Vector3 (-890, 858, -560);
		Debug.Log (camera_trans);
	}
}
