using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnnemyMoveSin : EnemyMove 
{
	//public float vitesse;
	private Vector2 deplacement;
	private Vector2 positionDepart;
	private float temps;
	private float deplacementSin;

	public float tempsTirGroupeMax;
	public float tempsTirGroupeAct;
	public bool shootGr;
	public bool shootInd;
	
	public float tempsTirIndMax;
	public float tempsTirIndAct;
	public int cptTir;

	private List<Transform> listEnemyIntern;
	
	// Use this for initialization
	void Start () 
	{
		vitesse = 3;

		shootGr = false;
		shootInd = false;
		//vitesse = 2;
		tempsTirGroupeMax = 1;
		tempsTirIndMax = 0.3f;
		cptTir = 0;

		listEnemyIntern = new List<Transform> ();
		for(int i = 0; i < listEnemy.Count; i++)
		{
			listEnemyIntern.Add(listEnemy[i]);
		}

		positionDepart = listEnemyIntern[0].transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		temps = Time.time;
		//On déplace le gropupe horizontalement
		deplacement = new Vector2 (-vitesse, 0);
		transform.Translate (deplacement * Time.deltaTime, Space.World);

		for(int i = 0; i < listEnemyIntern.Count; i++)
		{
			if(listEnemyIntern[i] != null)
			{
				deplacementSin = Mathf.Cos(i * 0.1f * 2 * Mathf.PI)*Mathf.Sin(2 * Mathf.PI * temps * 0.3f) + positionDepart.y ;
				listEnemyIntern[i].transform.position  = new Vector3(listEnemyIntern[i].transform.position.x ,deplacementSin, listEnemyIntern[i].transform.position.z);
			}
		}

		//__TIR__//
		if(tempsTirGroupeAct > tempsTirGroupeMax)
		{
			shootGr = true;
			tempsTirGroupeAct = 0;
		}
		else
		{
			tempsTirGroupeAct += Time.deltaTime;
		}

		//On commence la phase de tir
		if(shootGr)
		{
			if(shootInd)
			{
				if(cptTir < listEnemy.Count)
				{
					listEnemy[cptTir].GetComponent<ChassisEnemy>().Fire();
					cptTir++;
				}
				else
				{
					shootGr = false;
				}
				shootInd = false;
			}
			else
			{
				tempsTirIndAct += Time.deltaTime;
				if(tempsTirIndAct > tempsTirIndMax)
				{
					shootInd = true;
					tempsTirIndAct = 0;
				}
			}
		}

		chkIfEmpty ();
	}
}
