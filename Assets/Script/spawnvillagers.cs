using UnityEngine;
using System.Collections;

public class spawnvillagers : MonoBehaviour {

	public GameObject villager;
	public GameObject villager1;
	public GameObject center;
	public GameObject[] obj;
	float angle = 45;
	static public float pangle = 0;

	// Use this for initialization
	void Awake () {
		int i = -1;
		int j;

		//PlayerPrefs.SetInt("Init", 1);	
		if (PlayerPrefs.GetInt ("Init") != 42) {
			PlayerPrefs.SetInt("Stade", 1);
			PlayerPrefs.SetInt("Objects", 0);
			PlayerPrefs.SetInt("Init", 42);
			PlayerPrefs.SetInt("OBJ" + 0, 0);
			PlayerPrefs.SetInt("OBJ" + 1, 0);
			PlayerPrefs.SetInt("OBJ" + 2, 0);
			PlayerPrefs.SetInt("OBJ" + 3, 0);
			PlayerPrefs.SetInt("OBJ" + 4, 0);
			PlayerPrefs.SetInt("OBJ" + 5, 0);
			PlayerPrefs.SetInt("OBJ" + 6, 0);
			PlayerPrefs.SetInt("OBJ" + 7, 0);
			PlayerPrefs.SetInt("OBJ" + 8, 0);
			PlayerPrefs.SetInt("OBJ" + 9, 0);

			while (++i < 10)
				PlayerPrefs.SetInt("OBJ" + i, 0);
		}
		PlayerPrefs.SetInt ("DMG", 10);
		PlayerPrefs.SetInt ("CAVE_SIZE", 100);
		PlayerPrefs.SetInt ("LIFE", 100);
		PlayerPrefs.SetInt ("LEECH", 0);
		PlayerPrefs.SetInt ("STAMINA", 100);
		PlayerPrefs.SetFloat ("SPEED", 10);



		j = 0;
		i = -1;
		while (++i < 10)
			if (PlayerPrefs.GetInt ("OBJ" + i) >= 1)
				j++;
		PlayerPrefs.SetInt("Objects", j);
		i = -1;
		j = -1;
		while (++i < 10) {
			if (PlayerPrefs.GetInt ("OBJ" + i) >= 1) {
				PlayerPrefs.SetInt ("YOBJ" + ++j, i);
				PlayerPrefs.SetInt ("YOBJNB" + j, PlayerPrefs.GetInt ("OBJ" + i));
			}
		}
		pangle = 0;
		i = -1;
		int pas = 360 / PlayerPrefs.GetInt ("Stade");
		int angle = 45;
		Transform pos;

		pos = center.transform;
		pos.position = new Vector3 (0, 0, 0);
		while (++i < PlayerPrefs.GetInt ("Stade")) {
			pos.position = new Vector3 (-1, 2, -1);
			angle += pas;
			pos.rotation = Quaternion.Euler (90, angle, 0);
			pos.position += pos.transform.up * 4;
			Instantiate (villager1, pos.position, pos.rotation);
		}
		pos.position = new Vector3 (0, 0, 0);
		pos.rotation = Quaternion.Euler (90, 45, 0);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		int	i = -1;
		int pas = 360 / PlayerPrefs.GetInt ("Stade");
		Transform pos;

		if (pangle > 0) {
			pangle += 0.03f;
			angle += pangle;
			pos = center.transform;
			pos.position = new Vector3 (0, 0, 0);
			while (++i < PlayerPrefs.GetInt ("Stade")) {
				pos.position = new Vector3 (-1, 2, -1);
				angle += pas;
				pos.rotation = Quaternion.Euler (90, angle, 0);
				pos.position += pos.transform.up * 4;
				Instantiate (villager, pos.position, pos.rotation);
			}
			pos.position = new Vector3 (0, 0, 0);
			pos.rotation = Quaternion.Euler (90, 45, 0);
			if (pangle > 3 * (PlayerPrefs.GetInt ("Stade") + 1))
				Application.LoadLevel (2);
		}
	}
}
