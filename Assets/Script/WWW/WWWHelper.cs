using UnityEngine;
using System.Collections;

public class WWWHelper : MonoBehaviour {

	public delegate void HttpRequestDelegate(int id, WWW www);

	public event HttpRequestDelegate OnHttpRequest;

	private int requestId;

	static WWWHelper current = null;

	static GameObject container = null;

	public static WWWHelper Instance {
		get {
			if (current == null) {
				container = new GameObject ();
				container.name = "WWWHelper";
				current = container.AddComponent (typeof(WWWHelper)) as WWWHelper;
			}
			return current;
		}
	}

	public void get(int id, string url){
		WWW www = new WWW (url);
		StartCoroutine (WaitForRequest (id, www));
	}


	private IEnumerator WaitForRequest(int id, WWW www){
		yield return www;

		bool hasCompleteListener = (OnHttpRequest != null);

		if (hasCompleteListener) {
			OnHttpRequest (id, www);
		}
		www.Dispose ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
