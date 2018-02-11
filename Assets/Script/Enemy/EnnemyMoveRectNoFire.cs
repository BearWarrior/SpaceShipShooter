using UnityEngine;
using System.Collections;

public class EnnemyMoveRectNoFire : EnemyMove 
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


	// Use this for initialization
	void Start () 
	{
		shootGr = false;
		shootInd = false;
		vitesse = 3.5f;
		tempsTirGroupeMax = 1;
		tempsTirIndMax = 0.3f;
		cptTir = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		deplacement = new Vector2 (-vitesse, 0);
		transform.Translate (deplacement * Time.deltaTime, Space.World);

		if(tempsTirGroupeAct > tempsTirGroupeMax)
		{
			shootGr = true;
			tempsTirGroupeAct = 0;
		}
		else
		{
			tempsTirGroupeAct += Time.deltaTime;
		}

		chkIfEmpty ();
	}
}