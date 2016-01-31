using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour
{
	public Color jaune, gris;
	public float stamemax, regenstam, stamina, consostam, frappedist, offset, roulT, attT,attTi;
	public bool roul, att;
 	public AudioSource audio;
	public AudioClip clip;
	public float speed, dashspeed = 2.5f;
	private Vector3 moveDirection = Vector3.zero;
	Vector3 tmp;
	public CharacterController controller;
	public GameObject frappe;
	private BoxCollider box;
	public Animator anim;
	// Use this for initialization
	void Start ()
	{
		box = frappe.GetComponent<BoxCollider> ();
		roul = true;
		att = false;
		box.enabled = false;
	}

	void Update ()
	{
		set_frappe ();
		handle_button();
		set_move();
		manage_stamina();
		timer ();
		do_anim ();
	}

	void	set_move()
	{
		
		if (!att) {
			moveDirection = new Vector3 (Input.GetAxis ("Horizontal") * -1, 0, Input.GetAxis ("Vertical") * -1);
			moveDirection = transform.TransformDirection (moveDirection);
			moveDirection *= speed;
			controller.Move (moveDirection * Time.deltaTime);
		}
		Vector3 tmp = transform.position;
		tmp.y = -0.1f;
		transform.position = tmp;
	}


	void	handle_button()
	{
		if (Input.GetButtonDown ("ButtonA")) {
				roulade();
			//Debug.Log ("Button A");
		} else if (Input.GetButtonDown ("ButtonB")) {
			Debug.Log ("Button B");
		} if (Input.GetButtonDown ("ButtonX") && !att) {
			attaque ();
			//Debug.Log ("Button X");
		} 
			
		if (Input.GetButtonDown ("ButtonY")) {
			Debug.Log ("Button Y");
		}

	}

	void	roulade()
	{
		if (Input.GetAxis ("Horizontal") > 0) {
			if (stamina - 45 > 0) {
				stamina -= 45;
				controller.Move (transform.TransformDirection(Vector3.right) * -dashspeed);
			}
		} else if (Input.GetAxis ("Horizontal") < 0) {
			if (stamina - consostam > 0) {
				stamina -= consostam;
				controller.Move (transform.TransformDirection(Vector3.left) * -dashspeed);
			}
		} else if (Input.GetAxis ("Vertical") > 0) {
			if (stamina - consostam > 0) {
				stamina -= consostam;
				controller.Move (transform.TransformDirection(Vector3.forward) * -dashspeed);
			}
		} else if (Input.GetAxis ("Vertical") < 0) {
			if (stamina - consostam > 0) {
				stamina -= consostam;
				controller.Move (transform.TransformDirection (Vector3.forward) * dashspeed);
			}
		}
		roul = false;
	}

	void	attaque()
	{
		box.enabled = true;
		att = true;
		attT = attTi;
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

	void set_frappe()
	{
		tmp = transform.position;
		if (Input.GetAxis ("Horizontal") > 0) {
			tmp.x -= frappedist;
			tmp.z += frappedist;

		} 
		else if (Input.GetAxis ("Horizontal") < 0) {
			tmp.x += (frappedist + offset);
			tmp.z-= (frappedist + offset);


		} if (Input.GetAxis ("Vertical") > 0) {
			tmp.x-= frappedist;
			tmp.z-= frappedist;

		} else if (Input.GetAxis ("Vertical") < 0) {
			tmp.x += frappedist;
			tmp.z += frappedist;

		}
		if (Input.GetAxis ("Horizontal") != 0 ||Input.GetAxis ("Vertical") !=0)
		frappe.transform.position = tmp;
	}

	void	timer()
	{
		if (roulT > 0)
			roulT -= 0.1f;
		else if (roulT <= 0)
			roul = true;
		if (attT > 0)
			attT -= 0.1f;
		else if (attT <= 0) {
			att = false;
			box.enabled = false;
		}
	}

	void do_anim()
	{
		if (Input.GetAxis ("Horizontal") > 0.5f)
			anim.Play ("perso autre cote");
		else if (Input.GetAxis ("Horizontal") < -0.5f) 
			anim.Play ("perso coter");
		else if (Input.GetAxis ("Vertical") > -0.5f) 
			anim.Play ("perso dos");
		else if (Input.GetAxis ("Vertical") < 0.5f) 
				anim.Play ("perso devant");
	}

	void	OnTriggerEnter(Collider hit)
	{
		if (hit.transform.tag == "item") {
			audio.PlayOneShot(clip);
			Destroy (hit.gameObject);
		}
	}
	void	OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (hit.transform.tag == "fantome") {
			lifeb scri = this.GetComponent<lifeb>();
			scri.life -= 5;
		}
	}
}