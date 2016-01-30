using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour
{
	public Color jaune, gris;
	public float stamemax, regenstam, stamina, consostam;
	bool roul;
 	public AudioSource audio;
	public AudioClip clip;
	public float speed, dashspeed, roulT = 2.5f;
	private Vector3 moveDirection = Vector3.zero;
	public CharacterController controller;
	// Use this for initialization
	void Start ()
	{
		roul = true;
	}

	void Update ()
	{
		handle_button();
		set_move();
		manage_stamina();
	}

	void	set_move()
	{
		moveDirection = new Vector3(Input.GetAxis("Horizontal") * -1, 0, Input.GetAxis("Vertical") * -1);
		moveDirection = transform.TransformDirection(moveDirection);
		moveDirection *= speed;
		controller.Move(moveDirection * Time.deltaTime);
	}


	void	handle_button()
	{
		if (Input.GetButtonDown ("ButtonA")) {
				roulade();
			//Debug.Log ("Button A");
		} else if (Input.GetButtonDown ("ButtonB")) {
			Debug.Log ("Button B");
		} else if (Input.GetButtonDown ("ButtonX")) {
			attaque ();
			//Debug.Log ("Button X");
		} else if (Input.GetButtonDown ("ButtonY")) {
			Debug.Log ("Button Y");
		}
		if (roulT > 0)
			roulT -= 0.1f;
		else if (roulT == 0)
			roul = true;
	}

	void	roulade()
	{
		if (Input.GetAxis ("Horizontal") > 0) {
			if (stamina - 45 > 0) {
				stamina -= 45;
				controller.Move (transform.TransformDirection(Vector3.right) * -dashspeed);
			}
			Debug.Log ("DROITE");
		} else if (Input.GetAxis ("Horizontal") < 0) {
			if (stamina - consostam > 0) {
				stamina -= consostam;
				controller.Move (transform.TransformDirection(Vector3.left) * -dashspeed);
			}
			Debug.Log ("GAUCHE");
		} else if (Input.GetAxis ("Vertical") > 0) {
			if (stamina - consostam > 0) {
				stamina -= consostam;
				controller.Move (transform.TransformDirection(Vector3.forward) * -dashspeed);
			}
			Debug.Log ("HAUT");
		} else if (Input.GetAxis ("Vertical") < 0) {
			if (stamina - consostam > 0) {
				stamina -= consostam;
				controller.Move (transform.TransformDirection (Vector3.forward) * dashspeed);
			}
			Debug.Log ("BAS");
		}
		roul = false;
	}

	void	attaque()
	{
		Debug.Log ("ATTAQUE");
	}

	void	manage_stamina()
	{
		this.transform.FindChild ("stamina").GetComponent<Transform> ().localScale = new Vector3 ((stamina / stamemax), 0.1f, 0.2f);
		if (stamina < stamemax)
			stamina += regenstam;
		if (stamina < consostam)
			this.transform.FindChild ("stamina").GetComponent<MeshRenderer> ().material.color = gris;
		else
			this.transform.FindChild ("stamina").GetComponent<MeshRenderer> ().material.color = jaune;
	}

	void	OnTriggerEnter(Collider hit)
	{
		if (hit.transform.tag == "item") {
			audio.PlayOneShot(clip);
			Destroy (hit.gameObject);
		}
	}
}