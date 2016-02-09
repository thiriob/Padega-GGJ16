using UnityEngine;
using System.Collections;

public class center : MonoBehaviour {

	public Sprite[] img;
	int i = 0;
	// Use this for initialization
	void Start () {
		this.transform.GetComponent<SpriteRenderer>().sprite = img[PlayerPrefs.GetInt("Stade") - 1];
	}
	
	// Update is called once per frame
	void Update () {
		if (this.name == "PLAY")
			this.transform.rotation = Quaternion.Euler (0, 0, i);
		i += 3;
		if (i >= 360)
			i = 0;
	}
}
