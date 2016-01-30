using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour
{
	bool roul;
	public float roulT = 2.5f;
	public float speed, dashspeed = 2.5f;
	public Rigidbody rig;
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
		if (roulT > 0)
			roulT -= 0.1f;
		else if (roulT == 0)
			roul = true;
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
	}

	void	roulade()
	{
		if (Input.GetAxis ("Horizontal") > 0) {
			controller.Move (transform.TransformDirection(Vector3.right) * -dashspeed);
			Debug.Log ("DROITE");
		} else if (Input.GetAxis ("Horizontal") < 0) {
			controller.Move (transform.TransformDirection(Vector3.left) * -dashspeed);
			Debug.Log ("GAUCHE");
		} else if (Input.GetAxis ("Vertical") > 0) {
			controller.Move (transform.TransformDirection(Vector3.forward) * -dashspeed);
			Debug.Log ("HAUT");
		} else if (Input.GetAxis ("Vertical") < 0) {
			controller.Move (transform.TransformDirection(Vector3.forward) * dashspeed);
			Debug.Log ("BAS");
		}
		roul = false;

	}

	void	attaque()
	{
		Debug.Log ("ATTAQUE");
	}
}