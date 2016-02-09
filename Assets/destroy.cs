using UnityEngine;
using System.Collections;

public class destroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.LookAt(Camera.main.transform.position, -Vector3.up);
		if (this.name == "Villager(Clone)")
			Destroy (this.gameObject, Time.fixedDeltaTime);
		else if (spawnvillagers.pangle > 0)
			Destroy (this.gameObject);
	}

	void Update(){
		if (this.name == "Villager(Clone)")
			Destroy (this.gameObject, Time.fixedDeltaTime);
		else if (spawnvillagers.pangle > 0)
			Destroy (this.gameObject);
	}
}
