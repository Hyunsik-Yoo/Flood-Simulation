using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class clicked_building : MonoBehaviour {
	public float dist = 100.0f;
	public float height = 100.0f;
	public float dampRotate = 0.0f;
	public static Transform camera_back;


	// Use this for initializatio
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		
	}

	void OnMouseDown()
	{

		CameraMove ();
		GameObject Panel = GameObject.Find ("Panel");
		Panel.transform.FindChild ("info_box").gameObject.SetActive (true);
		Debug.Log ("building was clicked");
	}

	void CameraMove(){
		Transform camera_trans = GameObject.Find("Main Camera").GetComponent<Transform>();
		camera_back = camera_trans;
		Transform target_trans = GetComponent<Transform> ();


		float currYAngle = Mathf.LerpAngle(camera_trans.eulerAngles.y,target_trans.eulerAngles.y,dampRotate*Time.deltaTime);
		
		Quaternion rot = Quaternion.Euler (0,currYAngle,0);
		
		camera_trans.position = target_trans.position - (rot * Vector3.forward * dist) + (Vector3.up * height);
		camera_trans.LookAt(target_trans);
	}
}
