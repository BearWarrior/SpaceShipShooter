using UnityEngine;
using System.Collections;

public class GoToLocalPos : MonoBehaviour 
{
	public Vector3 goTo;
	public float vitesse;

	// Update is called once per frame
	void Update () 
	{
		if(goTo != null)
		{
			transform.localPosition = Vector3.Lerp (transform.localPosition, goTo, vitesse*Time.deltaTime);
			if(Vector3.Distance(transform.localPosition, goTo) < 0.05f)
			{
				transform.localPosition = goTo;
			}

			if(transform.localPosition == goTo)
			{
				Destroy(GetComponent<GoToLocalPos>());
			}
		}
	}
}
