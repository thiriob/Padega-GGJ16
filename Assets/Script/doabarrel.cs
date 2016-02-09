using UnityEngine;
using System.Collections;

public class doabarrel : MonoBehaviour {

    public bool go;
    public float speed;
    public float Ymax;

	void Update()
    {
        if (go)
            transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.up, speed);
        if (transform.position.y > Ymax)
            Destroy(gameObject);
	}
}
