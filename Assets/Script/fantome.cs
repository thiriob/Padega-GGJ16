using UnityEngine;
using System.Collections;

public class fantome : MonoBehaviour {

	public Sprite deg, nor;
	public SpriteRenderer spri;
	bool lol;
	float life, time;
	public AudioClip clip;
	public AudioSource audio;
	// Use this for initialization
	void Start () {
		life = 50;
		lol = false;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 tmp = transform.position;
		tmp.y = 0.4f;
		transform.position = tmp;
		if (lol) {
			spri.sprite = deg;
		}
		else
			spri.sprite = nor;
		if (life <= 0)
			Destroy (this.gameObject);
		timer ();
	}

	void	OnTriggerEnter(Collider hit)
	{
		if (hit.transform.tag == "frappe") {
			audio.PlayOneShot (clip);
			life -= PlayerPrefs.GetInt ("DMG");;
			time += 2f;
			lol = true;
			Vector3 vec = transform.position - hit.transform.position;
			CharacterController carac = GetComponent<CharacterController> ();
			carac.Move (vec * 0.5f);
		}
	}
	void	timer()
	{
		if (time > 0)
			time -= 0.1f;
		else if (time <= 0)
			lol = false;
	}
}
