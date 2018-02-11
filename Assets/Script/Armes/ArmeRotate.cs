using UnityEngine;
using System.Collections;

public class ArmeRotate : MonoBehaviour 
{

	public float vitesse = 15;
	// Update is called once per frame
	void Update () 
	{
		transform.Rotate (0, vitesse * Time.deltaTime, 0, Space.World);
	}
}
