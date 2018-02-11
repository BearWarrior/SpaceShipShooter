using UnityEngine;
using System.Collections;

public class FireSideBySide : MonoBehaviour 
{
	private float tempsFireAct;
	public float tempsFireMax;
	private bool side;
	public bool fire;

	// Use this for initialization
	void Start () 
	{
		side = true;
		fire = false;
	}
	
	// Update is called once per frame
	void Update () 
	{	
		if(fire)
		{
			if(tempsFireAct > tempsFireMax)
			{
				if(side)
				{
					GetComponent<ChassisEnemy> ().FireFromTo (0, 10);
					side = !side;
				}
				else
				{
					GetComponent<ChassisEnemy> ().FireFromTo (10, 18);
					side = !side;
				}
				
				tempsFireAct = 0;
			}
			else
			{
				tempsFireAct += Time.deltaTime;
			}
		}
	}
}
