using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnnemyMoveEscapeDown : EnemyMove 
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

	private float angleMax;
	private List<float> angleAct;
	public float vitesseAng;
	
	// Use this for initialization
	void Start () 
	{
		shootGr = false;
		shootInd = false;
		vitesse = 2.5f;
		tempsTirGroupeMax = 1;
		tempsTirIndMax = 0.3f;
		cptTir = 0;
		
		listEnemyIntern = new List<Transform> ();
		for(int i = 0; i < listEnemy.Count; i++)
		{
			listEnemyIntern.Add(listEnemy[i]);
		}

		angleMax = 2*vitesse;
		angleAct = new List<float>();
		for(int i = 0; i < listEnemyIntern.Count; i++)
		{
			angleAct.Add(0);
		}
		vitesseAng = vitesse;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		//Phase 1 deplacement gauche
		temps = Time.time;
		//On déplace le gropupe horizontalement
		
		for(int i = 0; i < listEnemyIntern.Count; i++)
		{
			deplacement = new Vector2 (-vitesse, 0);
			
			if(listEnemyIntern[i] != null)
			{
				listEnemyIntern[i].transform.Translate (deplacement * Time.deltaTime, Space.World);
				
				if(listEnemyIntern[i].transform.position.x < GameObject.FindWithTag("CaracFenetre").GetComponent<CaracFenetre>().CornerDownLeft.x * 1/4 /*+ (GameObject.FindWithTag("CaracFenetre").GetComponent<CaracFenetre>().CornerUpRight.y - transform.position.y)*/)
				{
					if(angleAct[i] < angleMax)
					{
						angleAct[i] += vitesseAng*Time.deltaTime;
					}
					else
					{
						angleAct[i] = angleMax;
					}
					deplacement = new Vector2 (-vitesse, -angleAct[i]);
					listEnemyIntern[i].transform.Translate (deplacement * Time.deltaTime, Space.World);
					
					//rotation
					Quaternion quat = Quaternion.Euler (0, 0, 45 * angleAct[i]/angleMax);
					listEnemyIntern[i].transform.rotation = quat;
				}
			}
		}

		chkIfEmpty ();
	}
}
