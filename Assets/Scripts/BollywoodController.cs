using UnityEngine;
using System.Collections;

public class BollywoodController : MonoBehaviour
{
	public GameObject B;
	public GameObject O;
	public GameObject L;
	public GameObject L_1;
	public GameObject Y;
	public GameObject hyphen;
	public GameObject W;
	public GameObject O_1;
	public GameObject O_2;
	public GameObject D;
	public int tries;
	public int perm;
	private GameObject[] parts;

	public bool isOver
	{
		get { return tries < 0; }
	}

	// Use this for initialization
	void Start()
	{
		parts = new GameObject[] { D, O_2, O_1, W, hyphen, Y, L_1, L, O, B };
		reset();
	}

	public void wrong()
	{
		//Debug.Log ("Punishing Player");
		if (tries >= 0)
		{
			parts[tries--].SetActive(false);
		}
		/*if(perm == tries + 5)
        {
			parts[tries--].SetActive(false);
		}*/
	}

	public void reset()
	{
		if (parts == null)
			return;

		tries = parts.Length - 1;
		perm = parts.Length - 1;
		foreach (GameObject g in parts)
		{
			g.SetActive(true);
		}
	}
}
