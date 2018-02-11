using UnityEngine;
using System.Collections;

public class MoveLoot : MonoBehaviour 
{
	public float vitesse;
	private Vector2 deplacement;
	private Vector2 positionDepart;

	private float vitHB;

	// Use this for initialization
	void Start () 
	{
		vitHB = Random.Range (-0.5f, 0.5f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		deplacement = new Vector2 (-vitesse, vitHB);
		transform.Translate (deplacement * Time.deltaTime, Space.World);
		transform.Rotate (15*Time.deltaTime, 15*Time.deltaTime, 15*Time.deltaTime);
	}
}
