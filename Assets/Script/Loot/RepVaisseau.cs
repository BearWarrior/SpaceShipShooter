using UnityEngine;
using System.Collections;

public class RepVaisseau : MonoBehaviour {

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
				if(GameObject.FindWithTag("Player").GetComponent<ChassisPlayer>().Repair(5))
				{
					Destroy(this.gameObject);
				}
			}
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if(other.tag == "ChassiAllie")
		{
			if(other.GetComponentInParent<ChassisAllie>().Repair(5))
			{
				Destroy(this.gameObject);
			}
		}

		if(other.tag == "Player")
		{
			if(other.GetComponent<ChassisPlayer>().Repair(5))
			{
				Destroy(this.gameObject);
			}
		}
	}
}
