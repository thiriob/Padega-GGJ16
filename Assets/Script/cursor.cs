using UnityEngine;
using System.Collections;

public class cursor : MonoBehaviour {

	float posx = 7.4f;
	float posy = 8.3f;
	bool touch = false;
	int idx = 0;
	int idx2 = 0;
	public GameObject[] selected;
	public GameObject center2;
	public GameObject cursor2;
	public GameObject play;
	public GameObject particle;
	int i;

	// Use this for initialization
	void Start () {
		i = -1;
		while (++i < PlayerPrefs.GetInt ("Objects")) {
			Instantiate (GameObject.Find("OBJSS").GetComponent<OBJS>().objs [PlayerPrefs.GetInt ("YOBJ"+ i)], new Vector3 (7.4f + 1.2f * (i % 2), 8.3f - 1.2f * ((i / 2) % 6), 21f), Quaternion.Euler(0, 0, 0));
		}
		Spawnobjs ();
	}

	int real(int idx){
		int i = -1;
		int j = 0;

		while (++i < 10 && j <= idx)
			if (GameObject.Find ("OBJ" + i + "(Clone)"))
				j++;
		return (i - 1);
	}

	int nreal(int idx){
		int i = 9;
		int j =  PlayerPrefs.GetInt ("Objects") - 1;

		while (j > idx && --i > -1)
			if (GameObject.Find ("OBJ" + i + "(Clone)"))
				j--;
		return (i - 1);
	}

	void description(){
		GameObject.Find ("description").SetActive (true);
		if (GameObject.Find ("OBJSS").GetComponent<OBJS> ().objs [PlayerPrefs.GetInt ("YOBJ" + idx)].name == "OBJ0")
			GameObject.Find ("description").GetComponent<TextMesh> ().text = 
				"Random effect";
		else if (GameObject.Find ("OBJSS").GetComponent<OBJS> ().objs [PlayerPrefs.GetInt ("YOBJ" + idx)].name == "OBJ1")
			GameObject.Find ("description").GetComponent<TextMesh> ().text = 
				"Deals 20 more dmg";
		else if (GameObject.Find ("OBJSS").GetComponent<OBJS> ().objs [PlayerPrefs.GetInt ("YOBJ" + idx)].name == "OBJ2")
			GameObject.Find ("description").GetComponent<TextMesh> ().text = 
				"Longer cave";
		else if (GameObject.Find ("OBJSS").GetComponent<OBJS> ().objs [PlayerPrefs.GetInt ("YOBJ" + idx)].name == "OBJ3")
			GameObject.Find ("description").GetComponent<TextMesh> ().text = 
				"Shorter cave";
		else if (GameObject.Find ("OBJSS").GetComponent<OBJS> ().objs [PlayerPrefs.GetInt ("YOBJ" + idx)].name == "OBJ4")
			GameObject.Find ("description").GetComponent<TextMesh> ().text = 
				"Leech (lower life, regeneration with kills)";
		else if (GameObject.Find ("OBJSS").GetComponent<OBJS> ().objs [PlayerPrefs.GetInt ("YOBJ" + idx)].name == "OBJ5")
			GameObject.Find ("description").GetComponent<TextMesh> ().text = 
				"Beautifully useless";
		else if (GameObject.Find ("OBJSS").GetComponent<OBJS> ().objs [PlayerPrefs.GetInt ("YOBJ" + idx)].name == "OBJ6")
			GameObject.Find ("description").GetComponent<TextMesh> ().text = 
				"More life ( + 30 %)";
		else if (GameObject.Find ("OBJSS").GetComponent<OBJS> ().objs [PlayerPrefs.GetInt ("YOBJ" + idx)].name == "OBJ7")
			GameObject.Find ("description").GetComponent<TextMesh> ().text = 
				"More dmg ( + 30 %)";
		else if (GameObject.Find ("OBJSS").GetComponent<OBJS> ().objs [PlayerPrefs.GetInt ("YOBJ" + idx)].name == "OBJ8")
			GameObject.Find ("description").GetComponent<TextMesh> ().text = 
				"More stamina ( + 30 %)";
		else if (GameObject.Find ("OBJSS").GetComponent<OBJS> ().objs [PlayerPrefs.GetInt ("YOBJ" + idx)].name == "OBJ9")
			GameObject.Find ("description").GetComponent<TextMesh> ().text = 
				"More speed ( + 50 %)";
	}

	void Spawnobjs()
	{
		int	i = -1;
		int pas = 360 / PlayerPrefs.GetInt ("Stade");
		int angle = 0; 
		Transform pos;

		pos = center2.transform;	
		pos.position = new Vector3 (16, 5, 10);
		while (++i <= PlayerPrefs.GetInt ("Stade") - 1) {
			pos.position = new Vector3 (16, 5, 10);
			angle = pas * i;
			pos.rotation = Quaternion.Euler (0, 0, -angle);
			pos.position += pos.transform.up * ((PlayerPrefs.GetInt ("Stade") == 4) ? (2 + i % 2) : (2));
			Object lastobj = Instantiate(selected[i], pos.position, Quaternion.Euler(0, 0, 0));
			lastobj.name = "OBJ" + i;
		}
		pos.position = new Vector3 (16, 5, 10);
		pos.rotation = Quaternion.Euler (0, 0, 0);
	}

