using UnityEngine;
using System.Collections;

public class rotationX : MonoBehaviour 
{
	// Update is called once per frame
	void Update () 
	{
		transform.Rotate(new Vector3(0, 2.5f*Time.deltaTime, 0));
	}
}
