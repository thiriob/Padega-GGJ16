using UnityEngine;
using System.Collections;

public class iapourrie : MonoBehaviour {

    public Transform target;
    public CharacterController c;

    public Vector3 speed;
    public float detect;
	
	void Update ()
    {
        if (Vector3.Distance(transform.position, target.position) <= detect)
            c.Move(-((transform.position - target.transform.position).normalized) * Random.Range(speed.x, speed.y));
	}
}