	void Spawncursor()
	{
		int	i = -1;
		int pas = 360 / PlayerPrefs.GetInt ("Stade");
		int angle = 0; 
		Transform pos;

		pos = center2.transform;
		pos.position = new Vector3 (16, 5, 9);
		while (++i <= idx2) {
			pos.position = new Vector3 (16, 5, 9);
			angle = pas * i;
			pos.rotation = Quaternion.Euler (0, 0, -angle);
			pos.position += pos.transform.up * ((PlayerPrefs.GetInt ("Stade") == 4) ? (2 + i % 2) : (2));
		}
		if (idx2 >= 0) {
			cursor2.transform.position = pos.position;
			play.GetComponent<Renderer>().enabled = false;
		}
		else {
			cursor2.transform.position = new Vector3 (22, 2, 9);
			play.GetComponent<Renderer>().enabled = true;
		}
		pos.position = new Vector3 (16, 5, 10);
		pos.rotation = Quaternion.Euler (0, 0, 0);
	}

	void affnbr(){
		int i = -1;

		i = -1;
		while (++i < PlayerPrefs.GetInt ("Objects"))
			GameObject.Find ("OBJ" + real(i) + "(Clone)").transform.FindChild ("NB").GetComponent<TextMesh> ().text = PlayerPrefs.GetInt ("YOBJNB" + i).ToString ();
	}

	void finish(){
		i = -1;
		while (++i < PlayerPrefs.GetInt ("Stade")) {
			if ((int)(GameObject.Find ("OBJ" + i).transform.localScale.z - 10) == 0)
				PlayerPrefs.SetInt ("DMG", PlayerPrefs.GetInt ("DMG") + 10);
			else if ((int)(GameObject.Find ("OBJ" + i).transform.localScale.z - 10) == 1)
				PlayerPrefs.SetInt ("DMG", PlayerPrefs.GetInt ("DMG") + 20);
			else if ((int)(GameObject.Find ("OBJ" + i).transform.localScale.z - 10) == 2)
				PlayerPrefs.SetInt ("CAVE_SIZE", PlayerPrefs.GetInt ("CAVE_SIZE") + 20);
			else if ((int)(GameObject.Find ("OBJ" + i).transform.localScale.z - 10) == 3)
				PlayerPrefs.SetInt ("CAVE_SIZE", PlayerPrefs.GetInt ("CAVE_SIZE") - 20);
			else if ((int)(GameObject.Find ("OBJ" + i).transform.localScale.z - 10) == 4) {
				PlayerPrefs.SetInt ("LIFE", (int)((float)PlayerPrefs.GetInt ("LIFE") * 0.8));
				PlayerPrefs.SetInt ("LEECH", 1);
			}else if ((int)(GameObject.Find ("OBJ" + i).transform.localScale.z - 10) == 6)
				PlayerPrefs.SetInt ("LIFE", (int)((float)PlayerPrefs.GetInt ("LIFE") * 1.3f));
			else if ((int)(GameObject.Find ("OBJ" + i).transform.localScale.z - 10) == 7)
				PlayerPrefs.SetInt ("DMG", (int)((float)PlayerPrefs.GetInt ("DMG") * 1.3f));
			else if ((int)(GameObject.Find ("OBJ" + i).transform.localScale.z - 10) == 8)
				PlayerPrefs.SetInt ("STAMINA", (int)((float)PlayerPrefs.GetInt ("STAMINA") * 1.3f));
			else if ((int)(GameObject.Find ("OBJ" + i).transform.localScale.z - 10) == 9)
				PlayerPrefs.SetFloat ("SPEED", PlayerPrefs.GetFloat ("SPEED") * 1.5f);
			print (PlayerPrefs.GetInt ("LIFE"));
		}
	}

