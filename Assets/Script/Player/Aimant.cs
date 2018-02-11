using UnityEngine;
using System.Collections;

public class Aimant : MonoBehaviour 
{
	// Update is called once per frame
	void Update () 
	{
		//On suit la position du joueur
		if(GameObject.FindWithTag ("Player"))
		{
			transform.position = GameObject.FindWithTag ("Player").transform.position;
		}
		else
		{
			Destroy(this.gameObject);
		}
	}

	//RECUPERATION DE L'ARGENT
	void OnTriggerStay2D(Collider2D other)
	{
		if(other.transform.tag == "Argent")
		{
			other.transform.position = Vector2.MoveTowards(other.transform.position, this.gameObject.transform.position, 7*Time.deltaTime);
		}
	}
}
