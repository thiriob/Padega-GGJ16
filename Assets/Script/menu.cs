using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{

    public GameObject b1;
    public GameObject b2;

    public bool esc;
    public bool play;

    void Start()
    {
        if (esc)
        {
            b1.SetActive(false);
            b2.SetActive(false);
        }
    }

    void Update()
    {
        if (esc /*&& start*/)
        {
            b1.SetActive(true);
            b2.SetActive(true);
        }
    }

    void OnMouseDown()
    {
        if (esc)
        {
            if (play)
            {
                b1.SetActive(false);
                b2.SetActive(false);
            }
            else
                SceneManager.LoadScene(0);
        }
        else
        {
            if (play)
                SceneManager.LoadScene(1);
            else
                Application.Quit();
        }
    }
}
