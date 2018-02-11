using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour 
{
	public float tempsFireAct;
	public float tempsFireMax;

	public bool fire;

	// Use this for initialization
	void Start () 
	{
		fire = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(fire)
		{
			if(tempsFireAct > tempsFireMax)
			{
				GetComponent<ChassisEnemy> ().Fire ();
				tempsFireAct = 0;
			}
			else
			{
				tempsFireAct += Time.deltaTime;
			}
		}
	}
}