	// Update is called once per frame
	void Update () {
		if (spawnvillagers.pangle == 0) {
			description ();
			affnbr ();
			if (PlayerPrefs.GetInt ("Objects") == 0)
				posx = -10;
			if (Input.GetAxis ("Horizontal") > 0 && posx < 8.5f && touch == false && idx + 1 < PlayerPrefs.GetInt ("Objects")) {
				posx += 1.2f;
				idx += 1;
				touch = true;
			} else if (Input.GetAxis ("Horizontal") < 0 && posx > 7.5f && touch == false && idx - 1 < PlayerPrefs.GetInt ("Objects")) {
				posx -= 1.2f;
				idx -= 1;
				touch = true;
			} else if (Input.GetAxis ("Vertical") > 0 && posy < 8.2f && touch == false && idx - 2 < PlayerPrefs.GetInt ("Objects")) {
				posy += 1.2f;
				idx -= 2;
				touch = true;
			} else if (Input.GetAxis ("Vertical") < 0 && posy > 2.5f && touch == false && idx + 2 < PlayerPrefs.GetInt ("Objects")) {
				posy -= 1.2f;
				idx += 2;
				touch = true;
			} 

			else if (Input.GetKeyDown (KeyCode.Return) || Input.GetButtonDown ("Fire1")) {
				if (idx2 == -1) {
					
					GameObject.Find ("description").SetActive (false);
					Instantiate (particle);
					GameObject.Find ("mouse or x").SetActive (false);
					finish();
					spawnvillagers.pangle++;
				} else if (idx2 < PlayerPrefs.GetInt ("Stade") - 1) {
					if ((PlayerPrefs.GetInt ("Objects") > 0) && PlayerPrefs.GetInt ("YOBJNB" + idx) > 0) {
						//i = (int)GameObject.Find ("OBJ" + idx2).transform.localScale.z - 10;
						//print ("cacac" + i + "real" + real(0) + "nreal" + nreal(i));
						//PlayerPrefs.SetInt ("YOBJNB" + nreal(i), PlayerPrefs.GetInt ("YOBJNB" + nreal(i)) + 1);
						GameObject.Find ("OBJ" + idx2).GetComponent<SpriteRenderer> ().sprite = GameObject.Find ("OBJSS").GetComponent<OBJS> ().objs [PlayerPrefs.GetInt ("YOBJ" + idx)].GetComponent<SpriteRenderer> ().sprite;
						GameObject.Find ("OBJ" + idx2).transform.localScale = GameObject.Find ("OBJSS").GetComponent<OBJS> ().objs [PlayerPrefs.GetInt ("YOBJ" + idx)].transform.localScale;
						PlayerPrefs.SetInt ("YOBJNB" + idx, PlayerPrefs.GetInt ("YOBJNB" + idx) - 1);
						PlayerPrefs.SetInt ("YFOBJ" + idx2, PlayerPrefs.GetInt ("YOBJ" + idx));
					} else {
						GameObject.Find ("OBJ" + idx2).GetComponent<SpriteRenderer> ().sprite = null;
						PlayerPrefs.SetInt ("YFOBJ" + idx2, -1);
					}
					idx2++;
				} else if (idx2 == PlayerPrefs.GetInt ("Stade") - 1) {
					if ((PlayerPrefs.GetInt ("Objects") > 0) && PlayerPrefs.GetInt ("YOBJNB" + idx) > 0) {
						GameObject.Find ("OBJ" + idx2).GetComponent<SpriteRenderer> ().sprite = GameObject.Find ("OBJSS").GetComponent<OBJS> ().objs [PlayerPrefs.GetInt ("YOBJ" + idx)].GetComponent<SpriteRenderer> ().sprite;
						GameObject.Find ("OBJ" + idx2).transform.localScale = GameObject.Find ("OBJSS").GetComponent<OBJS> ().objs [PlayerPrefs.GetInt ("YOBJ" + idx)].transform.localScale;
						PlayerPrefs.SetInt ("YOBJNB" + idx, PlayerPrefs.GetInt ("YOBJNB" + idx) - 1);
						PlayerPrefs.SetInt ("YFOBJ" + idx2, PlayerPrefs.GetInt ("YOBJ" + idx));
					} else {
						GameObject.Find ("OBJ" + idx2).GetComponent<SpriteRenderer> ().sprite = null;
						PlayerPrefs.SetInt ("YFOBJ" + idx2, -1);
					}
					idx2 = -1;
				} 
			} 

			else if (Input.GetButtonDown ("Fire3")) {
				if (idx2 == -1) {
					GameObject.Find ("description").SetActive (false);
					GameObject.Find ("mouse or x").SetActive (false);
					Instantiate (particle);
					spawnvillagers.pangle++;
				} else if (idx2 < PlayerPrefs.GetInt ("Stade") - 1) {
					GameObject.Find ("OBJ" + idx2).GetComponent<SpriteRenderer> ().sprite = null;
					PlayerPrefs.SetInt ("YFOBJ" + idx2, -1);
					idx2++;
				} else if (idx2 == PlayerPrefs.GetInt ("Stade") - 1) {
					GameObject.Find ("OBJ" + idx2).GetComponent<SpriteRenderer> ().sprite = null;
					PlayerPrefs.SetInt ("YFOBJ" + idx2, -1);
					idx2 = -1;
				}	

			}

			else if (Input.GetKeyDown (KeyCode.Backspace) || Input.GetButtonDown ("Fire2")) {
				if (idx2 > 0) {
					idx2--;
				} else if (idx2 == -1) {
					idx2 = PlayerPrefs.GetInt ("Stade") - 1;
				}
			}
			if (Input.GetAxis ("Vertical") == 0 && Input.GetAxis ("Horizontal") == 0)
				touch = false;
			transform.position = new Vector3 (posx, posy, 21.1f);
			Spawncursor ();
		} else {
			this.GetComponent<Renderer>().enabled = false;
			cursor2.GetComponent<Renderer>().enabled = false;
		}
			
	}
}
