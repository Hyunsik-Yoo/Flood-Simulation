using UnityEngine;
using System.Collections;

public class anim_script : MonoBehaviour {
	// Use this for initialization
	void Start () {
		Animation anim = GetComponent<Animation> ();
		AnimationCurve curve = new AnimationCurve();
		curve.AddKey (0, 1);
		//curve.AddKey (1, 2);
		//curve.AddKey (2, 3);
		curve.AddKey (3, 4);
		curve.AddKey (6, -4);

		AnimationClip clip = new AnimationClip ();
		clip.legacy = true;
		clip.SetCurve ("", typeof(Transform), "localPosition.x", curve);
		anim.AddClip (clip, "test");
		//anim.PlayQueued ("test2", QueueMode.CompleteOthers);

		//anim ["test"].speed = 3;
		anim.Play ("test");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
