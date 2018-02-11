using UnityEngine;
using System.Collections;

public class EnnemyMoveRect : MonoBehaviour 
{
	private Vector2 deplacement;
	private Vector2 positionDepart;
	private float vitesse = 3.5f;

	// Update is called once per frame
	void Update () 
	{
		deplacement = new Vector2 (-vitesse, 0);
		transform.Translate (deplacement * Time.deltaTime, Space.World);
	}
}
