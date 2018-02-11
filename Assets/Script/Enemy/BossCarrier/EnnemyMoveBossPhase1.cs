using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class EnnemyMoveBossPhase1 : Group 
{
	//public float vitesse;
	private Vector2 deplacement;
	private Vector2 positionDepart;

	public float positionXDroite;
	public float positionXGauche;

	public float tempsTirMax;
	private float tempsTirAct;

	public bool haut;
	public bool bas;
	public bool canMove;

	public float tempsPause;
	public float tempsPauseMax;

	public bool joinCenter;
	public bool waitingForOrder;

	public bool bossDead;

	private float vitesse;
	
	// Use this for initialization
	void Start () 
	{
		vitesse = 1.5f;
		tempsTirMax = 8;
		haut = false;
		bas = true;
		tempsPauseMax = 2;
		canMove = true;
		waitingForOrder = false;
		bossDead = false;

		positionXDroite = GameObject.FindWithTag ("CaracFenetre").GetComponent<CaracFenetre> ().CornerUpRight.x + 5;
		listEnemy[0].GetComponent<Fire>().fire = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		chkIfEmpty ();

		if(listEnemy.Count == 1)
		{
			//SI il a plus de la moitié de sa vie
			if(listEnemy[0].GetComponent<ChassisEnemy>().pointDeVie > listEnemy[0].GetComponent<ChassisEnemy>().nbPointDeVieMax/2)
			{
				deplacement = new Vector2 (-vitesse, 0);
				if(transform.position.x >  1)
				{
					transform.Translate (deplacement * Time.deltaTime, Space.World);
				}
			}
			else //On passe a la phase 2 : on le fait partir a droite, disapraitre puis réaparaitre plus armé
			{

				if(listEnemy[0] != null)
				{
					if(transform.position.x <  GameObject.FindWithTag("CaracFenetre").GetComponent<CaracFenetre>().CornerUpRight.x)
					{
						listEnemy[0].GetComponent<Fire>().fire = true;
					}
					else
					{
						listEnemy[0].GetComponent<Fire>().fire = false;
					}
				}


				if( transform.position.x <  GameObject.FindWithTag("CaracFenetre").GetComponent<CaracFenetre>().CornerUpRight.x + 1)
				{
					listEnemy[0].gameObject.GetComponent<ChassisEnemy>().invulnerable = true;
				}
				else
				{
					listEnemy[0].gameObject.GetComponent<ChassisEnemy>().invulnerable = false;
				}

				deplacement = new Vector2 (vitesse, 0);
				if(transform.position.x <  positionXDroite)
				{
					transform.Translate (deplacement * Time.deltaTime, Space.World);
				}
				else
				{
					listEnemy[0].GetComponent<ChassisEnemy>().Touche(1000000, false);
					chkIfEmpty ();
				}
			}
		}

		chkIfEmpty ();
	}
}
