using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Group : MonoBehaviour {
	public List<Transform> listEnemy;
	public string pathName;

	// Update is called once per frame
	void Update () 
	{
		chkIfEmpty ();
	}

	protected void chkIfEmpty()
	{
		if(listEnemy.Count == 0)
		{
			Destroy(this.gameObject);
		}
	}
}
