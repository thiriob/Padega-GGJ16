using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class mix : MonoBehaviour
{

    public GameObject Grounds;
    public GameObject Walls;
    public Vector3 tmp;
    public GameObject[] sprites;
    public int nbsalle;

    void pathmerge(GameObject parent)
    {
        MeshFilter[] meshFilters = parent.GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        for (int i = 0; i < meshFilters.Length; i++)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);
        }
        parent.transform.GetComponent<MeshFilter>().mesh = new Mesh();
        parent.transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
        parent.transform.gameObject.SetActive(true);
        parent.GetComponent<MeshCollider>().sharedMesh = parent.GetComponent<MeshFilter>().mesh;
        for (int i = 1; i < meshFilters.Length; i++)
            Destroy(meshFilters[i].gameObject);
    }

    void chemin(int len, int width, int freq, float ang, int idx)
    {
        GameObject clone;
        float decal = 0.0001f;
        float decal2 = 0.0001f;

        for (int i = 0; i <= len; i++)
        {
            if (i % freq == 0)
            {
                clone = (GameObject)Instantiate(sprites[idx], transform.position + new Vector3(0, decal *= -1, 0), transform.localRotation);
                clone.transform.parent = Grounds.transform;
                clone = (GameObject)Instantiate(sprites[idx + 1], transform.position + tmp * -width + new Vector3(0, decal2, 0), transform.localRotation);
                clone.transform.parent = Walls.transform;
                clone = (GameObject)Instantiate(sprites[idx + 1], transform.position + tmp * width + new Vector3(0, decal2 *= -1, 0), transform.localRotation);
                clone.transform.parent = Walls.transform;
            }
            transform.Rotate(Vector3.up * Random.Range(-ang, ang));
            if (transform.rotation.y < 0.76)
                transform.rotation = Quaternion.Euler(0, 100, 0);
            if (transform.rotation.y > 0.99)
                transform.rotation = Quaternion.Euler(0, 170, 0);
            transform.Translate(Vector3.right);
        }
    }

    void salle(int width, int height, int dist)
    {
        GameObject clone;
		Vector3 tmp = transform.position;
		float decal = 0.0002f;

        int i = 1, j = 1;

        while (--i >= -width)
        {
            j = 1;
            while (--j >= -height)
            {
                clone = (GameObject)Instantiate(sprites[0], tmp + -transform.right * dist * j + transform.forward * dist * i, transform.rotation);
                clone.transform.parent = Grounds.transform;
                if ((j == 0 && i!= 0) || (j == -height && i != -width))
                {
					clone = (GameObject)Instantiate(sprites[2], tmp + -transform.right * dist * (j - ((j == 0) ? (-0.5f) : (0.5f))) + transform.forward * dist * i + new Vector3(0, decal *= -1, 0), transform.rotation);
                    clone.transform.parent = Walls.transform;
                }
                if (i == 0 || i == -width)
                {
					clone = (GameObject)Instantiate(sprites[2], tmp + -transform.right * dist * j + transform.forward * dist * (i - ((i == 0) ? (-0.5f) : (0.5f))) + new Vector3(0, decal *= -1, 0), transform.rotation);
                    clone.transform.Rotate(Vector3.up, 90);
                    clone.transform.parent = Walls.transform;
                }
            }
            
        }
        transform.position = tmp + -transform.right * dist * j + transform.forward * dist * (i + 1);
    }

    void Start()
    {
        for (int i = 0; i <= nbsalle; i++)
        {
            chemin(Random.Range(50, 50), 4, 7, 10, 0);
            salle(Random.Range(1, 5), Random.Range(1, 5), 11);
        }
        pathmerge(Grounds);
        pathmerge(Walls);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.R))
            SceneManager.LoadScene(0);
    }
}
