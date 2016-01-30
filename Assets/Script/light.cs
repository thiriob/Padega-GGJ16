using UnityEngine;
using System.Collections;

public class light : MonoBehaviour {

	public float time;
	public Light li;
	// Use this for initialization
	void Start () {
		InvokeRepeating("changelight",time,time);
	}
	
	// Update is called once per frame
	void changelight () {
		li.intensity = Random.Range (1.5f, 2.5f);
	}

}