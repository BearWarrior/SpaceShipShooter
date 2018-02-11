using UnityEngine;
using System.Collections;

public class PointToPlayer : MonoBehaviour 
{
	private Transform player;
	
	// Use this for initialization
	void Start () 
	{
		if(GameObject.FindWithTag("Player") != null)
		{
			player = GameObject.FindWithTag("Player").transform;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(player != null)
		{
			int signeY;
			int signeX;
			if(this.transform.position.y - player.transform.position.y < 0)
			{
				signeY = -1;
			}
			else
			{
				signeY = 1;
			}

			if(this.transform.position.x - player.transform.position.x < 0)
			{
				signeX = -1;
			}
			else
			{
				signeX = 1;
			}

			Quaternion quat = new Quaternion();
			quat.eulerAngles = new Vector3(0, 0,  signeY * signeX * 360 / ( 2 * Mathf.PI ) *  Mathf.Acos((this.transform.position.x - player.transform.position.x)/(Mathf.Sqrt(Mathf.Pow((this.transform.position.x - player.transform.position.x), 2) + Mathf.Pow(this.transform.position.y - player.transform.position.y, 2)))) + 180);
			this.transform.rotation = quat;
		}
	}
}
