using UnityEngine;
using System.Collections;

public class AddPlayerScript : MonoBehaviour {

	// Use this for initialization
	void Awake () 
	{
		if(GameObject.FindWithTag("PlayerScript") == null)
		{
			GameObject playerScript = Instantiate(Resources.Load("PlayerScript"), new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0)) as GameObject;
			DontDestroyOnLoad(playerScript);
			GameObject caracFenetre = Instantiate(Resources.Load("CaracFenetre"), new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0)) as GameObject;
			DontDestroyOnLoad(playerScript);
		}
	}
}
