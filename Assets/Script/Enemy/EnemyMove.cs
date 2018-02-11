using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class EnemyMove : MonoBehaviour 
{
	public List<Transform> listEnemy;
	public float vitesse;

	// Use this for initialization
	void Start () 
	{
		vitesse = 3.5f;
	}

	protected void chkIfEmpty()
	{
		if(listEnemy.Count == 0)
		{
			Destroy(this.gameObject);
		}
	}
}