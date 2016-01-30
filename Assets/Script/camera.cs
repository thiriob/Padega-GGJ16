using UnityEngine;
using System.Collections;

public class camera : MonoBehaviour {

	public GameObject player;
	public Vector3 offset;
	
	// Update is called once per frame
	void Update () {
		this.transform.position = new Vector3 (player.transform.position.x + offset.x, this.transform.position.y, player.transform.position.z + offset.z);
	}
}
