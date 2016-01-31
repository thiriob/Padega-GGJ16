using UnityEngine;
using System.Collections;

public class lifeb : MonoBehaviour {
	public float lifemax;
	public float life;
	public Color green= new Color(0,1,0,1);
	public Color orange= new Color(1,0.2f,0,1);
	public Color red = new Color(1,0,0,1);
	// Use this for initialization
	void Start () {
		lifemax = 100;
		life = 100;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.FindChild ("lifebar").GetComponent<Transform> ().localScale = new Vector3 ((life / lifemax), 0.1f, 0.2f);

		//changement de couleur
		if ((life/lifemax) >= 0.66f)
			this.transform.FindChild ("lifebar").GetComponent<MeshRenderer> ().material.color = green;
		else if((life/lifemax) >= 0.33f)
			this.transform.FindChild ("lifebar").GetComponent<MeshRenderer> ().material.color = orange;
			else 
			this.transform.FindChild ("lifebar").GetComponent<MeshRenderer> ().material.color = red;
	}
}
