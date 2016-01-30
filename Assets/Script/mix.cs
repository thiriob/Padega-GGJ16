using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class mix : MonoBehaviour
{

    public GameObject Grounds;
    public GameObject Walls;
    public int len;
    public int width;
    public int freq;
    public float ang;
    public Vector3 tmp;
    public GameObject[] sprites;

	void pathmerge(GameObject parent, bool recalc)
    {
        MeshFilter[] meshFilters = parent.GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
		MeshCollider collid = parent.GetComponent<MeshCollider> ();

        int i = 0;
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);
            i++;
        }
        parent.transform.GetComponent<MeshFilter>().mesh = new Mesh();
        parent.transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
        parent.transform.gameObject.SetActive(true);
	//	collid.sharedMesh.RecalculateNormals();
    }

    void Start()
    {
        GameObject clone;

        for (int i = 0; i <= len; i++)
        {
            if (i % freq == 0)
            {
                clone = (GameObject)Instantiate(sprites[0], transform.position, transform.localRotation);
                clone.transform.parent = Grounds.transform;
            }
            if (i % (freq / 2) == 0)
            { 
                clone = (GameObject)Instantiate(sprites[1], transform.position + tmp * -width, transform.localRotation);
                clone.transform.parent = Walls.transform;
                clone = (GameObject)Instantiate(sprites[1], transform.position + tmp * width, transform.localRotation);
                clone.transform.parent = Walls.transform;
            }
            transform.Rotate(Vector3.up * Random.Range(-ang, ang));
            if (transform.rotation.y < 0.76)
                transform.rotation = Quaternion.Euler(0, 100, 0);
            if (transform.rotation.y > 0.99)
                transform.rotation = Quaternion.Euler(0, 170, 0);
            transform.Translate(Vector3.right);
        }
		pathmerge(Grounds, false);
		pathmerge(Walls, true);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.R))
            SceneManager.LoadScene(0);
    }
}
