using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class end : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.timeSinceLevelLoad >= 100 || Input.GetKey(KeyCode.Space) /* || button */)
            SceneManager.LoadScene(0);
	}
}
