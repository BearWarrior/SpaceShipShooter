using UnityEngine;
using System.Collections;

public class MunPerforantes: MonoBehaviour 
{
	Transform player;
	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(player != null)
		{
			if(Vector2.Distance(player.transform.position, this.transform.position) < 1)
			{
				GameObject.FindWithTag("Player").GetComponent<ChassisPlayer>().ChangerMun(2, 7);	
				Destroy(this.gameObject);
			}
		}
	}
}
