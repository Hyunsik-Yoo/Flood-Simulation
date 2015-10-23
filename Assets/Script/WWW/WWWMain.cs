using UnityEngine;
using System.Collections;

public class WWWMain : MonoBehaviour {

	string url = "http://119.81.246.250:8080/simple-service-webapp/webapi/myresource";
	
	// Use this for initialization
	void Start () {
		WWWHelper helper = WWWHelper.Instance;
		helper.OnHttpRequest += OnHttpRequest;
		helper.get (100, url);
	}
	
	void OnHttpRequest(int id, WWW www) {
		if (www.error != null) {
			Debug.Log ("[Error] " + www.error);
		} else {
			Debug.Log (www.text);
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
