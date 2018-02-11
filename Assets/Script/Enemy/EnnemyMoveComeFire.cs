using UnityEngine;
using System.Collections;

public class EnnemyMoveComeFire : Group 
{
	//public float vitesse;
	private Vector2 deplacement;
	private Vector2 positionDepart;
	
	public float tempsTirGroupeMax;
	public float tempsTirGroupeAct;
	public bool shootGr;
	public bool shootInd;

	public float tempsTirIndMax;
	public float tempsTirIndAct;
	public int cptTir;

	public float distance;

	private float vitesse;
	// Use this for initialization
	void Start () 
	{
		shootGr = false;
		shootInd = false;
		vitesse = 2;
		tempsTirGroupeMax = 1;
		tempsTirIndMax = 0.3f;
		cptTir = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		deplacement = new Vector2 (-vitesse, 0);
		if(transform.position.x > GameObject.FindWithTag("CaracFenetre").GetComponent<CaracFenetre>().CornerUpRight.x - distance)
		{
			transform.Translate (deplacement * Time.deltaTime, Space.World);
		}

		chkIfEmpty ();
	}
}
