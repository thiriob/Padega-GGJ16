using UnityEngine;
using System.Collections;

public class frappe : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	void	OnColliderStay(Collider hit)
	{
		Debug.Log ("lol");
		if (hit.transform.tag == "fantome") {
			Debug.Log ("TAPE");
			hit.gameObject.SendMessage ("degat", 0.75f);
		}
	}
}
