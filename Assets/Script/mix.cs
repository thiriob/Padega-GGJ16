using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class mix : MonoBehaviour
{

   	public GameObject Objects;
    public int len;
    public int width;
    public float ang;
	public Vector3 tmp;
    public GameObject[] sprites;

    void Start()
    {
        GameObject clone;
        
        for (int i = 0; i <= len; i++)
        {
            for (int j = -width; j <= width; j++)
            {
                if (j == -width || j == width)
                    clone = (GameObject)Instantiate(sprites[1], transform.position + tmp * j, Quaternion.identity);
                else
                    clone = (GameObject)Instantiate(sprites[0], transform.position + tmp * j, Quaternion.identity);
				clone.transform.parent = Objects.transform;
            }
            transform.Rotate(Vector3.up * Random.Range(-ang, ang));
            if (transform.rotation.y < 0.76)
                transform.rotation = Quaternion.Euler(0, 100, 0);
            if (transform.rotation.y > 0.99)
                transform.rotation = Quaternion.Euler(0, 170, 0);
			transform.Translate(Vector3.right);
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.R))
            SceneManager.LoadScene(0);
    }
}
