using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class mix : MonoBehaviour
{
    public GameObject boss;
    public GameObject Grounds;
    public GameObject Walls;
    public Vector3 tmp;
    public GameObject[] sprites;
    public int nbsalle;
    public GameObject[] objets;
    public float[] pourcentage;
    public Vector3[] decalobj;
    public bool[] insalle;
	public GameObject BOSS;

    void putobject(int j, int k, int dist, float mult, bool is_salle)
    {
        float rnd = Random.Range(0, 100);

        for (int i = 0; i < pourcentage.Length; i++)
            if (pourcentage[i] * mult >= rnd && insalle[i] == is_salle) 
            {
                Instantiate(objets[i], transform.position + transform.right * dist * (-k + decalobj[i].y) + transform.forward * dist * (j + decalobj[i].x) + transform.up, objets[i].transform.rotation);
                return;
            }
    }

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
                putobject(1, 1, 1, 0.5f, false);
            }
            transform.Rotate(Vector3.up * Random.Range(-ang, ang));
            if (transform.rotation.y < 0.76)
                transform.rotation = Quaternion.Euler(0, 100, 0);
            if (transform.rotation.y > 0.99)
                transform.rotation = Quaternion.Euler(0, 170, 0);
            transform.Translate(Vector3.right);
        }
    }

    void salle(int width, int height, int dist, bool ferme)
    {
        GameObject clone;
        Vector3 tmp = transform.position + transform.right * 3;
        float decal = 0.0002f;
        int i = 1, j = 1;

        while (--i >= -width)
        {
            j = 1;
            while (--j >= -height)
            {
                clone = (GameObject)Instantiate(sprites[0], tmp + -transform.right * dist * j + transform.forward * dist * i, transform.rotation);
                clone.transform.parent = Grounds.transform;
                if ((j == 0 && i != 0) || (j == -height && i != -width) || (j == -height && ferme == true))
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
                if (ferme == false)
                    putobject(i, j, dist, 1, true);
            }

        }
        transform.position = tmp + -transform.right * dist * (j + 0.5f) + transform.forward * dist * (i + 1);
    }

    void Start()
    {
        GameObject clone = (GameObject)Instantiate(sprites[2], transform.position, transform.rotation);
        clone.transform.position += transform.right * - 5f + transform.forward * 0.5f;
        clone.transform.parent = Walls.transform;
        chemin(Random.Range(50, 200), 4, 7, 10, 0);
        for (int i = 0; i <= nbsalle; i++)
        {
            salle(Random.Range(1, 5), Random.Range(1, 5), 11, false);
            chemin(Random.Range(50, 200), 4, 7, 10, 0);
        }
        salle(5, 5, 11, true);
        pathmerge(Grounds);
        pathmerge(Walls);
Instantiate(boss, transform.position + transform.right * -30 + transform.forward * 30 + transform.up * 5, boss.transform.rotation);
    }

	}
